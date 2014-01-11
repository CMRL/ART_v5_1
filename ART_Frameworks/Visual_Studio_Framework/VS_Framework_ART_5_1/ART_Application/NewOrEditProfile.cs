using System;
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

namespace ART_Application
{
    public partial class NewOrEditProfile : Form
    {
        FrmProfileHandler parentForm;
        XDocument xmldoc;
        SortedDictionary<string, string> ProfileDetails = new SortedDictionary<string, string>();
        string output;
        string InputFileName;
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



        // Constructure Called when this Form is being created 
        public NewOrEditProfile(Form parentCaller)
        {
            
            this.parentForm = parentCaller as FrmProfileHandler;
            // Obtain input profiles file name 
            InputFileName = parentForm.InputFileName;
            // initialize all controls first 
            InitializeComponent();

            if (parentForm.ButtonPressed == "Edit")
            {
                //UN-Hide Combo Box,delete, Update Button
                comboEditProfile.Visible = true;
                btnAdvCameraOptions.Visible = true;
                btnDeleteProfile.Visible = true;
                btnUpdateProfile.Visible = true;
                lblProfileNotFound.Visible = true;
                txtProfileUserName.ReadOnly = true;
                // Set Label accordingly
                lblCreateEdit.Text = "Please choose a profile to edit";
                lblProfileNotFound.Text = "*Not listed? Did you create this profile first?";

                //Hide Create button
                btnCreateProfile.Visible = false;

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
                    comboEditProfile.Items.Add(new Profiles(profName, pID));
                }
                comboEditProfile.Sorted = true;
                comboEditProfile.AutoCompleteMode = AutoCompleteMode.Suggest;

            }
            else if (parentForm.ButtonPressed == "New")
            {
                //Hide Combo Box,delete, Update Button
                comboEditProfile.Visible = false;
                btnAdvCameraOptions.Visible = false;
                btnDeleteProfile.Visible = false;
                btnUpdateProfile.Visible = false;
                lblProfileNotFound.Visible = false;
                // Set Label accordingly
                lblCreateEdit.Text = "Please fill profile details.\nDefault camera options are already filled:";
                

                // Enable controls to make them editable
                txtProfileUserName.Enabled = true;
                txtProfileUserName.ReadOnly = false;
                btnAdvCameraOptions.Enabled = true;
                btnAdvCameraOptions.Visible= true;

                // Now Load values from Default Profile into the text fields
                // Read All profiles
                // Find profile with ID=1 which is Default Profile
                xmldoc = XDocument.Load(InputFileName);
                                
                ProfileDetails = parentForm.FindaProfile(xmldoc, 1);

                // Now ProfileDetails contains TextFields Names as Keys and their values to fill in 
                
                //--------New Profile ID will be assigned automatically by looking at all previous IDs and find ing the latest one--------//
                int MaxID=parentForm.FindMaximumProfileID(xmldoc);
                txtProfileID.Text = (MaxID+1).ToString();
                

                //-------Left Camera Options ------------//
                
                if (ProfileDetails.ContainsKey("GainLeft"))
                    txtProfileGainLeft.Text=ProfileDetails["GainLeft"];
                if (ProfileDetails.ContainsKey("BrightnessLeft"))
                    txtProfileBrightnessLeft.Text = ProfileDetails["BrightnessLeft"];
                if (ProfileDetails.ContainsKey("ContrastLeft"))
                    txtProfileContrastLeft.Text = ProfileDetails["ContrastLeft"];
                if (ProfileDetails.ContainsKey("ExposureLeft"))
                    txtProfileExposureLeft.Text = ProfileDetails["ExposureLeft"];
                if (ProfileDetails.ContainsKey("SaturationLeft"))
                    txtProfileSaturationLeft.Text = ProfileDetails["SaturationLeft"];
                if (ProfileDetails.ContainsKey("WhiteBalanceLeft"))
                    txtProfileWhiteBalLeft.Text = ProfileDetails["WhiteBalanceLeft"];
                if (ProfileDetails.ContainsKey("HueLeft"))
                    txtProfileHueLeft.Text = ProfileDetails["HueLeft"];
                if (ProfileDetails.ContainsKey("BackgroundThresholdLeft"))
                    txtProfileBackgroundThresholdLeft.Text = ProfileDetails["BackgroundThresholdLeft"];

                //-------Right Camera Options ------------//
                
                if (ProfileDetails.ContainsKey("GainRight"))
                    txtProfileGainRight.Text = ProfileDetails["GainRight"];
                if (ProfileDetails.ContainsKey("BrightnessRight"))
                    txtProfileBrightnessRight.Text = ProfileDetails["BrightnessRight"];
                if (ProfileDetails.ContainsKey("ContrastRight"))
                    txtProfileContrastRight.Text = ProfileDetails["ContrastRight"];
                if (ProfileDetails.ContainsKey("ExposureRight"))
                    txtProfileExposureRight.Text = ProfileDetails["ExposureRight"];
                if (ProfileDetails.ContainsKey("SaturationRight"))
                    txtProfileSaturationRight.Text = ProfileDetails["SaturationRight"];
                if (ProfileDetails.ContainsKey("WhiteBalanceRight"))
                    txtProfileWhiteBalRight.Text = ProfileDetails["WhiteBalanceRight"];
                if (ProfileDetails.ContainsKey("HueRight"))
                    txtProfileHueRight.Text = ProfileDetails["HueRight"];
                if (ProfileDetails.ContainsKey("BackgroundThresholdRight"))
                    txtProfileBackgroundThresholdRight.Text = ProfileDetails["BackgroundThresholdRight"];
           


            }
            
            
        }

      

