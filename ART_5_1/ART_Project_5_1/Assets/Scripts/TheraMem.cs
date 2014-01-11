using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class TheraMem : MonoBehaviour {
	
	private string left;
	private string right;
	private int numPlanes = 12;
	private int totalNumModels;
	private GameObject[] leftMemoryPlanes;
	private GameObject[] rightMemoryPlanes;
	
	public GameObject[] leftModels;
	public GameObject[] rightModels;
	
	public ArrayList modelsMixedLeft;
	public ArrayList modelsMixedRight;
	
	private GameObject leftPlane;
	private GameObject rightPlane;
	
	GameObject timeDisplay;
	GameObject scoreDisplay;
	GameObject finalDisplay;
	
	// Variables for OnEnable()
	private System.Random randomGenLeft;
	private System.Random randomGenRight;
	private System.Random randomGen;
	
	// Variables for OnDisable()
	private bool gameFinished = false;
	
	// variables for Update()
	public RaycastHit leftHitInfo;
	public RaycastHit rightHitInfo;
	public RaycastHit leftHitInfo2; // for one handed mode
	public RaycastHit rightHitInfo2; // for one handed mode
	
	private float distance;
	
	private float elapsedTimeLeft = 0.0f;
	private float elapsedTimeRight = 0.0f;
	
	private float elapsedTimeLocked = 0.0f;
	
	private GameObject leftModelShowing;
	private GameObject rightModelShowing;
	
	string prevLeft = "";
	string currentLeft = "";
	string prevRight = "";
	string currentRight = "";
	
	int leftPlaneChecked = -1;
	int rightPlaneChecked = -1;
	
	bool lockLeft = false;
	bool lockRight = false;
	
	bool leftShowing = false;
	bool rightShowing = false;
	
	bool collisionOccurLeft = false;
	bool collisionOccurRight = false;
	
	private string gameMode;
	private string oneHandedMode_side;
	
	float totalTime = 0.0f;
	
	const float reset = 0.0f;
	const float hoverTime = 1.0f;
	const float showTime = 1.2f;
	
	private int numberOfTries = 0;
	private int matchesMade = 0;
	
	private int planeIndex = 0;
	public Camera mainCam;
	
	// Toggles the renderer of the given GO off or on.
	// onOrOff will either be 0 or 1. 0 = off, 1 = on.
	private void toggleRenderer(GameObject go, int onOrOff) {
		
		if (go != null) {
			
			if (onOrOff == 0) {
				if (go.renderer)
				{
					go.renderer.enabled = false;
				}
			} else {
				go.renderer.enabled = true;
			}
		} else {
			print("ToggleRenderer: GO is NULL..");
			// go is null
		}
	}
	
	// Toggles the collider of the given GO off or on.
	// onOrOff will either be 0 or 1. 0 = off, 1 = on.
	private void toggleCollider(GameObject go, int onOrOff) {
		if (go != null) {
			
			if (onOrOff == 0) {
				if (go.collider)
				{
					go.collider.enabled = false;
				}
			} else {
				go.collider.enabled = true;
			}
		} else {
			print("ToggleCollider: GO is NULL..");
			// go is null
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
	
	public string SetOneHandedMode_side 
	{
		get
		{
			return this.oneHandedMode_side;
		}
		set
		{
			this.oneHandedMode_side = value;
		}
	}
	
	void OnEnable() {
		
		//totalTime = 0.0f;
		//Time.timeScale = 0.0f;
		
		left = "LeftSide";
		right = "RightSide";
		
		totalNumModels = leftModels.Length;
		
		leftPlane = GameObject.Find("LeftPlane");
		rightPlane = GameObject.Find("RightPlane");
		
		// two new arrays to hold all of the memory planes
		leftMemoryPlanes = new GameObject[numPlanes];
		rightMemoryPlanes = new GameObject[numPlanes];
		
		GameObject tempLeft;
		GameObject tempRight;
		
		// Initialization of arrays which hold the mixed models.
		modelsMixedLeft = new ArrayList();
		modelsMixedRight = new ArrayList();
		
		this.SetGameMode = mainCam.GetComponent<CommandHandler>().SetGameMode;
		this.SetOneHandedMode_side = mainCam.GetComponent<CommandHandler>().SetOneHandedModeSide;
		//this.SetGameMode = "OneHanded";
		//this.SetOneHandedMode_side = "left";
		
		Debug.Log("Initializing TheraMem in " + this.SetGameMode + " game mode.");
			
		for (int i = 0; i < numPlanes; i++) {
			
			// putting the memory planes in an array for ease of use
			// only needs to be done once at the start of the App.
			if (this.gameMode == "OneHanded")
			{
				if (oneHandedMode_side == "left")
				{
					int whichPlane = i + 1;
					tempLeft = GameObject.Find(left + whichPlane);
					tempLeft.renderer.enabled =  true;
					leftMemoryPlanes[i] = tempLeft;
				}
				else if (oneHandedMode_side == "right")
				{
					int whichPlane = i + 1;
					tempRight = GameObject.Find(right + whichPlane);
					tempRight.renderer.enabled =  true;
					rightMemoryPlanes[i] = tempRight;
				}
				else
				{
					// should never reach..
				}
			}
			else
			{
				int whichPlane = i + 1;
				tempLeft = GameObject.Find(left + whichPlane);
				tempLeft.renderer.enabled =  true;
				leftMemoryPlanes[i] = tempLeft;
				
				tempRight = GameObject.Find(right + whichPlane);
				tempRight.renderer.enabled =  true;
				rightMemoryPlanes[i] = tempRight;
			}
			
		}
		
		// The random number generators. One for each side. If you use 
		// the same one it produces identical results.
		randomGenLeft = new System.Random((int) DateTime.Now.Ticks & 0x0000FFFF);
		Thread.Sleep(20);
		randomGenRight = new System.Random((int) DateTime.Now.Ticks & 0x0000FFFF);
		Thread.Sleep(20);
		randomGen = new System.Random((int) DateTime.Now.Ticks & 0x0000FFFF);
		
		////////////////////////////////////////////////////////////////////////////
		
		int[] modelsAlreadyIncluded = new int[numPlanes];
		
		// leftModels and rightModels are set on startup of script
		// and this is just a copy of those arrays. I empty them
		// as I fill the mixed arrays. 
		ArrayList copyOfLeft = new ArrayList();
		ArrayList copyOfRight = new ArrayList();
		
		int randIndex = 0;
		int modelCountIndex = 0;
		bool included = false;
		
		while (copyOfLeft.Count < numPlanes) {
			randIndex = randomGen.Next(totalNumModels);
			
			foreach (int i in modelsAlreadyIncluded) {
				
				if (randIndex == i) {
					included = true;
				}
			}
			
			if (!included) {
				modelsAlreadyIncluded[modelCountIndex] = randIndex;
				modelCountIndex++;
				
				copyOfLeft.Add(leftModels[randIndex]);
				copyOfRight.Add(rightModels[randIndex]);
			}
			
			included = false;
		}
		
		if (gameMode == "OneHanded") // one sided TheraMem
		{
			// Creating the left models array with pairs of models (also randomized)
			int index = 0;
			
			GameObject model;
			
			ArrayList models = new ArrayList();
			
			while (index < 12)
			{
				models.Add (copyOfLeft[index]);
				models.Add (copyOfRight[index]);
				index+=2;
			}
			
			index = 0;
			while (models.Count > 0) 
			{
				if (oneHandedMode_side == "left")
				{
					int rand_index = randomGenLeft.Next(models.Count);
					
					model = (GameObject) models[rand_index];
					model.GetComponentInChildren<MeshRenderer>().enabled = false;
					
					Vector3 origin = new Vector3(0.0f,0.0f,0.0f);
					
					Vector3 placeToBe = leftMemoryPlanes[index].transform.position;
					
					model.transform.position =  (origin + placeToBe) + new Vector3(0.0f, 1.0f, 0.0f);
					
					modelsMixedLeft.Add(model);
					models.RemoveAt(rand_index);
				}
				else if (oneHandedMode_side == "right") 
				{
					int rand_index = randomGenRight.Next(models.Count);
					
					model = (GameObject) models[rand_index];
					model.GetComponentInChildren<MeshRenderer>().enabled = false;
					
					Vector3 origin = new Vector3(0.0f,0.0f,0.0f);
					
					Vector3 placeToBe = rightMemoryPlanes[index].transform.position;
					
					model.transform.position =  (origin + placeToBe) + new Vector3(0.0f, 1.0f, 0.0f);
					
					modelsMixedRight.Add(model);
					models.RemoveAt(rand_index);
				}
				else 
				{
					// nothing
				}
				
				index++;
			}
		}
		else // Two-sided TheraMem (2 Versions)
		{
			
			// Creating the mixed array of models. Left and Right should be different. 
			// This should also be done each time the game is started.
			
			// to temprarily hold ecah model as it is processed
			GameObject tempModelLeft;
			GameObject tempModelRight;
			
			int index = 0;
			
			while (copyOfLeft.Count > 0) {
			
				int leftIndex = randomGenLeft.Next(copyOfLeft.Count);
				int rightIndex = randomGenRight.Next(copyOfRight.Count);
				
				tempModelLeft = (GameObject) copyOfLeft[leftIndex];
				tempModelRight = (GameObject) copyOfRight[rightIndex];
				
				tempModelLeft.GetComponentInChildren<MeshRenderer>().enabled = false;
				tempModelRight.GetComponentInChildren<MeshRenderer>().enabled = false;
//				toggleRenderer(GameObject.FindGameObjectWithTag(tempModelLeft.name+"Left"), 0);
//				toggleRenderer(GameObject.FindGameObjectWithTag(tempModelRight.name+"Right"), 0);
				
				Vector3 origin = new Vector3(0.0f,0.0f,0.0f);
				
				Vector3 placeToBe_left = leftMemoryPlanes[index].transform.position;
				Vector3 placeToBe_right = rightMemoryPlanes[index].transform.position;
				
				tempModelLeft.transform.position =  (origin + placeToBe_left) + new Vector3(0.0f, 1.0f, 0.0f);
				tempModelRight.transform.position = (origin + placeToBe_right) + new Vector3(0.0f, 1.0f, 0.0f);
				
				modelsMixedLeft.Add(tempModelLeft);
				copyOfLeft.RemoveAt(leftIndex);
				
				modelsMixedRight.Add(tempModelRight);
				copyOfRight.RemoveAt(rightIndex);
				
				toggleRenderer(leftMemoryPlanes[index], 1);
				toggleCollider(leftMemoryPlanes[index], 1);
				
				toggleRenderer(rightMemoryPlanes[index], 1);
				toggleCollider(rightMemoryPlanes[index], 1);
				index++;
			}
		}
		
		timeDisplay = GameObject.Find("Timer");
		scoreDisplay = GameObject.Find("NumTries");
		finalDisplay = GameObject.Find("Final_Text");
		
		gameFinished = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!gameFinished) {
		
			float timeTaken = Time.deltaTime;
			totalTime += timeTaken;
			
			timeDisplay.guiText.text = "Time: " + totalTime.ToString("f1");
			scoreDisplay.guiText.text = "Tries: " + numberOfTries;
			
			Vector3 leftPointForRay = leftPlane.GetComponent<Coord_Handler>().screen_pos[0];
			Vector3 rightPointForRay = rightPlane.GetComponent<Coord_Handler>().screen_pos[0];
			Vector3 leftPointForRay2 = leftPlane.GetComponent<Coord_Handler>().screen_pos[0]; // for the one-sided
			Vector3 rightPointForRay2 = rightPlane.GetComponent<Coord_Handler>().screen_pos[0]; // for the one-sided
			
			Ray leftRay = mainCam.ScreenPointToRay(leftPointForRay);
			Ray rightRay = mainCam.ScreenPointToRay(rightPointForRay);
			Ray leftRay2 = mainCam.ScreenPointToRay(leftPointForRay2); // one-sided version
			Ray rightRay2 = mainCam.ScreenPointToRay(rightPointForRay2); // one-sided version
			
			distance = Mathf.Infinity;
			
			
			
			if (gameMode == "OneHanded") /////////////IMPLEMENTATION ONE-HANDED\\\\\\\\\\\\\\\\\\
			{
				
				GameObject[] memoryPlanes;
				Ray ray1, ray2;
				RaycastHit hitInfo1, hitInfo2;
				ArrayList modelsMixed;
				
				if (oneHandedMode_side == "left")
				{
					modelsMixed = modelsMixedLeft;
					memoryPlanes = leftMemoryPlanes;
					ray1 = leftRay;
					ray2 = leftRay2;
					hitInfo1 = leftHitInfo;
					hitInfo2 = leftHitInfo2;
				}
				else if (oneHandedMode_side == "right")
				{
					modelsMixed = modelsMixedRight;
					memoryPlanes = rightMemoryPlanes;
					ray1 = rightRay;
					ray2 = rightRay2;
					hitInfo1 = rightHitInfo;
					hitInfo2 = rightHitInfo2;
				}
				else 
				{
					modelsMixed = new ArrayList();
					memoryPlanes = null;
					ray1 = new Ray();
					ray2 = new Ray();
					// nothing
				}
				collisionOccurLeft = false;
				collisionOccurRight = false;
				
				if (!lockLeft)
				{
					planeIndex = 0;
					foreach (GameObject go in memoryPlanes) 
					{
						if (planeIndex != rightPlaneChecked)
						{
							if (go.collider.Raycast(ray1, out hitInfo1, distance)) 
							{
								collisionOccurLeft = true;
								go.renderer.material.color = Color.red;
								prevLeft = currentLeft;
								currentLeft = go.name;
								
								if (currentLeft == prevLeft) 
								{
									elapsedTimeLeft += timeTaken;
									
									if (elapsedTimeLeft > hoverTime) {
										
										elapsedTimeLeft = reset;
										lockLeft = true;
										
										go.renderer.material.color = Color.green;
										
										// show pictures
										leftModelShowing = (GameObject) modelsMixed[planeIndex];
										leftModelShowing.GetComponentInChildren<MeshRenderer>().enabled = true;
										
										leftPlaneChecked = planeIndex;
									}
								}
								else 
								{
									elapsedTimeLeft = reset;
								}
							}
							else
							{
								go.renderer.material.color = Color.white;
							}
						}
						planeIndex++;
					}
					if (!collisionOccurLeft) {
						elapsedTimeLeft = reset;
					}
				}
				
				if (!lockRight) {
					planeIndex = 0;
					foreach (GameObject go in memoryPlanes) {
						
						if (planeIndex != leftPlaneChecked) 
						{
							if (go.collider.Raycast(ray2, out hitInfo2, distance)) {
								
								collisionOccurRight = true;
								prevRight = currentRight;
								currentRight = go.name;
								go.renderer.material.color = Color.red;
								
								
								if (currentRight == prevRight) {
									elapsedTimeRight += timeTaken;
									
									if (elapsedTimeRight > hoverTime) {
										elapsedTimeRight = reset;
										lockRight = true;
										
										go.renderer.material.color = Color.green;
										
										// show pictures
										rightModelShowing = (GameObject) modelsMixed[planeIndex];
										rightModelShowing.GetComponentInChildren<MeshRenderer>().enabled = true;
										
										rightPlaneChecked = planeIndex;
										
									}
								} else {
									elapsedTimeRight = reset;
								}
							} else {
								go.renderer.material.color = Color.white;
							}
							
						}
						else
						{
							// Keep searching until you turn another tile..
						}
						planeIndex++;
					}
					
					if (!collisionOccurRight) {
						elapsedTimeRight = reset;
					}
				}
				
				if (lockLeft && lockRight) {
					elapsedTimeLocked += timeTaken;
					
					
					if (leftModelShowing.name == rightModelShowing.name) {
						
						foreach (GameObject go in memoryPlanes) {
							if (go.renderer.enabled == true) {
								go.renderer.material.color = Color.cyan;
							}
						}
						
						if (elapsedTimeLocked >= showTime) {
							
							elapsedTimeLocked = reset;
							numberOfTries++;
							matchesMade+=2;
							lockLeft = false;
							lockRight = false;
							
							leftModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
							rightModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
							
							toggleRenderer(GameObject.Find(currentLeft), 0);
							toggleRenderer(GameObject.Find(currentRight), 0);
							toggleCollider(GameObject.Find(currentLeft), 0);
							toggleCollider(GameObject.Find(currentRight), 0);
							
							foreach (GameObject go in memoryPlanes) {
								if (go.renderer.enabled == true) {
									go.renderer.material.color = Color.white;
								}
							}
						}
						
					} else {
						
						if (elapsedTimeLocked >= showTime) {
							
							elapsedTimeLocked = reset;
							numberOfTries++;
							lockLeft = false;
							lockRight = false;
							leftPlaneChecked = -1;
							
							leftModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
							rightModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
							
							GameObject.Find(currentLeft).renderer.material.color = Color.white;
							GameObject.Find(currentRight).renderer.material.color = Color.white;
							
						}	
					}
				}
			}
			else if (gameMode == "Original") { /////////////IMPLEMENTATION ORIGINAL\\\\\\\\\\\\\\\\\\
			
				collisionOccurLeft = false;
				
				if (!lockLeft) {
					planeIndex = 0;
					foreach (GameObject go in leftMemoryPlanes) {
						
						if (go.collider.Raycast(leftRay, out leftHitInfo, distance)) {
							
							collisionOccurLeft = true;
							go.renderer.material.color = Color.red;
							prevLeft = currentLeft;
							currentLeft = go.name;
							
							if (prevLeft == currentLeft) {
								
								elapsedTimeLeft += timeTaken;
								
								if (elapsedTimeLeft > hoverTime) {
									
									go.renderer.material.color = Color.green;
									leftShowing = true;
									
									leftModelShowing = (GameObject) modelsMixedLeft[planeIndex];
									leftModelShowing.GetComponentInChildren<MeshRenderer>().enabled = true;
//									toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 1);
									
									leftPlaneChecked = planeIndex;
									
								}
								
							} else {
								elapsedTimeLeft = reset;
								leftShowing = false;
								if (leftPlaneChecked != -1) {
									GameObject tempModelHolder = (GameObject) modelsMixedLeft[leftPlaneChecked];
									if (tempModelHolder.GetComponentInChildren<MeshRenderer>().enabled == true) {
										tempModelHolder.GetComponentInChildren<MeshRenderer>().enabled = false;
										//toggleRenderer(GameObject.FindGameObjectWithTag(tempModelHolder.name+"Left"), 0);
										numberOfTries++;
									}
								}
							}
							
						} else {
							if (go.renderer.material.color != Color.white) {
								go.renderer.material.color = Color.white;
							}
						}
						planeIndex++;
					} // foreach
					
					if (!collisionOccurLeft) {
						elapsedTimeLeft = reset;
						leftShowing = false;
						if (leftPlaneChecked != -1) {
							GameObject tempModelHolder = (GameObject) modelsMixedLeft[leftPlaneChecked];
							if (tempModelHolder.GetComponentInChildren<MeshRenderer>().enabled == true) {
								tempModelHolder.GetComponentInChildren<MeshRenderer>().enabled = false;
								//toggleRenderer(GameObject.FindGameObjectWithTag(tempModelHolder.name+"Left"), 0);
								numberOfTries++;
							}
						}
					}
				}
				
				collisionOccurRight = false;
				
				if (!lockRight) {
				
					planeIndex = 0;
					foreach (GameObject go in rightMemoryPlanes) {
						
						if (go.collider.Raycast(rightRay, out rightHitInfo, distance)) {
							
							collisionOccurRight = true;
							prevRight = currentRight;
							currentRight = rightHitInfo.collider.name;
							go.renderer.material.color = Color.red;
							
							if (prevRight == currentRight) {
								
								elapsedTimeRight += timeTaken;
								
								if (elapsedTimeRight > hoverTime) {
									go.renderer.material.color = Color.green;
									rightShowing = true;
										
									rightModelShowing = (GameObject) modelsMixedRight[planeIndex];
									rightModelShowing.GetComponentInChildren<MeshRenderer>().enabled = true;
									//toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 1);
									
									rightPlaneChecked = planeIndex;
								}
								
							} else {
								elapsedTimeRight = reset;
								rightShowing = false;
								if (rightPlaneChecked != -1) {
									GameObject tempModelHolder = (GameObject) modelsMixedRight[rightPlaneChecked];
									if (tempModelHolder.GetComponentInChildren<MeshRenderer>().enabled == true) {
										tempModelHolder.GetComponentInChildren<MeshRenderer>().enabled = false;
										//toggleRenderer(GameObject.FindGameObjectWithTag(tempModelHolder.name+"Right"), 0);
										numberOfTries++;
									}
								}
							}
							
						} else {
							if (go.renderer.material.color != Color.white) {
								go.renderer.material.color = Color.white;
							}
						}
						planeIndex++;
					} // foreach
					
					if (!collisionOccurRight) {
						elapsedTimeRight = reset;
						rightShowing = false;
						if (rightPlaneChecked != -1) {
							GameObject tempModelHolder = (GameObject) modelsMixedRight[rightPlaneChecked];
							if (tempModelHolder.GetComponentInChildren<MeshRenderer>().enabled == true) {
								tempModelHolder.GetComponentInChildren<MeshRenderer>().enabled = false;
								//toggleRenderer(GameObject.FindGameObjectWithTag(tempModelHolder.name+"Right"), 0);
								numberOfTries++;
							}
						}
					}
					
				} //if 
				if (leftShowing && rightShowing) {
					
					if (leftModelShowing.name == rightModelShowing.name) {
						
						
						elapsedTimeLocked += timeTaken;
						
						lockLeft = true;
						lockRight = true;
							
						foreach (GameObject go in leftMemoryPlanes) {
							go.renderer.material.color = Color.cyan;
						}
						
						foreach (GameObject go in rightMemoryPlanes) {
							go.renderer.material.color = Color.cyan;
						}
						
						if (elapsedTimeLocked > showTime) {
							
							leftModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
							rightModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
//							toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 0);
//							toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 0);
							numberOfTries++;
							
							toggleRenderer(GameObject.Find(currentLeft), 0);
							toggleRenderer(GameObject.Find(currentRight), 0);
							toggleCollider(GameObject.Find(currentLeft), 0);
							toggleCollider(GameObject.Find(currentRight), 0);
							
							leftShowing = false;
							rightShowing = false;
							lockLeft = false;
							lockRight = false;
							
							elapsedTimeLocked = reset;
							
							elapsedTimeLeft = reset;
							elapsedTimeRight = reset;
							
							matchesMade++;
							
						}
						
					}
				} // if
			} // if (Original)
			
			////////////////////////////////GAME LOGIC FOR MEMORY VERSION//////////////////////////////
			
			else if (this.gameMode == "Memory") {
				
				collisionOccurLeft = false;
				
				if (!lockLeft) {
					
		            planeIndex = 0;
					foreach (GameObject go in leftMemoryPlanes) {
						
						if (go.collider.Raycast(leftRay, out leftHitInfo, distance)) {
							
							collisionOccurLeft = true;
							go.renderer.material.color = Color.red;
							prevLeft = currentLeft;
							currentLeft = go.name;
							
							if (currentLeft == prevLeft) {
								elapsedTimeLeft += timeTaken;
								
								if (elapsedTimeLeft > hoverTime) {
									
									elapsedTimeLeft = reset;
									lockLeft = true;
									
									go.renderer.material.color = Color.green;
									// show pictures
									
									leftModelShowing = (GameObject) modelsMixedLeft[planeIndex];
									leftModelShowing.GetComponentInChildren<MeshRenderer>().enabled = true;
									//toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 1);
								}
							} else {
								elapsedTimeLeft = reset;
							}
						} else {
							go.renderer.material.color = Color.white;
						}
						
						planeIndex++;
					}
					
					if (!collisionOccurLeft) {
						elapsedTimeLeft = reset;
					}
				}
				
				collisionOccurRight = false;
				
				if (!lockRight) {
					planeIndex = 0;
					foreach (GameObject go in rightMemoryPlanes) {
						
						if (go.collider.Raycast(rightRay, out rightHitInfo, distance)) {
							
							collisionOccurRight = true;
							prevRight = currentRight;
							currentRight = rightHitInfo.collider.name;
							go.renderer.material.color = Color.red;
							
							
							if (currentRight == prevRight) {
								elapsedTimeRight += timeTaken;
								
								if (elapsedTimeRight > hoverTime) {
									elapsedTimeRight = reset;
									lockRight = true;
									
									go.renderer.material.color = Color.green;
									// show pictures
									
									rightModelShowing = (GameObject) modelsMixedRight[planeIndex];
									
									rightModelShowing.GetComponentInChildren<MeshRenderer>().enabled = true;
									//toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 1);
									
								}
							} else {
								elapsedTimeRight = reset;
							}
						} else {
							go.renderer.material.color = Color.white;
						}
						planeIndex++;
					}
					
					if (!collisionOccurRight) {
						elapsedTimeRight = reset;
					}
				}
				
				if (lockLeft && lockRight) {
					elapsedTimeLocked += timeTaken;
					
					
					if (leftModelShowing.name == rightModelShowing.name) {
						
						foreach (GameObject go in leftMemoryPlanes) {
							if (go.renderer.enabled == true) {
								go.renderer.material.color = Color.cyan;
							}
						}
						
						foreach (GameObject go in rightMemoryPlanes) {
							if (go.renderer.enabled == true) {
								go.renderer.material.color = Color.cyan;
							}
						}
						
						if (elapsedTimeLocked >= showTime) {
							
							elapsedTimeLocked = reset;
							numberOfTries++;
							matchesMade++;
							lockLeft = false;
							lockRight = false;
							
							leftModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
							rightModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
//							toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 0);
//							toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 0);
							
							toggleRenderer(GameObject.Find(currentLeft), 0);
							toggleRenderer(GameObject.Find(currentRight), 0);
							toggleCollider(GameObject.Find(currentLeft), 0);
							toggleCollider(GameObject.Find(currentRight), 0);
							
							foreach (GameObject go in leftMemoryPlanes) {
								if (go.renderer.enabled == true) {
									go.renderer.material.color = Color.white;
								}
							}
							
							foreach (GameObject go in rightMemoryPlanes) {
								if (go.renderer.enabled == true) {
									go.renderer.material.color = Color.white;
								}
							}
						}
						
					} else {
						
						if (elapsedTimeLocked >= showTime) {
							
							elapsedTimeLocked = reset;
							numberOfTries++;
							lockLeft = false;
							lockRight = false;
							
							leftModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
							rightModelShowing.GetComponentInChildren<MeshRenderer>().enabled = false;
//							toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 0);
//							toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 0);
							
							GameObject.Find(currentLeft).renderer.material.color = Color.white;
							GameObject.Find(currentRight).renderer.material.color = Color.white;
							
						}	
					}
				}
				
			////////////////////////////////////END OF GAME LOGIC FOR MEMORY VERSION////////////////////////////
			
			}
		}
		
		if (matchesMade == numPlanes) {
			timeDisplay.guiText.text = "";
			scoreDisplay.guiText.text = "";
			
			gameFinished = true;
			string finalText = "Completed in " + totalTime.ToString("f1") + " and " + numberOfTries + " tries";
			finalDisplay.guiText.text = finalText;
		}
	}
	
	void renderMemoryPlanesOff()
	{
		bool left = true;
		bool right = true;
		
		if (gameMode == "OneHanded")
		{
			if (oneHandedMode_side == "left")
			{
				right = false;
			}
			else
			{
				left = false;
			}
		}
		if (left)
		{
			foreach (GameObject plane in leftMemoryPlanes)
			{
				if (plane.renderer)
				{
					plane.renderer.enabled = false;
				}
			}
		}
		
		if (right)
		{
			foreach (GameObject plane in rightMemoryPlanes)
			{
				if (plane.renderer)
				{
					plane.renderer.enabled = false;
				}
			}
		}
	}
	
	void OnDisable() {
		
		resetVariables();
		renderMemoryPlanesOff();
		if (gameMode == "OneHanded")
		{
			if (oneHandedMode_side == "left")
			{
				modelsMixedLeft.Clear();
			}
			else if (oneHandedMode_side == "right")
			{
				modelsMixedRight.Clear();
			}
			else
			{
				// nothing
			}
		}
		else
		{
			modelsMixedLeft.Clear();
			modelsMixedRight.Clear();
		}		
	}
	
	void resetVariables() {
		this.totalTime = 0.0f;
		numberOfTries = 0;
		gameFinished = false;
		matchesMade = 0;
	}
}


