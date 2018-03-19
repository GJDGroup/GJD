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
    public partial class RectangleDialog : Form
    {
        public RectangleDialog()
        {
            InitializeComponent();
        }
        float x, y, w, h, angle;
        public float X
        {
            get
            {
                return x;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
        }
        public float W
        {
            get
            {
                return w;
            }
        }
        public float H
        {
            get
            {
                return h;
            }
        }
        public float Angle
        {
            get
            {
                return angle / 180 * (float)Math.PI;
                

            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            bool xcorrect = float.TryParse(txtX.Text, out x);
            bool ycorrect = float.TryParse(txtY.Text, out y);
            bool wcorrect = float.TryParse(txtW.Text, out w);
            bool hcorrect = float.TryParse(txtH.Text, out h);
            bool acorrect = float.TryParse(txtAngle.Text, out angle);
            if (xcorrect && ycorrect && wcorrect && hcorrect && acorrect)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请输入合适的矩形的参数");
            }
        }

        private void btnCnacel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
