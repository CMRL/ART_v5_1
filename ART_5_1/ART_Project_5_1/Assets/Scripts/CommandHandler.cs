using UnityEngine;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;
using System.IO;


public class CommandHandler : MonoBehaviour {

	private bool clientConnected;
	private bool listening = true;
	// Tells about source application sending commands
	// Code 0=> Main ART
	// Code 1=> From Profile
	// Code 2=> TheraMem
	
	public string ApplicationCode;
	// variables for the dialog
	private string data = "";
	private string dataPath;
	
	// Thread for running the network loop in
	private Thread serverThread_listener;
	private Thread serverThread_data;
	
	// Network server and client objects
	private TcpListener serverListener;
	private TcpClient client;
	private ArrayList clientArray;
	
	// variables for actions
	private string currentGame = "";
	
	// variables for TheraMem actions
	private string gameMode;
	private string oneHandedModeSide;
	
	// Planes showing video
	GameObject leftPlane;
	GameObject rightPlane;
	public Camera mainCam;
	
	ThreadStart listenerThread;
	
	// Initialization function
	void Start() {
		// path where the exe should be found
		dataPath = Application.dataPath;
		
		// Thread starts and runs the 'SayHello' function within
//		listenerThread = new ThreadStart(GetDataFromClient);
//		serverThread_listener = new Thread(listenerThread);
//		serverThread_listener.Start();
		
		clientArray = new ArrayList();
		
		this.SetGameMode = "";
		
		leftPlane = GameObject.Find("LeftPlane");
		rightPlane = GameObject.Find("RightPlane");
		
		int port = 1234;
		serverListener = new TcpListener(port);
		serverListener.Start();
		
		print("Server started..");
		
		for (int i = 0; i < 2; i++)
		{
			Thread thread = new Thread(new ThreadStart(GetDataFromClient));
			thread.Start();
		}
		
		System.Diagnostics.Process.Start(dataPath + "/ART_Application.exe", ApplicationCode);
	}
	
