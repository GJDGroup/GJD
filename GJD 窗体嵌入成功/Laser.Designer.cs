namespace GJD
{
    partial class Laser
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
            this.components = new System.ComponentModel.Container();
            this.butLaserOn = new System.Windows.Forms.Button();
            this.laserserialPort = new System.IO.Ports.SerialPort(this.components);
            this.butLaserClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labPower = new System.Windows.Forms.Label();
            this.labInterGateFre = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labPulseRate = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labTrigger = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxExternal = new System.Windows.Forms.CheckBox();
            this.checkBoxInternal = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textInterGateFre = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textPowerReguPercent = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.texLaserStatus = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.butInfoClear = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // butLaserOn
            // 
            this.butLaserOn.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.butLaserOn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butLaserOn.Location = new System.Drawing.Point(215, 26);
            this.butLaserOn.Name = "butLaserOn";
            this.butLaserOn.Size = new System.Drawing.Size(130, 40);
            this.butLaserOn.TabIndex = 0;
            this.butLaserOn.Text = "激光器开";
            this.butLaserOn.UseVisualStyleBackColor = false;
            this.butLaserOn.Click += new System.EventHandler(this.butLaserOn_Click);
            // 
            // laserserialPort
            // 
            this.laserserialPort.BaudRate = 57600;
            this.laserserialPort.PortName = "COM3";
            this.laserserialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.laserserialPort_DataReceived);
            // 
            // butLaserClose
            // 
            this.butLaserClose.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.butLaserClose.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butLaserClose.Location = new System.Drawing.Point(408, 26);
            this.butLaserClose.Name = "butLaserClose";
            this.butLaserClose.Size = new System.Drawing.Size(130, 40);
            this.butLaserClose.TabIndex = 0;
            this.butLaserClose.Text = "激光器关";
            this.butLaserClose.UseVisualStyleBackColor = false;
            this.butLaserClose.Click += new System.EventHandler(this.butLaserClose_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(99, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "激光1:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(388, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 31);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "20.00";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(205, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "2.8A [2.8A]";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(205, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 40);
            this.label3.TabIndex = 5;
            this.label3.Text = "5.5A [5.5A]";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(99, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 40);
            this.label4.TabIndex = 4;
            this.label4.Text = "激光2:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(861, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 40);
            this.label5.TabIndex = 6;
            this.label5.Text = "触发方式:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label7.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(274, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 31);
            this.label7.TabIndex = 8;
            this.label7.Text = "电流1:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label8.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(624, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 28);
            this.label8.TabIndex = 9;
            this.label8.Text = "电流2:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(740, 29);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 28);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "20.00";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(6, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1118, 70);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电流";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label10.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(846, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 31);
            this.label10.TabIndex = 11;
            this.label10.Text = "A";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(494, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 31);
            this.label6.TabIndex = 10;
            this.label6.Text = "A";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label9.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(861, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 40);
            this.label9.TabIndex = 11;
            this.label9.Text = "功率:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPower
            // 
            this.labPower.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labPower.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPower.ForeColor = System.Drawing.Color.Black;
            this.labPower.Location = new System.Drawing.Point(960, 80);
            this.labPower.Name = "labPower";
            this.labPower.Size = new System.Drawing.Size(107, 40);
            this.labPower.TabIndex = 12;
            this.labPower.Text = "10%";
            this.labPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labInterGateFre
            // 
            this.labInterGateFre.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labInterGateFre.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labInterGateFre.ForeColor = System.Drawing.Color.Black;
            this.labInterGateFre.Location = new System.Drawing.Point(600, 25);
            this.labInterGateFre.Name = "labInterGateFre";
            this.labInterGateFre.Size = new System.Drawing.Size(100, 40);
            this.labInterGateFre.TabIndex = 15;
            this.labInterGateFre.Text = "100k";
            this.labInterGateFre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label12.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(686, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 40);
            this.label12.TabIndex = 14;
            this.label12.Text = "HZ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label13.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(467, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(132, 40);
            this.label13.TabIndex = 13;
            this.label13.Text = "内部门频率:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label14.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(467, 80);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(136, 40);
            this.label14.TabIndex = 16;
            this.label14.Text = "最大脉冲频率:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPulseRate
            // 
            this.labPulseRate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labPulseRate.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPulseRate.ForeColor = System.Drawing.Color.Black;
            this.labPulseRate.Location = new System.Drawing.Point(600, 80);
            this.labPulseRate.Name = "labPulseRate";
            this.labPulseRate.Size = new System.Drawing.Size(100, 40);
            this.labPulseRate.TabIndex = 18;
            this.labPulseRate.Text = "2M";
            this.labPulseRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label16.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(686, 80);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 40);
            this.label16.TabIndex = 17;
            this.label16.Text = "HZ";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.labTrigger);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.labPower);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.labPulseRate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.labInterGateFre);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(8, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1125, 138);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数显示";
            // 
            // labTrigger
            // 
            this.labTrigger.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labTrigger.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTrigger.ForeColor = System.Drawing.Color.Black;
            this.labTrigger.Location = new System.Drawing.Point(960, 25);
            this.labTrigger.Name = "labTrigger";
            this.labTrigger.Size = new System.Drawing.Size(107, 40);
            this.labTrigger.TabIndex = 20;
            this.labTrigger.Text = "内部触发";
            this.labTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox3.Controls.Add(this.checkBoxExternal);
            this.groupBox3.Controls.Add(this.checkBoxInternal);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(6, 108);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1118, 70);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "触发方式";
            // 
            // checkBoxExternal
            // 
            this.checkBoxExternal.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxExternal.Location = new System.Drawing.Point(663, 25);
            this.checkBoxExternal.Name = "checkBoxExternal";
            this.checkBoxExternal.Size = new System.Drawing.Size(122, 31);
            this.checkBoxExternal.TabIndex = 11;
            this.checkBoxExternal.Text = "外部触发";
            this.checkBoxExternal.UseVisualStyleBackColor = true;
            this.checkBoxExternal.CheckedChanged += new System.EventHandler(this.checkBoxExternal_CheckedChanged);
            // 
            // checkBoxInternal
            // 
            this.checkBoxInternal.Checked = true;
            this.checkBoxInternal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxInternal.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxInternal.Location = new System.Drawing.Point(303, 25);
            this.checkBoxInternal.Name = "checkBoxInternal";
            this.checkBoxInternal.Size = new System.Drawing.Size(122, 31);
            this.checkBoxInternal.TabIndex = 10;
            this.checkBoxInternal.Text = "内部触发";
            this.checkBoxInternal.UseVisualStyleBackColor = true;
            this.checkBoxInternal.CheckedChanged += new System.EventHandler(this.checkBoxInternal_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.textInterGateFre);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(6, 188);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1118, 70);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "频率";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label20.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(890, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(32, 30);
            this.label20.TabIndex = 22;
            this.label20.Text = "HZ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label17.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(508, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 30);
            this.label17.TabIndex = 21;
            this.label17.Text = "HZ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label19.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(626, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(152, 31);
            this.label19.TabIndex = 10;
            this.label19.Text = "最大脉冲频率:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textInterGateFre
            // 
            this.textInterGateFre.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textInterGateFre.ForeColor = System.Drawing.Color.Black;
            this.textInterGateFre.Location = new System.Drawing.Point(402, 26);
            this.textInterGateFre.Name = "textInterGateFre";
            this.textInterGateFre.Size = new System.Drawing.Size(100, 31);
            this.textInterGateFre.TabIndex = 1;
            this.textInterGateFre.Text = "100k";
            this.textInterGateFre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label18.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(201, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(195, 31);
            this.label18.TabIndex = 8;
            this.label18.Text = "内部门频率:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox4.ForeColor = System.Drawing.Color.Black;
            this.textBox4.Location = new System.Drawing.Point(784, 26);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 28);
            this.textBox4.TabIndex = 1;
            this.textBox4.Text = "2M";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(601, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 20;
            this.button1.Text = "参数激活";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(794, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 40);
            this.button2.TabIndex = 21;
            this.button2.Text = "参数失效";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.textPowerReguPercent);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(4, 268);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1118, 70);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "调制";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label22.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(715, 25);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(33, 30);
            this.label22.TabIndex = 21;
            this.label22.Text = "%";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textPowerReguPercent
            // 
            this.textPowerReguPercent.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textPowerReguPercent.ForeColor = System.Drawing.Color.Black;
            this.textPowerReguPercent.Location = new System.Drawing.Point(609, 25);
            this.textPowerReguPercent.Name = "textPowerReguPercent";
            this.textPowerReguPercent.Size = new System.Drawing.Size(100, 31);
            this.textPowerReguPercent.TabIndex = 1;
            this.textPowerReguPercent.Text = "10";
            this.textPowerReguPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label24.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(408, 24);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(195, 31);
            this.label24.TabIndex = 8;
            this.label24.Text = "功率:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Controls.Add(this.groupBox9);
            this.groupBox6.Controls.Add(this.button5);
            this.groupBox6.Controls.Add(this.butInfoClear);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.Location = new System.Drawing.Point(0, 344);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1118, 263);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "系统控制";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(915, 198);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 40);
            this.button3.TabIndex = 29;
            this.button3.Text = "重置";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.texLaserStatus);
            this.groupBox9.Font = new System.Drawing.Font("微软雅黑 Light", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox9.Location = new System.Drawing.Point(155, 9);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(718, 238);
            this.groupBox9.TabIndex = 28;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "状态信息";
            // 
            // texLaserStatus
            // 
            this.texLaserStatus.Location = new System.Drawing.Point(6, 24);
            this.texLaserStatus.Multiline = true;
            this.texLaserStatus.Name = "texLaserStatus";
            this.texLaserStatus.Size = new System.Drawing.Size(697, 205);
            this.texLaserStatus.TabIndex = 25;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(915, 33);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(150, 40);
            this.button5.TabIndex = 26;
            this.button5.Text = "读取错误";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.buterrorRead);
            // 
            // butInfoClear
            // 
            this.butInfoClear.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.butInfoClear.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butInfoClear.Location = new System.Drawing.Point(915, 112);
            this.butInfoClear.Name = "butInfoClear";
            this.butInfoClear.Size = new System.Drawing.Size(150, 40);
            this.butInfoClear.TabIndex = 24;
            this.butInfoClear.Text = "清除错误";
            this.butInfoClear.UseVisualStyleBackColor = false;
            this.butInfoClear.Click += new System.EventHandler(this.butInfoClear_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox7.Controls.Add(this.groupBox5);
            this.groupBox7.Controls.Add(this.groupBox6);
            this.groupBox7.Controls.Add(this.groupBox3);
            this.groupBox7.Controls.Add(this.groupBox1);
            this.groupBox7.Controls.Add(this.groupBox4);
            this.groupBox7.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.Location = new System.Drawing.Point(8, 156);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1125, 617);
            this.groupBox7.TabIndex = 25;
            this.groupBox7.TabStop = false;
            this.groupBox7.Tag = "";
            this.groupBox7.Text = "参数设置";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox8.Controls.Add(this.textBox6);
            this.groupBox8.Controls.Add(this.butLaserClose);
            this.groupBox8.Controls.Add(this.button2);
            this.groupBox8.Controls.Add(this.button1);
            this.groupBox8.Controls.Add(this.butLaserOn);
            this.groupBox8.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox8.Location = new System.Drawing.Point(8, 786);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(1125, 98);
            this.groupBox8.TabIndex = 26;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "激光器控制";
            // 
            // textBox6
            // 
            this.textBox6.AllowDrop = true;
            this.textBox6.Location = new System.Drawing.Point(56, 101);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 91);
            this.textBox6.TabIndex = 22;
            // 
            // Laser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(1144, 856);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox7);
            this.Name = "Laser";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butLaserOn;
        private System.IO.Ports.SerialPort laserserialPort;
        private System.Windows.Forms.Button butLaserClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labPower;
        private System.Windows.Forms.Label labInterGateFre;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labPulseRate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labTrigger;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxExternal;
        private System.Windows.Forms.CheckBox checkBoxInternal;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textInterGateFre;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textPowerReguPercent;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox texLaserStatus;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button butInfoClear;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
    }
}