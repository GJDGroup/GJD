namespace GJD
{
    partial class LineInsertingDialog
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
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.txtEndPointY = new System.Windows.Forms.TextBox();
            this.txtEndPointX = new System.Windows.Forms.TextBox();
            this.txtStartPointY = new System.Windows.Forms.TextBox();
            this.txtStartPointX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(158, 207);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(81, 22);
            this.BtnCancel.TabIndex = 29;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(47, 207);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 28;
            this.BtnOk.Text = "确定";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // txtEndPointY
            // 
            this.txtEndPointY.Location = new System.Drawing.Point(108, 154);
            this.txtEndPointY.Name = "txtEndPointY";
            this.txtEndPointY.Size = new System.Drawing.Size(87, 21);
            this.txtEndPointY.TabIndex = 27;
            this.txtEndPointY.TextChanged += new System.EventHandler(this.txtEndPointY_TextChanged);
            // 
            // txtEndPointX
            // 
            this.txtEndPointX.Location = new System.Drawing.Point(108, 113);
            this.txtEndPointX.Name = "txtEndPointX";
            this.txtEndPointX.Size = new System.Drawing.Size(88, 21);
            this.txtEndPointX.TabIndex = 26;
            this.txtEndPointX.TextChanged += new System.EventHandler(this.txtEndPointX_TextChanged);
            // 
            // txtStartPointY
            // 
            this.txtStartPointY.Location = new System.Drawing.Point(109, 76);
            this.txtStartPointY.Name = "txtStartPointY";
            this.txtStartPointY.Size = new System.Drawing.Size(87, 21);
            this.txtStartPointY.TabIndex = 25;
            this.txtStartPointY.TextChanged += new System.EventHandler(this.txtStartPointY_TextChanged);
            // 
            // txtStartPointX
            // 
            this.txtStartPointX.Location = new System.Drawing.Point(109, 32);
            this.txtStartPointX.Name = "txtStartPointX";
            this.txtStartPointX.Size = new System.Drawing.Size(86, 21);
            this.txtStartPointX.TabIndex = 24;
            this.txtStartPointX.TextChanged += new System.EventHandler(this.txtStartPointX_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "终点Y";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "终点X";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "起点Y";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "起点X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // LineInsertingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.txtEndPointY);
            this.Controls.Add(this.txtEndPointX);
            this.Controls.Add(this.txtStartPointY);
            this.Controls.Add(this.txtStartPointX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LineInsertingDialog";
            this.Text = "LineInsertingDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.TextBox txtEndPointY;
        private System.Windows.Forms.TextBox txtEndPointX;
        private System.Windows.Forms.TextBox txtStartPointY;
        private System.Windows.Forms.TextBox txtStartPointX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}