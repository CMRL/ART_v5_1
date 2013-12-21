/**
 * This class is made to deal with actions (e.g. a button being pushed). Actions come 
 * from a dialog with controls for changing applications, and view features within 
 * the ART application built in Unity3D. This is expandable and can be made to manipulate 
 * much more (e.g. Amplified movement). 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ART_Application
{

    /**
     * Main class that is called when the dialog is actually
     * required. The constructor method calls on InitializeComponent()
     * which makes the Form and all controls associated with it.
     * */
    public partial class Main_Dialog : Form
    {
        TcpClient clientSocket;
        ArrayList commandArray = new ArrayList();
        bool gameRunning = false;
        string gameMode = "";

        public string SetGameMode
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
        /**
         * Function to append lines of text to the text box.
         * This shows what actions have been sent to Unity and will
         * show any errors that are caught. 
         * 
         * @param str The string to append to the textbox
         */
        private void AppendLine(string str)
        {
            if (textBox.Text.Length > 0)
            {
                textBox.AppendText(Environment.NewLine);
            }
            textBox.AppendText(">> " + str);
        }

        /**
         * The constructor method for the class. This initializes
         * the Form and all controls associated to it as well as
         * attempts a Socket connection to the Unity application.
         * This is the Client and Unity acts as the server.
         */
        public Main_Dialog()
        {
            Screen[] screens = Screen.AllScreens;

            // Start the dialog
            InitializeComponent();

            try
            {
                // Connect to the server running in Unity
                int port = 1234;
                clientSocket = new TcpClient("127.0.0.1", port);
                string text = "Connected to server on port " + port;
                AppendLine(text);

            }
            catch (Exception ex)
            {
                AppendLine("No Server to connect to..");
            }
        }

        /**
         * Below is a list of commands which are sent through to Unity.
         * This list should be added to to keep it clear what functionality
         * this form has.
         * 
         * COMMANDS:
         * 
         * LeftOnLeft (RadioButton)
         * RightOnLeft (RadioButton)
         * NothingOnLeft (RadioButton)
         * LeftMirror (CheckBox)
         * LeftUnMirror (CheckBox)
         * 
         * RightOnRight (RadioButton)
         * LeftOnRight (RadioButton)
         * NothingOnRight (RadioButton)
         * RightMirror (CheckBox)
         * RightUnMirror (CheckBox)
         * 
         * 
         */

        /**
         * Function for the 'Apply' button of the form. When clicked,
         * this button will send all accumulated commands from the global variable
         * 'commandArray'. 
         */
        private void Apply_Click(object sender, EventArgs e)
        {
            string finalCommands = "";
            int numCommands = commandArray.Count;
            AppendLine("The following " + numCommands + " commands will be performed:");

            // make the string to send
            for (int i = 0; i < numCommands; i++)
            {
                string word = commandArray[i] as string;
                finalCommands += "$" + word;
                AppendLine(word);
            }

            commandArray.Clear();

            string toSend = numCommands + finalCommands;
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

            try {
                NetworkStream clientStream = clientSocket.GetStream();

                clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                AppendLine(toSend);

            } catch (Exception ex) {
                AppendLine(ex.ToString());
            }
        }

        // Cancel button listener
        private void Quit_Click(object sender, EventArgs e)
        {
            string toSend = "1$Shutdown";
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

            try
            {
                NetworkStream clientStream = clientSocket.GetStream();

                clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                AppendLine(toSend.Substring(2));
            }
            catch (Exception ex)
            {
                AppendLine(ex.ToString());
            }

            // Close application after waiting 1 second for Unity to stop the server etc.
            Thread.Sleep(1000);
            this.Close();
        }

        // RadioButton for left view, left camera
        private void rb_leftViewLeftCam_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_leftViewLeftCam.Checked)
            {
                // add to commands to perform
                commandArray.Insert(commandArray.Count, "LeftOnLeft");

                
                // sort pictures
                if (cb_leftViewMirror.Checked)
                {
                    pb_leftViewLeftHandMirror.Visible = true;
                }
                else
                {
                    pb_leftViewLeftHand.Visible = true;
                }
            }
            else
            {
                // remove from commands to perform
                commandArray.Remove("LeftOnLeft");

                if (cb_leftViewMirror.Checked)
                {
                    pb_leftViewLeftHandMirror.Visible = false;
                }
                else
                {
                    pb_leftViewLeftHand.Visible = false;
                }
            }
        }

        // RadioButton for left view, right camera
        private void rb_leftViewRightCam_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_leftViewRightCam.Checked)
            {
                commandArray.Insert(commandArray.Count, "RightOnLeft");

                // sort pictures
                if (cb_leftViewMirror.Checked)
                {
                    pb_leftViewRightHandMirror.Visible = true;
                }
                else
                {
                    pb_leftViewRightHand.Visible = true;
                }
            }
            else
            {
                commandArray.Remove("RightOnLeft");

                if (cb_leftViewMirror.Checked)
                {
                    pb_leftViewRightHandMirror.Visible = false;
                }
                else
                {
                    pb_leftViewRightHand.Visible = false;
                }
            }
        }

        // RadioButton for left view, no camera
        private void rb_leftViewNoCam_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_leftViewNoCam.Checked)
            {
                commandArray.Insert(commandArray.Count, "NothingOnLeft");

                // No pictures should be shown now

            }
            else
            {
                commandArray.Remove("NothingOnLeft");
                // Do nothing
            }
        }

        // Checkbox to mirror the left plane
        private void cb_leftViewMirror_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_leftViewMirror.Checked)
            {
                commandArray.Remove("LeftUnMirror");
                commandArray.Insert(commandArray.Count, "LeftMirror");

                // Picture Stuff
                if (rb_leftViewLeftCam.Checked) 
                {
                    pb_leftViewLeftHand.Visible = false;
                    pb_leftViewLeftHandMirror.Visible = true;
                }
                else if (rb_leftViewRightCam.Checked)
                {
                    pb_leftViewRightHand.Visible = false;
                    pb_leftViewRightHandMirror.Visible = true;
                }
                else
                {
                    // there should be no hands shown on the screen
                }
            }
            else
            {
                commandArray.Remove("LeftMirror");
                commandArray.Insert(commandArray.Count, "LeftUnMirror");

                if (rb_leftViewLeftCam.Checked)
                {
                    pb_leftViewLeftHand.Visible = true;
                    pb_leftViewLeftHandMirror.Visible = false;
                }
                else if (rb_leftViewRightCam.Checked)
                {
                    pb_leftViewRightHand.Visible = true;
                    pb_leftViewRightHandMirror.Visible = false;
                }
                else
                {
                    // there should be no hands shown on the screen
                }
            }
        }

        private void rb_rightViewLeftCam_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_rightViewLeftCam.Checked)
            {
                // add to commands to perform
                commandArray.Insert(commandArray.Count, "LeftOnRight");


                // sort pictures
                if (cb_rightViewMirror.Checked)
                {
                    pb_rightViewLeftHandMirror.Visible = true;
                }
                else
                {
                    pb_rightViewLeftHand.Visible = true;
                }
            }
            else
            {
                // remove from commands to perform
                commandArray.Remove("LeftOnRight");

                if (cb_rightViewMirror.Checked)
                {
                    pb_rightViewLeftHandMirror.Visible = false;
                }
                else
                {
                    pb_rightViewLeftHand.Visible = false;
                }
            }
        }

        private void rb_rightViewRightCam_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_rightViewRightCam.Checked)
            {
                commandArray.Insert(commandArray.Count, "RightOnRight");

                // sort pictures
                if (cb_rightViewMirror.Checked)
                {
                    pb_rightViewRightHandMirror.Visible = true;
                }
                else
                {
                    pb_rightViewRightHand.Visible = true;
                }
            }
            else
            {
                commandArray.Remove("RightOnRight");

                if (cb_rightViewMirror.Checked)
                {
                    pb_rightViewRightHandMirror.Visible = false;
                }
                else
                {
                    pb_rightViewRightHand.Visible = false;
                }
            }
        }

        private void rb_rightViewNoCam_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_rightViewNoCam.Checked)
            {
                commandArray.Insert(commandArray.Count, "NothingOnRight");

                // No pictures should be shown now

            }
            else
            {
                commandArray.Remove("NothingOnRight");
            }
        }

        private void cb_rightViewMirror_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_rightViewMirror.Checked)
            {
                commandArray.Remove("RightUnMirror");
                commandArray.Insert(commandArray.Count, "RightMirror");

                // Picture Stuff
                if (rb_rightViewLeftCam.Checked)
                {
                    pb_rightViewLeftHand.Visible = false;
                    pb_rightViewLeftHandMirror.Visible = true;
                }
                else if (rb_rightViewRightCam.Checked)
                {
                    pb_rightViewRightHand.Visible = false;
                    pb_rightViewRightHandMirror.Visible = true;
                }
                else
                {
                    // there should be no hands shown on the screen
                }
            }
            else
            {
                commandArray.Remove("RightMirror");
                commandArray.Insert(commandArray.Count, "RightUnMirror");

                if (rb_rightViewLeftCam.Checked)
                {
                    pb_rightViewLeftHand.Visible = true;
                    pb_rightViewLeftHandMirror.Visible = false;
                }
                else if (rb_rightViewRightCam.Checked)
                {
                    pb_rightViewRightHand.Visible = true;
                    pb_rightViewRightHandMirror.Visible = false;
                }
                else
                {
                    // there should be no hands shown on the screen
                }
            }
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            string boxValue = "";

            boxValue += (string) comboBox_gamesList.SelectedItem;

            if (StartGame.Text == "Start Game") // Then the options box should appear for whichever game is running
            {
                if (boxValue == "TheraMem")
                {
                    Form optionsForm = new GameOptionsTheraMem(this);

                    optionsForm.ShowDialog();

                    boxValue += this.SetGameMode;
                }
                //else if (boxValue == "")
                //{
                //}
            }

            if (boxValue != "")
            {

                string toSend = "1$" + boxValue;
                byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

                try
                {
                    NetworkStream clientStream = clientSocket.GetStream();

                    clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                    AppendLine("Starting " + toSend);

                }
                catch (Exception ex)
                {
                    AppendLine(ex.ToString());
                }

                warning.Text = "";
                //tabControl1.SelectTab(0);
                if (StartGame.Text == "Start Game")
                {
                    gameRunning = true;
                    comboBox_gamesList.Enabled = false;
                    Pause.Text = "Resume";
                    StartGame.Text = "Stop Game";
                }
                else if (StartGame.Text == "Stop Game")
                {
                    gameRunning = false;
                    comboBox_gamesList.Enabled = true;
                    Pause.Text = "Resume";
                    StartGame.Text = "Start Game";
                }
                else
                {
                    // should never get here..
                }
                //string gameName = comboBox_gamesList.
            }
            else
            {
                warning.Text = "You must select a valid application before starting...";
            }
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            if (gameRunning)
            {
                string pause = "1$";

                if (Pause.Text == "Pause")
                {
                    Pause.Text = "Resume";
                    pause += "PauseGame";
                }
                else
                {
                    Pause.Text = "Pause";
                    pause += "ResumeGame";
                }

                if (pause != "")
                {

                    string toSend = pause;
                    byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

                    try
                    {
                        NetworkStream clientStream = clientSocket.GetStream();

                        clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                        AppendLine("Starting " + toSend);

                    }
                    catch (Exception ex)
                    {
                        AppendLine(ex.ToString());
                    }

                    warning.Text = "";
                }
                else
                {
                    // will never make it here
                }
            }
            else
            {
                warning.Text = "No game is currently running!";
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {

            if (gameRunning)
            {
                string boxValue = "";

                boxValue += (string)comboBox_gamesList.SelectedItem;

                if (boxValue != "")
                {

                    string toSend = "1$" + "Restart" + boxValue + this.SetGameMode;
                    byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

                    try
                    {
                        NetworkStream clientStream = clientSocket.GetStream();

                        clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                        AppendLine("Starting " + toSend);

                    }
                    catch (Exception ex)
                    {
                        AppendLine(ex.ToString());
                    }

                    warning.Text = "";

                    Pause.Text = "Resume";
                    //string gameName = comboBox_gamesList.
                }
                else
                {
                    warning.Text = "You must select a valid application before starting...";
                }
            }
            else
            {
                warning.Text = "No game is currently running to restart!";
            }
        }

        private void comboBox_gamesList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdvOptLeft_Click(object sender, EventArgs e)
        {
            // Get list of cameras attached using direct show
            // Pass left camera to change its properties.




        }

      

      
    }
}
