using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace GJD
{
    public partial class ParametersSet : Form
    {
        public ParametersSet()
        {
            InitializeComponent();
        }

        private void buttonParaSet_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.GetFullPath(@"..\..\") + "data\\Parameters.xml";
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(path);
                XmlNode xmlLaserF = doc.SelectSingleNode("GJD/Laser/power");
                xmlLaserF.InnerText = textLaserF.Text;
                XmlNode xmlLaserP = doc.SelectSingleNode("GJD/Laser/frequencyset");
                xmlLaserP.InnerText = textLaserP.Text;

                XmlNode xmlScanV = doc.SelectSingleNode("GJD/Scanner/scanspeed");
                xmlScanV.InnerText = textScanV.Text;
                XmlNode xmlScanNum = doc.SelectSingleNode("GJD/Scanner/scancount");
                xmlScanNum.InnerText = textScanNum.Text;
                XmlNode xmlScanRC = doc.SelectSingleNode("GJD/Scanner/scanR");
                xmlScanRC.InnerText = textScanRC.Text;

                XmlNode xml840dVx = doc.SelectSingleNode("GJD/Simens840D/Speedx");
                XmlNode xml840dVy = doc.SelectSingleNode("GJD/Simens840D/Speedy");
                XmlNode xml840dVz = doc.SelectSingleNode("GJD/Simens840D/Speedz");
                XmlNode xml840dVa = doc.SelectSingleNode("GJD/Simens840D/Speeda");
                XmlNode xml840dVb = doc.SelectSingleNode("GJD/Simens840D/Speedb");
                xml840dVx.InnerText = text840dVx.Text;
                xml840dVy.InnerText = text840dVy.Text;
                xml840dVz.InnerText = text840dVz.Text;
                xml840dVa.InnerText = text840dVa.Text;
                xml840dVb.InnerText = text840dVb.Text;
                doc.Save(path);
                labelParaset.Text = "参数设置成功";
            }
            catch
            {
                labelParaset.Text = "参数设置失败";
            }
            

        }
    }
}
