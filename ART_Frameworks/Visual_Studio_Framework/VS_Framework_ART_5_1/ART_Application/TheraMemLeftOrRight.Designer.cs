namespace ART_Application
{
    partial class TheraMemLeftOrRight
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
            this.rb_TheraMemLeftOrRight_Left = new System.Windows.Forms.RadioButton();
            this.rb_TheraMemLeftOrRight_Right = new System.Windows.Forms.RadioButton();
            this.button_TheraMemLeftOrRight_OK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Which side would you like to play?";
            // 
            // rb_TheraMemLeftOrRight_Left
            // 
            this.rb_TheraMemLeftOrRight_Left.AutoSize = true;
            this.rb_TheraMemLeftOrRight_Left.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_TheraMemLeftOrRight_Left.Location = new System.Drawing.Point(20, 10);
            this.rb_TheraMemLeftOrRight_Left.Name = "rb_TheraMemLeftOrRight_Left";
            this.rb_TheraMemLeftOrRight_Left.Size = new System.Drawing.Size(47, 17);
            this.rb_TheraMemLeftOrRight_Left.TabIndex = 1;
            this.rb_TheraMemLeftOrRight_Left.Text = "Left";
            this.rb_TheraMemLeftOrRight_Left.UseVisualStyleBackColor = true;
            // 
            // rb_TheraMemLeftOrRight_Right
            // 
            this.rb_TheraMemLeftOrRight_Right.AutoSize = true;
            this.rb_TheraMemLeftOrRight_Right.Checked = true;
            this.rb_TheraMemLeftOrRight_Right.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_TheraMemLeftOrRight_Right.Location = new System.Drawing.Point(112, 10);
            this.rb_TheraMemLeftOrRight_Right.Name = "rb_TheraMemLeftOrRight_Right";
            this.rb_TheraMemLeftOrRight_Right.Size = new System.Drawing.Size(55, 17);
            this.rb_TheraMemLeftOrRight_Right.TabIndex = 2;
            this.rb_TheraMemLeftOrRight_Right.TabStop = true;
            this.rb_TheraMemLeftOrRight_Right.Text = "Right";
            this.rb_TheraMemLeftOrRight_Right.UseVisualStyleBackColor = true;
            // 
            // button_TheraMemLeftOrRight_OK
            // 
            this.button_TheraMemLeftOrRight_OK.Location = new System.Drawing.Point(156, 106);
            this.button_TheraMemLeftOrRight_OK.Name = "button_TheraMemLeftOrRight_OK";
            this.button_TheraMemLeftOrRight_OK.Size = new System.Drawing.Size(100, 24);
            this.button_TheraMemLeftOrRight_OK.TabIndex = 3;
            this.button_TheraMemLeftOrRight_OK.Text = "OK";
            this.button_TheraMemLeftOrRight_OK.UseVisualStyleBackColor = true;
            this.button_TheraMemLeftOrRight_OK.Click += new System.EventHandler(this.button_TheraMemLeftOrRight_OK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_TheraMemLeftOrRight_Right);
            this.panel1.Controls.Add(this.rb_TheraMemLeftOrRight_Left);
            this.panel1.Location = new System.Drawing.Point(27, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(189, 42);
            this.panel1.TabIndex = 4;
            // 
            // TheraMemLeftOrRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 142);
            this.Controls.Add(this.button_TheraMemLeftOrRight_OK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(2100, 280);
            this.Name = "TheraMemLeftOrRight";
            this.Text = "TheraMemLeftOrRight";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_TheraMemLeftOrRight_Left;
        private System.Windows.Forms.RadioButton rb_TheraMemLeftOrRight_Right;
        private System.Windows.Forms.Button button_TheraMemLeftOrRight_OK;
        private System.Windows.Forms.Panel panel1;
    }
}