namespace TheraMem
{
    partial class TheraMem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TheraMem));
            this.QuitApplication = new System.Windows.Forms.Button();
            this.tabTheraMem = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.warning = new System.Windows.Forms.Label();
            this.comboGameStyle = new System.Windows.Forms.ComboBox();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.btnCreateProfile = new System.Windows.Forms.Button();
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadProfile = new System.Windows.Forms.Button();
            this.comboProfile = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkShowHideTime = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label_rightCamera = new System.Windows.Forms.Label();
            this.label_leftCamera = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numUDAccDecRight = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Accelerate = new System.Windows.Forms.Label();
            this.numUDAccDecLeft = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAccDec = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.tabTheraMem.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAccDecRight)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAccDecLeft)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // QuitApplication
            // 
            this.QuitApplication.Location = new System.Drawing.Point(290, 666);
            this.QuitApplication.Name = "QuitApplication";
            this.QuitApplication.Size = new System.Drawing.Size(90, 23);
            this.QuitApplication.TabIndex = 2;
            this.QuitApplication.Text = "Quit Application";
            this.QuitApplication.UseVisualStyleBackColor = true;
            this.QuitApplication.Click += new System.EventHandler(this.QuitApplication_Click);
            // 
            // tabTheraMem
            // 
            this.tabTheraMem.Controls.Add(this.tabGeneral);
            this.tabTheraMem.Controls.Add(this.tabConfig);
            this.tabTheraMem.Location = new System.Drawing.Point(12, 12);
            this.tabTheraMem.Name = "tabTheraMem";
            this.tabTheraMem.SelectedIndex = 0;
            this.tabTheraMem.Size = new System.Drawing.Size(463, 638);
            this.tabTheraMem.TabIndex = 3;
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.Color.Transparent;
            this.tabGeneral.Controls.Add(this.warning);
            this.tabGeneral.Controls.Add(this.comboGameStyle);
            this.tabGeneral.Controls.Add(this.txtDebug);
            this.tabGeneral.Controls.Add(this.btnCreateProfile);
            this.tabGeneral.Controls.Add(this.btnEditProfile);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.btnLoadProfile);
            this.tabGeneral.Controls.Add(this.comboProfile);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.pictureBox1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(455, 612);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            // 
            // warning
            // 
            this.warning.AutoSize = true;
            this.warning.Location = new System.Drawing.Point(262, 124);
            this.warning.Name = "warning";
            this.warning.Size = new System.Drawing.Size(0, 13);
            this.warning.TabIndex = 16;
            // 
            // comboGameStyle
            // 
            this.comboGameStyle.FormattingEnabled = true;
            this.comboGameStyle.Items.AddRange(new object[] {
            "TheraMem ( Orignal )",
            "TheraMem ( Memory )"});
            this.comboGameStyle.Location = new System.Drawing.Point(257, 98);
            this.comboGameStyle.Name = "comboGameStyle";
            this.comboGameStyle.Size = new System.Drawing.Size(186, 21);
            this.comboGameStyle.TabIndex = 15;
            this.comboGameStyle.Tag = "";
            this.comboGameStyle.Text = "TheraMem ( Orignal )";
            this.comboGameStyle.SelectedIndexChanged += new System.EventHandler(this.comboGameStyle_SelectedIndexChanged);
            // 
            // txtDebug
            // 
            this.txtDebug.Location = new System.Drawing.Point(12, 485);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDebug.Size = new System.Drawing.Size(431, 121);
            this.txtDebug.TabIndex = 14;
            // 
            // btnCreateProfile
            // 
            this.btnCreateProfile.Location = new System.Drawing.Point(389, 55);
            this.btnCreateProfile.Name = "btnCreateProfile";
            this.btnCreateProfile.Size = new System.Drawing.Size(54, 23);
            this.btnCreateProfile.TabIndex = 13;
            this.btnCreateProfile.Text = "Create";
            this.btnCreateProfile.UseVisualStyleBackColor = true;
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.Location = new System.Drawing.Point(323, 55);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(54, 23);
            this.btnEditProfile.TabIndex = 12;
            this.btnEditProfile.Text = "Edit";
            this.btnEditProfile.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "*Not listed? Create a new player first";
            // 
            // btnLoadProfile
            // 
            this.btnLoadProfile.Location = new System.Drawing.Point(257, 55);
            this.btnLoadProfile.Name = "btnLoadProfile";
            this.btnLoadProfile.Size = new System.Drawing.Size(54, 23);
            this.btnLoadProfile.TabIndex = 8;
            this.btnLoadProfile.Text = "Load";
            this.btnLoadProfile.UseVisualStyleBackColor = true;
            // 
            // comboProfile
            // 
            this.comboProfile.FormattingEnabled = true;
            this.comboProfile.Items.AddRange(new object[] {
            "Default"});
            this.comboProfile.Location = new System.Drawing.Point(257, 15);
            this.comboProfile.Name = "comboProfile";
            this.comboProfile.Size = new System.Drawing.Size(186, 21);
            this.comboProfile.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Choose an existing player and load profile:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose a game style to play:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(39, 140);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(365, 328);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.groupBox1);
            this.tabConfig.Controls.Add(this.label_rightCamera);
            this.tabConfig.Controls.Add(this.label_leftCamera);
            this.tabConfig.Controls.Add(this.groupBox2);
            this.tabConfig.Location = new System.Drawing.Point(4, 22);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfig.Size = new System.Drawing.Size(455, 612);
            this.tabConfig.TabIndex = 1;
            this.tabConfig.Text = "Settings";
            this.tabConfig.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.chkShowHideTime);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(9, 302);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 197);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Misclineous Settings";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 42;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chkShowHideTime
            // 
            this.chkShowHideTime.AutoSize = true;
            this.chkShowHideTime.Location = new System.Drawing.Point(129, 29);
            this.chkShowHideTime.Name = "chkShowHideTime";
            this.chkShowHideTime.Size = new System.Drawing.Size(15, 14);
            this.chkShowHideTime.TabIndex = 24;
            this.chkShowHideTime.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Show / Hide Timings";
            // 
            // label_rightCamera
            // 
            this.label_rightCamera.AutoSize = true;
            this.label_rightCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_rightCamera.Location = new System.Drawing.Point(263, 22);
            this.label_rightCamera.Name = "label_rightCamera";
            this.label_rightCamera.Size = new System.Drawing.Size(155, 25);
            this.label_rightCamera.TabIndex = 40;
            this.label_rightCamera.Text = "Right Camera";
            // 
            // label_leftCamera
            // 
            this.label_leftCamera.AutoSize = true;
            this.label_leftCamera.BackColor = System.Drawing.Color.Transparent;
            this.label_leftCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_leftCamera.Location = new System.Drawing.Point(56, 22);
            this.label_leftCamera.Name = "label_leftCamera";
            this.label_leftCamera.Size = new System.Drawing.Size(140, 25);
            this.label_leftCamera.TabIndex = 39;
            this.label_leftCamera.Text = "Left Camera";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btnAccDec);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(9, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 220);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Accelerate / Decelerate Hand Movement";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButton4);
            this.groupBox6.Controls.Add(this.radioButton5);
            this.groupBox6.Controls.Add(this.radioButton6);
            this.groupBox6.Location = new System.Drawing.Point(227, 85);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(207, 98);
            this.groupBox6.TabIndex = 50;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Direction";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(7, 68);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(168, 17);
            this.radioButton4.TabIndex = 2;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Both ( Horizontal and Vertical )";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(7, 44);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(60, 17);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Vertical";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(7, 20);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(72, 17);
            this.radioButton6.TabIndex = 0;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "Horizontal";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numUDAccDecRight);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(227, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(207, 65);
            this.groupBox5.TabIndex = 51;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Speed";
            // 
            // numUDAccDecRight
            // 
            this.numUDAccDecRight.DecimalPlaces = 1;
            this.numUDAccDecRight.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numUDAccDecRight.Location = new System.Drawing.Point(6, 25);
            this.numUDAccDecRight.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDAccDecRight.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.numUDAccDecRight.Name = "numUDAccDecRight";
            this.numUDAccDecRight.Size = new System.Drawing.Size(87, 20);
            this.numUDAccDecRight.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 46;
            this.label7.Text = "Accelerate   (+ value)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(99, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Deccelerate (- value )";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.Accelerate);
            this.groupBox4.Controls.Add(this.numUDAccDecLeft);
            this.groupBox4.Location = new System.Drawing.Point(9, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(207, 65);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Speed";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Deccelerate (- value )";
            // 
            // Accelerate
            // 
            this.Accelerate.AutoSize = true;
            this.Accelerate.Location = new System.Drawing.Point(99, 19);
            this.Accelerate.Name = "Accelerate";
            this.Accelerate.Size = new System.Drawing.Size(108, 13);
            this.Accelerate.TabIndex = 46;
            this.Accelerate.Text = "Accelerate   (+ value)";
            // 
            // numUDAccDecLeft
            // 
            this.numUDAccDecLeft.DecimalPlaces = 1;
            this.numUDAccDecLeft.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numUDAccDecLeft.Location = new System.Drawing.Point(5, 25);
            this.numUDAccDecLeft.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDAccDecLeft.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.numUDAccDecLeft.Name = "numUDAccDecLeft";
            this.numUDAccDecLeft.Size = new System.Drawing.Size(87, 20);
            this.numUDAccDecLeft.TabIndex = 47;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(349, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 43;
            this.button2.Text = "Apply";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnAccDec
            // 
            this.btnAccDec.Location = new System.Drawing.Point(129, 189);
            this.btnAccDec.Name = "btnAccDec";
            this.btnAccDec.Size = new System.Drawing.Size(75, 23);
            this.btnAccDec.TabIndex = 43;
            this.btnAccDec.Text = "Apply";
            this.btnAccDec.UseVisualStyleBackColor = true;
            this.btnAccDec.Click += new System.EventHandler(this.btnAccDec_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton3);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Location = new System.Drawing.Point(9, 85);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 98);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Direction";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 68);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(168, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Both ( Horizontal and Vertical )";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 44);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Vertical";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(72, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Horizontal";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(98, 666);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(90, 23);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(194, 666);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(90, 23);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // TheraMem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 701);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.tabTheraMem);
            this.Controls.Add(this.QuitApplication);
            this.Location = new System.Drawing.Point(2100, 280);
            this.Name = "TheraMem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TheraMem v5.1";
            this.tabTheraMem.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabConfig.ResumeLayout(false);
            this.tabConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAccDecRight)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAccDecLeft)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button QuitApplication;
        private System.Windows.Forms.TabControl tabTheraMem;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadProfile;
        private System.Windows.Forms.ComboBox comboProfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.Button btnCreateProfile;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.ComboBox comboGameStyle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label warning;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkShowHideTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_rightCamera;
        private System.Windows.Forms.Label label_leftCamera;
        private System.Windows.Forms.Button btnAccDec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numUDAccDecRight;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Accelerate;
        private System.Windows.Forms.NumericUpDown numUDAccDecLeft;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

