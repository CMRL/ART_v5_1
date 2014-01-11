namespace ART_Application
{
    partial class FrmProfileHandler
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
            this.btnCancelProfileOption = new System.Windows.Forms.Button();
            this.btnEditProfileOption = new System.Windows.Forms.Button();
            this.btnLoadProfileOption = new System.Windows.Forms.Button();
            this.btnNewProfileOption = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancelProfileOption
            // 
            this.btnCancelProfileOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelProfileOption.Location = new System.Drawing.Point(39, 110);
            this.btnCancelProfileOption.Name = "btnCancelProfileOption";
            this.btnCancelProfileOption.Size = new System.Drawing.Size(192, 40);
            this.btnCancelProfileOption.TabIndex = 10;
            this.btnCancelProfileOption.Text = "Cancel";
            this.btnCancelProfileOption.UseVisualStyleBackColor = true;
            this.btnCancelProfileOption.Click += new System.EventHandler(this.btnCancelProfileOption_Click);
            // 
            // btnEditProfileOption
            // 
            this.btnEditProfileOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditProfileOption.Location = new System.Drawing.Point(144, 64);
            this.btnEditProfileOption.Name = "btnEditProfileOption";
            this.btnEditProfileOption.Size = new System.Drawing.Size(87, 40);
            this.btnEditProfileOption.TabIndex = 9;
            this.btnEditProfileOption.Text = "Edit";
            this.btnEditProfileOption.UseVisualStyleBackColor = true;
            this.btnEditProfileOption.Click += new System.EventHandler(this.btnEditProfileOption_Click);
            // 
            // btnLoadProfileOption
            // 
            this.btnLoadProfileOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadProfileOption.Location = new System.Drawing.Point(39, 64);
            this.btnLoadProfileOption.Name = "btnLoadProfileOption";
            this.btnLoadProfileOption.Size = new System.Drawing.Size(89, 40);
            this.btnLoadProfileOption.TabIndex = 8;
            this.btnLoadProfileOption.Text = "Load";
            this.btnLoadProfileOption.UseVisualStyleBackColor = true;
            this.btnLoadProfileOption.Click += new System.EventHandler(this.btnLoadProfileOption_Click);
            // 
            // btnNewProfileOption
            // 
            this.btnNewProfileOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewProfileOption.Location = new System.Drawing.Point(39, 18);
            this.btnNewProfileOption.Name = "btnNewProfileOption";
            this.btnNewProfileOption.Size = new System.Drawing.Size(192, 40);
            this.btnNewProfileOption.TabIndex = 7;
            this.btnNewProfileOption.Text = "New";
            this.btnNewProfileOption.UseVisualStyleBackColor = true;
            this.btnNewProfileOption.Click += new System.EventHandler(this.btnNewProfileOption_Click);
            // 
            // FrmProfileHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 173);
            this.Controls.Add(this.btnCancelProfileOption);
            this.Controls.Add(this.btnEditProfileOption);
            this.Controls.Add(this.btnLoadProfileOption);
            this.Controls.Add(this.btnNewProfileOption);
            this.Location = new System.Drawing.Point(2100, 280);
            this.Name = "FrmProfileHandler";
            this.Text = "Profile Options";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelProfileOption;
        private System.Windows.Forms.Button btnEditProfileOption;
        private System.Windows.Forms.Button btnLoadProfileOption;
        private System.Windows.Forms.Button btnNewProfileOption;




    }
}

