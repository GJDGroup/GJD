namespace GJD
{
    partial class ArcInsertDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btncancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtangle = new System.Windows.Forms.TextBox();
            this.txtstartangle = new System.Windows.Forms.TextBox();
            this.txtr = new System.Windows.Forms.TextBox();
            this.txtcenterY = new System.Windows.Forms.TextBox();
            this.txtcenterX = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(146, 208);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 33;
            this.btncancel.Text = "取消";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(36, 208);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 32;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtangle
            // 
            this.txtangle.Location = new System.Drawing.Point(80, 170);
            this.txtangle.Name = "txtangle";
            this.txtangle.Size = new System.Drawing.Size(100, 21);
            this.txtangle.TabIndex = 27;
            this.txtangle.TextChanged += new System.EventHandler(this.txtangle_TextChanged);
            // 
            // txtstartangle
            // 
            this.txtstartangle.Location = new System.Drawing.Point(80, 137);
            this.txtstartangle.Name = "txtstartangle";
            this.txtstartangle.Size = new System.Drawing.Size(100, 21);
            this.txtstartangle.TabIndex = 28;
            this.txtstartangle.TextChanged += new System.EventHandler(this.txtstartangle_TextChanged);
            // 
            // txtr
            // 
            this.txtr.Location = new System.Drawing.Point(80, 103);
            this.txtr.Name = "txtr";
            this.txtr.Size = new System.Drawing.Size(100, 21);
            this.txtr.TabIndex = 29;
            this.txtr.TextChanged += new System.EventHandler(this.txtr_TextChanged);
            // 
            // txtcenterY
            // 
            this.txtcenterY.Location = new System.Drawing.Point(80, 66);
            this.txtcenterY.Name = "txtcenterY";
            this.txtcenterY.Size = new System.Drawing.Size(100, 21);
            this.txtcenterY.TabIndex = 30;
            this.txtcenterY.TextChanged += new System.EventHandler(this.txtcenterY_TextChanged);
            // 
            // txtcenterX
            // 
            this.txtcenterX.Location = new System.Drawing.Point(80, 32);
            this.txtcenterX.Name = "txtcenterX";
            this.txtcenterX.Size = new System.Drawing.Size(100, 21);
            this.txtcenterX.TabIndex = 31;
            this.txtcenterX.TextChanged += new System.EventHandler(this.txtcenterX_TextChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(204, 118);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 26;
            this.radioButton2.Text = "弧度";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(204, 86);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 25;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "角度";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "掠角";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "起始角";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "半径R";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "圆心Y";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "圆心X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ArcInsertDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtangle);
            this.Controls.Add(this.txtstartangle);
            this.Controls.Add(this.txtr);
            this.Controls.Add(this.txtcenterY);
            this.Controls.Add(this.txtcenterX);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ArcInsertDialog";
            this.Text = "ArcInsertDialog";
            this.Load += new System.EventHandler(this.ArcInsertDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtangle;
        private System.Windows.Forms.TextBox txtstartangle;
        private System.Windows.Forms.TextBox txtr;
        private System.Windows.Forms.TextBox txtcenterY;
        private System.Windows.Forms.TextBox txtcenterX;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}