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
    public partial class LineInsertingDialog : Form
    {
        public LineInsertingDialog()
        {
            InitializeComponent();
        }
        float startX;
        float startY;
        float endX;
        float endY;
        public float StartX
        {
            get
            {
                return startX;
            }
        }



        public float StartY
        {
            get
            {
                return startY;
            }
        }

        public float EndX
        {
            get
            {
                return endX;
            }
        }


        public float EndY
        {
            get
            {
                return endY;
            }
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            bool startXCorrect = float.TryParse(txtStartPointX.Text, out startX);
            bool startYCorrect = float.TryParse(txtStartPointY.Text, out startY);
            bool endXCorrect = float.TryParse(txtEndPointX.Text, out endX);
            bool endYCorrect = float.TryParse(txtEndPointY.Text, out endY);

            if (startXCorrect && startYCorrect && endXCorrect && endYCorrect)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtEndPointY_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEndPointX_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStartPointY_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStartPointX_TextChanged(object sender, EventArgs e)
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
    }
}
