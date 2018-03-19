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
    public partial class ScaleDialog : Form
    {
        public ScaleDialog()
        {
            InitializeComponent();
        }
        float scaleFactor;
        public float ScaleFactor
        {
            get
            {
                return scaleFactor;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool correct = float.TryParse(textBox1.Text, out scaleFactor);
            if (correct)
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
