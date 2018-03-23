using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;

namespace GJD
{
    public partial class mainForm : Form
    {
        #region 界面组织与管理
        //预变量定义
        List<SplitContainer> splitContainers = new List<SplitContainer>();
        List<Size> containerSizes = new List<Size>();
        List<int> splitDistances = new List<int>();
        public static Form[] formlist = new Form[7];
        public mainForm()
        {
            InitializeComponent();
            //this.skinEngine1.SkinFile = Application.StartupPath + "\\Skins\\DiamondBlue.ssk";
            splitContainers.Add(splitContainer1);
            splitContainers.Add(splitContainer2);
            splitContainers.Add(splitContainer3);
            splitContainers.Add(splitContainer4);
            splitContainers.Add(splitContainer5);
            splitContainers.Add(splitContainer6);

            containerSizes.Add(splitContainer1.Size);
            containerSizes.Add(splitContainer2.Size);
            containerSizes.Add(splitContainer3.Size);
            containerSizes.Add(splitContainer4.Size);
            containerSizes.Add(splitContainer5.Size);
            containerSizes.Add(splitContainer6.Size);

            splitDistances.Add(splitContainer1.SplitterDistance);
            splitDistances.Add(splitContainer2.SplitterDistance);
            splitDistances.Add(splitContainer3.SplitterDistance);
            splitDistances.Add(splitContainer4.SplitterDistance);
            splitDistances.Add(splitContainer5.SplitterDistance);
            splitDistances.Add(splitContainer6.SplitterDistance);
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            //一次性创建所有窗体
            string[] formClass = { "GJD.MachParas", "GJD.Scanner", "GJD.Laser", "GJD.Sen840D", "GJD.Database", " GJD.ParametersSet", "GJD.MachResults" };
            for (int i = 0; i < formClass.Length; i++)
            {
                GenerateForm(formClass[i], i);
            }
            //默认显示
            menuFoldclose();
            formlist[1].Parent = panel1;
            formlist[1].Show();
        }

        //窗体预生成函数
        public void GenerateForm(string form, int i)
        {  
            //反射生成窗体
            string strName = form;
            Form fm = null;
            fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);
            //if (strName == "GJD.Scanner")
            //{
            //    Assembly ass = Assembly.LoadFrom("振镜新版.exe");
            //    if (ass != null)
            //    {
            //        Type myType = ass.GetType(strName);
            //        fm = myType.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic
            //            | BindingFlags.Instance | BindingFlags.CreateInstance | BindingFlags.Static, null, null, null) as Form;
            //    }
            //    else
            //    {
            //        fm = (Form)Assembly.Load(assemblyPath).CreateInstance(strName);
            //    }
            //}
            //else
            //{
            //    fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);
            //}
            //设置窗体没有边框，加入到选项卡中
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = this.panel1;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            formlist[i] = fm;

        }
        //根据按钮选择显示的窗体

        //状态显示
        private void button1_Click(object sender, EventArgs e)
        {
            Button currentBution = (Button)sender;
            switch (currentBution.Text)
            {
                case "状态显示":
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(formlist[0]);
                    formlist[0].Show();
                    break;
                case "工艺参数":
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(formlist[5]);
                    formlist[5].Show();
                    break;
                case "扫描振镜":
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(formlist[1]);
                    formlist[1].Show();
                    break;
                case "激光器":
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(formlist[2]);
                    formlist[2].Show();
                    break;
                case "机床":
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(formlist[3]);
                    formlist[3].Show();
                    break;
                case "功率补偿":
                    break;
                case "光路指正":
                    break;
                case "结果显示":
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(formlist[6]);
                    formlist[6].Show();
                    break;
                case "工艺数据库":
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(formlist[4]);
                    formlist[4].Show();
                    break;
                case "开始加工":
                    menuFoldclose();
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(formlist[0]);
                    formlist[0].Show();
                    buttonrun.Enabled = false;
                    initmain();
                    break;
                case "暂停加工":
                    buttonrun.Enabled = true;
                    break;
                case "继续加工":
                    buttonrun.Enabled = true;
                    break;
                default:
                    break;
            }

        }

