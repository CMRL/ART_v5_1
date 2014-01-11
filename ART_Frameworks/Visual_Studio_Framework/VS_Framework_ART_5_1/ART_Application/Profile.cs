using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;


namespace ART_Application
{
    public partial class FrmProfileHandler : Form
    {

        XDocument xmldoc;
        string output;
        public string InputFileName = "Profiles.xml";
        public string ButtonPressed;
       
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

        
        // Function Responsible for laoding values from XML file and Displaying them in ComboBoxes
        private int[] ProfileConfigurations;
        

        public int [] profileConfigurations 
        {
            get
            {
                return ProfileConfigurations;
            }
            set
            {
                ProfileConfigurations = value;
            }
        }



        public void LoadProfiles(string FileName, ComboBox Combotarget)
        {
            xmldoc = XDocument.Load(FileName);

            // Traverse through all Profile Names and fill them in a Dropdown menu
            String profName;
            int pID = 0;
            foreach (XElement el in xmldoc.Root.Elements())
            {
                //Profile ProfileItem = new Profile();
                profName = el.Attribute("UserName").Value;
                pID = Convert.ToInt32(el.Attribute("ID").Value);
                Combotarget.Items.Add(new Profiles(profName, pID));
            }
            Combotarget.Sorted = true;
            Combotarget.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

      

        public FrmProfileHandler()
        {
            int i;
            ProfileConfigurations= new int[50];
            // Assign defaule values in this array

            for (i = 0; i < 50; i++)
            {
                ProfileConfigurations[i] = 0;
            }

            InitializeComponent();
        }

       

        // Read all IDs in the Profile file and find the maximum which is then returned

        public int FindMaximumProfileID(XDocument xmldoc)
        {
            int MaxID = 0;

            var AllProfiles = xmldoc.Descendants("Profile");
            foreach (XElement eachProfile in AllProfiles)
            {
                int curID = int.Parse(eachProfile.Attribute("ID").Value);

                if (curID > MaxID)
                    MaxID = curID;
            }

            return MaxID;
        }


        // This function open up windows for for new Creating a New Profile
        private void btnNewProfileOption_Click(object sender, EventArgs e)
        {
            
            ButtonPressed = "New";
            Form NewProfileFrm = new NewOrEditProfile(this);
            
            NewProfileFrm.ShowDialog();

            
        }

        private void btnLoadProfileOption_Click(object sender, EventArgs e)
        {

            ButtonPressed = "Load";

            Form LoadProfileFrm = new LoadProfile(this);

            LoadProfileFrm.ShowDialog();
        }

        private void btnEditProfileOption_Click(object sender, EventArgs e)
        {
            ButtonPressed = "Edit";

            Form NewProfileFrm = new NewOrEditProfile(this);

            NewProfileFrm.ShowDialog();
        }

        private void btnCancelProfileOption_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Given a Profile ID, this function reads all its details and return them in a SortedDictionary Contairer
        // The Key of container contains nameofText field and Value of container contains desired Value
        public SortedDictionary<string, string> FindaProfile(XDocument xmldoc, int ProfileID)
        {

            
            SortedDictionary<string, string> RecalledProfileDetails = new SortedDictionary<string, string>();

            // Read profile details based on this unique profile ID

            XElement Selectedprofile = xmldoc.Descendants("Profile").Single(el => int.Parse(el.Attribute("ID").Value) == ProfileID);
            
            /************ FOLLOWING TASK BELOW ARE MISSING ****************/
            // Personal Details Nodes
      
      
            // Obtain UserName As well
            var UserName = Selectedprofile.Attribute("UserName");
            RecalledProfileDetails.Add("UserName", UserName.Value);
           
            // Obtain Rest of the Details
            var PersonalDetails = Selectedprofile.Descendants("PersonalDetails");

            // Display All elements and their values underneath this node
            foreach (var Node in PersonalDetails.Descendants())
            {
                output = Node.Name + " : " + Node.Value;

                if (Node.Name == "FirstName")
                {
                    RecalledProfileDetails.Add("FirstName", Node.Value);
                }
                else if (Node.Name == "LastName")
                {
                    RecalledProfileDetails.Add("LastName", Node.Value);
                }
                else
                {

                }
            }

            //// LeftCameraParameters Details Nodes
            var LeftCameraParameters = Selectedprofile.Descendants("LeftCameraParameters");

            // Display All elements and their values underneath this node
            foreach (var Node in LeftCameraParameters.Elements())
            {
                // Suppose for Luminance and Color Categories we have further child nodes and we would like to traverse them
                // We need to check whether there are any decendents there or not
                // If there are not any it will return null so loop wont execute
                var childrens = Node.Elements();
                if (childrens.Count() > 0)
                {
                    foreach (var childNode in Node.Elements())
                    {
                        if (childNode.Name == "Gain")
                            RecalledProfileDetails.Add("GainLeft",childNode.Value.ToString());
                        else if (childNode.Name == "Brightness")
                            RecalledProfileDetails.Add("BrightnessLeft",childNode.Value.ToString());
                        else if (childNode.Name == "Contrast")
                            RecalledProfileDetails.Add("ContrastLeft", childNode.Value.ToString());
                        else if (childNode.Name == "Exposure")
                            RecalledProfileDetails.Add("ExposureLeft",childNode.Value.ToString());
                        else if (childNode.Name == "Saturation")
                            RecalledProfileDetails.Add("SaturationLeft",childNode.Value.ToString());
                        else if (childNode.Name == "WhiteBalance")
                            RecalledProfileDetails.Add("WhiteBalanceLeft",childNode.Value.ToString());
                        else if (childNode.Name == "Hue")
                            RecalledProfileDetails.Add("HueLeft",childNode.Value.ToString());
                        else
                        {

                        }

                    }

                }
                else
                {
                    if (Node.Name == "BackgroundThreshold")
                        RecalledProfileDetails.Add("BackgroundThresholdLeft",Node.Value.ToString());

                }

            }

            // RightCameraParameters Details Nodes
            var RightCameraParameters = Selectedprofile.Descendants("RightCameraParameters");

            // Display All elements and their values underneath this node
            foreach (var Node in RightCameraParameters.Elements())
            {
                // Suppose for Luminance and Color Categories we have further child nodes and we would like to traverse them
                // We need to check whether there are any decendents there or not
                // If there are not any it will return null so loop wont execute
                var childrens = Node.Elements();
                if (childrens.Count() > 0)
                {
                    foreach (var childNode in Node.Elements())
                    {
                        if (childNode.Name == "Gain")
                            RecalledProfileDetails.Add("GainRight",childNode.Value.ToString());
                        else if (childNode.Name == "Brightness")
                            RecalledProfileDetails.Add("BrightnessRight",childNode.Value.ToString());
                        else if (childNode.Name == "Contrast")
                            RecalledProfileDetails.Add("ContrastRight", childNode.Value.ToString());
                        else if (childNode.Name == "Exposure")
                            RecalledProfileDetails.Add("ExposureRight", childNode.Value.ToString());
                        else if (childNode.Name == "Saturation")
                            RecalledProfileDetails.Add("SaturationRight", childNode.Value.ToString());
                        else if (childNode.Name == "WhiteBalance")
                            RecalledProfileDetails.Add("WhiteBalanceRight", childNode.Value.ToString());
                        else if (childNode.Name == "Hue")
                            RecalledProfileDetails.Add("HueRight", childNode.Value.ToString());
                        else
                        {

                        }
                    }

                }
                else
                {
                    if (Node.Name == "BackgroundThreshold")
                        RecalledProfileDetails.Add("BackgroundThresholdRight",Node.Value.ToString());

                }
            }
            return RecalledProfileDetails;
        }



      }
}
