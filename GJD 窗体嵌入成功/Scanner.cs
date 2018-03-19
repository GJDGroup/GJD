using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Linq;

using Cti.Hardware.Extension.Files;
using Cti.Hardware.ScanDevice;
using Cti.Hardware.ScanDevice.Base;
using Cti.Hardware.Extension.Shapes;
using Cti.Hardware.Extension.License;
using Cti.Hardware.Extension.Controls.Canvas;


namespace GJD
{
    public interface IScan
    {
        void Scan(object i);//加工序号为i的区域

        void ConnectScanner();//连接设备
        void ConnectScanner1();//连接设备

        void DisconnectScanner();//断开设备连接

        void Stop();//停止扫描

        void PauseContinue();//暂停/继续扫描

        void SetOffset(float x, float y);//设置原点偏置
    }
    public partial class Scanner : Form,IScan
    {
        public Scanner()
        {
            InitializeComponent();
            Cti.Hardware.Extension.License.LicenseManager.EnableEvaluationLicense();
            Cti.Hardware.Extension.License.LicenseManager.EnableDeviceBasedLicense(scanDeviceManager);
            Cti.Hardware.Extension.License.LicenseManager.EnableEvaluationLicense();
            Cti.Hardware.Extension.License.LicenseManager.EnableDeviceBasedLicense(scanDeviceManager);
            CheckLicense();                      
        }
        #region 字段     
        bool showdirec = false;//当前是否显示加工方向，默认不显示
        bool showjumpclick = false;//当前是否显示跳转，默认不显示
        
        float totaltime = 0, markdistance = 0, jumpdistance = 0, polycount = 0;
        float cuttercompensationwidth = 0f;
        bool inner = true;
        float markspeed = 20, jumpspeed = 8000, power, limit, aggressiveness,  frequency;
        int markdelay = 100, jumpdelay = 100, laserondelay, laseroffdelay, polydelay = 50, repeatcount;
        VelocityCompensationMode vel;
        class Device
        {
            public string DeviceUniqueName { get; set; }
            public string DeviceFriendlyName { get; set; }

