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
    public partial class ArcInsertDialog : Form
    {
        public ArcInsertDialog()
        {
            InitializeComponent();
        }
        public float CenterX
        {
            get
            {
                return centerX;

            }
        }
        public float CenterY
        {
            get
            {
                return centerY;

            }
        }
        public float R
        {
            get
            {
                return r;

            }
        }
        public float Startangle
        {
            get
            {
      
                    return startangle / 180 * (float)Math.PI;


            }
        }
        public float Angle
        {
            get
            {
                
                    return angle / 180 * (float)Math.PI;
                

            }
        }
        float centerX, centerY, r, startangle, angle;
        private void btnOk_Click(object sender, EventArgs e)
        {
            bool centerXcorrect = float.TryParse(txtcenterX.Text, out centerX);
            bool centerYcorrect = float.TryParse(txtcenterY.Text, out centerY);
            bool rcorrect = float.TryParse(txtr.Text, out r);
            bool startanglecorrect = float.TryParse(txtstartangle.Text, out startangle);
            bool anglecorrect = float.TryParse(txtangle.Text, out angle);
            if (centerYcorrect && centerXcorrect && rcorrect && startanglecorrect && anglecorrect)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtangle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtstartangle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtr_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcenterY_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcenterX_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ArcInsertDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
