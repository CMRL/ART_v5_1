using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ART_Application
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_Dialog());

            //Application.Run(new FrmProfileHandler());


            //// Create a XML reader
            //XmlDocument doc = new XmlDocument();

            //// Load an XML file
            //doc.Load("Profiles.xml");

            //// Traverse through complete file
            //XmlNode element = doc.DocumentElement;
            //XmlNodeList elementchilds = element.ChildNodes;
            //foreach (XmlNode profile in elementchilds)
            //{
            //    string text = profile.InnerText;
            //    Console.Write(text);


            //}
        }
    }
}