            public override string ToString()
            {
                return DeviceFriendlyName;
            }
        }
        List<ShapeBase> shape = new List<ShapeBase>();//用于存放所有图形，计算总共所用时间
        List<ShapeBase> shapelist = new List<ShapeBase>();//用于存放除圆之外所有的图形
        List<ShapeBase> newshapelist = new List<ShapeBase>();//用于存放除圆之外所有图形的刀补之后的图形。
        int number = -1;//图形的序号
        ScanDocument scanDocument;
        ScanDeviceManager scanDeviceManager;
        string selectedDeviceName;      
        OffsetVector offset = new OffsetVector(0,0,0);
        List<Circle> circlelist = new List<Circle>();//圆集合
        List<Polyline> polylist = new List<Polyline>();//封闭折线集合
        List<Polyline> polylist1 = new List<Polyline>();//非封闭折线的原图集合
        List<Polyline> polylist2 = new List<Polyline>();//非封闭折线的刀补轨迹集合
        List<List<Point3D>> polyverticelist = new List<List<Point3D>>();//用于存放各个非封闭折线的点集合的集合
        List<Line> linelist = new List<Line>();//用于存放直线的集合
        List<Line> linelist2 = new List<Line>();//用于存放直线的刀补轨迹直线的集合
        List<float[]> linepointlist = new List<float[]>();//用于存放组成直线的点的坐标数组的集合
        List<Arc> arclist = new List<Arc>();//用于存放圆弧的集合
        List<Arc> arclist2 = new List<Arc>();//用于存放圆弧刀补轨迹圆弧的集合
        List<float[]> arcpointlist = new List<float[]>();//用于存放圆弧的各个数据数组的集合
        List<Point3D> pointlist = new List<Point3D>();
        public static int connectstatus=2;//控制卡连接状态。0代表连接成功，1代表连接失败，2代表断开连接成功，也代表设备当前未连接，与1不同，3代表断开连接失败
        public static int scanstatus = 2;//设备扫描状态。0代表正在扫描，1代表扫描暂停，2代表没有进行扫描,设备空闲
        #endregion
        #region 事件
       
        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void scanDeviceManager_DeviceListChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(scanDeviceManager_DeviceListChanged), sender, e);
            }
            else
            {
                RefreshDeviceList();
            }
        }	

        private void scanDeviceManager_ScanDeviceGatewayFailed(object sender, ScanDeviceGatewayFailedEventArgs e)
        {
            MessageBox.Show(e.Message, "错误提示");
        }
      
        private void scanDocument_ScanningStatusChanged(object sender, DocumentScanningStatusEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<DocumentScanningStatusEventArgs>(scanDocument_ScanningStatusChanged), sender, e);
            }
            else
            {
                
            }
        }

        private void scanDocument_ScriptMessageReceived(object sender, ScriptMessageEventArgs e)
        {
            MessageBox.Show("以下信息来自于所连设备\n" + e.ScriptMessage.ToString());
        }

        #endregion
        #region 方法
        #region 接口方法
        public void Scan(object i)//接口函数，导入对应序号的文件并设置刀具补偿，然后进行扫描
        {
            //ClearCanvas();
            //ClearPaint();
            //string path=null;
            //switch (i)//根据加工序号确定加工文件的位置
            //#region 确定加工文件的位置
            //{
                
            //    case 1:
            //        path = Application.StartupPath + "\\a.txt";
            //        break;
            //    case 2:
            //        path = Application.StartupPath + "\\b.txt";
            //        break;
            //    case 3:
            //        path = Application.StartupPath + "\\b.txt";
            //        break;
            //    case 4:
            //        path = Application.StartupPath + "\\a.txt";
            //        break;
            //    case 5:
            //        path = Application.StartupPath + "\\b.txt";
            //        break;
            //    case 6:
            //        path = Application.StartupPath + "\\b.txt";
            //        break;
            //    case 7:
            //        path = Application.StartupPath + "\\b.txt";
            //        break;
            //    case 8:
            //        path = Application.StartupPath + "\\b.txt";
            //        break;
            //    case 9:
            //        path = Application.StartupPath + "\\b.txt";
            //        break;
            //}
            //#endregion 
            //ImportDataToCanvas(path);//将文件内容加载到画板上
            //CutterCompensation(cuttercompensationwidth,inner);//设置刀具补偿
            //StartScan();//开始加工，激光参数在这个函数里面进行设置
            scanstatus = 2;
        }

        public void ConnectScanner()//连接振镜控制卡
        {
            if (selectedDeviceName == null)
            {
                connectstatus = 1;
                MessageBox.Show("请选择一个设备连接", "错误提示");
                return;
            }
            try
            {
                scanDeviceManager.Connect(selectedDeviceName);
                devicestatesgow.Text = "当前设备已连接";
                string xml = File.ReadAllText(Application.StartupPath + "\\CorrTable_24-BitGeneric160mm.xml");
                scanDeviceManager.UploadCorrectionFile(selectedDeviceName, xml, "corrfile", true, 1);
                scanDeviceManager.UploadCorrectionFile(selectedDeviceName, xml, "corrfile", true, 3);
                connectstatus = 0;
            }
            #region Exceptions
            catch (DeviceNotFoundException)
            {
                connectstatus = 1;
                MessageBox.Show("未找到可用设备", "错误提示");
            }
            catch (DeviceAlreadyInUseException)
            {
                connectstatus = 1;
                MessageBox.Show("当前设备正在使用，请稍后重试", "错误提示");
            }
            catch (DeviceCommunicationFailureException ex3)
            {
                connectstatus = 1;
                MessageBox.Show("通信失败，请稍后重试\nMessage: " + ex3.DeviceMessage, "错误提示");
            }
            catch (DeviceFailureException ex4)
            {
                MessageBox.Show("连接设备失败\nMessage: " + ex4.DeviceMessage, "错误提示");
            }
            #endregion
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
            showtimer.Enabled = true;
        }
        public void ConnectScanner1()//连接设备
        {
            connectstatus = 0;
        }
        public void SetOffset(float x, float y)//设置原点偏置
        {
            offset.X = x;
            offset.Y = y;
            scanDocument.Offset = offset;
        }

        #endregion
        public Point3D getnewpoint(Point3D point1, Point3D point2, Point3D point3, float r, int m)//获取刀补后的拐点
        {
            Point3D point = new Point3D();
            float x01 = point1.X;
            float y01 = point1.Y;
            float x02 = point2.X;
            float y02 = point2.Y;
            float x03 = point3.X;
            float y03 = point3.Y;
            float x04, y04, x05, y05, x06, y06,sina0,sinb0,cosa0,cosb0,tana0,tanb0;
            sina0 = (y02 - y01) / ((float)Math.Sqrt((x02 - x01) * (x02 - x01) + (y02 - y01) * (y02 - y01)));
            cosa0 = (x02 - x01) / Math.Abs(((float)Math.Sqrt((x02 - x01) * (x02 - x01) + (y02 - y01) * (y02 - y01))));
            sinb0 = (y03 - y02) / ((float)Math.Sqrt((x03 - x02) * (x03 - x02) + (y03 - y02) * (y03 - y02)));
            cosb0 = (x03 - x02) / Math.Abs(((float)Math.Sqrt((x03 - x02) * (x03 - x02) + (y03 - y02) * (y03 - y02))));
            x04 = x01 + m * r * sina0;
            y04 = y01 - m * r * cosa0;
            x06 = x03 + m * r * sinb0;
            y06 = y03 - m * r * cosb0;
            if (x01 != x02 && x02 != x03)
            {
                tana0 = (y02 - y01) / (x02 - x01);
                tanb0 = (y03 - y02) / (x03 - x02);
                if (tana0 != tanb0)
                {
                    x05 = (y06 - y04 - x06 * tanb0 + x04 * tana0) / (tana0 - tanb0);
                }
                else 
                {
                    x05 = x04 + x02 - x01;
                }
                y05 = y04 + tana0 * (x05 - x04);
                point.X = x05; point.Y = y05;
            }
            if (x01 == x02 && x02 == x03)
            {
                x05 = x04;
                y05 = y02;
                point.X = x05; point.Y = y05;
            }
            if(x01 == x02 && x02 != x03)
            {
                x05 = x04;
                tanb0 = (y03 - y02) / (x03 - x02);
                y05 = y06 - tanb0 * (x06 - x05);
                point.X = x05; point.Y = y05;
            }
            if (x01 != x02 && x02 == x03)
            {
                x05 = x06;
                tana0 = (y02 - y01) / (x02 - x01);
                y05 = y04 + (x05 - x04) * tana0;
                point.X = x05; point.Y = y05;
            }
            
            return point;
        }

        public void cutterpolyline(Polyline polyline, float cuttercompensationwidth, int m)//获得刀补后的封闭折线
        {            
            Point3D endpoint = new Point3D();//终点
            List<Point3D> verticelist1 = new List<Point3D>();//用于存放刀补后的线段起点
            int c= polyline.Vertices.Count;
            #region 计算第一个点
            Point3D point01 = new Point3D();
            Point3D point02 = new Point3D();
            Point3D point03 = new Point3D();
            point01 = polyline.Vertices[c-1];
            point02 = polyline.Vertices[0];
            point03 = polyline.Vertices[1];
            float x01 = point01.X;
            float y01 = point01.Y;
            float x02 = point02.X;
            float y02 = point02.Y;
            float x03 = point03.X;
            float y03 = point03.Y;
            float[] f01 = newlinedata(x01, y01, x02, y02, cuttercompensationwidth, m);
            float[] f02 = newlinedata(x02, y02, x03, y03, cuttercompensationwidth, m);
            float sina0, cosa0, sinb0, cosb0, sin0c, cos0c;
            sina0 = (y02 - y01) / ((float)Math.Sqrt((x02 - x01) * (x02 - x01) + (y02 - y01) * (y02 - y01)));
            cosa0 = (x02 - x01) / Math.Abs(((float)Math.Sqrt((x02 - x01) * (x02 - x01) + (y02 - y01) * (y02 - y01))));
            sinb0 = (y03 - y02) / ((float)Math.Sqrt((x03 - x02) * (x03 - x02) + (y03 - y02) * (y03 - y02)));
            cosb0 = (x03 - x02) / ((float)Math.Sqrt((x03 - x02) * (x03 - x02) + (y03 - y02) * (y03 - y02)));
            cos0c = cosa0 * cosb0 + sina0 * sinb0;
            sin0c = cosa0 * sinb0 - sina0 * cosb0;
            Point3D point001 = new Point3D(); Point3D point002 = new Point3D();//插入圆弧的起点和终点
            point001.X = f01[2];
            point001.Y = f01[3];
            point002.X = f02[0];
            point002.Y = f02[1];
            if (polyline.Closed)
            {
                if (cos0c < 0 && sin0c < 0 && m == -1)//插入型左刀补
                {
                    Arc newarc = new Arc();
                    newarc.SweepAngle = (float)(-Math.Asin(sin0c) - Math.PI);
                    newarc.CenterPoint = point02;
                    newarc.StartAngle = StartAngle(f01[2], f01[3], point02.X, point02.Y);
                    newarc.Radius = cuttercompensationwidth;
                    newshapelist.Add(newarc);
                    AddShapeToSelectedLayer(newarc);
                    canvas.UpdateDrawing();
                    verticelist1.Add(point002);
                    endpoint = point001;
                }
                else if (m == 1 && cos0c < 0 && sin0c > 0)//右刀补插入型
                {
                    Arc newarc = new Arc();
                    newarc.SweepAngle = (float)(-Math.Asin(sin0c) + Math.PI);
                    newarc.CenterPoint = point02;
                    newarc.StartAngle = StartAngle(f01[2], f01[3], point02.X, point02.Y);
                    newarc.Radius = cuttercompensationwidth;
                    newshapelist.Add(newarc);
                    AddShapeToSelectedLayer(newarc);
                    canvas.UpdateDrawing();
                    verticelist1.Add(point002);
                    endpoint = point001;
                }
                else//缩短型和延长性
                {
                    Point3D point = new Point3D();
                    float r = cuttercompensationwidth;
                    point = getnewpoint(point01, point02, point03, r, m);
                    endpoint = point;
                    verticelist1.Add(point);
                }
            }
            else
            {
                verticelist1.Add(point002);
            }
            
            #endregion
            #region 中间部分
            for (int i = 0; i < c-2; i++)
            {
                Point3D point1 = new Point3D();
                Point3D point2 = new Point3D();
                Point3D point3 = new Point3D();
                point1 = polyline.Vertices[i];
                point2 = polyline.Vertices[i + 1];
                point3 = polyline.Vertices[i + 2];
                float x11 = point1.X;
                float y11 = point1.Y;
                float x12 = point2.X;
                float y12 = point2.Y;
                float x13 = point3.X; 
                float y13 = point3.Y;
                float[] f1 = newlinedata(x11, y11, x12, y12, cuttercompensationwidth, m);
                float[] f2 = newlinedata(x12, y12, x13, y13, cuttercompensationwidth, m);
                float sina1, cosa1, sinb1, cosb1,sin1c,cos1c;
                sina1 = (y12 - y11) / ((float)Math.Sqrt((x12 - x11) * (x12 - x11) + (y12 - y11) * (y12 - y11)));
                cosa1 = (x12 - x11) / Math.Abs(((float)Math.Sqrt((x12 - x11) * (x12 - x11) + (y12 - y11) * (y12 - y11))));
                sinb1 = (y13 - y12) / ((float)Math.Sqrt((x13 - x12) * (x13 - x12) + (y13 - y12) * (y13 - y12)));
                cosb1 = (x13 - x12) / ((float)Math.Sqrt((x13 - x12) * (x13 - x12) + (y13 - y12) * (y13 - y12)));
                cos1c = cosa1 * cosb1 + sina1 * sinb1;
                sin1c = cosa1 * sinb1 - sina1 * cosb1;
                Point3D point101 = new Point3D();
                Point3D point102 = new Point3D();
                point101.X = f1[2];
                point101.Y = f1[3];
                point102.X = f2[0];
                point102.Y = f2[1];
                if (cos1c < 0 && sin1c < 0 && m == -1)//插入型左刀补
                {
                    Line line1 = new Line();
                    line1.StartPoint = verticelist1[i];
                    line1.EndPoint = point101;
                    newshapelist.Add(line1);
                    AddShapeToSelectedLayer(line1);
                    verticelist1.Add(point102);
                    Arc arc = new Arc();
                    arc.CenterPoint = point2;
                    arc.StartAngle = StartAngle(point101.X, point101.Y, point2.X, point2.Y);
                    arc.SweepAngle = (float)(-Math.Asin(sin1c) - Math.PI);
                    arc.Radius = cuttercompensationwidth;
                    newshapelist.Add(arc);
                    AddShapeToSelectedLayer(arc);
                    canvas.UpdateDrawing();
                }
                else if (m == 1 && cos1c < 0 && sin1c > 0)
                {
                    Line line1 = new Line();
                    line1.StartPoint = verticelist1[i];
                    line1.EndPoint = point101;
                    newshapelist.Add(line1);
                    AddShapeToSelectedLayer(line1);
                    verticelist1.Add(point102);
                    Arc arc = new Arc();
                    arc.CenterPoint = point2;
                    arc.StartAngle = StartAngle(point101.X, point101.Y, point2.X, point2.Y);
                    arc.SweepAngle = (float)(-Math.Asin(sin1c) + Math.PI);
                    arc.Radius = cuttercompensationwidth;
                    newshapelist.Add(arc);
                    AddShapeToSelectedLayer(arc);
                    canvas.UpdateDrawing();
                }
                else//伸长型和缩短型
                {
                    Point3D point = new Point3D();
                    float r = cuttercompensationwidth;
                    point = getnewpoint(point1, point2, point3, r, m);                    
                    verticelist1.Add(point);
                    Line newline = new Line();
                    newline.StartPoint = verticelist1[i];
                    newline.EndPoint = point;
                    newshapelist.Add(newline);
                    AddShapeToSelectedLayer(newline);
                    canvas.UpdateDrawing();
                }
            }
            #endregion
            #region 结尾连接开头
            Point3D point21 = new Point3D();
            Point3D point22 = new Point3D();
            Point3D point23 = new Point3D();
            point21 = polyline.Vertices[c - 2];
            point22 = polyline.Vertices[c-1];
            point23 = polyline.Vertices[0];
            float x21 = point21.X;
            float y21 = point21.Y;
            float x22 = point22.X;
            float y22 = point22.Y;
            float x23 = point23.X;
            float y23 = point23.Y;
            float[] f21 = newlinedata(x21, y21, x22, y22, cuttercompensationwidth, m);
            float[] f22 = newlinedata(x22, y22, x23, y23, cuttercompensationwidth, m);
            float sina2, cosa2, sinb2, cosb2, sin2c, cos2c;
            sina2 = (y22 - y21) / ((float)Math.Sqrt((x22 - x21) * (x22 - x21) + (y22 - y21) * (y22 - y21)));
            cosa2 = (x22 - x21) / Math.Abs(((float)Math.Sqrt((x22 - x21) * (x22 - x21) + (y22 - y21) * (y22 - y21))));
            sinb2 = (y23 - y22) / ((float)Math.Sqrt((x23 - x22) * (x23 - x22) + (y23 - y22) * (y23 - y22)));
            cosb2 = (x23 - x22) / ((float)Math.Sqrt((x23 - x22) * (x23 - x22) + (y23 - y22) * (y23 - y22)));
            cos2c = cosa2 * cosb2 + sina2 * sinb2;
            sin2c = cosa2 * sinb2 - sina2 * cosb2;
            Point3D point201 = new Point3D(); Point3D point202 = new Point3D();//插入圆弧的起点和终点
            point201.X = f21[2];
            point201.Y = f21[3];
            point202.X = f22[0];
            point202.Y = f22[1];
            if (polyline.Closed == true)
            {
                if (cos2c < 0 && sin2c < 0 && m == -1)//插入型左刀补
                {
                    Line line = new Line();
                    line.StartPoint = verticelist1[c - 2];
                    line.EndPoint = point201;
                    newshapelist.Add(line);
                    AddShapeToSelectedLayer(line);
                    verticelist1.Add(point202);
                    Arc arc = new Arc();
                    arc.CenterPoint = point22;
                    arc.StartAngle = StartAngle(point201.X, point201.Y, point202.X, point202.Y);
                    arc.SweepAngle = (float)(-Math.Asin(sin2c) - Math.PI);
                    arc.Radius = cuttercompensationwidth;
                    newshapelist.Add(arc);
                    AddShapeToSelectedLayer(arc);
                    canvas.UpdateDrawing();
                }
                else if (m == 1 && cos2c < 0 && sin2c > 0)//右刀补插入型
                {
                    Line line = new Line();
                    line.StartPoint = verticelist1[c - 2];
                    line.EndPoint = point201;
                    newshapelist.Add(line);
                    AddShapeToSelectedLayer(line);
                    verticelist1.Add(point202);
                    Arc arc = new Arc();
                    arc.CenterPoint = point22;
                    arc.StartAngle = StartAngle(point201.X, point201.Y, point202.X, point202.Y);
                    arc.SweepAngle = (float)(-Math.Asin(sin2c) + Math.PI);
                    arc.Radius = cuttercompensationwidth;
                    newshapelist.Add(arc);
                    AddShapeToSelectedLayer(arc);
                    canvas.UpdateDrawing();
                }
                else//缩短型和延长性
                {
                    Point3D point = new Point3D();
                    float r = cuttercompensationwidth;
                    point = getnewpoint(point21, point22, point23, r, m);
                    verticelist1.Add(point);
                    Line newline = new Line();
                    newline.StartPoint = verticelist1[c - 2];
                    newline.EndPoint = point;
                    newshapelist.Add(newline);
                    AddShapeToSelectedLayer(newline);
                    canvas.UpdateDrawing();
                }
                //最后连接
                Line endline = new Line();
                endline.StartPoint = verticelist1[c - 1];
                endline.EndPoint = endpoint;
                newshapelist.Add(endline);
                AddShapeToSelectedLayer(endline);
                canvas.UpdateDrawing();
            }
            else
            {
                Line line = new Line();
                line.StartPoint = verticelist1[c - 2];
                float[] f = newlinedata(polyline.Vertices[c-2].X,polyline.Vertices[c-2].Y,polyline.Vertices[c-1].X,polyline.Vertices[c-1].Y,cuttercompensationwidth,m);
                Point3D point = new Point3D();
                point.X = f[2];
                point.Y = f[3];
                line.EndPoint = point;
                newshapelist.Add(line);
                AddShapeToSelectedLayer(line);
                canvas.UpdateDrawing();
            }
            #endregion
        }           

        public float StartAngle(float startX, float startY, float centerX, float centerY)//获取圆弧的起始角
        {
            float startangle = 0f;
            if (startY > centerY && startX > centerX)
            {
                startangle = (float)Math.Atan((startY - centerY) / (startX - centerX));
            }
            else if (startY > centerY && startX < centerX)
            {
                startangle = (float)Math.PI + (float)Math.Atan((startY - centerY) / (startX - centerX));
            }
            else if (startY < centerY && startX < centerX)
            {
                startangle = (float)Math.Atan((startY - centerY) / (startX - centerX)) + (float)Math.PI;
            }
            else if (startY < centerY && startX > centerX)
            {
                startangle = (float)Math.Atan((startY - centerY) / (startX - centerX));
            }
            if(startX==centerX && startY >centerY )
            {
                startangle = (float)Math.PI / 2;
            }
            if (startX == centerX && startY < centerY)
            {
                startangle = (float)Math.PI / 2*3;
            }
            if (startX > centerX && startY == centerY)
            {
                startangle = 0;
            }
            if (startX < centerX && startY == centerY)
            {
                startangle = (float)Math.PI;
            }
            return startangle;
        }

        public void StartScan()//振镜开始扫描
        {
            scanDocument = null;
            if (selectedDeviceName == null)
            {
                MessageBox.Show("开始加工前请选择一个扫描设备", "错误提示");
                return;
            }
            try
            {
                scanDocument = scanDeviceManager.CreateScanDocument(selectedDeviceName, DistanceUnit.Millimeters);
            }
            #region Exceptions
            catch (DeviceNotFoundException)
            {
                MessageBox.Show("设备未找到", "错误提示");
            }
            catch (DeviceAlreadyInUseException)
            {
                MessageBox.Show("当前设备正在使用", "错误提示");
            }
            #endregion
            if (scanDocument != null)
            {
                scanDocument.DocumentScanningStatusChanged += new EventHandler<DocumentScanningStatusEventArgs>(scanDocument_ScanningStatusChanged);
                scanDocument.ScriptMessageReceived += new EventHandler<ScriptMessageEventArgs>(scanDocument_ScriptMessageReceived);
                VectorImage vectorImage = scanDocument.CreateVectorImage("Image1", DistanceUnit.Millimeters);
                SetLaserParameterValues(vectorImage);

                ScanningHelper.LoadDocument(canvas.Document, vectorImage, DistanceUnit.Millimeters);
                //设置加工完成城之后激光的状态
                ScanningCompletionState afterCompletion = new ScanningCompletionState();
                afterCompletion.BeamHomeEnabled = true;
                scanDocument.Offset = offset;
                afterCompletion.DisableLaser = false;//加工完成保持激光打开
                afterCompletion.SetLaserOn = true;//调试时注意对象，观察这些语句的作用
                afterCompletion.BeamHomePosition = new Point3D(0, 0, 0);//振镜镜片回初始位置
                scanDocument.AfterCompletion = afterCompletion;
                try
                {
                    scanDocument.Scripts.Add(new ScanningScriptChunk("Default", "ScanAll()"));
                    progressBar1.Value = 0;
                    if (totaltime < 0.05)
                    {
                        progressBar1.Maximum = 1;
                    }
                    else
                    {
                        progressBar1.Maximum = (int)(totaltime * 10);
                    }
                    progressBar1.Minimum = 0;
                    timetimer.Enabled = true;
                    painttimer.Enabled = true;
                    scanDocument.StartScanning();
                }
                #region Exceptions
                catch (DeviceNotConnectedException)
                {
                    MessageBox.Show("设备未连接", "错误提示");
                }
                catch (DeviceCommunicationFailureException exp1)
                {
                    MessageBox.Show("通信失败./nMessage: " + exp1.DeviceMessage, "错误提示");
                }
                catch (DeviceFailureException exp2)
                {
                    MessageBox.Show("开始加工失败.\nMessage: " + exp2.DeviceMessage, "错误提示");
                }
                #endregion

            }
            else
            {
                MessageBox.Show("当前没有需要加工的图形");
            }
        }

        public void DisconnectScanner()//控制卡断开连接
        {
              try
            {
                scanDeviceManager.Connect(selectedDeviceName);
                devicestatesgow.Text = "当前设备未连接";
                showtimer.Enabled = false;
                painttimer.Enabled = false;
                timetimer.Enabled = false;
            }
            catch (DeviceNotFoundException)
            {
                MessageBox.Show("未找到设备", "错误提示");
            }
            catch (DeviceFailureException ex2)
            {
                MessageBox.Show("无法断开连接.\nMessage: " + ex2.DeviceMessage, "错误提示");
            }
            btnDisconnect.Enabled = false;
            btnConnect.Enabled = true;
        }

        public void PauseContinue()//暂停/继续扫描
        {
            DeviceStatusSnapshot status = scanDeviceManager.GetDeviceStatusSnapshot(selectedDeviceName);
            if (status.ScanningStatus == DocumentScanningStatus.Paused)
            {
                scanDocument.ResumeScanning();
                labpause.Text = "暂停";
                btnPauseJob.Image = Image.FromFile(Application.StartupPath + "\\undo.png");
                painttimer.Stop();
            }
            else if (status.ScanningStatus == DocumentScanningStatus.Scanning)
            {
                scanDocument.PauseScanning();
                labpause.Text = "继续";
                btnPauseJob.Image = Image.FromFile(Application.StartupPath + "\\pause.png");
                painttimer.Start();
            }
        }

        public void Stop()//停止扫描
        {
            scanDocument.StopScanning();
            scanningstatesgow.Text = "设备空闲";
            painttimer.Enabled=false;
        }

        public void CutterCompensation(float cuttercompensationwidth,bool inner)//刀具补偿
        {
            #region
            int m;
            if (inner)//左刀补
            {
                m = -1;
            }
            else//右刀补
            {
                m = 1;
            }
            for (int i = 0; i < newshapelist.Count; i++)//取消之前的刀补，设置其不可见，不加工
            {
                newshapelist[i].Markable = false;
                newshapelist[i].Visible = false;
            }
            newshapelist.Clear();
            pointlist.Clear();            
            for (int i = 0; i < polylist.Count; i++)//对折线进行刀具补偿
            {
                polylist[i].Markable = false;
                cutterpolyline(polylist[i], cuttercompensationwidth, m);
            }
            for (int i = 0; i < circlelist.Count; i++)//对圆进行刀具补偿
            {
                if (m == -1)
                {
                    circlelist[i].DirectionOfCutterCompensation = CutterCompensationDirection.Inner;
                }
                else
                {
                    circlelist[i].DirectionOfCutterCompensation = CutterCompensationDirection.Outer;
                }
                circlelist[i].CutterCompensationWidth = cuttercompensationwidth;
                canvas.UpdateDrawing();
            }
            #endregion
            for (int i = 0; i < shapelist.Count; i++)//pointlist只存放每条线的起点
            {

                switch (shapelist[i].ShapeType)
                {
                    case ShapeType.Line:
                        #region
                        float x11, x12, y11, y12, cos1c, sin1c;
                        float x7, y7;
                        Line line = shapelist[i] as Line;
                        line.Markable = false;
                        if (i == 0)//该直线是第一条直线，因此直接偏置获得起点
                        {
                            float[] f = newlinedata(line.StartPoint.X, line.StartPoint.Y,line.EndPoint.X, line.EndPoint.Y, cuttercompensationwidth, m);
                            Point3D point = new Point3D();
                            point.X = f[0];
                            point.Y = f[1];
                            point.Z = 0;
                            pointlist.Add(point);
                        }

                        if (i == shapelist.Count - 1)//该直线是最后一条直线
                        {
                            float[] f = newlinedata(line.StartPoint.X, line.StartPoint.Y, line.EndPoint.X, line.EndPoint.Y, cuttercompensationwidth, m);
                            Point3D point = new Point3D();
                            point.X = f[2];
                            point.Y = f[3];
                            point.Z = 0;
                            Line newline = new Line();
                            newline.StartPoint = pointlist[i];
                            newline.EndPoint = point;
                            newline.Color = Color.DodgerBlue;
                            newshapelist.Add(newline);
                            AddShapeToSelectedLayer(newline);
                            canvas.UpdateDrawing();
                        }
                        else //分两种，一种直线与下一条线相连，另一种直线与下一条不连
                        {
                            float sina1, cosa1, sinb1, cosb1;
                            x11 = line.StartPoint.X;
                            x12 = line.EndPoint.X;
                            y11 = line.StartPoint.Y;
                            y12 = line.EndPoint.Y;
                            float[] f1 = newlinedata(x11, y11, x12, y12, cuttercompensationwidth, m);
                            sina1 = (y12 - y11) / ((float)Math.Sqrt((x12 - x11) * (x12 - x11) + (y12 - y11) * (y12 - y11)));
                            cosa1 = (x12 - x11) / Math.Abs(((float)Math.Sqrt((x12 - x11) * (x12 - x11) + (y12 - y11) * (y12 - y11))));
                            switch (shapelist[i + 1].ShapeType)
                            {
                                case ShapeType.Line://下一条线是直线
                                    #region
                                    Line line1 = shapelist[i + 1] as Line;

                                    Point3D point1 = line1.EndPoint;
                                    x7 = point1.X;
                                    y7 = point1.Y;
                                    sinb1 = (y7 - y12) / ((float)Math.Sqrt((x7 - x12) * (x7 - x12) + (y7 - y12) * (y7 - y12)));
                                    cosb1 = (x7 - x12) / ((float)Math.Sqrt((x7 - x12) * (x7 - x12) + (y7 - y12) * (y7 - y12)));
                                    cos1c = cosa1 * cosb1 + sina1 * sinb1;
                                    sin1c = cosa1 * sinb1 - sina1 * cosb1;

                                    float[] f2 = newlinedata(x12, y12, x7, y7, cuttercompensationwidth, m);
                                    if (line.EndPoint.X == line1.StartPoint.X && line.EndPoint.Y == line1.StartPoint.Y)//两条直线相连，然后要判断转接类型
                                    {
                                        #region

                                        if (cos1c < 0 && sin1c >= -1 && sin1c < 0 && m == -1)//插入型左刀补
                                        {
                                            Line newline1 = new Line();
                                            Line newline2 = new Line();
                                            newline1.StartPoint = pointlist[i];
                                            newline1.EndPoint.X = f1[2];
                                            newline1.EndPoint.Y = f1[3];
                                            newline1.EndPoint.Z = 0;
                                            newline1.Color = Color.DodgerBlue;
                                            newshapelist.Add(newline1);
                                            AddShapeToSelectedLayer(newline1);
                                            canvas.UpdateDrawing();
                                            Point3D point = new Point3D();//下一条直线的起点，要存入列表中
                                            point.X = f2[0];
                                            point.Y = f2[1];
                                            point.Z = 0;
                                            pointlist.Add(point);
                                            Arc arc = new Arc();
                                            arc.CenterPoint = line.EndPoint;
                                            arc.StartAngle = StartAngle(f1[2], f1[3], line.EndPoint.X, line.EndPoint.Y);
                                            arc.SweepAngle = (float)(-Math.Asin(sin1c) - Math.PI); ;
                                            arc.Radius = cuttercompensationwidth;
                                            arc.Color = Color.DodgerBlue;
                                            newshapelist.Add(arc);
                                            AddShapeToSelectedLayer(arc);
                                            canvas.UpdateDrawing();
                                        }
                                        else if (m == 1 && cos1c < 0 && sin1c > 0)//右刀补插入型
                                        {
                                            Line newline1 = new Line();
                                            Line newline2 = new Line();
                                             
                                            newline1.StartPoint = pointlist[i];
                                            newline1.EndPoint.X = f1[2];
                                            newline1.EndPoint.Y = f1[3];
                                            newline1.Color = Color.DodgerBlue;
                                            newshapelist.Add(newline1);
                                            AddShapeToSelectedLayer(newline1);
                                            canvas.UpdateDrawing();
                                            Point3D point = new Point3D();//下一条直线的起点，要存入列表中
                                            point.X = f2[0];
                                            point.Y = f2[1];
                                            point.Z = 0;
                                            pointlist.Add(point);
                                            Arc arc = new Arc();
                                            arc.CenterPoint = line.EndPoint;
                                            arc.StartAngle = StartAngle(f1[2], f1[3], line.EndPoint.X, line.EndPoint.Y);
                                            arc.SweepAngle = (float)(-Math.Asin(sin1c) + Math.PI);
                                            arc.Radius = cuttercompensationwidth;
                                            arc.Color = Color.DodgerBlue;
                                            newshapelist.Add(arc);
                                            AddShapeToSelectedLayer(arc);
                                            canvas.UpdateDrawing();

                                        }
                                        else //缩短型和延长型
                                        {
                                            Point3D point3 = new Point3D();
                                            float r = cuttercompensationwidth;
                                            point3 = getnewpoint(line.StartPoint, line.EndPoint, line1.EndPoint, r, m); Line newline1 = new Line();
                                            newline1.StartPoint = pointlist[i];
                                            newline1.EndPoint = point3;
                                            newline1.Color = Color.DodgerBlue;
                                            newshapelist.Add(newline1);
                                            pointlist.Add(point3);
                                            AddShapeToSelectedLayer(newline1);
                                            canvas.UpdateDrawing();
                                        }
                                        #endregion

                                    }

                                    else//两条直线不相连
                                    {
                                        #region
                                        Line line3 = new Line();
                                        line3.StartPoint = pointlist[i];
                                        line3.EndPoint.X = f1[2];
                                        line3.EndPoint.Y = f1[3];
                                        line3.Color = Color.DodgerBlue;
                                        AddShapeToSelectedLayer(line3);
                                        canvas.UpdateDrawing();
                                        Point3D point = new Point3D();
                                        float[] f3 = newlinedata(line1.StartPoint.X, line1.StartPoint.Y, line1.EndPoint.X, line1.EndPoint.Y, cuttercompensationwidth, m);
                                        point.X = f3[0];
                                        point.Y = f3[1];
                                        point.Z = 0;
                                        pointlist.Add(point);
                                        #endregion
                                    }
                                    break;
                                    #endregion
                                case ShapeType.Arc://下一条是圆弧
                                    #region
                                    Arc arc1 = shapelist[i + 1] as Arc;//arc1是下一条线
                                    Point3D startpoint = new Point3D();
                                    startpoint.X = arc1.CenterPoint.X + arc1.Radius * (float)Math.Cos(arc1.StartAngle);
                                    startpoint.Y = arc1.CenterPoint.Y + arc1.Radius * (float)Math.Sin(arc1.StartAngle);
                                    startpoint.Z = 0;
                                    if (distance(line.EndPoint, startpoint) < 0.001)//两条线相连,下面要判断夹角
                                    {
                                        #region
                                        float angle = new float();//圆弧切线方向角

                                        float newr = new float();//刀补后的圆弧半径
                                        if (arc1.SweepAngle > 0)//逆圆
                                        {
                                            angle = arc1.StartAngle + (float)Math.PI / 2;
                                            newr = arc1.Radius + m * cuttercompensationwidth;
                                        }
                                        else//顺圆
                                        {
                                            angle = arc1.StartAngle - (float)Math.PI / 2;
                                            newr = arc1.Radius - m * cuttercompensationwidth;
                                        }
                                        float cosac, sinac, coslc, sinlc;
                                        cosac = (float)Math.Cos(angle);
                                        sinac = (float)Math.Sin(angle);
                                        coslc = cosa1 * cosac + sina1 * sinac;
                                        sinlc = cosa1 * sinac - sina1 * cosac;
                                        //逆圆

                                        #region 判断转折类型，进行刀具补偿
                                        if (((((sinlc > 0 || (sinlc == 0 && coslc == 1)) && m == -1) || ((sinlc < 0 || (sinlc == 0)) && m == 1)) && arc1.SweepAngle > 0) || ((arc1.SweepAngle < 0 && (((sinlc > 0 || sinlc == 0) && m == -1) || (m == 1 && (sinlc < 0 || (sinlc == 0 && coslc == 0)))))))//缩短型
                                        {
                                            #region
                                            Point3D point = new Point3D(); Point3D point11 = new Point3D(); Point3D point12 = new Point3D();
                                            float a, b, c, b0, d;
                                            {
                                                if (cosa1 != 0)//直线斜率存在
                                                {
                                                    float k = sina1 / cosa1;
                                                    a = 1 + k * k;
                                                    float[] f = newlinedata(line.StartPoint.X, line.StartPoint.Y, line.EndPoint.X, line.EndPoint.Y, cuttercompensationwidth, m);
                                                    b0 = f[1] - k * f[0];
                                                    b = -2 * arc1.CenterPoint.X + 2 * k * b0 - 2 * k * arc1.CenterPoint.Y;
                                                    c = -(newr * newr - arc1.CenterPoint.X * arc1.CenterPoint.X - b0 * b0 - arc1.CenterPoint.Y * arc1.CenterPoint.Y + 2 * b0 * arc1.CenterPoint.Y);
                                                    point11.X = (-b + (float)Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

                                                    point12.X = (-b - (float)Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

                                                    point11.Y = k * point11.X + b0;
                                                    point12.Y = k * point12.X + b0;
                                                }
                                                else//直线斜率不存在
                                                {
                                                    d = (float)Math.Sqrt(newr * newr - (line.EndPoint.X - arc1.CenterPoint.X) * (line.EndPoint.X - arc1.CenterPoint.X));
                                                    point11.X = line.StartPoint.X;
                                                    point12.X = line.StartPoint.X;
                                                    point11.Y = arc1.CenterPoint.Y + d;
                                                    point12.Y = arc1.CenterPoint.Y - d;
                                                }

                                                if (distance(point11, line.EndPoint) < distance(point12, line.EndPoint))
                                                {
                                                    point = point11;
                                                }
                                                else
                                                {
                                                    point = point12;
                                                }
                                                pointlist.Add(point);//将新圆弧的起点添加到列表中
                                                Line newline = new Line();
                                                newline.StartPoint = pointlist[i];
                                                newline.EndPoint = point;
                                                newline.Color = Color.DodgerBlue;
                                                newshapelist.Add(newline);
                                                AddShapeToSelectedLayer(newline);
                                                canvas.UpdateDrawing();
                                            }
                                            #endregion
                                        }
                                        else//插入型
                                        {
                                            #region
                                            Line lineac = new Line();
                                            lineac.StartPoint = pointlist[i];
                                            lineac.EndPoint.X = f1[2];
                                            lineac.EndPoint.Y = f1[3];
                                            lineac.Color = Color.DodgerBlue;
                                            newshapelist.Add(lineac);
                                            AddShapeToSelectedLayer(lineac);
                                            canvas.UpdateDrawing();
                                            Point3D point = new Point3D();
                                            point.X = arc1.CenterPoint.X + newr * (float)Math.Cos(arc1.StartAngle);
                                            point.Y = arc1.CenterPoint.Y + newr * (float)Math.Sin(arc1.StartAngle);
                                            point.Z = 0;
                                            pointlist.Add(point);
                                            Arc newarc = new Arc();
                                            newarc.CenterPoint = line.EndPoint;
                                            newarc.StartAngle = StartAngle(f1[2], f1[3], line.EndPoint.X, line.EndPoint.Y);
                                            if (m == -1)
                                            {
                                                if (coslc < 0 || coslc == 0)
                                                {
                                                    newarc.SweepAngle = (float)(-Math.Asin(sinlc) - Math.PI);
                                                }
                                                else
                                                {
                                                    newarc.SweepAngle = -(float)(Math.Acos(coslc));
                                                }
                                            }
                                            else
                                            {
                                                if ((coslc < 0 || coslc == 0))
                                                {
                                                    newarc.SweepAngle = (float)(-Math.Asin(sinlc) + Math.PI);
                                                }
                                                else
                                                {
                                                    newarc.SweepAngle = (float)(Math.Asin(sinlc));
                                                }
                                            }
                                            newarc.Color = Color.DodgerBlue;
                                            newarc.Radius = cuttercompensationwidth;
                                            newshapelist.Add(newarc);
                                            AddShapeToSelectedLayer(newarc);
                                            canvas.UpdateDrawing();
                                            #endregion

                                        }
                                        #endregion


                                        #endregion

                                    }
                                    else//两条线不相连
                                    {
                                        #region
                                        Line line3 = new Line();
                                        line3.StartPoint = pointlist[i];
                                        line3.EndPoint.X = f1[2];
                                        line3.EndPoint.Y = f1[3];
                                        line3.Color = Color.DodgerBlue;
                                        AddShapeToSelectedLayer(line3);
                                        canvas.UpdateDrawing();
                                        Point3D point = new Point3D();
                                        point.X = arc1.CenterPoint.X + (float)Math.Cos(arc1.StartAngle);
                                        point.Y = arc1.CenterPoint.Y + (float)Math.Sin(arc1.StartAngle);
                                        pointlist.Add(point);
                                        #endregion
                                    }
                                    break;
                                    #endregion
                                //case ShapeType.Polyline:
                                //    #region

                                //    break;
                                //    #endregion 
                            }
                        }

                        #endregion
                        break;
                    case ShapeType.Arc://圆弧

                        #region
                        Arc arc0 = shapelist[i] as Arc;
                        arc0.Markable = false;
                        int n;//n=-1逆圆，n=1顺圆
                        float newr0;//新圆弧的半径
                        float endanglefa;//圆弧终点的法向

                        if (arc0.SweepAngle > 0)//逆圆
                        {
                            newr0 = arc0.Radius + m * cuttercompensationwidth;
                            n = -1;
                            endanglefa = arc0.StartAngle + arc0.SweepAngle + (float)Math.PI / 2;
                        }
                        else//顺圆
                        {
                            newr0 = arc0.Radius - m * cuttercompensationwidth;
                            n = 1;
                            endanglefa = arc0.StartAngle + arc0.SweepAngle - (float)Math.PI / 2;
                        }

                        #region 该圆弧是起始圆弧

                        if (i == 0)//该圆弧是第一条，直接偏置获得起点
                        {
                            Point3D point = new Point3D();
                            point.X = arc0.CenterPoint.X + newr0 * (float)Math.Cos(arc0.StartAngle);
                            point.Y = arc0.CenterPoint.Y + newr0 * (float)Math.Sin(arc0.StartAngle);
                            pointlist.Add(point);
                        }
                        if (i == shapelist.Count - 1)//该圆弧是最后一条圆弧
                        {
                            float startangle = StartAngle(pointlist[i].X, pointlist[i].Y, arc0.CenterPoint.X, arc0.CenterPoint.Y);

                            Point3D pointend = new Point3D();

                            pointend.X = arc0.CenterPoint.X + newr0 * (float)Math.Cos(arc0.StartAngle + arc0.SweepAngle);
                            pointend.Y = arc0.CenterPoint.Y + newr0 * (float)Math.Sin(arc0.StartAngle + arc0.SweepAngle);
                            float sweepangle = SweepAngle(pointlist[i].X, pointlist[i].Y, pointend.X, pointend.Y, arc0.CenterPoint.X, arc0.CenterPoint.Y, n);
                            Arc newarc = new Arc();
                            newarc.CenterPoint = arc0.CenterPoint;
                            newarc.StartAngle = startangle;
                            newarc.SweepAngle = sweepangle;
                            newarc.Radius = newr0;
                            newarc.Color = Color.DodgerBlue;
                            AddShapeToSelectedLayer(newarc);
                            canvas.UpdateDrawing();
                            newshapelist.Add(newarc);
                        }
                        #endregion
                        else//不是最后一条
                        {
                            #region
                            Point3D endpoint = new Point3D();//圆弧的终点
                            endpoint.X = arc0.CenterPoint.X + arc0.Radius * (float)Math.Cos(arc0.StartAngle + arc0.SweepAngle);
                            endpoint.Y = arc0.CenterPoint.Y + arc0.Radius * (float)Math.Sin(arc0.StartAngle + arc0.SweepAngle);


                            switch (shapelist[i + 1].ShapeType)
                            {
                                case ShapeType.Line://下一条线是直线
                                    #region
                                    float xl1, xl2, yl1, yl2;
                                    Line line1 = shapelist[i + 1] as Line;
                                    float sinline, cosline, sinarc, cosarc, sinlc, coslc;
                                    xl1 = line1.StartPoint.X;
                                    xl2 = line1.EndPoint.X;
                                    yl1 = line1.StartPoint.Y;
                                    yl2 = line1.EndPoint.Y;
                                    float[] f1 = newlinedata(xl1, yl1, xl2, yl2, cuttercompensationwidth, m);
                                    sinline = (yl2 - yl1) / ((float)Math.Sqrt((xl2 - xl1) * (xl2 - xl1) + (yl2 - yl1) * (yl2 - yl1)));
                                    cosline = (xl2 - xl1) / Math.Abs(((float)Math.Sqrt((xl2 - xl1) * (xl2 - xl1) + (yl2 - yl1) * (yl2 - yl1))));
                                    sinarc = (float)Math.Sin(endanglefa);
                                    cosarc = (float)Math.Cos(endanglefa);
                                    sinlc = sinline * cosarc - cosline * sinarc;
                                    coslc = cosarc * cosline + sinarc * sinline;
                                    #endregion
                                    #region 两条线相连
                                    if (distance(line1.StartPoint, endpoint) < 0.001)
                                    {
                                        #region
                                        if (((arc0.SweepAngle > 0) && (((m == -1) && (sinlc > 0 || (sinlc == 0 && coslc == 1))) || ((m == 1) && (sinlc < 0 || sinlc == 0)))) || ((arc0.SweepAngle < 0) && (((m == -1) && (sinlc > 0 || sinlc == 0)) || ((m == 1) && (sinlc < 0 || (sinlc == 0 && coslc == 1))))))//缩短型
                                        {
                                            float a, b, b0, c, d;

                                            Point3D point = new Point3D();
                                            Point3D point11 = new Point3D();
                                            Point3D point12 = new Point3D();
                                            //圆弧与直线的交点
                                            if (cosline != 0)//直线斜率存在
                                            {
                                                float k = sinline / cosline;
                                                a = 1 + k * k;

                                                b0 = f1[1] - k * f1[0];
                                                b = -2 * arc0.CenterPoint.X + 2 * k * b0 - 2 * k * arc0.CenterPoint.Y;
                                                c = -(newr0 * newr0 - arc0.CenterPoint.X * arc0.CenterPoint.X - b0 * b0 - arc0.CenterPoint.Y * arc0.CenterPoint.Y + 2 * b0 * arc0.CenterPoint.Y);
                                                point11.X = (-b + (float)Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

                                                point12.X = (-b - (float)Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

                                                point11.Y = k * point11.X + b0;
                                                point12.Y = k * point12.X + b0;
                                            }
                                            else//斜率不存在
                                            {
                                                d = (float)Math.Sqrt(newr0 * newr0 - (f1[0] - arc0.CenterPoint.X) * (f1[0] - arc0.CenterPoint.X));
                                                point11.X = f1[0];
                                                point12.X = f1[0];
                                                point11.Y = arc0.CenterPoint.Y + d;
                                                point12.Y = arc0.CenterPoint.Y - d;
                                            }
                                            if (distance(point11, line1.StartPoint) < distance(point12, line1.StartPoint))
                                            {
                                                point = point11;
                                            }
                                            else
                                            {
                                                point = point12;
                                            }
                                            pointlist.Add(point);
                                            float startangle = StartAngle(pointlist[i].X, pointlist[i].Y, arc0.CenterPoint.X, arc0.CenterPoint.Y);
                                            float sweepangle = SweepAngle(pointlist[i].X, pointlist[i].Y, point.X, point.Y, arc0.CenterPoint.X, arc0.CenterPoint.Y, n);
                                            Arc newarc = new Arc();
                                            newarc.CenterPoint = arc0.CenterPoint;
                                            newarc.Radius = newr0;
                                            newarc.StartAngle = startangle;
                                            newarc.SweepAngle = sweepangle;
                                            newarc.Color = Color.DodgerBlue;
                                            AddShapeToSelectedLayer(newarc);
                                            canvas.UpdateDrawing();
                                            newshapelist.Add(newarc);
                                        }
                                        #endregion
                                        else//插入型
                                        {
                                            #region
                                            Arc newarc = new Arc();//新圆弧
                                            Arc arcinsert = new Arc();//插入的圆弧
                                            newarc.Radius = newr0;
                                            newarc.CenterPoint = arc0.CenterPoint;
                                            newarc.StartAngle = StartAngle(pointlist[i].X, pointlist[i].Y, arc0.CenterPoint.X, arc0.CenterPoint.Y);
                                            newarc.SweepAngle = SweepAngle(pointlist[i].X, pointlist[i].Y, endpoint.X, endpoint.Y, arc0.CenterPoint.X, arc0.CenterPoint.Y, n);
                                            newarc.Color = Color.DodgerBlue;
                                            AddShapeToSelectedLayer(newarc);
                                            canvas.UpdateDrawing();
                                            newshapelist.Add(newarc);
                                            Point3D point0 = new Point3D();
                                            Point3D linepoint = new Point3D();
                                            linepoint.X = f1[0];
                                            linepoint.Y = f1[1];
                                            point0.X = arc0.CenterPoint.X + newr0 * (float)Math.Cos(arc0.StartAngle + arc0.SweepAngle);
                                            point0.Y = arc0.CenterPoint.Y + newr0 * (float)Math.Sin(arc0.StartAngle + arc0.SweepAngle);

                                            arcinsert.CenterPoint = endpoint;
                                            arcinsert.Radius = cuttercompensationwidth;
                                            arcinsert.StartAngle = StartAngle(point0.X, point0.Y, endpoint.X, endpoint.Y);

                                            arcinsert.SweepAngle = SweepAngle(point0.X, point0.Y, f1[0], f1[1], endpoint.X, endpoint.Y, n);
                                            arcinsert.Color = Color.DodgerBlue;
                                            if (Math.Abs(arcinsert.SweepAngle) > Math.PI)
                                            {
                                                arcinsert.SweepAngle = (float)((Math.Abs(arcinsert.SweepAngle) - 2 * Math.PI) * Math.Abs(arcinsert.SweepAngle) / (double)arcinsert.SweepAngle);
                                            }
                                            AddShapeToSelectedLayer(arcinsert);
                                            canvas.UpdateDrawing();
                                            newshapelist.Add(arcinsert);
                                            pointlist.Add(linepoint);
                                            #endregion
                                        }
                                    }

                                    #endregion
                                    #region 两条线不相连
                                    else
                                    {
                                        float[] f = newlinedata(xl1, yl1, xl2, yl2, cuttercompensationwidth, m);
                                        Point3D point = new Point3D();
                                        point.X = f[0];
                                        point.Y = f[1];
                                        pointlist.Add(point);
                                        Point3D pointend = new Point3D();

                                        pointend.X = arc0.CenterPoint.X + newr0 * (float)Math.Cos(arc0.StartAngle + arc0.SweepAngle);
                                        pointend.Y = arc0.CenterPoint.Y + newr0 * (float)Math.Sin(arc0.StartAngle + arc0.SweepAngle);
                                        float sweepangle = SweepAngle(pointlist[i].X, pointlist[i].Y, pointend.X, pointend.Y, arc0.CenterPoint.X, arc0.CenterPoint.Y, n);
                                        Arc newarc = new Arc();
                                        newarc.CenterPoint = arc0.CenterPoint;
                                        float startangle = StartAngle(pointlist[i].X, pointlist[i].Y, arc0.CenterPoint.X, arc0.CenterPoint.Y);
                                        newarc.StartAngle = startangle;
                                        newarc.SweepAngle = sweepangle;
                                        newarc.Radius = newr0;
                                        newarc.Color = Color.DodgerBlue;
                                        AddShapeToSelectedLayer(newarc);
                                        canvas.UpdateDrawing();
                                        newshapelist.Add(newarc);
                                    }
                                    #endregion
                                    break;
                                case ShapeType.Arc://下一条线是圆弧
                                    #region
                                    #endregion
                                    break;
                            }
                            #endregion
                        }
                        #endregion
                        break;
                    #region//连接折线（无用）
                    //case ShapeType.Polyline:
                    //         Polyline polyline = shapelist[i] as Polyline;
                    //         polyline.Markable = false;
                    //                List<Point3D> verticelist1 = new List<Point3D>();
                    //                int c0 = polyline.Vertices.Count;
                    //                Point3D pointstart = new Point3D();
                    //                if (i == 0)
                    //                {
                    //                    float[] f01 = newlinedata(polyline.Vertices[0].X, polyline.Vertices[0].Y, polyline.Vertices[1].X, polyline.Vertices[1].Y, cuttercompensationwidth, m);
                    //                    pointstart.X = f01[0];
                    //                    pointstart.Y = f01[1];
                    //                }
                    //                else 
                    //                { 
                    //                    pointstart=pointlist[i];
                    //                }
                    //                verticelist1.Add(pointstart);
                    //                #region 中间部分
                    //                for (int ii = 0; ii < c0 - 2; ii++)
                    //                {
                    //                    Point3D point11 = new Point3D();
                    //                    Point3D point2 = new Point3D();
                    //                    Point3D point3 = new Point3D();
                    //                    point11 = polyline.Vertices[ii];
                    //                    point2 = polyline.Vertices[ii + 1];
                    //                    point3 = polyline.Vertices[ii + 2];
                    //                    float x011 = point11.X;
                    //                    float y011 = point11.Y;
                    //                    float x012 = point2.X;
                    //                    float y012 = point2.Y;
                    //                    float x013 = point3.X;
                    //                    float y13 = point3.Y;
                    //                    float[] f01 = newlinedata(x011, y011, x012, y012, cuttercompensationwidth, m);
                    //                    float[] f02 = newlinedata(x012, y012, x013, y13, cuttercompensationwidth, m);
                    //                    float sina01, cosa01, sinb01, cosb01, sin01c, cos01c;
                    //                    sina01 = (y012 - y011) / ((float)Math.Sqrt((x012 - x011) * (x012 - x011) + (y012 - y011) * (y012 - y011)));
                    //                    cosa01 = (x012 - x011) / Math.Abs(((float)Math.Sqrt((x012 - x011) * (x012 - x011) + (y012 - y011) * (y012 - y011))));
                    //                    sinb01 = (y13 - y012) / ((float)Math.Sqrt((x013 - x012) * (x013 - x012) + (y13 - y012) * (y13 - y012)));
                    //                    cosb01 = (x013 - x012) / ((float)Math.Sqrt((x013 - x012) * (x013 - x012) + (y13 - y012) * (y13 - y012)));
                    //                    cos01c = cosa01 * cosb01 + sina01 * sinb01;
                    //                    sin01c = cosa01 * sinb01 - sina01 * cosb01;
                    //                    Point3D point101 = new Point3D();
                    //                    Point3D point102 = new Point3D();
                    //                    point101.X = f01[2];
                    //                    point101.Y = f01[3];
                    //                    point102.X = f02[0];
                    //                    point102.Y = f02[1];
                    //                    if (cos01c < 0 && sin01c < 0 && m == -1)//插入型左刀补
                    //                    {
                    //                        Line line01 = new Line();
                    //                        line01.StartPoint = verticelist1[ii];
                    //                        line01.EndPoint = point101;
                    //                        newshapelist.Add(line01);
                    //                        AddShapeToSelectedLayer(line01);
                    //                        verticelist1.Add(point102);
                    //                        Arc arc = new Arc();
                    //                        arc.CenterPoint = point2;
                    //                        arc.StartAngle = StartAngle(point101.X, point101.Y, point2.X, point2.Y);
                    //                        arc.SweepAngle = (float)(-Math.Asin(sin01c) - Math.PI);
                    //                        arc.Radius = cuttercompensationwidth;
                    //                        newshapelist.Add(arc);
                    //                        AddShapeToSelectedLayer(arc);
                    //                        canvas.UpdateDrawing();
                    //                    }
                    //                    else if (m == 1 && cos01c < 0 && sin01c > 0)
                    //                    {
                    //                        Line line01 = new Line();
                    //                        line01.StartPoint = verticelist1[ii];
                    //                        line01.EndPoint = point101;
                    //                        newshapelist.Add(line01);
                    //                        AddShapeToSelectedLayer(line01);
                    //                        verticelist1.Add(point102);
                    //                        Arc arc = new Arc();
                    //                        arc.CenterPoint = point2;
                    //                        arc.StartAngle = StartAngle(point101.X, point101.Y, point2.X, point2.Y);
                    //                        arc.SweepAngle = (float)(-Math.Asin(sin01c) + Math.PI);
                    //                        arc.Radius = cuttercompensationwidth;
                    //                        newshapelist.Add(arc);
                    //                        AddShapeToSelectedLayer(arc);
                    //                        canvas.UpdateDrawing();
                    //                    }
                    //                    else//伸长型和缩短型
                    //                    {
                    //                        Point3D point = new Point3D();
                    //                        float r = cuttercompensationwidth;
                    //                        point = getnewpoint(point11, point2, point3, r, m);
                    //                        verticelist1.Add(point);
                    //                        Line newline = new Line();
                    //                        newline.StartPoint = verticelist1[ii];
                    //                        newline.EndPoint = point;
                    //                        newshapelist.Add(newline);
                    //                        AddShapeToSelectedLayer(newline);
                    //                        canvas.UpdateDrawing();
                    //                    }
                    //                }
                    //                #endregion
                    //    break;                               
                }
                    #endregion
            }
        }

        public void ImportDataToCanvas(string filename)//根据文件路径导入文本并绘制图形
        {
            float x1, x2, y1, y2;
            if (filename == null)
            {
                return;
            }
            try
            {
                #region 读取文件并转化数据
                StreamReader sr = File.OpenText(filename);
                string nextline = sr.ReadLine();
                string[] str = nextline.Split('\t', ' ');//将数据分割,分割完成后插入列表中，然后加载到折线中去 
                Polyline polyline = new Polyline();
                List<Point3D> verticelist = new List<Point3D>();

                for (int i = 1; i < str.Length / 2; i++)//将所有的数据变成点添加到点的列表中去
                {
                    Point3D point = new Point3D(float.Parse(str[2 * i]), float.Parse(str[2 * i + 1]), 0);
                    verticelist.Add(point);
                }
                #endregion
                #region 封闭折线
                if (str[1] == "closed")
                {
                    polyline.Closed = true;
                    polyline.Vertices = verticelist;
                    polyline.Color = Color.Black;
                    polylist.Add(polyline);
                    shape.Add(polyline);
                    number++;
                    int n = polyline.Vertices.Count;
                    Point3D point1 = new Point3D();
                    for (int i = 0; i < n - 1; i++)
                    {
                        markdistance += distance(polyline.Vertices[i], polyline.Vertices[i + 1]);
                    }

                    if (number == 0)
                    {

                    }
                    else
                    {
                        jumpdistance += distance(point(shape[number - 1]), polyline.Vertices[0]);
                    }
                    polycount += n - 1;

                    AddShapeToSelectedLayer(polyline);
                }
                #endregion
                #region 不封闭的折线
                else
                {
                    #region 图形为直线
                    if (str.Length < 8)//图形只是一条直线，不是折线
                    {
                        x1 = float.Parse(str[2]);
                        y1 = float.Parse(str[3]);
                        x2 = float.Parse(str[4]);
                        y2 = float.Parse(str[5]);
                        Line line = new Line();
                        line.StartPoint.X = x1;
                        line.StartPoint.Y = y1;
                        line.EndPoint.X = x2;
                        line.EndPoint.Y = y2;


                        line.Color = Color.Black;
                        AddShapeToSelectedLayer(line);
                        linelist.Add(line);
                        shape.Add(line);
                        number++;

                        markdistance += distance(line.StartPoint, line.EndPoint);

                        if (number == 0)
                        {

                        }
                        else
                        {
                            jumpdistance += distance(point(shape[number - 1]), line.StartPoint);
                        }

                        float[] f0 = { x1, y1, x2, y2 };
                        
                    }
                    #endregion
                    #region 图形为折线
                    else//图形为折线
                    {
                        Polyline polyline0 = new Polyline();//源折线，没有进行刀具补偿，这条线不进行加工，polyline 是真正的走刀轨迹
                        polyline0.Vertices = verticelist;


                        polyline0.Closed = false;
                        polyline0.Color = Color.Black;
                        polylist.Add(polyline0);
                        polyverticelist.Add(verticelist);
                        AddShapeToSelectedLayer(polyline0);
                        shape.Add(polyline0);
                        shapelist.Add(polyline0);
                        number++;
                        int n = polyline0.Vertices.Count;
                        Point3D point1 = new Point3D();
                        for (int i = 0; i < n - 1; i++)
                        {
                            markdistance += distance(polyline0.Vertices[i], polyline0.Vertices[i + 1]);
                        }

                        if (number == 0)
                        {

                        }
                        else
                        {
                            jumpdistance += distance(point(shape[number - 1]), polyline0.Vertices[0]);
                        }
                        polycount += n - 1;

                    }
                    #endregion
                    canvas.UpdateDrawing();
                }
            }
                #endregion

            catch
            { }
        }

        public void DrawRuler(Graphics g)//在显示位置的画板上绘制坐标图
        {                         
            Pen penBlack = new Pen(Color.Black,0.5f);
            Font font = new Font("Arial",8f/4,FontStyle.Regular);
            g.DrawRectangle(penBlack, -30, -30, 60, 60);
            Pen penblue = new Pen(Color.DarkGray, 0.3f);
            for (int i = 0; i < 11; i++)
            {
                g.DrawLine(penblue, -30f, -25f + 5f * i, 30f, -25f + 5f * i);
            }
            for (int i = 0; i < 11; i++)
            {
                g.DrawLine(penblue, -25f + 5f * i, -30f, -25f + 5f * i, 30f);
            }
            for (float x = -30; x < 30; x += 5f)
            {
                g.DrawLine(penBlack, x, -30, x , -30 + 60f / 240 * 5);
            }
            g.ScaleTransform(1, -1);  
            g.DrawString((-30).ToString(), font, Brushes.Black, -32.5f , 30.5f);
            g.DrawString((-25).ToString(), font, Brushes.Black, -27.5f, 30.5f);
            g.DrawString((-20).ToString(), font, Brushes.Black, -22.2f, 30.5f);
            g.DrawString((-15).ToString(), font, Brushes.Black, -17.5f, 30.5f);
            g.DrawString((-10).ToString(), font, Brushes.Black, -12.3f, 30.5f);
            g.DrawString((-5).ToString(), font, Brushes.Black, -7f, 30.5f);
            g.DrawString((0).ToString(), font, Brushes.Black, -1.3f, 30.5f);
            g.DrawString((30).ToString(), font, Brushes.Black, 28.4f, 30.5f);
            g.DrawString((25).ToString(), font, Brushes.Black, 23f, 30.5f);
            g.DrawString((20).ToString(), font, Brushes.Black, 18.4f, 30.5f);
            g.DrawString((15).ToString(), font, Brushes.Black, 13f, 30.5f);
            g.DrawString((10).ToString(), font, Brushes.Black, 8.2f, 30.5f);
            g.DrawString((5).ToString(), font, Brushes.Black, 3.9f, 30.5f);
            g.ScaleTransform(1, -1);
            for (float y = -30; y < 30; y += 5f)
            {
                g.DrawLine(penBlack, -30f, y, -30 + 60f / 240*5, y);     
            }
            g.ScaleTransform(1, -1);
            g.DrawString((-30).ToString(), font, Brushes.Black, -36f, 28.5f);
            g.DrawString((-20).ToString(), font, Brushes.Black, -36f, 18.5f);
            g.DrawString((-10).ToString(), font, Brushes.Black, -36f, 8.5f);
            g.DrawString((0).ToString(), font, Brushes.Black, -34f, -1.5f);
            g.DrawString((10).ToString(), font, Brushes.Black, -35f, -11.5f);
            g.DrawString((20).ToString(), font, Brushes.Black, -35f, -21.5f);
            g.DrawString((30).ToString(), font, Brushes.Black, -35f, -31.5f);
            g.DrawString((-25).ToString(), font, Brushes.Black, -36f, 23.5f);
            g.DrawString((-15).ToString(), font, Brushes.Black, -36f, 13.5f);
            g.DrawString((-5).ToString(), font, Brushes.Black, -34.7f, 3.5f);
            g.DrawString((5).ToString(), font, Brushes.Black, -34f, -6.5f);
            g.DrawString((15).ToString(), font, Brushes.Black, -35f, -16.5f); 
            g.DrawString((25).ToString(), font, Brushes.Black, -35f, -26.5f);
            g.ScaleTransform(1, -1);
        }

        public void DrawDot(float x,float y)//在显示位置的画板上绘制点，实际上是绘制很短的直线，只有一个像素点大小
        {
            Graphics g = Graphics.FromImage(PicShow.BackgroundImage);
            Pen pen = new Pen(Color.Red, 0.3f);
            g.TranslateTransform(160, 140);
            g.ScaleTransform(4, -4);
            g.DrawLine(pen, x, y,x+0.04f, y);
            PicShow.Refresh();
        }

        public void ClearPaint()//清除位置画板上所绘制的加工图，保留坐标和网格
        {
            Graphics g = Graphics.FromImage(PicShow.BackgroundImage);
            g.Clear(Color.White);
            PicShow.Refresh();   
        }

        public void AddLine(float x1,float y1,float x2,float y2)//添加直线
        {
            Line line = new Line();
            line.StartPoint.X = x1;
            line.StartPoint.Y = y1;
            line.EndPoint.X = x2;
            line.EndPoint.Y = y2;
            line.Color = Color.Black;
            AddShapeToSelectedLayer(line);
            linelist.Add(line);
            shape.Add(line);
            shapelist.Add(line);
            number++;
            markdistance += distance(line.StartPoint, line.EndPoint);
            if (number == 0)
            {
                jumpdistance = 0;
            }
            else
            {
                jumpdistance += distance(point(shape[number - 1]), line.StartPoint);
            }
            canvas.UpdateDrawing();
        }

        public void AddCircle(float x,float y,float r)//添加圆
        {
            Circle circle = new Circle();
            circle.CenterPoint = new Point3D(x, y, 0);
            circle.Radius = r;
            circle.Color = Color.Black;
            circle.CutterCompensationWidth = cuttercompensationwidth;
            if (inner)
            {
                circle.DirectionOfCutterCompensation = CutterCompensationDirection.Inner;
            }
            else
            {
                circle.DirectionOfCutterCompensation = CutterCompensationDirection.Outer;
            }
            AddShapeToSelectedLayer(circle);
            circlelist.Add(circle);
            shape.Add(circle);
            number++;
            Point3D point1 = new Point3D(circle.CenterPoint.X + circle.Radius, circle.CenterPoint.Y, 0);//圆的起点和终点
            markdistance += (float)Math.PI * circle.Radius * 2;
            if (number == 0)
            {
                jumpdistance = 0;
            }
            else
            {
                jumpdistance += distance(point(shape[number - 1]), point1);
            }
            canvas.UpdateDrawing();
        }

        public void AddArc(float x, float y, float r, float startangle, float sweepangle)//添加圆弧
        {
            Arc arc = new Arc();
            arc.CenterPoint.X = x;
            arc.CenterPoint.Y = y;            
            arc.Radius = r;
            arc.StartAngle = startangle;
            arc.SweepAngle = sweepangle;
            arclist.Add(arc);
            arc.Color = Color.Black;
            shape.Add(arc);
            shapelist.Add(arc);
            AddShapeToSelectedLayer(arc);
            canvas.UpdateDrawing();
            number++;
            Point3D point1 = new Point3D(arc.CenterPoint.X + arc.Radius * (float)Math.Cos(arc.StartAngle), arc.CenterPoint.X + arc.Radius * (float)Math.Sin(arc.StartAngle), 0);
            markdistance += (float)Math.PI * arc.Radius * arc.SweepAngle * 2;
            if (number == 0)
            {
                jumpdistance = 0;
            }
            else
            {
                jumpdistance += distance(point(shape[number - 1]), point1);
            }
            canvas.UpdateDrawing();
        }

        public void AddRec(float x,float y,float w,float h,float angle)//添加矩形
        {
            Polyline polyline = new Polyline();
            List<Point3D> verticeList = new List<Point3D>();
            verticeList.Add(new Point3D(x, y, 0));
            verticeList.Add(new Point3D(x + w * (float)Math.Cos(angle), y + w * (float)Math.Sin(angle), 0));
            verticeList.Add(new Point3D(x + w * (float)Math.Cos(angle) + h * (float)Math.Cos(angle + Math.PI / 2), y + w * (float)Math.Sin(angle) + h * (float)Math.Sin(angle + Math.PI / 2), 0));
            verticeList.Add(new Point3D(x + h * (float)Math.Cos(angle + Math.PI / 2), y + h * (float)Math.Sin(angle + Math.PI / 2), 0));
            polyline.Vertices = verticeList;
            polyline.Closed = true;
            polyline.Color = Color.Black;
            polylist.Add(polyline);
            shape.Add(polyline);
            shapelist.Add(polyline);
            number++;
            int n = polyline.Vertices.Count;
            Point3D point1 = new Point3D();

            for (int i = 0; i < n - 1; i++)
            {
                markdistance += distance(polyline.Vertices[i], polyline.Vertices[i + 1]);
            }

            if (number == 0)
            {
                jumpdistance = 0;
            }
            else
            {
                jumpdistance += distance(point(shape[number - 1]), polyline.Vertices[0]);
            }
            polycount += 3;
            AddShapeToSelectedLayer(polyline);
        }

        public void ClearCanvas()//清除画板上的所有图形
        {
            canvas.ClearAllActions();
            canvas.Document = new ShapeDocument();
            pointlist.Clear();
            polylist.Clear();
            shape.Clear();
            shapelist.Clear();
            newshapelist.Clear();
            circlelist.Clear();
            polylist1.Clear();
            polylist2.Clear();
            arclist.Clear();
            arclist2.Clear();
            arcpointlist.Clear();
            markdistance = 0;
            jumpdistance = 0;
            polycount = 0;
            number = -1;
        }

        public float SweepAngle(float startX, float startY, float endX, float endY, float centerX, float centerY,int n)//获取圆弧的角度,n=-1逆圆，n=1顺圆
        {
            float sweepangle, endangle, startangle;
            startangle = StartAngle(startX,startY,centerX,centerY);
            endangle = StartAngle(endX,endY,centerX,centerY);
            if (n == -1)//逆圆,sweepangle>0
            {
                if (endangle > startangle)
                {
                    sweepangle = endangle - startangle;
                }
                else
                {
                    sweepangle = endangle - startangle + (float)Math.PI * 2;
                }
            }
            else//顺圆
            {
                if (endangle > startangle)
                {
                    sweepangle = endangle - startangle - (float)Math.PI * 2;
                }
                else
                {
                    sweepangle = endangle - startangle;
                }
            }
            return sweepangle;
        }

        private void LoadDxfFile(string filepath)//加载DXF图形
        {
            FileReader reader = new FileReader();
            FileDocument fileDocument = reader.Read(filepath);
            if (!string.IsNullOrEmpty(reader.Log))
            {
                MessageBox.Show("File reading log:\n" + reader.Log, "错误提示");
            }
            var shapes = fileDocument.Shapes.ToList();
            Layer layerAdded = new Layer();
            layerAdded.LayerName = Path.GetFileNameWithoutExtension(filepath);
            layerAdded.AddShapes(shapes);
            canvas.Document.LayerList.Add(layerAdded);
            canvas.UpdateDrawing();
        }

        public float[] newlinedata(float x1, float y1, float x2, float y2, float r, int m)//将直线的点进行刀具补偿获得刀补后直线的起点和终点坐标
        {
            float  x3, x4,y3,y4, cosa, sina;
            float[] newline = new float[4];
            sina = (y2 - y1) / ((float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
            cosa = (x2 - x1) / ((float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
            if (m == -1)
            {
                x3 = x1 - r * sina;
                y3 = y1 + r * cosa;
                x4 = x2 - r * sina;
                y4 = y2 + r * cosa;
            }
            else
            {
                x3 = x1 + r * sina;
                y3 = y1 - r * cosa;
                x4 = x2 + r * sina;
                y4 = y2 - r * cosa;
            }
            newline[0] = x3;
            newline[1] = y3;
            newline[2] = x4;
            newline[3] = y4;
            return newline;
        }

        public float distance(Point3D point1, Point3D point2)//获得两点之间的距离
        {
            float distance = 0;
            float x1 = point1.X;
            float y1 = point1.Y;
            float x2 = point2.X;
            float y2 = point2.Y;
            distance = (float)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            return distance;
        }

        public Point3D point(ShapeBase shape)//获取每个图形的终点
        {
            Point3D point = new Point3D();
            switch (shape.ShapeType)
            {
                case ShapeType.Line:
                    Line line = (Line)shape;
                    point = line.EndPoint;
                    return point;
                case ShapeType.Arc:
                    Arc arc = (Arc)shape;
                    point.X = arc.CenterPoint.X + arc.Radius * (float)Math.Cos(arc.StartAngle + arc.SweepAngle);
                    point.Y = arc.CenterPoint.Y + arc.Radius * (float)Math.Sin(arc.StartAngle + arc.SweepAngle);
                    point.Z = 0;
                    return point;
                case ShapeType.Circle:
                    Circle circle = (Circle)shape;
                    point.X = circle.CenterPoint.X + circle.Radius;
                    point.Y = circle.CenterPoint.Y;
                    point.Z = 0;
                    return point;
                case ShapeType.Polyline:
                    Polyline polyline = (Polyline)shape;
                    if (polyline.Closed)
                    {
                        point = polyline.Vertices[0];
                    }
                    else
                    {
                        int n = polyline.Vertices.Count;
                        point = polyline.Vertices[n - 1];
                    }
                    return point;
            }
            return point;
        }

        public void showtime()//显示所需加工时间
        {
            while (true)
            {
                totaltime = markdistance / markspeed + jumpdistance / jumpspeed + (markdelay + jumpdelay) * shape.Count / 1000000 + polycount * polydelay / 1000000;
                double f = Math.Round(totaltime, 3);
                label23.Text = f.ToString() + "s";
            }
        }      

        private void AddShapeToSelectedLayer(ShapeBase shapeEntity)//将图形添加到画板上
        {
            if (canvas.SelectedLayer != null)
            {
                canvas.SelectedLayer.AddShapes(shapeEntity);
            }
            else
            {
                canvas.Document.LayerList[0].AddShapes(shapeEntity);
            }        
            canvas.UpdateDrawing();                        
        }       

        private void CheckLicense()//检查许可文件
        {
            if (!Cti.Hardware.Extension.License.LicenseManager.IsFeatureEnabled(LicenseFeature.Canvas))
            {
                MessageBox.Show("没有找到合适的许可文件.", "错误提示");
            }
        }

        public void Showposition(DeviceStatusSnapshot status)//显示激光加工点的位置
        {
            while (true)
            {
                try
                {
                    Point3D newpoint = status.LaserPositionStatus;
                    laserpositionshow.Text = newpoint.X.ToString() + "," + newpoint.Y.ToString();
                    DrawDot(newpoint.X,newpoint.Y);
                }
                catch
                { }
            }
        }

        private void InitializeScanDeviceManager()//初始化扫描设备
        {
            bool loadingSuccess = false;
            scanDeviceManager = new ScanDeviceManager();
            scanDeviceManager.DeviceListChanged += new EventHandler(scanDeviceManager_DeviceListChanged);
            scanDeviceManager.ScanDeviceGatewayFailed += new EventHandler<ScanDeviceGatewayFailedEventArgs>(scanDeviceManager_ScanDeviceGatewayFailed);
            scanDeviceManager.EnabledStatusCategories |= DeviceStatusCategories.ConnectionStatus | DeviceStatusCategories.ScanningStatus;
            try
            {
                scanDeviceManager.LoadConfiguration();
                loadingSuccess = true;
            }
            #region Exceptions
            catch (ConfigurationLoadingException ex1)
            {
                MessageBox.Show(ex1.Message, "错误提示");
            }
            catch (ScanDeviceGatewayLoadingException ex2)
            {
                MessageBox.Show(ex2.Message, "错误提示");
            }
            #endregion

            if (loadingSuccess)
            {
                scanDeviceManager.InitializeHardware();
            }
        }

        private void RefreshDeviceList()//刷新设备列表
        {
            string[] deviceList = new string[0];
            deviceList = scanDeviceManager.GetDeviceList();
            cmbDevices.Items.Clear();
            for (int i = 0; i < deviceList.Length; i++)
            {
                Device device = new Device();
                device.DeviceUniqueName = deviceList[i];
                device.DeviceFriendlyName = scanDeviceManager.GetDeviceFriendlyName(device.DeviceUniqueName);
                cmbDevices.Items.Add(device);
            }
            if (cmbDevices.Items.Count > 0)
            {
                cmbDevices.SelectedIndex = 0;
            }
            if (deviceList.Length > 0)
            {
                selectedDeviceName = deviceList[0];
            }
        }        

        private void SetLaserParameterValues(VectorImage vectorImage)//设置激光属性
        {
            vectorImage.SetMarkSpeed(markspeed);
            vectorImage.SetJumpSpeed(jumpspeed);
            vectorImage.SetLaserPowerPercentage(power);
            vectorImage.SetJumpDelay(jumpdelay);
            vectorImage.SetLaserOnDelay(laserondelay);
            vectorImage.SetLaserOffDelay(laseroffdelay);
            vectorImage.SetMarkDelay(markdelay);
            vectorImage.SetPolyDelay(polydelay);
            vectorImage.SetRepeatCount(repeatcount);
            vectorImage.SetVelocityCompensationMode(vel, limit, aggressiveness);
            
            vectorImage.SetModulationFrequency(frequency);
        }     

        private void MainForm_Load(object sender, EventArgs e)//主窗体加载
        {
            InitializeScanDeviceManager();
            
            string licenseStatusMessage =LicenseException.LicenseStatusMessage;
            
            cmbMode.SelectedIndex = 0;  
            btnDisconnect.Enabled = false;
            txtjumpspeed.Text = "8000";
            txtMarkspeed.Text = "20";
            txtmarkdelay.Text = "100";
            txtrepeatcount.Text = "1";
            txtjumpdelay.Text = "100";
            txtlaseroffdelay.Text = "100";
            txtlaserondelay.Text = "100";
            txtpower.Text = "50";
            txtfrequence.Text = "10";
            txtpolydelay.Text = "10";            
            txtlimit.Text = "50";
            txtaggressiveness.Text = "1200";
            btnstart.Enabled = false;
            btnStopJob.Enabled = false;
            btnPauseJob.Enabled = false;
            btnDisconnect.Enabled = false;
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            canvas.ZoomIn();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            canvas.ZoomOut();
        }

        private void btnMoveToOrigin_Click(object sender, EventArgs e)
        {
            canvas.UpdateViewport(0, 0,1.8f);
        }

        private void btnScale_Click(object sender, EventArgs e)
        {
            if (canvas.SelectedShapes.Count > 0)
            {
                ScaleDialog scaleDialog = new ScaleDialog();
                if (scaleDialog.ShowDialog() == DialogResult.OK)
                {
                    canvas.ScaleShapes(canvas.SelectedShapes, scaleDialog.ScaleFactor);
                }
            }
        }

        private void btnline_Click(object sender, EventArgs e)
        {
            float x1, x2, y1, y2;
            LineInsertingDialog lineInsertDialog = new LineInsertingDialog();
            if (lineInsertDialog.ShowDialog() == DialogResult.OK)
            {
                x1 = lineInsertDialog.StartX;
                y1 = lineInsertDialog.StartY;
                x2 = lineInsertDialog.EndX;
                y2 = lineInsertDialog.EndY;
                AddLine(x1,y1,x2,y2);
            }
        }

        private void btncircle_Click(object sender, EventArgs e)
        {
            CircleInsertDialog circleinsertdialog = new CircleInsertDialog();
            if (circleinsertdialog.ShowDialog() == DialogResult.OK)
            {
                float x = circleinsertdialog.CenterX;
                float y = circleinsertdialog.CenterY;
                float r = circleinsertdialog.R;
                AddCircle(x,y,r);
            }
        }
        private void btnarc_Click(object sender, EventArgs e)
        {
            ArcInsertDialog arcinsert = new ArcInsertDialog();
            if (arcinsert.ShowDialog() == DialogResult.OK)
            {
                float x = arcinsert.CenterX;
                float y = arcinsert.CenterY;
                float r = arcinsert.R;
                float startangle = arcinsert.Startangle;
                float sweepangle = arcinsert.Angle;
                AddArc(x,y,r,startangle,sweepangle);
            }
        }

        private void btnrectangle_Click(object sender, EventArgs e)
        {
            RectangleDialog recdia = new RectangleDialog();
            if (recdia.ShowDialog() == DialogResult.OK)
            {
                float x = recdia.X;
                float y = recdia.Y;
                float w = recdia.W; 
                float h = recdia.H;
                float angle = recdia.Angle;
                AddRec(x,y,w,h,angle);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        { 
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择要打开的数据文本";
            ofd.Filter = "文本文档|*.txt";
            ofd.ShowDialog();
            string filename = ofd.FileName;
            ImportDataToCanvas(filename);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Multiselect = false;
            openDialog.Filter = "Dxf files (*.dxf)|*.dxf";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openDialog.FileName;
                if (File.Exists(filePath))
                {
                    MessageBoxOptions options = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft ? MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign : (MessageBoxOptions)0;
                    try
                    {
                        LoadDxfFile(filePath);
                    }
                    catch (LicenseException)
                    {
                        MessageBox.Show("未找到有效的许可文件", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, options);
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("文件格式错误", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, options);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("文件无法加载 \nException occurred.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, options);
                    }
                }
            }
        }    

        private void cuttercompensation_Click(object sender, EventArgs e)
        {         
            cuttercompensationDialog cut = new cuttercompensationDialog();
            cuttercompensationwidth = cut.CutterCompensation;
            inner = cut.Inner;
            try
            {
                if (cut.ShowDialog() == DialogResult.OK)
                {                    
                    CutterCompensation(cut.CutterCompensation,inner);
                }
            }
                catch
            { }
        }

        private void BtnRotate_Click(object sender, EventArgs e)
        {
            if (canvas.SelectedShapes.Count > 0)
            {
                RotationDialog rotationDialog = new RotationDialog();
                if (rotationDialog.ShowDialog() == DialogResult.OK)
                {
                    float angle = rotationDialog.Angle;
                    
                    canvas.RotateShapes(canvas.SelectedShapes, angle, rotationDialog.CenterX, rotationDialog.CenterY);
                }
            }
        }

        private void btnmove_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            canvas.DeleteSelectedShapes();
        }

        private void clearall_Click(object sender, EventArgs e)
        {
            ClearCanvas();
        }

        private void BtnShowJump1_Click(object sender, EventArgs e)
        {
            if (!showjumpclick)
            {
                showjumpclick = true;
                canvas.ShowJumps = true;
            }
            else
            {
                showjumpclick = false;
                canvas.ShowJumps = false;
            }
        }

        private void btnshowmarkingdirection_Click(object sender, EventArgs e)
        {
            if (!showdirec)
            {
                showdirec = true;
                canvas.ShowMarkingDirection = true;
            }
            else
            {
                showdirec = false;
                canvas.ShowMarkingDirection = false;
            }
        }

        private void btnopen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Drawing files (*.abc)|*.abc";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                if (!String.IsNullOrEmpty(fileName))
                {
                    ShapeDocument deserializeDoc = new ShapeDocument();

                    FileSerializeHelper.OpenDocument(fileName, deserializeDoc);
                    canvas.Document = deserializeDoc;
                    canvas.UpdateDrawing();
                }
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Drawing files (*.abc)|*.abc";
            saveDialog.OverwritePrompt = false;
            saveDialog.ValidateNames = true;
            DialogResult dialogResult = saveDialog.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                FileSerializeHelper.SaveDocument(saveDialog.FileName, canvas.Document);
            }
            saveDialog.Dispose();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectScanner();           
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectScanner();            
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            ClearPaint();
            StartScan();
        }

        private void btnPauseJob_Click(object sender, EventArgs e)
        {
            PauseContinue();
        }

        private void btnStopJob_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void btnpropertyok_Click(object sender, EventArgs e)
        {
            bool jumpspeedcorrect = float.TryParse(txtjumpspeed.Text, out jumpspeed);
            bool markspeedcorrect = float.TryParse(txtMarkspeed.Text, out markspeed);
            bool powercorrect = float.TryParse(txtpower.Text, out power);
            bool frequencecorrect = float.TryParse(txtfrequence.Text, out frequency);
            bool repeatcountcorrect = int.TryParse(txtrepeatcount.Text, out repeatcount);
            bool jumpdelaycorrect = int.TryParse(txtjumpdelay.Text, out jumpdelay);
            bool markdelaycorrect = int.TryParse(txtmarkdelay.Text, out markdelay);
            bool polydelaycorrect = int.TryParse(txtpolydelay.Text, out polydelay);
            bool laserondelaycorrect = int.TryParse(txtlaserondelay.Text, out laserondelay);
            bool laseroffdelaycorrect = int.TryParse(txtlaseroffdelay.Text, out laseroffdelay);
            bool limitcorrect = float.TryParse(txtlimit.Text, out limit);
            bool aggressivenesscorrect = float.TryParse(txtaggressiveness.Text, out aggressiveness);
            //bool breakanglecorrect = float.TryParse(txtBreakangle.Text, out breakangle);
            //bool maxerrorcorrect = float.TryParse(txterror.Text, out maxerror);
            if (jumpspeedcorrect && markspeedcorrect && powercorrect && frequencecorrect && repeatcountcorrect && jumpdelaycorrect && markdelaycorrect && polydelaycorrect && laseroffdelaycorrect && laserondelaycorrect)
            {
                if (cmbMode.SelectedIndex == 0)
                {
                    vel = VelocityCompensationMode.Disabled;

                }

                if (cmbMode.SelectedIndex == 1)
                {
                    vel = VelocityCompensationMode.DutyCycle;

                }
                if (cmbMode.SelectedIndex == 2)
                {

                    vel = VelocityCompensationMode.Frequency;
                }
                if (cmbMode.SelectedIndex == 3)
                {
                    vel = VelocityCompensationMode.Power;

                }
            }
            else
            {
                MessageBox.Show("请设置合适的激光参数");
            }
        }

        private void chkdefault_CheckedChanged(object sender, EventArgs e) //默认激光属性框是否选择发生改变事件
        {
            if (chkdefault.Checked)
            {
                txtjumpspeed.Text = "8000";
                txtMarkspeed.Text = "20";
                txtmarkdelay.Text = "100";
                txtrepeatcount.Text = "1";
                txtjumpdelay.Text = "100";
                txtlaseroffdelay.Text = "100";
                txtlaserondelay.Text = "100";
                txtpower.Text = "50";
                txtfrequence.Text = "10";
                txtpolydelay.Text = "10";
                //txterror.Text = "0.1";
                //txtBreakangle.Text = "45";
                txtlimit.Text = "50";
                txtaggressiveness.Text = "1200";
            }
            else
            {
                txtjumpspeed.Text = null;
                txtMarkspeed.Text = null;
                txtmarkdelay.Text = null;
                txtrepeatcount.Text = null;
                txtjumpdelay.Text = null;
                txtlaseroffdelay.Text = null;
                txtlaserondelay.Text = null;
                txtpower.Text = null;
                txtfrequence.Text = null;
                txtpolydelay.Text = null;
                //txterror.Text = null;
                //txtBreakangle.Text = null;
                txtlimit.Text = null;
                txtaggressiveness.Text = null;
            }
        
        }

        

        private void timer1_Tick(object sender, EventArgs e)//用于实时更新加工时间
        {
            label19.Visible = true;
            label20.Visible = true;
            label21.Visible = true;
            label22.Visible = true;
            label23.Visible = true;
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value++;
                float f = (float)progressBar1.Value / progressBar1.Maximum;
                label19.Text = ((int)(f * 100)).ToString() + "%";
                label21.Text = (((float)progressBar1.Maximum - (float)progressBar1.Value) / 10).ToString() + "s";//显示剩余加工时间
                
            }
            else
            {
                timetimer.Enabled = false;
                progressBar1.Value = 0;
            }
        }

        private void 显示定时器_Tick(object sender, EventArgs e)
        {
            DeviceStatusSnapshot status = scanDeviceManager.GetDeviceStatusSnapshot(selectedDeviceName);//获取扫描状态 
            Showposition(status);//显示位置
            label23.Visible = true;
            totaltime = markdistance / markspeed + jumpdistance / jumpspeed + (markdelay + jumpdelay) * shape.Count / 1000000 + polycount * polydelay / 1000000;
            double f = Math.Round(totaltime, 3);
            label23.Text = f.ToString() + "s";           
            scanningstatesgow.Visible = true;
            if (status.ScanningStatus == DocumentScanningStatus.Paused)
            {
                scanningstatesgow.Text = "扫描暂停";
                scanstatus = 1;
                btnPauseJob.Enabled = true;
                btnstart.Enabled = false;
                btnStopJob.Enabled = true;
            }

            else if (status.ScanningStatus == DocumentScanningStatus.Scanning)
            {
                scanningstatesgow.Text = "正在扫描";
                scanstatus = 0;
                btnStopJob.Enabled = true;
                btnPauseJob.Enabled = true;
                btnstart.Enabled = false;
            }
            else
            {
                scanningstatesgow.Text = "设备空闲";
                scanstatus = 2;
                btnStopJob.Enabled = false;
                btnstart.Enabled = true;
                btnStopJob.Enabled = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            scanDeviceManager.Close();
            Application.Exit();  
        }

        private void picshow_Paint(object sender, PaintEventArgs e)//画板绘图
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(160,140);
            g.ScaleTransform(4, -4);   
            DrawRuler(g);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            for (int i = 0; i < 6; i++)
            {
                float x = 0.1f*i;
                float y = 3f;
                DrawDot(x, y);
            }          
        }

        private void laserpositionshow_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearPaint(); 
        }

        private void painttimer_Tick(object sender, EventArgs e)//画图的定时器
        {
            DeviceStatusSnapshot status = scanDeviceManager.GetDeviceStatusSnapshot(selectedDeviceName);
            Point3D newpoint = status.LaserPositionStatus;
            DrawDot(newpoint.X,newpoint.Y);//画图，加工完成自动停止
            if((status.ScanningStatus == DocumentScanningStatus.NotScanning))
            {
                painttimer.Enabled=false;
            }
        }

        private void btnsetoffset_Click(object sender, EventArgs e)
        {
            SetOffseDialogt seto = new SetOffseDialogt();
            if (seto.ShowDialog() == DialogResult.OK)
            {
                offset.X = seto.X;
                offset.Y = seto.Y;
                scanDocument.Offset = offset;
            }
         
        }
    }
}
        #endregion