	void OnDisable()
	{
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
	
	public string SetOneHandedModeSide
	{
		get
		{
			return this.oneHandedModeSide;
		}
		set
		{
			this.oneHandedModeSide = value;
		}
	}
	
	// Connects to the windows form and loops to wait for actions from the user
	void GetDataFromClient() {
		
		try 
		{			
			Socket socketForClient = serverListener.AcceptSocket();
			
			if (socketForClient.Connected)
			{
				print ("Client: " + socketForClient.RemoteEndPoint + " has now connected");
				
				NetworkStream clientStream = new NetworkStream(socketForClient);
				
				StreamWriter streamWriter = new StreamWriter(clientStream);
				StreamReader streamReader = new StreamReader(clientStream);
				
				while (true)
				{
					data = streamReader.ReadLine();
					print ("recieved: " + data);
					
					int LeftBracketIndex=data.IndexOf("{");
					
					//Obtain a substring like this "{$shutdown}; without application tag and number of command tag"
					string subData=data.Substring(LeftBracketIndex);
					if (subData == "{$shutdown};") 
					{
						break;
					}
				}
//				streamReader.Close ();
//				streamWriter.Close ();
//				clientStream.Close ();
//				socketForClient.Close();
			}
			
		} catch (Exception ex)
		{
			print ("Error in thread: "+ex.Message);
		}
	}
	
	// Update functions contantly monitoring for data from the user (Windows Form)
	// and when it does come, a switch statement will select which actions to perform
	// within Unity.
	void Update() {
		// the data from the Form
		string newData = data;
		// 'data' reset to empty string to wait for more commands
		data = "";
		
		if (newData != "") {
			print(newData);
		}
		
		//   x,TotalCommands{$dothis$dothat$donothing};

        // Here x,y tells which Application is generating this command
        // TotalCommands tells about total command 
        // Each command Packet is bounded by curly brackets
        // $ sign separate commands within a Command packet

        //-----------Appication Code---------------//   
        //----------- 0 = ART(should'nt be changed)// 
        //----------- 1 = TheraMem -----------//
        //----------- 2 = Profile Forms-----------------//
		
		
		
		if (newData != null && newData != "") 
		{
			
			//"1,20{$Default$1$Default$Default$3$3$2$2$4$4$0$0$4$4$4650$4650$0$0$17$17};"
			
			// First Obtain ApplicationCode
			string[] commands;
			string applicationCode = newData[0].ToString();
			//print ("App Code: " + applicationCode);
			int FirstCommaIndex=newData.IndexOf(",");
			int LeftCurlyBracket=newData.IndexOf("{");
			
			string numberOfCommands="";
			if (FirstCommaIndex != -1)
			{
				// Numnber of commands in between FirstComma and LeftCurlyBrackets
				
				for(int i=FirstCommaIndex+1;i<LeftCurlyBracket;i++)
				{
					numberOfCommands+=newData[i].ToString();
				}
				
				int numCommands = Int32.Parse(numberOfCommands);
				
				commands = new string[numCommands];
				
				if (numCommands > 0) 
				{
					// Obtain All Commands substring   after '{$' character
					newData = newData.Substring(LeftCurlyBracket+2);
					
					// Now Obtain Index of '}' in this newData string
					int RightCurlyBracket=newData.IndexOf("}");
					
					int index = 0;
					int currStartPos = 0;
					
					for (int i = 0; i < RightCurlyBracket; i++) 
					{
						
						if (newData[i] == '$') {
							int wordLength = i - currStartPos;
							commands[index] = newData.Substring(currStartPos, wordLength);
							currStartPos = i+1;
							index++;
						} else if (i == RightCurlyBracket-1) {
							int wordLength = i - currStartPos + 1;
							commands[index] = newData.Substring(currStartPos, wordLength);
						}
					}
				}
				else
				{
					print("String not formatted properly.");
					// String not formated propperly
				}
			
				
				// Process the commands according to their Source application generated this command
				// ART Application Command
				if(applicationCode == "0")
				{
						// For each command in the list from the form, an action must be performed
					foreach (string s in commands) 
					{
						print ("From ART: "+s);
						switch (s)
						{
						case "Shutdown":
							print("Shutdown");
							try 
							{
								Application.Quit();
							} 
							catch (Exception e) 
							{
								Debug.Log("Error When Application.Quit() Called..... " + e.ToString());
							}
							break;
						case "LeftOnLeft":
							print("LeftOnLeft");
							if (leftPlane.renderer.enabled == false) {
								leftPlane.renderer.enabled = true;
							}
							mainCam.GetComponent<Plugin_Data>().plane_layout.left_plane = 1;
							break;
							
						case "RightOnLeft":
							print("RightOnLeft");
							if (leftPlane.renderer.enabled == false) {
								leftPlane.renderer.enabled = true;
							}
							mainCam.GetComponent<Plugin_Data>().plane_layout.left_plane = -1;
							break;
							
						case "NothingOnLeft":
							print("NothingOnLeft");
							mainCam.GetComponent<Plugin_Data>().plane_layout.left_plane = 0;
							break;
							
						case "LeftMirror":
							print("LeftMirror");
							float tempX_3 = leftPlane.transform.localScale.x;
							leftPlane.transform.localScale = new Vector3(tempX_3 * -1, leftPlane.transform.localScale.y, leftPlane.transform.localScale.z);
							break;
							
						case "LeftUnMirror":
							print("LeftUnMirror");
							float tempX_4 = leftPlane.transform.localScale.x;
							leftPlane.transform.localScale = new Vector3(tempX_4 * -1, leftPlane.transform.localScale.y, leftPlane.transform.localScale.z);
							break;
							
						case "RightOnRight":
							print("RightOnRight");
							if (rightPlane.renderer.enabled == false) {
								rightPlane.renderer.enabled = true;
							}
							mainCam.GetComponent<Plugin_Data>().plane_layout.right_plane = 1;
							break;
							
						case "LeftOnRight":
							print("LeftOnRight");
							if (rightPlane.renderer.enabled == false) {
								rightPlane.renderer.enabled = true;
							}
							mainCam.GetComponent<Plugin_Data>().plane_layout.right_plane = -1;
							break;
							
						case "NothingOnRight":
							print("NothingOnRight");
							mainCam.GetComponent<Plugin_Data>().plane_layout.right_plane = 0;
							break;
							
						case "RightMirror":
							print("RightMirror");
							float tempX_1 = rightPlane.transform.localScale.x;
							rightPlane.transform.localScale = new Vector3(tempX_1 * -1, rightPlane.transform.localScale.y, rightPlane.transform.localScale.z);
							break;
						case "RightUnMirror":
							print("RightUnMirror");
							float tempX_2 = rightPlane.transform.localScale.x;
							rightPlane.transform.localScale = new Vector3(tempX_2 * -1, rightPlane.transform.localScale.y, rightPlane.transform.localScale.z);
							break;
						case "TheraMem":
							print ("Command: TheraMem");
							clientConnected = false;
							Application.LoadLevel(1); //  Load TheraMem scene
							break;
						}
					}
				}// TheraMem Form Commands
				else if(applicationCode == "1")
				{
					currentGame = "TheraMem";
					
					foreach (string s in commands) 
					{
						print ("From TheraMem: "+s);
						switch (s)
						{
						case "Shutdown":
							print("Shutdown");
							try 
							{
								Application.Quit();
							} 
							catch (Exception e) 
							{
								Debug.Log("Error When Application.Quit() Called..... " + e.ToString());
							}
							break;
						case "PauseGame":
							Time.timeScale = 0;
							break;
						case "ResumeGame":
							Time.timeScale = 1;
							break;
						case "TheraMem_Stop":
							print ("Stopping TheraMem");
							mainCam.GetComponent<TheraMem>().enabled = false;
							break;
						case "TheraMemMemory":
							print("Starting TheraMem (Memory Version)");
							this.SetGameMode = "Memory";
							mainCam.GetComponent<TheraMem>().enabled = true;
							Time.timeScale = 0;
							break;
						case "TheraMemOriginal":
							print("Starting TheraMem (Original Version)");
							this.SetGameMode = "Original";
							mainCam.GetComponent<TheraMem>().enabled = true;
							Time.timeScale = 0;
							break;
						case "TheraMemOneHanded_left":
							print("Starting TheraMem (One-Handed Version (left))");
							this.SetGameMode = "OneHanded";
							this.SetOneHandedModeSide = "left";
							mainCam.GetComponent<TheraMem>().enabled = true;
							Time.timeScale = 0;
							break;
						case "TheraMemOneHanded_right":
							print("Starting TheraMem (One-Handed Version (right))");
							this.SetGameMode = "OneHanded";
							this.SetOneHandedModeSide = "right";
							mainCam.GetComponent<TheraMem>().enabled = true;
							Time.timeScale = 0;
							break;
						case "RestartTheraMemMemory":
							print("Restarting");
							mainCam.GetComponent<TheraMem>().enabled = false;
							Thread.Sleep(50);
							this.SetGameMode = "Memory";
							mainCam.GetComponent<TheraMem>().enabled = true;
							Time.timeScale = 0;
							break;
						case "RestartTheraMemOriginal":
							print("Restarting");
							mainCam.GetComponent<TheraMem>().enabled = false;
							Thread.Sleep(50);
							this.SetGameMode = "Original";
							mainCam.GetComponent<TheraMem>().enabled = true;
							Time.timeScale = 0;
							break;
						case "RestartTheraMemOneHanded_left":
							print("Restarting");
							mainCam.GetComponent<TheraMem>().enabled = false;
							Thread.Sleep(50);
							this.SetGameMode = "OneHanded";
							this.SetOneHandedModeSide = "left";
							mainCam.GetComponent<TheraMem>().enabled = true;
							Time.timeScale = 0;
							break;
						case "RestartTheraMemOneHanded_right":
							print("Restarting");
							mainCam.GetComponent<TheraMem>().enabled = false;
							Thread.Sleep(50);
							this.SetGameMode = "OneHanded";
							this.SetOneHandedModeSide = "right";
							mainCam.GetComponent<TheraMem>().enabled = true;
							Time.timeScale = 0;
							break;
						case "Options":
							//System.Diagnostics.Process.Start(dataPath + "/ART_Application.exe","0");
							break;
						}
					}
				}// Profiles Form Commands
				else if(applicationCode == "2")
				{
					// Either these commands are not camera Parameters and will be less than 20
					if(commands.Length < 20 )
					{
						
						foreach (string s in commands) 
						{
					
							switch (s)
							{
							case "Shutdown":
								print("Shutdown");
								try 
								{
									
								} 
								catch (Exception e) 
								{
									Debug.Log("Error When Application.Quit() Called..... " + e.ToString());
								}
								break;
							}
						}
					}
					else
					{
					
					
					
					
					// Each Index of this Command array is a Camera Parameter Value 
					// Following are commands an their indices in commands Array
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
						
					}
					
					
				}
				else
				{
					print ("Application Code Invalid");
					// nothing yet
				}
			} 
			else 
			{
				// string is empty...do nothing while we wait for a command
			}
		}
	}
}