using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace GJD
{
    public interface Ilaser
    {
        void LaserOn();
        void InitLaserConnFun();
        void LaserClose();
        void delay(int i);
    }
    public partial class Laser : Form, Ilaser
    {
        public static bool laserHasOn = false;
        public static bool lasercHaslose = false;
        public static bool laserHasinit = false;
        static string statusText = null;
        //临时延时函数
        public void delay(int i)
        {
            while (!(i == 1))
            {
                i--;
            }
        }  
        public Laser()
        {
            InitializeComponent();
        }
        public void InitLaserConnFun()
        {
            try
            {
                if (!laserserialPort.IsOpen)
                {
                    laserserialPort.Open();                    
                }
                CheckForIllegalCrossThreadCalls = false;
                laserHasinit = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("激光器连接不成功" + ex.Message);
            }
           
        }
        //激光器开闭
        private void butLaserOn_Click(object sender, EventArgs e)
        {
            LaserOn();
        }
        public void LaserOn()
        {
            try
            {
                laserserialPort.WriteLine("w60 1\r");
            }
            catch
            {
                texLaserStatus.Text = "Laser on fail\n";
            }
            laserHasOn = true;
        }
        public void LaserClose()
        {
            try
            {
                laserserialPort.WriteLine("w60 0\r");
            }
            catch
            {
                texLaserStatus.Text = "Laser close fail\n";
            }
        }


        private void butLaserClose_Click(object sender, EventArgs e)
        {
            LaserClose();
        }
        //参数激活
        private void btnActivate_Click(object sender, EventArgs e)
        {
            //功率
            try
            {
                laserserialPort.WriteLine(string.Format("w175 {0}\r", textPowerReguPercent.Text));
                labPower.Text = textPowerReguPercent.Text + "%";
            }
            catch
            {
                texLaserStatus.Text = "PowerPercent activate fail\n";
            }
            //Intarnal Gate Frequency
            try
            {
                string interGateFre = textInterGateFre.Text;
                labInterGateFre.Text = textInterGateFre.Text;
                interGateFre = interGateFre.ToLower();
                if (interGateFre.Contains("k"))
                {
                    string orderInterGateFre = interGateFre.Substring(0, interGateFre.IndexOf("k")) + "000";
                    laserserialPort.WriteLine(string.Format("w73 {0}\r", orderInterGateFre));
                }
                else
                {
                    laserserialPort.WriteLine(string.Format("w73 {0}\r", textInterGateFre.Text));
                }

            }
            catch
            {
                texLaserStatus.Text = "InterGateFrequent activate fail\n";
            }
        }
        //复选框限制
        private void checkBoxInternal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxExternal.Checked)
            {
                checkBoxInternal.Checked = false;
            }
            if (checkBoxInternal.Checked)
            {
                labTrigger.Text = "Internal";
                try
                {
                    laserserialPort.WriteLine("w71 4\r");
                }
                catch
                {
                    texLaserStatus.Text = "Internal Trigger fail\n";
                }
            }
        }
        private void checkBoxExternal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInternal.Checked)
            {
                checkBoxExternal.Checked = false;
            }
            if (checkBoxExternal.Checked)
            {
                labTrigger.Text = "External";
                try
                {
                    laserserialPort.WriteLine("w71 1\r");
                }
                catch
                {
                    texLaserStatus.Text = "EXternal Trigger fail\n";
                }
            }
        }
        private void butInfoClear_Click(object sender, EventArgs e)
        {
            statusText = null;
            texLaserStatus.Text = statusText;
        }
        //串口数据反馈
        private void laserserialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            String backRespond = sp.ReadExisting();
            //声明一Action<string>的委托实例
            Action<string> action = (data) =>
            {
                statusText += data + System.Environment.NewLine;
                this.texLaserStatus.Text = statusText;
            };
            Invoke(action, backRespond);
        }

    }
}
