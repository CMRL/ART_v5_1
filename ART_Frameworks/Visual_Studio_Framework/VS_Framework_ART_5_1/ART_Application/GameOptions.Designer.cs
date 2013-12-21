namespace ART_Application
{
    partial class GameOptionsTheraMem
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
            this.label1 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_theraMemMemory = new System.Windows.Forms.RadioButton();
            this.rb_theraMemOriginal = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select your game style:";
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(255, 150);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(97, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_theraMemMemory);
            this.panel1.Controls.Add(this.rb_theraMemOriginal);
            this.panel1.Location = new System.Drawing.Point(99, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(146, 83);
            this.panel1.TabIndex = 2;
            // 
            // rb_theraMemMemory
            // 
            this.rb_theraMemMemory.AutoSize = true;
            this.rb_theraMemMemory.Location = new System.Drawing.Point(4, 46);
            this.rb_theraMemMemory.Name = "rb_theraMemMemory";
            this.rb_theraMemMemory.Size = new System.Drawing.Size(122, 17);
            this.rb_theraMemMemory.TabIndex = 1;
            this.rb_theraMemMemory.TabStop = true;
            this.rb_theraMemMemory.Text = "TheraMem (Memory)";
            this.rb_theraMemMemory.UseVisualStyleBackColor = true;
            this.rb_theraMemMemory.CheckedChanged += new System.EventHandler(this.rb_theraMemMemory_CheckedChanged);
            // 
            // rb_theraMemOriginal
            // 
            this.rb_theraMemOriginal.AutoSize = true;
            this.rb_theraMemOriginal.Checked = true;
            this.rb_theraMemOriginal.Location = new System.Drawing.Point(4, 22);
            this.rb_theraMemOriginal.Name = "rb_theraMemOriginal";
            this.rb_theraMemOriginal.Size = new System.Drawing.Size(120, 17);
            this.rb_theraMemOriginal.TabIndex = 0;
            this.rb_theraMemOriginal.TabStop = true;
            this.rb_theraMemOriginal.Text = "TheraMem (Original)";
            this.rb_theraMemOriginal.UseVisualStyleBackColor = true;
            this.rb_theraMemOriginal.CheckedChanged += new System.EventHandler(this.rb_theraMemOriginal_CheckedChanged);
            // 
            // GameOptionsTheraMem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 185);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(2050, 290);
            this.Name = "GameOptionsTheraMem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TheraMem Options";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rb_theraMemMemory;
        private System.Windows.Forms.RadioButton rb_theraMemOriginal;
    }
}