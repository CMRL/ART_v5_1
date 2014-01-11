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
    public partial class GameOptionsTheraMem : Form
    {
        Main_Dialog parentForm;
        private string gameMode;

        public string setGameMode
        {
            get
            {
                return this.gameMode;
            }
            set
            {
                this.gameMode = value;
            }
        }

        public GameOptionsTheraMem(Form parentCaller)
        {
            InitializeComponent();
            this.parentForm = parentCaller as Main_Dialog;

            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                RadioButton tempButton = panel1.Controls[i] as RadioButton;
                if (tempButton.Checked)
                {
                    string buttonName = tempButton.Name;
                    int indexOfFirstLetter = 0;
                    for (int j = tempButton.Name.Length-1; j > indexOfFirstLetter; j--)
                    {
                        if (Char.IsUpper(buttonName[j]))
                        {
                            indexOfFirstLetter = j;
                        }
                    }

                    this.setGameMode = tempButton.Name.Substring(indexOfFirstLetter, (buttonName.Length) - indexOfFirstLetter);
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                RadioButton tempButton = panel1.Controls[i] as RadioButton;
                if (tempButton.Checked)
                {
                    parentForm.SetGameMode = this.setGameMode;
                }
            }
            this.Close();
        }

        private void rb_theraMemOriginal_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_theraMemOriginal.Checked == true)
            {
                this.setGameMode = "Original";
            }
            else
            {
                // do nothing...
            }
        }

        private void rb_theraMemMemory_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_theraMemMemory.Checked == true)
            {
                this.setGameMode = "Memory";
            }
            else
            {
                // do nothing...
            }
        }
    }
}
