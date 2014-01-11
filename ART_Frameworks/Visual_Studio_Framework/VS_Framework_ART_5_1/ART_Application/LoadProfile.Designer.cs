namespace ART_Application
{
    partial class LoadProfile
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
            this.listDebugLoading = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtProfileID = new System.Windows.Forms.TextBox();
            this.txtProfileLastName = new System.Windows.Forms.TextBox();
            this.txtProfileFirstName = new System.Windows.Forms.TextBox();
            this.txtProfileUserName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnLoadProfile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboExistingProfile = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelLoad = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listDebugLoading
            // 
            this.listDebugLoading.FormattingEnabled = true;
            this.listDebugLoading.Location = new System.Drawing.Point(12, 261);
            this.listDebugLoading.Name = "listDebugLoading";
            this.listDebugLoading.Size = new System.Drawing.Size(436, 329);
            this.listDebugLoading.TabIndex = 26;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtProfileID);
            this.groupBox2.Controls.Add(this.txtProfileLastName);
            this.groupBox2.Controls.Add(this.txtProfileFirstName);
            this.groupBox2.Controls.Add(this.txtProfileUserName);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(12, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 171);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Profile Details";
            // 
            // txtProfileID
            // 
            this.txtProfileID.Location = new System.Drawing.Point(197, 56);
            this.txtProfileID.Name = "txtProfileID";
            this.txtProfileID.ReadOnly = true;
            this.txtProfileID.Size = new System.Drawing.Size(95, 20);
            this.txtProfileID.TabIndex = 26;
            // 
            // txtProfileLastName
            // 
            this.txtProfileLastName.Location = new System.Drawing.Point(197, 126);
            this.txtProfileLastName.Name = "txtProfileLastName";
            this.txtProfileLastName.Size = new System.Drawing.Size(189, 20);
            this.txtProfileLastName.TabIndex = 25;
            // 
            // txtProfileFirstName
            // 
            this.txtProfileFirstName.Location = new System.Drawing.Point(197, 89);
            this.txtProfileFirstName.Name = "txtProfileFirstName";
            this.txtProfileFirstName.Size = new System.Drawing.Size(189, 20);
            this.txtProfileFirstName.TabIndex = 24;
            // 
            // txtProfileUserName
            // 
            this.txtProfileUserName.Enabled = false;
            this.txtProfileUserName.Location = new System.Drawing.Point(197, 24);
            this.txtProfileUserName.Name = "txtProfileUserName";
            this.txtProfileUserName.ReadOnly = true;
            this.txtProfileUserName.Size = new System.Drawing.Size(189, 20);
            this.txtProfileUserName.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(54, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Last Name";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(54, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "First Name";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(54, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "User Name";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(54, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "ID";
            // 
            // btnLoadProfile
            // 
            this.btnLoadProfile.Location = new System.Drawing.Point(125, 596);
            this.btnLoadProfile.Name = "btnLoadProfile";
            this.btnLoadProfile.Size = new System.Drawing.Size(101, 23);
            this.btnLoadProfile.TabIndex = 15;
            this.btnLoadProfile.Text = "Load";
            this.btnLoadProfile.UseVisualStyleBackColor = true;
            this.btnLoadProfile.Click += new System.EventHandler(this.btnLoadProfile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(206, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "*Not listed? Create a new profile first";
            // 
            // comboExistingProfile
            // 
            this.comboExistingProfile.FormattingEnabled = true;
            this.comboExistingProfile.Location = new System.Drawing.Point(209, 9);
            this.comboExistingProfile.Name = "comboExistingProfile";
            this.comboExistingProfile.Size = new System.Drawing.Size(189, 21);
            this.comboExistingProfile.TabIndex = 23;
            this.comboExistingProfile.SelectedIndexChanged += new System.EventHandler(this.comboExistingProfile_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Select a profile to Load:";
            // 
            // btnCancelLoad
            // 
            this.btnCancelLoad.Location = new System.Drawing.Point(232, 596);
            this.btnCancelLoad.Name = "btnCancelLoad";
            this.btnCancelLoad.Size = new System.Drawing.Size(101, 23);
            this.btnCancelLoad.TabIndex = 27;
            this.btnCancelLoad.Text = "Cancel";
            this.btnCancelLoad.UseVisualStyleBackColor = true;
            this.btnCancelLoad.Click += new System.EventHandler(this.btnCancelLoad_Click);
            // 
            // LoadProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 642);
            this.Controls.Add(this.btnCancelLoad);
            this.Controls.Add(this.listDebugLoading);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboExistingProfile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadProfile);
            this.Location = new System.Drawing.Point(2100, 280);
            this.Name = "LoadProfile";
            this.Text = "LoadProfile";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listDebugLoading;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtProfileID;
        private System.Windows.Forms.TextBox txtProfileLastName;
        private System.Windows.Forms.TextBox txtProfileFirstName;
        private System.Windows.Forms.TextBox txtProfileUserName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnLoadProfile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboExistingProfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelLoad;
    }
}