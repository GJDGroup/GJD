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
    public partial class CircleInsertDialog : Form
    {
        public CircleInsertDialog()
        {
            InitializeComponent();
        }
        
        float centerX;
        float centerY;
        float r;
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
        private void btnOkcircle_Click(object sender, EventArgs e)
        {
            bool centerXCorrect = float.TryParse(txtcenterX.Text, out centerX);
            bool centerYCorrect = float.TryParse(txtcenterY.Text, out centerY);
            bool rCorrect = float.TryParse(txtr.Text, out r);
            if (centerXCorrect && centerYCorrect && rCorrect)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void btnCancelcircle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