        //菜单折叠效果实现
        public void menuFoldclose()
        {
            for (int i = 0; i < splitContainers.Count; i++)
            {
                splitContainers[i].Panel2Collapsed = true;
                splitContainers[i].Size = new Size(splitContainers[i].Size.Width, 25);
            }
        }
        private void menuFoldAndExpand(object sender, MouseEventArgs e)
        {
            int i;
            Label splitPanelLabel = (Label)sender;
            for (i = 0; i < splitContainers.Count; i++)
                if (splitContainers[i].Panel1 == splitPanelLabel.Parent)
                {
                    splitContainers[i].Panel2Collapsed = !splitContainers[i].Panel2Collapsed;
                    if (!splitContainers[i].Panel2Collapsed)
                    {
                        splitContainers[i].Size = containerSizes[i];
                        splitContainers[i].SplitterDistance = splitDistances[i];
                        for (int j = 0; j < splitContainers.Count; j++)
                        {
                            if (!(j == i))
                            {
                                splitContainers[j].Panel2Collapsed = true;
                                splitContainers[j].Size = new Size(splitContainers[i].Size.Width, 25);
                            }
                        }
                    }
                    else
                    {
                        splitContainers[i].Size = new Size(splitContainers[i].Size.Width, 25);

                    }
                    break;

                }
        }

        #endregion 界面组织与管理

        #region 控制参数
        ///控制参数
        //视觉测量运动 cv840Dmotion，由用户定义
        public static Dictionary<int, double[]> cv840Dmotion = new Dictionary<int, double[]>();
        public static Dictionary<int, double[]> recv840Dmotion = new Dictionary<int, double[]>();
        //视觉测量单次运动标志
        public static int cvCount = 0;
        public static int cvCountotal = 0;
        //视觉复检单次运动标志
        public static int recvCount = 0;
        public static int recvCountotal = 0;


        //特征结构加工过程过程机床运动控制参数
        //机床定位参数
        public static double[] coorcharac { get; set; }
        //特征加工区域编号与总区域数量
        public static int characnum = 1;
        public static int charactalnum = 3;
        //单个区域扫描次数
        public static int charascannumtal = 2;

        //边缘编号
        public static int edgecnum = 1;
        public static int edgectalnum = 3;
        //单个边缘加工次数
        public static int edgemachinetalnum = 2;
        //边缘当前加工次数
        public static int edgemachinenum = 1;

        //参考点坐标
        public static double[] coorref = new double[5];


        //接口
        Ilaser ilaser { get; set; }
        ICvMeasure icv { get; set; }
        I840D i840d { get; set; }
        IMachParahandle iMachParahandle { get; set; }
        IScan iscan { get; set; }

        //加工状态
        public int RunStep_Free = 0;       //空闲状态
        public int RunStep_CvMeasure840DMotion = 2;//视觉测量机床定位
        public int RunStep_CvMeasurePhotograph = 4;//视觉测量拍照
        public int RunStep_CharaStrucDataReceive = 6;//特征结构加工数据接收
        public int RunStep_CharaStrucG = 8;//特征结构加工G指令转换
        public int RunStep_CharaStruc840DMotion = 10;//特征结构加工机床定位
        public int RunStep_CharaStrucScanMotion = 12;//特征结构加工振镜扫描
        public int RunStep_EdgeStrucDataReceive = 14;//接收边缘特征的测量数据
        public int RunStep_EdgeStrucG = 16;//边缘测量数据转G代码
        public int RunStep_EdgeStrucLaserOn = 18;//边缘加工激光器出光
        public int RunStep_EdgeStruc840DMotion = 20;//边缘加工机床走G代码
        public int RunStep_EdgeStrucLaserClose = 22;//边缘加工激光器关
        public int RunStep_ReCvMeasure840DMotion = 24;//复检机床定位
        public int RunStep_ReCvMeasurePhotograph = 26;//复检拍照
        public int RunStep_ReCvMeasureResult = 28;//复检结果接收
        public int RunStep_Ref = 64;//机床回参考点
        //当前加工状态
        int currentstatus = 0;
        #endregion

