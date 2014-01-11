using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ART_Application
{
    public partial class TheraMemLeftOrRight : Form
    {
        private string side;
        TheraMem parentForm;

        public TheraMemLeftOrRight(Form parentCaller)
        {
            InitializeComponent();
            parentForm = parentCaller as TheraMem;
            this.side = "left"; // default set to left
        }

        private void button_TheraMemLeftOrRight_OK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                RadioButton tempButton = panel1.Controls[i] as RadioButton;
                if (tempButton.Checked)
                {
                    this.side = tempButton.Text;
                }
            }
            
            parentForm.SetLeftOrRight = this.side.ToLower();
            this.Close();
        }
    }
}
