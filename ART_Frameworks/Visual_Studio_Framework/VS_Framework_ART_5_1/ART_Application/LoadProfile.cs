using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ART_Application
{
    public partial class LoadProfile : Form
    {
        FrmProfileHandler parentForm;
        XDocument xmldoc;
        SortedDictionary<string, string> ProfileDetails = new SortedDictionary<string, string>();
        string output;
        string InputFileName;
        TcpClient clientSocket;
        ArrayList commandArray = new ArrayList();
        bool gameRunning = false;
        string gameMode = "";
        string ApplicationCode = "1";


        // This class is used to populate comboBoxes with "UserName + ID "
        public class Profiles
        {
            public string ProfileName;
            public int ProfileNumber;
            public Profiles(string ProfileName, int ProfileNumber)
            {
                this.ProfileName = ProfileName;
                this.ProfileNumber = ProfileNumber;
            }

            public override string ToString()
            {
                // For displaying in the Combo
                return ProfileName;
            }
        }

       
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
      

        public LoadProfile(Form parentCaller)
        {
            this.parentForm = parentCaller as FrmProfileHandler;
            // Obtain input profiles file name 
            InputFileName = parentForm.InputFileName;
            // initialize all controls first 
            InitializeComponent();


            // Finally Populate All profiles IDs and their Names in ComboBox

            xmldoc = XDocument.Load(InputFileName);

            // Traverse through all Profile Names and fill them in a Dropdown menu
            String profName;
            int pID = 0;
            foreach (XElement el in xmldoc.Root.Elements())
            {
                //Profile ProfileItem = new Profile();
                profName = el.Attribute("UserName").Value;
                pID = Convert.ToInt32(el.Attribute("ID").Value);
                comboExistingProfile.Items.Add(new Profiles(profName, pID));
            }
            comboExistingProfile.Sorted = true;
            comboExistingProfile.AutoCompleteMode = AutoCompleteMode.Suggest;

            // Also Socket commumnication for sending packets

            try
            {
                // Connect to the server running in Unity
                int port = 1234;
                clientSocket = new TcpClient("127.0.0.1", port);
                string text = "Connected to server on port " + port;
                listDebugLoading.Items.Add(text);

            }
            catch (Exception ex)
            {
                listDebugLoading.Items.Add("No Server to connect to..");
            }

        }

        // This function Sends the CommandPacket to 
        private void btnLoadProfile_Click(object sender, EventArgs e)
        {
            // Load Profile and Exit this Form
            // This will read Camera Parameters values from XML and send it to Plugin Handlers through unity


            string finalCommands = "";
            int numCommands = commandArray.Count;
            listDebugLoading.Items.Add(">>  The following " + numCommands + " commands will be performed:");

            // make the string to send
            // The Command Structure would be 

            //   x,TotalCommands{$dothis$dothat$donothing};y,TotalCommands{$hi$hello$hello};x,TotalCommands{$dothis$dothat$donothing}

            // Here x,y tell which Application is generating this command
            //TotalCommands tells about total command 
            // Each command Packet is bounded by curly brackets
            // $ sign separate commands within a Command packet

            //-----------Appication Code---------------//   
            //----------- 0 = ART(should'nt be changed)// 
            //----------- 1 = Profile Forms -----------//
            //----------- 2 = TheraMem-----------------//

            finalCommands = numCommands.ToString() + "{";
            for (int i = 0; i < numCommands; i++)
            {
                string word = commandArray[i] as string;
                finalCommands += "$" + word;
                //listDebugLoading.Items.Add(word);
            }
            finalCommands += "};";

           // commandArray.Clear();

            string toSend = ApplicationCode + "," + finalCommands;

           // listDebugLoading.Items.Add(toSend);

            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

            try
            {
                NetworkStream clientStream = clientSocket.GetStream();

                clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                listDebugLoading.Items.Add(toSend);

            }
            catch (Exception ex)
            {
                 listDebugLoading.Items.Add(ex.ToString());
            }




            //this.Close();
        }

        private void btnCancelLoad_Click(object sender, EventArgs e)
        {

            string toSend = ApplicationCode +",1{$Shutdown};";
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(toSend);

            try
            {
                NetworkStream clientStream = clientSocket.GetStream();

                clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                listDebugLoading.Items.Add(toSend);
            }
            catch (Exception ex)
            {
                listDebugLoading.Items.Add(ex.ToString());
            }

            // Close application after waiting 1 second for Unity to stop the server etc.
            Thread.Sleep(1000);

            this.Close();
        }

        private void comboExistingProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display profile details bsed on this unique profile Name
            listDebugLoading.Items.Clear();
            Profiles comboProfile = (Profiles)comboExistingProfile.SelectedItem;

            listDebugLoading.Items.Add(comboProfile.ProfileNumber + " : " + comboProfile.ProfileName);
            // Display this info in Textboxes
            txtProfileID.Text = comboProfile.ProfileNumber.ToString();
            txtProfileUserName.Text = comboProfile.ProfileName;

            // Now Load values from Default Profile into the text fields
            // Read All profiles
            // Find profile with ID=1 which is Default Profile
            xmldoc = XDocument.Load(InputFileName);
            ProfileDetails.Clear();
            ProfileDetails = parentForm.FindaProfile(xmldoc, comboProfile.ProfileNumber);
            //Get the FirstName and LAst Name from XML

            if (ProfileDetails.ContainsKey("FirstName"))
                txtProfileFirstName.Text = ProfileDetails["FirstName"];
            if (ProfileDetails.ContainsKey("LastName"))
                txtProfileLastName.Text = ProfileDetails["LastName"];

            // Generate Commands for all CameraParamets
            // Every time all parameters will be added in a command string
            // Order of Command is Fixed according to values in XML
            
            // Camera Parameters to Command Mapping
            // Indexes of values in Command String 
            // 0=>UserName
            // 1=>ProfileID
            // 2=>FirstName
            // 3=>LastName
            // 4=>GainLeft
            // 5=>GainRight
            // 6=>BrightnessLeft
            // 7=>BrightnessRight
            // 8=>ContrastLeft
            // 9=>ContrastRight
            // 10=>ExposureLeft
            // 11=>ExposureRight
            // 12=>SaturationLeft
            // 13=>SaturationRight
            // 14=>WhiteBalanceLeft
            // 15=>WhiteBalanceRight
            // 16=>HueLeft
            // 17=>HueRight
            // 18=>BackgroundThresholdLeft
            // 19=>BackgroundThresholdRight
            commandArray.Clear();
            // 0=>UserName
            commandArray.Insert(commandArray.Count, txtProfileUserName.Text);
            // 1=>ProfileID
            commandArray.Insert(commandArray.Count, txtProfileID.Text);
            // 2=>FirstName
            commandArray.Insert(commandArray.Count, txtProfileFirstName.Text);
            // 3=>LastName
            commandArray.Insert(commandArray.Count, txtProfileLastName.Text);
            // 4=>GainLeft
            if (ProfileDetails.ContainsKey("GainLeft"))
            commandArray.Insert(commandArray.Count,  ProfileDetails["GainLeft"]);
            // 5=>GainRight
            if (ProfileDetails.ContainsKey("GainRight"))
                commandArray.Insert(commandArray.Count, ProfileDetails["GainRight"]);
            // 6=>BrightnessLeft
            if (ProfileDetails.ContainsKey("BrightnessLeft"))
                commandArray.Insert(commandArray.Count, ProfileDetails["BrightnessLeft"]);
            // 7=>BrightnessRight
            if (ProfileDetails.ContainsKey("BrightnessRight"))
                commandArray.Insert(commandArray.Count, ProfileDetails["BrightnessRight"]);
            
            // 8=>ContrastLeft
            if (ProfileDetails.ContainsKey("ContrastLeft"))
                commandArray.Insert(commandArray.Count, ProfileDetails["ContrastLeft"]);
            // 9=>ContrastRight
            if (ProfileDetails.ContainsKey("ContrastRight"))
                commandArray.Insert(commandArray.Count, ProfileDetails["ContrastRight"]);
            // 10=>ExposureLeft
            if (ProfileDetails.ContainsKey("ExposureLeft"))
                commandArray.Insert(commandArray.Count, ProfileDetails["ExposureLeft"]);
            // 11=>ExposureRight
            if (ProfileDetails.ContainsKey("ExposureRight"))
                commandArray.Insert(commandArray.Count, ProfileDetails["ExposureRight"]);
            // 12=>SaturationLeft
            if (ProfileDetails.ContainsKey("SaturationLeft"))
                commandArray.Insert(commandArray.Count, ProfileDetails["SaturationLeft"]);
            // 13=>SaturationRight
            if (ProfileDetails.ContainsKey("SaturationRight"))
                commandArray.Insert(commandArray.Count, ProfileDetails["SaturationRight"]);
            // 14=>WhiteBalanceLeft
            if (ProfileDetails.ContainsKey("WhiteBalanceLeft"))
                commandArray.Insert(commandArray.Count, ProfileDetails["WhiteBalanceLeft"]);
            // 15=>WhiteBalanceRight
            if (ProfileDetails.ContainsKey("WhiteBalanceRight"))
                commandArray.Insert(commandArray.Count, ProfileDetails["WhiteBalanceRight"]);
            // 16=>HueLeft
            if (ProfileDetails.ContainsKey("HueLeft"))
                commandArray.Insert(commandArray.Count, ProfileDetails["HueLeft"]);
            // 17=>HueRight
            if (ProfileDetails.ContainsKey("HueRight"))
                commandArray.Insert(commandArray.Count, ProfileDetails["HueRight"]);
            // 18=>BackgroundThresholdLeft
            if (ProfileDetails.ContainsKey("BackgroundThresholdLeft"))
                commandArray.Insert(commandArray.Count, ProfileDetails["BackgroundThresholdLeft"]);
            // 19=>BackgroundThresholdRight
            if (ProfileDetails.ContainsKey("BackgroundThresholdRight"))
                commandArray.Insert(commandArray.Count, ProfileDetails["BackgroundThresholdRight"]);
            
           
        }
    }
}