        #region 控制逻辑
        private void timermain_Tick(object sender, EventArgs e)
        {
            //系统各部分状态
            switch (currentstatus)
            {
                #region 视觉测量
                case 1:
                    if (!(RunStep_CvMeasure840DMotion % 2 == 0))
                    {
                        i840d.Sen840Dmotion(cv840Dmotion[cvCount]);
                        RunStep_CvMeasure840DMotion++;
                    }
                    else
                    {
                        if (Sen840D.sen840DHasMotion == 0)
                        {
                            iMachParahandle.richboxshow("第" + cvCount.ToString() + "次" + "机床准备定位");
                        }
                        if (Sen840D.sen840DHasMotion == 1)
                        {
                            iMachParahandle.richboxshow("第" + cvCount.ToString() + "次" + "机床定位中");
                        }
                        if (Sen840D.sen840DHasMotion == 2)
                        {
                            iMachParahandle.richboxshow("第" + cvCount.ToString() + "次" + "机床定位完成");
                            RunStep_CvMeasurePhotograph--;
                            currentstatus = RunStep_CvMeasurePhotograph;
                        }
                    }
                    break;
                case 3:
                    if (!(RunStep_CvMeasurePhotograph % 2 == 0))
                    {
                        icv.CvPhotograph();
                        RunStep_CvMeasurePhotograph = RunStep_CvMeasurePhotograph + 1;
                    }
                    else
                    {
                        if (CvMeasure.cvHasMeasure == 0)
                        {
                            iMachParahandle.richboxshow("第" + cvCount.ToString() + "次" + "相机准备拍照");
                        }
                        if (CvMeasure.cvHasMeasure == 1)
                        {
                            iMachParahandle.richboxshow("第" + cvCount.ToString() + "次" + "相机拍照中");
                        }
                        if (CvMeasure.cvHasMeasure == 2)
                        {
                            iMachParahandle.richboxshow("第" + cvCount.ToString() + "次" + "相机拍照完成");
                            if (!(cvCount == cvCountotal - 1))
                            {
                                RunStep_CvMeasure840DMotion--;
                                currentstatus = RunStep_CvMeasure840DMotion;
                                cvCount += 1;
                            }
                            else
                            {
                                RunStep_CharaStrucDataReceive--;
                                currentstatus = RunStep_CharaStrucDataReceive;
                                //恢复区域计数器
                                cvCount = 0;
                            }
                        }
                    }
                    break;
                #endregion 视觉测量
                #region 特征结构加工
                //数据接收
                case 5:
                    if (!(RunStep_CharaStrucDataReceive % 2 == 0))
                    {
                        icv.CvCharacDataReceive(characnum);
                        RunStep_CharaStrucDataReceive++;
                    }
                    else
                    {
                        if (CvMeasure.cvCharacDataHasReceive == 0)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域数据准备接收");
                        }
                        if (CvMeasure.cvCharacDataHasReceive == 1)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域数据正在接收");
                        }
                        if (CvMeasure.cvCharacDataHasReceive == 2)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域数据接收完成");
                            RunStep_CharaStrucG--;
                            currentstatus = RunStep_CharaStrucG;
                        }
                    }
                    break;
                //G指令转换
                case 7:
                    if (!(RunStep_CharaStrucG % 2 == 0))
                    {
                        i840d.CharaStrucG(characnum);
                        RunStep_CharaStrucG++;
                    }
                    else
                    {
                        if (Sen840D.charaStrucHasG == 0)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域G指令准备转换");
                        }
                        if (Sen840D.charaStrucHasG == 1)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域G指令正在转换");
                        }
                        if (Sen840D.charaStrucHasG == 2)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域G指令转换完成");
                            RunStep_CharaStruc840DMotion--;
                            currentstatus = RunStep_CharaStruc840DMotion;
                        }
                    }
                    break;
                //机床定位
                case 9:
                    if (!(RunStep_CharaStruc840DMotion % 2 == 0))
                    {
                        i840d.Sen840Dmotion(coorcharac);
                        RunStep_CharaStruc840DMotion++;
                    }
                    else
                    {
                        if (Sen840D.sen840DHasMotion == 0)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域机床准备完成");
                        }
                        if (Sen840D.sen840DHasMotion == 1)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域机床正在");
                        }
                        if (Sen840D.sen840DHasMotion == 2)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域机床定位完成");
                            RunStep_CharaStrucScanMotion--;
                            currentstatus = RunStep_CharaStrucScanMotion;
                        }
                    }
                    break;
                //振镜扫描
                case 11:
                    if (!(RunStep_CharaStrucScanMotion % 2 == 0))
                    {
                        iscan.Scan(characnum);
                        RunStep_CharaStrucScanMotion++;
                    }
                    else
                    {
                        if (Scanner.scanstatus == 0)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域机床准备扫描");
                        }
                        if (Scanner.scanstatus == 1)
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域振镜正在扫描");
                        }
                        if ((Scanner.scanstatus == 2))
                        {
                            iMachParahandle.richboxshow("第" + characnum.ToString() + "个区域振镜扫描完成");
                            if (!(characnum == charactalnum))
                            {
                                RunStep_CharaStrucDataReceive--;
                                currentstatus = RunStep_CharaStrucDataReceive;
                                characnum += 1;
                            }
                            else
                            {
                                RunStep_EdgeStrucDataReceive--;
                                currentstatus = RunStep_EdgeStrucDataReceive;
                                //恢复区域计数器
                                characnum = 1;
                            }
                        }
                    }
                    break;
                #endregion
                #region 边缘加工
                //边缘数据接收
                case 13:
                    if (!(RunStep_EdgeStrucDataReceive % 2 == 0))
                    {
                        icv.CvEdgeStrucDataReceive(edgecnum);
                        RunStep_EdgeStrucDataReceive++;
                    }
                    else
                    {
                        if (CvMeasure.cvEdgeStrucDataHasReceive == 0)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "条边缘数据准备接收");
                        }
                        if (CvMeasure.cvEdgeStrucDataHasReceive == 1)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "条边缘数据正在接收");
                        }
                        if (CvMeasure.cvEdgeStrucDataHasReceive == 2)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "条边缘数据接收完成");
                            RunStep_EdgeStrucG--;
                            currentstatus = RunStep_EdgeStrucG;
                        }
                    }
                    break;
                //边缘G代码转换
                case 15:
                    if (!(RunStep_EdgeStrucG % 2 == 0))
                    {
                        i840d.EdgeStrucG(edgecnum);
                        RunStep_EdgeStrucG++;
                    }
                    else
                    {
                        if (Sen840D.edgeStrucHasG == 0)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "条边缘G代码准备转换");
                        }
                        if (Sen840D.edgeStrucHasG == 1)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "条边缘G代码正在转换");
                        }
                        if (Sen840D.edgeStrucHasG == 2)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "条边缘G代码转换完成");
                            RunStep_EdgeStrucLaserOn--;
                            currentstatus = RunStep_EdgeStrucLaserOn;
                        }
                    }
                    break;
                //开激光器
                case 17:
                    if (!(RunStep_EdgeStrucLaserOn % 2 == 0))
                    {
                        ilaser.LaserOn();
                        RunStep_EdgeStrucLaserOn++;
                    }
                    else
                    {
                        if (Laser.laserHasOn == 0)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘加工激光准备开");
                        }
                        if (Laser.laserHasOn == 1)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘加工激光器正在打开");
                        }
                        if (Laser.laserHasOn == 2)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘加工激光器开");
                            RunStep_EdgeStruc840DMotion--;
                            currentstatus = RunStep_EdgeStruc840DMotion;
                        }
                    }
                    break;
                //机床加工
                case 19:
                    if (!(RunStep_EdgeStruc840DMotion % 2 == 0))
                    {
                        i840d.EdgeStruc840DMachine(edgecnum);
                        RunStep_EdgeStruc840DMotion++;
                    }
                    else
                    {
                        if (Sen840D.sen840DHasEdgemachine == 0)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘机床准备加工");
                        }
                        if (Sen840D.sen840DHasEdgemachine == 1)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘机床正在加工");
                        }
                        if (Sen840D.sen840DHasEdgemachine == 2)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘机床加工完成");                         
                                RunStep_EdgeStrucLaserClose--;
                                currentstatus = RunStep_EdgeStrucLaserClose;                                                     
                        }

                    }
                    break;
                //关激光器
                case 21:
                    if (!(RunStep_EdgeStrucLaserClose % 2 == 0))
                    {
                        ilaser.LaserClose();
                        RunStep_EdgeStrucLaserClose++;
                    }
                    else
                    {
                        if (Laser.lasercHaslose == 0)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘加工激光准备关");
                        }
                        if (Laser.lasercHaslose == 1)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘加工激光器正在关闭");
                        }
                        if (Laser.lasercHaslose == 2)
                        {
                            iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘加工激光器关");
                            if (!(edgecnum == edgectalnum))
                            {
                                edgecnum += 1;
                                RunStep_EdgeStrucDataReceive--;
                                currentstatus = RunStep_EdgeStrucDataReceive;
                            }
                            else
                            {
                                RunStep_ReCvMeasure840DMotion--;
                                currentstatus = RunStep_ReCvMeasure840DMotion;
                                //恢复区域计数器
                                edgecnum = 1; 
                            }
                        }
                    }
                    break;
                #endregion
                #region 复检
                //定位
                case 23:
                    if (!(RunStep_ReCvMeasure840DMotion % 2 == 0))
                    {
                        i840d.Sen840Dmotion(recv840Dmotion[recvCount]);
                        RunStep_ReCvMeasure840DMotion++;
                    }
                    else
                    {
                        if (Sen840D.sen840DHasMotion == 0)
                        {
                            iMachParahandle.richboxshow("复检第" + recvCount.ToString() + "次机床准备定位");
                        }
                        if (Sen840D.sen840DHasMotion == 1)
                        {
                            iMachParahandle.richboxshow("复检第" + recvCount.ToString() + "次机床正在定位");
                        }
                        if (Sen840D.sen840DHasMotion == 2)
                        {
                            iMachParahandle.richboxshow("复检第" + recvCount.ToString() + "次机床定位完成");
                            RunStep_ReCvMeasurePhotograph--;
                            currentstatus = RunStep_ReCvMeasurePhotograph;
                        }
                    }
                    break;
                //拍照
                case 25:
                    if (!(RunStep_ReCvMeasurePhotograph % 2 == 0))
                    {
                        icv.CvPhotograph();
                        RunStep_ReCvMeasurePhotograph++;
                    }
                    else
                    {
                        if (CvMeasure.cvHasMeasure == 0)
                        {
                            iMachParahandle.richboxshow("复检第" + recvCount.ToString() + "次机床准备定位");
                        }
                        if (CvMeasure.cvHasMeasure == 1)
                        {
                            iMachParahandle.richboxshow("复检第" + recvCount.ToString() + "次机床正在定位");
                        }
                        if (CvMeasure.cvHasMeasure == 2)
                        {
                            iMachParahandle.richboxshow("复检第" + recvCount.ToString() + "拍照完成");
                            if (!(recvCount == recvCountotal - 1))
                            {
                                RunStep_ReCvMeasure840DMotion--;
                                currentstatus = RunStep_ReCvMeasure840DMotion;
                                recvCount += 1;
                            }
                            else
                            {
                                RunStep_ReCvMeasureResult--;
                                currentstatus = RunStep_ReCvMeasureResult;
                                recvCount = 0;
                            }
                        }
                    }
                    break;
                //接收复检数据
                case 27:
                    if (!(RunStep_ReCvMeasureResult % 2 == 0))
                    {
                        icv.CvReMeasureResult();
                        RunStep_ReCvMeasureResult++;
                    }
                    else
                    {
                        if (CvMeasure.cvHasMeasureResult == 0)
                        {
                            iMachParahandle.richboxshow("复检结果准备接收");
                        }
                        if (CvMeasure.cvHasMeasureResult == 1)
                        {
                            iMachParahandle.richboxshow("复检结果正在接收");
                        }
                        if (CvMeasure.cvHasMeasureResult == 2)
                        {
                            iMachParahandle.richboxshow("复检结果接收完成");
                            currentstatus = RunStep_Free;
                        }
                    }
                    break;
                #endregion
            }
        }
        private void initmain()
        {
            ilaser = formlist[2] as Ilaser;
            icv = new CvMeasure();
            i840d = formlist[3] as I840D;
            iscan = formlist[1] as IScan;
            iMachParahandle = formlist[0] as IMachParahandle;
            cvCountotal = 0;
            recvCountotal = 0;
            CV840Dmotion();          
            iMachParahandle.richboxclear(); 
            if (initsys())
            {
                RunStep_CvMeasure840DMotion--;
                currentstatus = RunStep_CvMeasure840DMotion;
                timermain.Enabled = true;
            }
            else
            {
                return;
            }
        }
        public void CV840Dmotion()
        {
            string path = Path.GetFullPath(@"..\..\") + "data\\CVPos.xml";
            XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(path);
            XmlNode root = doc.SelectSingleNode("GJD");
            XmlNode cvpos = root.ChildNodes[0];
            XmlNode recvpos = root.ChildNodes[1];
            foreach (XmlNode nd in cvpos.ChildNodes)
            {
                double[] pos = new double[5];
                string[] nodetext = nd.InnerText.Split(',');
                for (int i = 0; i < nodetext.Length; i++)
                {
                    string a = nodetext[0];
                    double b = double.Parse(a);
                    pos[i] = double.Parse(nodetext[i]);
                }
                cv840Dmotion.Add(cvCountotal, pos);
                cvCountotal += 1;
            }
            foreach (XmlNode nd in recvpos.ChildNodes)
            {
                double[] pos = new double[5];
                string[] nodetext = nd.InnerText.Split(',');
                for (int i = 0; i < nodetext.Length; i++)
                {
                    pos[i] = double.Parse(nodetext[i]);

                }
                recv840Dmotion.Add(recvCountotal, pos);
                recvCountotal += 1;
            }

            XmlNode refnode = root.ChildNodes[2];
            string[] noderef = refnode.InnerText.Split(',');
            for (int i = 0; i < noderef.Length; i++)
            {
                coorref[i] = double.Parse(noderef[i]);
            }
        }
        public bool initsys()
        {
            //执行子系统初始化操作
            //激光器初始化
            ilaser.InitLaserConnFun();
            //视觉测量系统初始化
            icv.CvMeasureInit();
            //振镜初始化  
           iscan.ConnectScanner1();
            if (Laser.laserHasinit == 2 & CvMeasure.cvMeasureHasinit == 2 & Scanner.scanstatus == 2)
            {
                iMachParahandle.richboxshow("系统初始化完成");
                return true;
            }
            else
            {
                if (!(Laser.laserHasinit == 2))
                {
                    if (Laser.laserHasinit == 1)
                    {
                        iMachParahandle.richboxshow("激光器正在初始化");
                    }
                    else
                    {
                        iMachParahandle.richboxshow("激光器初始化失败");
                    }
                }
                if (!(CvMeasure.cvMeasureHasinit == 2))
                {
                    if (CvMeasure.cvMeasureHasinit == 1)
                    {
                        iMachParahandle.richboxshow("视觉测量系统正在连接");
                    }
                    else
                    {
                        iMachParahandle.richboxshow("视觉测量系统连接失败");
                    }
                }
                if (!(Scanner.scanstatus == 2))
                {
                    if (Scanner.scanstatus == 1)
                    {
                        iMachParahandle.richboxshow("振镜正在连接");
                    }
                    else
                    {
                        iMachParahandle.richboxshow("振镜连接失败");
                    }
                }
                return false;
            }
        }
        #endregion

    }
}

