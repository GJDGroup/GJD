﻿using System;
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
    public partial class MoveShapeDialog : Form
    {
        public MoveShapeDialog()
        {
            InitializeComponent();
        }
        float dx, dy;
        public float DX
        {
            get
            {
                return dx;
            }
        }
        public float DY
        {
            get
            {
                return dy;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool dxcorrect = float.TryParse(textBox1.Text, out dx);
            bool dycorrect = float.TryParse(textBox2.Text, out dy);
            if (dxcorrect && dycorrect)
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
