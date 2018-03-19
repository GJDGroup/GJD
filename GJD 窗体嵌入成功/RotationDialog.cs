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
    public partial class RotationDialog : Form
    {
        public RotationDialog()
        {
            InitializeComponent();
        }
        float angle, centerx, centery;
        public float Angle
        {
            get
            {
                if (radioButton1.Checked)
                {
                    return angle / 180 * (float)Math.PI;
                }
                else
                {
                    return angle;
                }

            }
        }
        public float CenterX
        {
            get
            {
                return centerx;
            }
        }
        public float CenterY
        {
            get
            {
                return centery;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool anglecorrect = float.TryParse(textBox1.Text, out angle);
            bool centerxcorrect = float.TryParse(textBox2.Text, out centerx);
            bool centerycorrect = float.TryParse(textBox3.Text, out centery);
            if (anglecorrect && centerxcorrect && centerycorrect)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
