namespace GJD
{
    partial class CircleInsertDialog
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
            this.btnCancelcircle = new System.Windows.Forms.Button();
            this.btnOkcircle = new System.Windows.Forms.Button();
            this.txtr = new System.Windows.Forms.TextBox();
            this.txtcenterY = new System.Windows.Forms.TextBox();
            this.txtcenterX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancelcircle
            // 
            this.btnCancelcircle.Location = new System.Drawing.Point(156, 124);
            this.btnCancelcircle.Name = "btnCancelcircle";
            this.btnCancelcircle.Size = new System.Drawing.Size(75, 23);
            this.btnCancelcircle.TabIndex = 21;
            this.btnCancelcircle.Text = "取消";
            this.btnCancelcircle.UseVisualStyleBackColor = true;
            this.btnCancelcircle.Click += new System.EventHandler(this.btnCancelcircle_Click);
            // 
            // btnOkcircle
            // 
            this.btnOkcircle.Location = new System.Drawing.Point(46, 124);
            this.btnOkcircle.Name = "btnOkcircle";
            this.btnOkcircle.Size = new System.Drawing.Size(75, 23);
            this.btnOkcircle.TabIndex = 20;
            this.btnOkcircle.Text = "确定";
            this.btnOkcircle.UseVisualStyleBackColor = true;
            this.btnOkcircle.Click += new System.EventHandler(this.btnOkcircle_Click);
            // 
            // txtr
            // 
            this.txtr.Location = new System.Drawing.Point(114, 82);
            this.txtr.Name = "txtr";
            this.txtr.Size = new System.Drawing.Size(100, 21);
            this.txtr.TabIndex = 19;
            // 
            // txtcenterY
            // 
            this.txtcenterY.Location = new System.Drawing.Point(114, 53);
            this.txtcenterY.Name = "txtcenterY";
            this.txtcenterY.Size = new System.Drawing.Size(100, 21);
            this.txtcenterY.TabIndex = 18;
            // 
            // txtcenterX
            // 
            this.txtcenterX.Location = new System.Drawing.Point(114, 20);
            this.txtcenterX.Name = "txtcenterX";
            this.txtcenterX.Size = new System.Drawing.Size(100, 21);
            this.txtcenterX.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "半径R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "圆心Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "圆心X";
            // 
            // CircleInsertDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 170);
            this.Controls.Add(this.btnCancelcircle);
            this.Controls.Add(this.btnOkcircle);
            this.Controls.Add(this.txtr);
            this.Controls.Add(this.txtcenterY);
            this.Controls.Add(this.txtcenterX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CircleInsertDialog";
            this.Text = "CircleInsertDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelcircle;
        private System.Windows.Forms.Button btnOkcircle;
        private System.Windows.Forms.TextBox txtr;
        private System.Windows.Forms.TextBox txtcenterY;
        private System.Windows.Forms.TextBox txtcenterX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}