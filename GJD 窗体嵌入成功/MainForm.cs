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
            Form fm=null;
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
                    cuttentpause = currentstatus;
                    currentstatus = "stop";
                    buttonrun.Enabled = true;
                    break;
                case "继续加工":                    
                    currentstatus = cuttentpause;
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
        public static Dictionary<int, double[]> cv840Dmotion = new Dictionary<int,double[]>();
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
       //当前加工状态
        string currentstatus;
        string cuttentpause;
        #endregion

        #region 控制逻辑
        private void timermain_Tick(object sender, EventArgs e)
        {
            //系统各部分状态
            //激光器部分
            switch (currentstatus)
            {
                #region 初始化
                case "init":
                    currentstatus = "inited";
                    //执行子系统初始化操作
                    //激光器初始化
                    Thread tdlaser = new Thread(ilaser.InitLaserConnFun);
                    tdlaser.Start();                   
                    //视觉测量系统初始化
                    Thread tdcvinit = new Thread(icv.CvMeasureInit);
                    tdcvinit.Start();                  
                    //振镜初始化  
                     Thread tdscnainit = new Thread(iscan.ConnectScanner1);
                     tdscnainit.Start();
                break;
                #endregion
                #region 视觉测量
                case "CvMeasure840DMotion":
                    currentstatus = "CvMeasure840DMotioned";                   
                    Thread td840 = new Thread(i840d.Sen840Dmotion);
                    td840.Start(cv840Dmotion[cvCount]);                   
                    break;
                case "CvMeasurePhotograph":
                    currentstatus = "CvMeasurePhotographed";                    
                    Thread tdphoto = new Thread(icv.CvPhotograph);
                    tdphoto.Start();                  
                    break;
                case "Ref":
                    currentstatus = "Refed";                    
                    Thread tdref = new Thread(i840d.Sen840Dmotion);
                    tdref.Start(coorref);
                    break;
                #endregion
                #region 特征结构加工
                case "CharaStrucDataReceive":
                    currentstatus = "CharaStrucDataReceiveed";                    
                    Thread tdcharec = new Thread(icv.CvCharacDataReceive);
                    tdcharec.Start(characnum);
                    break;
                case "CharaStrucG":
                    currentstatus = "CharaStrucGed";                   
                    Thread tdchag = new Thread(i840d.CharaStrucG);
                    tdchag.Start(characnum);                    
                    break;
                case "CharaStruc840DMotion":
                    currentstatus = "CharaStruc840DMotioned";                   
                    Thread tdcha840 = new Thread(i840d.Sen840Dmotion);
                    tdcha840.Start(coorcharac);
                    break;
                case "CharaStrucScanMotion":
                    currentstatus = "CharaStrucScanMotioned";
                    Thread tdscan = new Thread(iscan.Scan);
                    tdscan.Start(characnum);
                    break;
                case "CharacRef":
                    currentstatus="CharacRefed";                  
                    Thread tdcharacref = new Thread(i840d.Sen840Dmotion);
                    tdcharacref.Start(coorref);
                    break;
                #endregion
                #region 边缘加工
                case "EdgeStrucDataReceive":
                    currentstatus = "EdgeStrucDataReceived";                   
                    Thread tdedgeR = new Thread(icv.CvEdgeStrucDataReceive);
                    tdedgeR.Start(edgecnum);                    
                    break;
                case "EdgeStrucG":
                    currentstatus = "EdgeStrucGed";                   
                    Thread tdedgeG = new Thread(i840d.EdgeStrucG);
                    tdedgeG.Start(edgecnum);                  
                    break;
                case "EdgeStrucLaserOn":
                    currentstatus = "EdgeStrucLaserOned";                  
                    Thread tdedgelaser = new Thread(ilaser.LaserOn);
                    tdedgelaser.Start();                    
                    break;
                case "EdgeStruc840DMotion":
                    currentstatus = "EdgeStruc840DMotioned";                    
                    Thread tdedge840 = new Thread(i840d.EdgeStruc840DMachine);
                    tdedge840.Start(edgecnum);
                    break;
                case "EdgeRef":
                    currentstatus="EdgeRefed";                  
                    Thread tdedgecref = new Thread(i840d.Sen840Dmotion);
                    tdedgecref.Start(coorref);
                    break;
                #endregion
                #region 复检
                case "ReCvMeasure840DMotion":
                    currentstatus = "ReCvMeasure840DMotioned";                 
                    Thread retd840 = new Thread(i840d.Sen840Dmotion);
                    retd840.Start(recv840Dmotion[recvCount]);
                    break;
                case "ReCvMeasurePhotograph":
                    currentstatus = "ReCvMeasurePhotographed";                    
                    Thread retdphoto = new Thread(icv.CvPhotograph);
                    retdphoto.Start();                  
                    break;
                case "ReCVRef":
                    currentstatus = "ReCVRefed";                  
                    Thread tdReCVref = new Thread(i840d.Sen840Dmotion);
                    tdReCVref.Start(coorref);
                    break;
                #endregion
            }
        }
        private void timersub_Tick(object sender, EventArgs e)
        {
            switch (currentstatus)
            {
                #region 初始化
                case "inited":
                    if (Laser.laserHasinit)
                    {
                        ///激光器初始化成功
                        iMachParahandle.richboxshow("激光器初始化成功");                       
                    }                   
                    if (CvMeasure.cvMeasureHasinit)
                    {
                        ///视觉测量初始化成功
                        iMachParahandle.richboxshow("视觉测量初始化");
                    }
                    if (Scanner.connectstatus==0)
                    {
                        ///扫描振镜初始化成功
                        iMachParahandle.richboxshow("扫描振镜初始化成功");
                    }

                    if (Laser.laserHasinit & CvMeasure.cvMeasureHasinit & (Scanner.connectstatus == 0))
                    {
                        //系统初始化完成
                        iMachParahandle.richboxshow("系统初始化完成");
                        currentstatus = "CvMeasure840DMotion";
                    }
                    break;

                #endregion
                #region 视觉测量
                case "CvMeasure840DMotioned":
                    if (Sen840D.sen840DHasMotion)
                    {
                        Sen840D.sen840DHasMotion = false;
                        iMachParahandle.richboxshow("第"+cvCount.ToString()+"次"+"840D定位完成");
                        currentstatus = "CvMeasurePhotograph";
                    }
                    break;
                case "CvMeasurePhotographed":
                    if (CvMeasure.cvHasMeasure)
                    {
                        CvMeasure.cvHasMeasure = false;
                        iMachParahandle.richboxshow("第"+cvCount.ToString()+"次"+"拍照完成");
                        if (!(cvCount == cvCountotal-1))
                        {
                            currentstatus = "CvMeasure840DMotion";
                            cvCount += 1;
                        }
                        else
                        {
                            currentstatus = "Ref";
                            //恢复区域计数器
                            cvCount = 0;
                        }
                    }
                    break;
                case "Refed":
                    if (Sen840D.sen840DHasMotion)
                    {
                        Sen840D.sen840DHasMotion = false;
                        currentstatus = "CharaStrucDataReceive";
                    }
                    break;
                #endregion
                #region 特征结构加工
                case "CharaStrucDataReceiveed":
                    if (CvMeasure.cvCharacDataHasReceive)
                    {
                        CvMeasure.cvCharacDataHasReceive = false;
                        iMachParahandle.richboxshow("第"+characnum.ToString()+"个区域特征结构数据接收完成");
                        currentstatus = "CharaStrucG";
                    }
                    break;
                case "CharaStrucGed":
                    if (Sen840D.charaStrucHasG)
                    {
                        Sen840D.charaStrucHasG = false;
                        iMachParahandle.richboxshow("特征区域"+characnum.ToString() + "G指令转换完成");
                        currentstatus = "CharaStruc840DMotion";
                    }
                    break;
                case "CharaStruc840DMotioned":
                    if (Sen840D.sen840DHasMotion)
                    {
                        Sen840D.sen840DHasMotion = false;
                        iMachParahandle.richboxshow("特征区域" + characnum.ToString() + "机床定位完成");
                        currentstatus = "CharaStrucScanMotion";
                    }
                    break;
                case "CharaStrucScanMotioned":
                    if ((Scanner.scanstatus==2))
                    {
                        Scanner.scanstatus = 0;
                        iMachParahandle.richboxshow("特征区域" + characnum.ToString() + "扫描振镜扫描完成");
                        if (!(characnum == charactalnum))
                        {
                            currentstatus = "CharaStrucDataReceive";
                            characnum += 1;
                        }
                        else
                        {
                            currentstatus = "CharacRef";
                            //恢复区域计数器
                            characnum = 1;
                        }
                    }
                    break;
                case "CharacRefed":
                    if (Sen840D.sen840DHasMotion)
                    {
                        Sen840D.sen840DHasMotion = false;
                        iMachParahandle.richboxshow("特征区域加工完成，机床回参考点");
                        currentstatus = "EdgeStrucDataReceive";
                    }
                    break;

                #endregion
                #region 边缘加工
                case "EdgeStrucDataReceived":
                    if (CvMeasure.cvEdgeStrucDataHasReceive)
                    {
                        CvMeasure.cvEdgeStrucDataHasReceive = false;
                        iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘数据接收完成");
                        currentstatus = "EdgeStrucG";
                    }
                    break;
                case "EdgeStrucGed":
                    if (Sen840D.edgeStrucHasG)
                    {
                        Sen840D.edgeStrucHasG = false;
                        iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘G代码转换完成");
                        currentstatus = "EdgeStrucLaserOn";
                    }
                    break;
                case "EdgeStrucLaserOned":
                    if (Laser.laserHasOn)
                    {
                        Laser.laserHasOn = false;
                        iMachParahandle.richboxshow("第" + edgecnum.ToString() + "个边缘加工激光器开");
                        currentstatus = "EdgeStruc840DMotion";
                    }
                    break;
                case "EdgeStruc840DMotioned":
                    if (Sen840D.sen840DHasEdgemachine)
                    {
                        Sen840D.sen840DHasEdgemachine = false;
                        iMachParahandle.richboxshow("边缘" + edgecnum.ToString()+"机床加工完成");
                        if (!(edgecnum == edgectalnum))
                        {                          
                                edgecnum += 1;
                                currentstatus = "EdgeStrucDataReceive";                                   
                        }
                        else
                        {
                            currentstatus = "EdgeRef";
                            //恢复区域计数器
                            edgecnum = 1;
                        }
                    }
                    
                    break;
                case "EdgeRefed":
                    if (Sen840D.sen840DHasMotion)
                    {
                        Sen840D.sen840DHasMotion = false;
                        iMachParahandle.richboxshow("机床回参考点");
                        currentstatus = "ReCvMeasure840DMotion";
                    }
                    break;

                #endregion
                #region 复检
                case "ReCvMeasure840DMotioned":
                    if (Sen840D.sen840DHasMotion)
                    {
                        Sen840D.sen840DHasMotion = false;
                        iMachParahandle.richboxshow("复检第"+recvCount.ToString()+"机床定位完成");
                        currentstatus = "ReCvMeasurePhotograph";
                    }
                    break;
                case "ReCvMeasurePhotographed":
                    if (CvMeasure.cvHasMeasure)
                    {
                        CvMeasure.cvHasMeasure = false;
                        iMachParahandle.richboxshow("复检第" + recvCount.ToString() + "拍照完成");
                        if (!(recvCount == recvCountotal-1))
                        {
                            currentstatus = "ReCvMeasure840DMotion";
                            recvCount += 1;
                        }
                        else
                        {
                            currentstatus = "ReCVRef";
                            recvCount = 0;
                        }
                    }
                    break;
                case "ReCVRefed":
                    if (Sen840D.sen840DHasMotion)
                    {
                        iMachParahandle.richboxshow("当前工件加工完成");
                        currentstatus = "next";
                        buttonrun.Enabled = true;
                        timermain.Enabled = false;
                        timersub.Enabled = false;
                    }
                    //单个加工件加工完成-转下一个                    
                    break;                
                #endregion
            }
        }
        private void initmain()
        {
            ilaser = formlist[2] as Ilaser;
            icv = new CvMeasure();
            i840d =formlist[3] as I840D;
            iscan =formlist[1] as IScan;
            iMachParahandle =formlist[0] as IMachParahandle;
            cvCountotal = 0;
            recvCountotal = 0;
            CV840Dmotion();
            timermain.Enabled = true;
            timersub.Enabled = true;
            currentstatus = "init";
            iMachParahandle.richboxclear();
           
        }
        public void CV840Dmotion()
        {
            string path = Path.GetFullPath(@"..\..\") + "data\\CVPos.xml";
            XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(path);
            XmlNode root = doc.SelectSingleNode("GJD");
            XmlNode cvpos = root.ChildNodes[0];
            XmlNode recvpos = root.ChildNodes[1];
            foreach(XmlNode nd in cvpos.ChildNodes)
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

            XmlNode refnode =root.ChildNodes[2];
            string[] noderef = refnode.InnerText.Split(',');
            for (int i = 0; i < noderef.Length; i++)
            {
                coorref[i] = double.Parse(noderef[i]);
            }
        }
        #endregion        
      
    }
}

