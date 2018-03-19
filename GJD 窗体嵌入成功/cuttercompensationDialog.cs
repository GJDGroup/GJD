using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GJD
{
    public partial class cuttercompensationDialog : Form
    {
        public cuttercompensationDialog()
        {
            InitializeComponent();
        }
        float cuttercompensationwidth;
        bool inner;
        public float CutterCompensation
        {
            get
            {
                return cuttercompensationwidth;
            }
        }
        public bool Inner
        {
            get
            {
                return inner;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool cutter = float.TryParse(textBox1.Text, out cuttercompensationwidth);
            if (cutter)
            {
                if (radioButton1.Checked)
                {
                    inner = true;
                }
                else
                {
                    inner = false;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请设置合适的刀具补偿值");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
