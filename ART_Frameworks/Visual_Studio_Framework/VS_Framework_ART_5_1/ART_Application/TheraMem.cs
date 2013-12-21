/**
 * This class is made to deal with actions (e.g. a button being pushed). Actions come 
 * from a dialog with controls for changing applications, and view features within 
 * the TheraMem application built in Unity3D.
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

namespace TheraMem
{
    /******
     * This is the main class for TheraMem application.
     * It contains code for managing different options on dialog boxes. 
     * It is also handles all communication between unity3d and this app.
     * It also Interacts with camera to access camera properties
     * ****/
    public partial class TheraMem : Form
    {
        TcpClient clientSocket;
        ArrayList commandArray = new ArrayList();
        bool gameRunning = false;
        string gameMode = "";
        bool AccDecLeft;
        bool AccDecRight;

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
            if (txtDebug.Text.Length > 0)
            {
                txtDebug.AppendText(Environment.NewLine);
            }
            txtDebug.AppendText(">> " + str);
        }

        /**
         * The constructor method for the class. This initializes
         * the Form and all controls associated to it as well as
         * attempts a Socket connection to the Unity application.
         * This is the Client and Unity acts as the server.
         */

        public TheraMem()
        {
            //--Gets list of all display devices connected --//
            Screen[] screens = Screen.AllScreens;

            InitializeComponent();


            try
            {
                // Connect to the server running in Unity
                int port = 1234;
                clientSocket = new TcpClient("127.0.0.1", port);
                string text = "Connected to server on port " + port;
                AppendLine(text);
                btnPause.Enabled = false;

            }
            catch (Exception ex)
            {
                AppendLine("No Server to connect to..");
            }

        }

        //----------------------------------  GENERIC COMMANDS FOR THERA-MEM DIALOG -----------------------//

        private void btnPlay_Click(object sender, EventArgs e)
        {
            string boxValue = "";
            string tmpString = "";
            bool CorrectOptionSelected = false;
            tmpString += comboGameStyle.SelectedItem.ToString();
            if (tmpString == "TheraMem ( Orignal )")
            {
                CorrectOptionSelected = true;
                boxValue = "TheraMemOrignal";
            }
            else if (tmpString == "TheraMem ( Memory )")
            {
                CorrectOptionSelected = true;
                boxValue = "TheraMemMemory";
            }


            if (CorrectOptionSelected)
            {
                string toSend;

                toSend = "1$" + boxValue;

                if (btnPlay.Text == "Stop")
                {
                    toSend += "_Stop";
                    AppendLine("Stoping " + toSend);

                }
                else
                {

                    AppendLine("Starting " + toSend);
                }



                byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

                try
                {
                    NetworkStream clientStream = clientSocket.GetStream();

                    clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);

                }
                catch (Exception ex)
                {
                    AppendLine(ex.ToString());
                }

                warning.Text = "";
                if (btnPlay.Text == "Play")
                {
                    gameRunning = true;
                    comboGameStyle.Enabled = false;
                    btnPause.Enabled = true;
                    btnPause.Text= "Pause";
                    btnPlay.Text = "Stop";
                }
                else if (btnPlay.Text == "Stop")
                {
                    gameRunning = false;
                    comboGameStyle.Enabled = true;
                    btnPause.Text = "Pause";
                    btnPause.Enabled = false;
                    btnPlay.Text = "Play";
                }
            }
            else
            {
                warning.Text = "Select one of the available game options and then Play";
            }
            
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
             
            string pause = "1$";
            if (btnPause.Text == "Pause")
            {
                btnPause.Text = "Resume";
                pause += "PauseGame";
            }
            else
            {
                btnPause.Text = "Pause";
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
                       
        }


        // ------- Function to handle "Quit Application" Button  ------------//
        private void QuitApplication_Click(object sender, EventArgs e)
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

        private void comboGameStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CurrentGameMode = "";
            CurrentGameMode +=  comboGameStyle.SelectedItem.ToString();
            
            //---------- Set this to Global Game mode variable 
            this.SetGameMode = CurrentGameMode;
            
        }

        private void btnAccDec_Click(object sender, EventArgs e)
        {

            decimal curval=numUDAccDecLeft.Value;


            string FinalCommand="AccDecLeft," + curval.ToString();

            commandArray.Insert(commandArray.Count, FinalCommand);


            
            //int numCommands = commandArray.Count;
            //AppendLine("The following " + numCommands + " commands will be performed:");

            //// make the string to send
            //for (int i = 0; i < numCommands; i++)
            //{
            //    string word = commandArray[i] as string;
            //    finalCommands += "$" + word;
            //    AppendLine(word);
            //}

            //commandArray.Clear();

            //string toSend = numCommands + finalCommands;
            //byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

            //try
            //{
            //    NetworkStream clientStream = clientSocket.GetStream();

            //    clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
            //    AppendLine(toSend);

            //}
            //catch (Exception ex)
            //{
            //    AppendLine(ex.ToString());
            //}
        }

      

        
       

     
     }
}
