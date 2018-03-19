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
    public partial class SetOffseDialogt : Form
    {
        public SetOffseDialogt()
        {
            InitializeComponent();
        }

        private float x;
        private float y;
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
        private void btnOK_Click(object sender, EventArgs e)
        {
            bool isx = float.TryParse(txtX.Text,out x);
            bool isy = float.TryParse(txtY.Text,out y);
            if(isx && isy)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
