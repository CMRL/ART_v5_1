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
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length > 0)
             for(int i = 0; i < args.Length; i++)
            {
          
                if (args[i] == "0") //----Start ART Application ---//
                {
                    Application.Run(new Main_Dialog());
                }
                else if (args[i] == "1")//----Start TheraMem ---//
                {
                    Application.Run(new TheraMem());
                    
                }
                else if (args[i] == "2")//----Start Profiles ---//
                {
                    Application.Run(new FrmProfileHandler());
                }
             }
            else
            {
                //Application.Run(new Main_Dialog());
            }
            

                                 
        }
    }
}
