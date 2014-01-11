using System;
using UnityEngine;
using System.Collections;




public class ApplicationInitializer {
	
	// Here we will get argument from BatchFile and start the Appropriate Application 
	// ApplicationCode: 0 => ART
	// ApplicationCode: 1 => Profiles
	// ApplicationCode: 2 => TheraMem
	
	
	void Start() 
    {
	    Console.WriteLine("Getting input Arguments");
	    //  Invoke this sample with an arbitrary set of command line arguments.
	    String[] arguments = Environment.GetCommandLineArgs();
	    Console.WriteLine("GetCommandLineArgs: {0}", String.Join(", ", arguments));
		
		//Now we need to set this value to the ApplicationCode property defined in CommandHandler
		
		CommandHandler obj=new CommandHandler();
		
		obj.ApplicationCode = arguments[0];			
		
    }
	
	
}