        private void comboEditProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display profile details bsed on this unique profile Name
            listDebugEditing.Items.Clear();
            Profiles comboProfile = (Profiles)comboEditProfile.SelectedItem;

            listDebugEditing.Items.Add(comboProfile.ProfileNumber + " : " + comboProfile.ProfileName);
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
          

            //-------Left Camera Options ------------//

            if (ProfileDetails.ContainsKey("GainLeft"))
                txtProfileGainLeft.Text = ProfileDetails["GainLeft"];
            if (ProfileDetails.ContainsKey("BrightnessLeft"))
                txtProfileBrightnessLeft.Text = ProfileDetails["BrightnessLeft"];
            if (ProfileDetails.ContainsKey("ContrastLeft"))
                txtProfileContrastLeft.Text = ProfileDetails["ContrastLeft"];
            if (ProfileDetails.ContainsKey("ExposureLeft"))
                txtProfileExposureLeft.Text = ProfileDetails["ExposureLeft"];
            if (ProfileDetails.ContainsKey("SaturationLeft"))
                txtProfileSaturationLeft.Text = ProfileDetails["SaturationLeft"];
            if (ProfileDetails.ContainsKey("WhiteBalanceLeft"))
                txtProfileWhiteBalLeft.Text = ProfileDetails["WhiteBalanceLeft"];
            if (ProfileDetails.ContainsKey("HueLeft"))
                txtProfileHueLeft.Text = ProfileDetails["HueLeft"];
            if (ProfileDetails.ContainsKey("BackgroundThresholdLeft"))
                txtProfileBackgroundThresholdLeft.Text = ProfileDetails["BackgroundThresholdLeft"];

            //-------Right Camera Options ------------//

            if (ProfileDetails.ContainsKey("GainRight"))
                txtProfileGainRight.Text = ProfileDetails["GainRight"];
            if (ProfileDetails.ContainsKey("BrightnessRight"))
                txtProfileBrightnessRight.Text = ProfileDetails["BrightnessRight"];
            if (ProfileDetails.ContainsKey("ContrastRight"))
                txtProfileContrastRight.Text = ProfileDetails["ContrastRight"];
            if (ProfileDetails.ContainsKey("ExposureRight"))
                txtProfileExposureRight.Text = ProfileDetails["ExposureRight"];
            if (ProfileDetails.ContainsKey("SaturationRight"))
                txtProfileSaturationRight.Text = ProfileDetails["SaturationRight"];
            if (ProfileDetails.ContainsKey("WhiteBalanceRight"))
                txtProfileWhiteBalRight.Text = ProfileDetails["WhiteBalanceRight"];
            if (ProfileDetails.ContainsKey("HueRight"))
                txtProfileHueRight.Text = ProfileDetails["HueRight"];
            if (ProfileDetails.ContainsKey("BackgroundThresholdRight"))
                txtProfileBackgroundThresholdRight.Text = ProfileDetails["BackgroundThresholdRight"];
            
        }

    
        //This function Saves a new Profile to XML file
        //Clears Persoonal detail Fields 
        //Reset ID for new Profile
        private void btnCreateProfile_Click(object sender, EventArgs e)
        {
            //Save filled form as a new profile in XML file

            xmldoc = XDocument.Load(InputFileName);

            XElement newProfile = new XElement("Profile",
                                        new XAttribute("ID", txtProfileID.Text),
                                        new XAttribute("UserName", txtProfileUserName.Text),
                                        new XElement("PersonalDetails",
                                            new XElement("FirstName", txtProfileFirstName.Text),
                                            new XElement("LastName", txtProfileLastName.Text)),
                                        new XElement("LeftCameraParameters",
                                            new XElement("Luminance",
                                                new XElement("Gain", txtProfileGainLeft.Text),
                                                new XElement("Brightness", txtProfileBrightnessLeft.Text),
                                                new XElement("Contrast", txtProfileContrastLeft.Text),
                                                new XElement("Exposure", txtProfileExposureLeft.Text)),
                                            new XElement("Color",
                                                new XElement("Saturation", txtProfileSaturationLeft.Text),
                                                new XElement("WhiteBalance", txtProfileWhiteBalLeft.Text),
                                                new XElement("Hue", txtProfileHueLeft.Text)),
                                            new XElement("BackgroundThreshold", txtProfileBackgroundThresholdLeft.Text)),
                                        new XElement("RightCameraParameters",
                                            new XElement("Luminance",
                                                new XElement("Gain", txtProfileGainRight.Text),
                                                new XElement("Brightness", txtProfileBrightnessRight.Text),
                                                new XElement("Contrast", txtProfileContrastRight.Text),
                                                new XElement("Exposure", txtProfileExposureRight.Text)),
                                            new XElement("Color",
                                                new XElement("Saturation", txtProfileSaturationRight.Text),
                                                new XElement("WhiteBalance", txtProfileWhiteBalRight.Text),
                                                new XElement("Hue", txtProfileHueRight.Text)),
                                            new XElement("BackgroundThreshold", txtProfileBackgroundThresholdRight.Text)));
            xmldoc.Element("Profiles").Add(newProfile);
            xmldoc.Save(InputFileName);

            // Generate Message for successfully saving profile

            lblProfileNotFound.Text = "Profile Saved!";
            lblProfileNotFound.Visible = true;
            
            // Reset Personal detail Fields

            txtProfileUserName.Text = "";
            txtProfileFirstName.Text = "";
            txtProfileLastName.Text = "";

            // Set a latest ID

            int MaxID = parentForm.FindMaximumProfileID(xmldoc);
            txtProfileID.Text = (MaxID + 1).ToString();


        }

        private void btnExitCreateEditProfile_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnProfileFormReset_Click(object sender, EventArgs e)
        {
            // Display this info in Textboxes
            txtProfileUserName.Text = "";

            // Reset Personal Details

            txtProfileFirstName.Text = "";
            txtProfileLastName.Text = "";

            // Now Load values from Default Profile into the text fields
            // Read All profiles
            // Find profile with ID=1 which is Default Profile
            xmldoc = XDocument.Load(InputFileName);

            ProfileDetails = parentForm.FindaProfile(xmldoc, 1);

            // Now ProfileDetails contains TextFields Names as Keys and their values to fill in 

            //--------New Profile ID will be assigned automatically by looking at all previous IDs and find ing the latest one--------//
            int MaxID = parentForm.FindMaximumProfileID(xmldoc);
            txtProfileID.Text = (MaxID + 1).ToString();


            //-------Left Camera Options ------------//

            if (ProfileDetails.ContainsKey("GainLeft"))
                txtProfileGainLeft.Text = ProfileDetails["GainLeft"];
            if (ProfileDetails.ContainsKey("BrightnessLeft"))
                txtProfileBrightnessLeft.Text = ProfileDetails["BrightnessLeft"];
            if (ProfileDetails.ContainsKey("ContrastLeft"))
                txtProfileContrastLeft.Text = ProfileDetails["ContrastLeft"];
            if (ProfileDetails.ContainsKey("ExposureLeft"))
                txtProfileExposureLeft.Text = ProfileDetails["ExposureLeft"];
            if (ProfileDetails.ContainsKey("SaturationLeft"))
                txtProfileSaturationLeft.Text = ProfileDetails["SaturationLeft"];
            if (ProfileDetails.ContainsKey("WhiteBalanceLeft"))
                txtProfileWhiteBalLeft.Text = ProfileDetails["WhiteBalanceLeft"];
            if (ProfileDetails.ContainsKey("HueLeft"))
                txtProfileHueLeft.Text = ProfileDetails["HueLeft"];
            if (ProfileDetails.ContainsKey("BackgroundThresholdLeft"))
                txtProfileBackgroundThresholdLeft.Text = ProfileDetails["BackgroundThresholdLeft"];

            //-------Right Camera Options ------------//

            if (ProfileDetails.ContainsKey("GainRight"))
                txtProfileGainRight.Text = ProfileDetails["GainRight"];
            if (ProfileDetails.ContainsKey("BrightnessRight"))
                txtProfileBrightnessRight.Text = ProfileDetails["BrightnessRight"];
            if (ProfileDetails.ContainsKey("ContrastRight"))
                txtProfileContrastRight.Text = ProfileDetails["ContrastRight"];
            if (ProfileDetails.ContainsKey("ExposureRight"))
                txtProfileExposureRight.Text = ProfileDetails["ExposureRight"];
            if (ProfileDetails.ContainsKey("SaturationRight"))
                txtProfileSaturationRight.Text = ProfileDetails["SaturationRight"];
            if (ProfileDetails.ContainsKey("WhiteBalanceRight"))
                txtProfileWhiteBalRight.Text = ProfileDetails["WhiteBalanceRight"];
            if (ProfileDetails.ContainsKey("HueRight"))
                txtProfileHueRight.Text = ProfileDetails["HueRight"];
            if (ProfileDetails.ContainsKey("BackgroundThresholdRight"))
                txtProfileBackgroundThresholdRight.Text = ProfileDetails["BackgroundThresholdRight"];
           
        }

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            Profiles comboProfile = (Profiles)comboEditProfile.SelectedItem;

            // Load XML Document
            xmldoc = XDocument.Load(InputFileName);

            // Find as Profile that has same 

            XElement Selectedprofile = xmldoc.Descendants("Profile").Single(el => int.Parse(el.Attribute("ID").Value) == comboProfile.ProfileNumber);

            // Personal Details Nodes
            var PersonalDetails = Selectedprofile.Descendants("PersonalDetails");

            // Display All elements and their values underneath this node
            foreach (var Node in PersonalDetails.Descendants())
            {
                output = Node.Name + " : " + Node.Value;

                if (Node.Name == "FirstName")
                {
                    Node.Value = txtProfileFirstName.Text;
                }
                else if (Node.Name == "LastName")
                {
                    Node.Value = txtProfileLastName.Text;
                }
                else
                {

                }

                listDebugEditing.Items.Add(output + "Updated");

            }

            // LeftCameraParameters Details Nodes
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
                            childNode.Value = txtProfileGainLeft.Text;
                        else if (childNode.Name == "Brightness")
                            childNode.Value = txtProfileBrightnessLeft.Text;
                        else if (childNode.Name == "Contrast")
                            childNode.Value = txtProfileContrastLeft.Text;
                        else if (childNode.Name == "Exposure")
                            childNode.Value = txtProfileExposureLeft.Text;
                        else if (childNode.Name == "Saturation")
                            childNode.Value = txtProfileSaturationLeft.Text;
                        else if (childNode.Name == "WhiteBalance")
                            childNode.Value = txtProfileWhiteBalLeft.Text;
                        else if (childNode.Name == "Hue")
                            childNode.Value = txtProfileHueLeft.Text;
                        else
                        {

                        }

                    }

                }
                else
                {
                    if (Node.Name == "BackgroundThreshold")
                        Node.Value = txtProfileBackgroundThresholdLeft.Text;

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
                            childNode.Value = txtProfileGainRight.Text;
                        else if (childNode.Name == "Brightness")
                            childNode.Value = txtProfileBrightnessRight.Text;
                        else if (childNode.Name == "Contrast")
                            childNode.Value = txtProfileContrastRight.Text;
                        else if (childNode.Name == "Exposure")
                            childNode.Value = txtProfileExposureRight.Text;
                        else if (childNode.Name == "Saturation")
                            childNode.Value = txtProfileSaturationRight.Text;
                        else if (childNode.Name == "WhiteBalance")
                            childNode.Value = txtProfileWhiteBalRight.Text;
                        else if (childNode.Name == "Hue")
                            childNode.Value = txtProfileHueRight.Text;
                        else
                        {

                        }
                    }

                }
                else
                {
                    if (Node.Name == "BackgroundThreshold")
                        Node.Value = txtProfileBackgroundThresholdRight.Text;

                }

            }

            xmldoc.Save(InputFileName);
        }

        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            // Load XML Document
            xmldoc = XDocument.Load(InputFileName);

            Profiles comboProfile = (Profiles)comboEditProfile.SelectedItem;

            // Find as Profile that has same ID

            XElement Selectedprofile = xmldoc.Descendants("Profile").Single(el => int.Parse(el.Attribute("ID").Value) == comboProfile.ProfileNumber);
            Selectedprofile.Remove();
            xmldoc.Save(InputFileName);

            // Alse Reload combobox with new Values


            // Finally Populate All profiles IDs and their Names in ComboBox

            xmldoc = XDocument.Load(InputFileName);
            comboEditProfile.Items.Clear();
            // Traverse through all Profile Names and fill them in a Dropdown menu
            String profName;
            int pID = 0;
            foreach (XElement el in xmldoc.Root.Elements())
            {
                //Profile ProfileItem = new Profile();
                profName = el.Attribute("UserName").Value;
                pID = Convert.ToInt32(el.Attribute("ID").Value);
                comboEditProfile.Items.Add(new Profiles(profName, pID));
            }
            comboEditProfile.Sorted = true;
            comboEditProfile.AutoCompleteMode = AutoCompleteMode.Suggest;




            // Clear All text fileds
            // Display this info in Textboxes
                txtProfileUserName.Text = "";
                txtProfileID.Text = "";

                // Reset Personal Details
            
                txtProfileFirstName.Text = "";
                txtProfileLastName.Text = "";

            //-------Left Camera Options ------------//


                txtProfileGainLeft.Text = "";
                txtProfileBrightnessLeft.Text = "";
                txtProfileContrastLeft.Text = "";
                txtProfileExposureLeft.Text = "";
                txtProfileSaturationLeft.Text = "";
                txtProfileWhiteBalLeft.Text = "";
                txtProfileHueLeft.Text = "";
                txtProfileBackgroundThresholdLeft.Text = "";

            //-------Right Camera Options ------------//


                txtProfileGainRight.Text = "";
                txtProfileBrightnessRight.Text = "";
                txtProfileContrastRight.Text = "";
                txtProfileExposureRight.Text = "";
                txtProfileSaturationRight.Text = "";
                txtProfileWhiteBalRight.Text = "";
                txtProfileHueRight.Text = "";
                txtProfileBackgroundThresholdRight.Text = "";



        }
    }
}
