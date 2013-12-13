using UnityEngine;
using System.Collections;
using System;
using System.Threading;
// (1) Randomise models and render them for different subplanes in left and right 
// memory planes.
//(2) Gets the position of fingerTip from Plugin_Handler and use that to cast a ray from camera and 
//see where that collide to different sub planes for each left and right sides
//(3) Changes color,displays object models on left and right side planes

public class TheraMem : MonoBehaviour {
	
	private string left;
	private string right;
	private int numPlanes = 12;
	private int totalNumModels = 17;
	private GameObject[] leftMemoryPlanes;
	private GameObject[] rightMemoryPlanes;
	
	public GameObject[] leftModels;
	public GameObject[] rightModels;
	
	public ArrayList modelsMixedLeft;
	public ArrayList modelsMixedRight;
	
	private GameObject leftPlane;
	private GameObject rightPlane;
	
	GameObject timeDisplay;
	
	// Variables for OnEnable()
	private System.Random randomGenLeft;
	private System.Random randomGenRight;
	private System.Random randomGen;
	
	// Variables for OnDisable()
	private bool gameFinished = false;
	
	// variables for Update()
	public RaycastHit leftHitInfo;
	public RaycastHit rightHitInfo;
	
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
	
	float totalTime = 0.0f;
	
	const float reset = 0.0f;
	const float hoverTime = 1.0f;
	const float showTime = 1.2f;
	
	private int numberOfTries = 0;
	private int matchesMade = 0;
	
	private int planeIndex = 0;
	
	private GameObject finalText;
	
	// used for searching an array
	/*private bool contains(ArrayList array, GameObject this_obj) {
		foreach (GameObject go in array) {
			if (go == this_obj) {
				return true;
			}
		}
		
		return false;
	}*/
	
	// Toggles the renderer of the given GO off or on.
	// onOrOff will either be 0 or 1. 0 = off, 1 = on.
	private void toggleRenderer(GameObject go, int onOrOff) {
		
		if (go != null) {
			
			if (onOrOff == 0) {
				go.renderer.enabled = false;
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
				go.collider.enabled = false;
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
	
	void OnEnable() {
		
		///////////////////////////////////////////////////////////////////////
		Debug.Log("Starting to enable..");
		
		left = "LeftSide";
		right = "RightSide";
		
		leftPlane = GameObject.Find("LeftPlane");
		rightPlane = GameObject.Find("RightPlane");
		
		
		// two new arrays to hold all of the memory planes
		leftMemoryPlanes = new GameObject[numPlanes];
		rightMemoryPlanes = new GameObject[numPlanes];
		
		// These holds left and right sub planes
		GameObject tempLeft;
		GameObject tempRight;
		
		// Initialization of arrays which hold the mixed models.
		modelsMixedLeft = new ArrayList();
		modelsMixedRight = new ArrayList();
		
		Debug.Log("Assigning Planes...\n=====================");
		for (int i = 0; i < numPlanes; i++) {
			
			// putting the memory planes in an array for ease of use
			// only needs to be done once at the start of the App.
			int whichPlane = i + 1;
			tempLeft = GameObject.Find(left + whichPlane);
			tempLeft.renderer.enabled =  true;
			leftMemoryPlanes[i] = tempLeft;
			
			tempRight = GameObject.Find(right + whichPlane);
			tempRight.renderer.enabled =  true;
			rightMemoryPlanes[i] = tempRight;
			
			//Debug.Log("Left Plane: " + tempLeft.name + "\tRight Plane: " + tempRight.name);
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
		
		// to temprarily hold ecah model as it is processed
		GameObject tempModelLeft;
		GameObject tempModelRight;
		
		
		Debug.Log("Assgning Models to temporary array...\n===============================");
		
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
		
		
		
		/*for (int i = 0; i < totalNumModels; i++) {
			copyOfLeft.Add(leftModels[i]);
			copyOfRight.Add(rightModels[i]);
			
			//Debug.Log("Left Model Added: " + leftModels[i].name + "\tRight Model Added: " + rightModels[i].name);
		}*/
		
		// Creating the mixed array of models. Left and Right should be
		// different. 
		// This should also be done each time the game is started.
		
		int index = 0;
		
		//Debug.Log("Before Main While....");
		
		while (copyOfLeft.Count > 0) {
		
			int leftIndex = randomGenLeft.Next(copyOfLeft.Count);
			int rightIndex = randomGenRight.Next(copyOfRight.Count);
			
			tempModelLeft = (GameObject) copyOfLeft[leftIndex];
			tempModelRight = (GameObject) copyOfRight[rightIndex];
			
			//Debug.Log("tempModelLeft: " + tempModelLeft.name + "    tempModelRight: " + tempModelRight.name);
			
			//Debug.Log("The rendered Object (object1):    " + GameObject.FindGameObjectWithTag(tempModelLeft.name+"Left").name);
			
			toggleRenderer(GameObject.FindGameObjectWithTag(tempModelLeft.name+"Left"), 0);
			toggleRenderer(GameObject.FindGameObjectWithTag(tempModelRight.name+"Right"), 0);
			
			Vector3 origin = new Vector3(0.0f,0.0f,0.0f);
			
			Vector3 placeToBe_left = leftMemoryPlanes[index].transform.position;
			Vector3 placeToBe_right = rightMemoryPlanes[index].transform.position;
			
			tempModelLeft.transform.position =  (origin + placeToBe_left) + new Vector3(0.0f, 1.0f, 0.0f);
			tempModelRight.transform.position = (origin + placeToBe_right) + new Vector3(0.0f, 1.0f, 0.0f);
			
			//GameObject.FindGameObjectWithTag(tempModelLeft.name+"Left").transform.position =  (/*origin +*/ placeToBe_left) + new Vector3(0.0f, 1.0f, 0.0f);
			//GameObject.FindGameObjectWithTag(tempModelRight.name+"Right").transform.position = (/*origin +*/ placeToBe_right) + new Vector3(0.0f, 1.0f, 0.0f);
			
			//Debug.Log("LEFT - Position of assigned plane: " + placeToBe_left);
			//Debug.Log("LEFT - Position of GO: " + ((origin + placeToBe_left) + new Vector3(0.0f, 5.0f, 0.0f)));
			
			//Debug.Log("RIGHT - Position of assigned plane: " + placeToBe_right);
			//Debug.Log("RIGHT - Position of GO: " + ((origin + placeToBe_right) + new Vector3(0.0f, 5.0f, 0.0f)));
			
			modelsMixedLeft.Add(tempModelLeft);
			copyOfLeft.RemoveAt(leftIndex);
			
			modelsMixedRight.Add(tempModelRight);
			copyOfRight.RemoveAt(rightIndex);
			
			int counter = 0;
			foreach (GameObject model in modelsMixedLeft) {
				GameObject tempModel = (GameObject) modelsMixedRight[counter];
				//Debug.Log("Left Model " + (counter+1) + ": " + model.name );
				//Debug.Log("Right Model " + (counter+1) + ": " + tempModel.name);
				counter++;
			}
			
			//print("We want to start the game!");
			toggleRenderer(leftMemoryPlanes[index], 1);
			toggleCollider(leftMemoryPlanes[index], 1);
			
			toggleRenderer(rightMemoryPlanes[index], 1);
			toggleCollider(rightMemoryPlanes[index], 1);
			index++;
		}
		
		timeDisplay = GameObject.Find("TimeDisplay");
		timeDisplay.guiText.fontSize = 18;
		timeDisplay.guiText.enabled = true;
		
		finalText = GameObject.Find("EndDisplay");
		gameFinished = false;
		
		this.SetGameMode = Camera.main.GetComponent<ConnectToTheraMemDialog>().SetGameMode;

		Debug.Log("End Of Enable with Game Mode Set To: " + this.SetGameMode);
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = 1;
		if (!gameFinished) {
		
			float timeTaken = Time.deltaTime;
			totalTime += timeTaken;
			
			timeDisplay.guiText.text = "Time: " + totalTime.ToString("f1") + "        Number Of Tries: " + numberOfTries;
			
			Vector3 leftPointForRay = leftPlane.GetComponent<Sort_Space>().sphere_screen_pos[0];
			Vector3 rightPointForRay = rightPlane.GetComponent<Sort_Space>().sphere_screen_pos[0];
			
			Ray leftRay = Camera.main.ScreenPointToRay(leftPointForRay);
			Ray rightRay = Camera.main.ScreenPointToRay(rightPointForRay);
			
			distance = Mathf.Infinity;
			
			// NEW IMPLEMENTATION TO GO HERE 
			// IMPLEMENTATION ORIGINAL
			if (gameMode == "Original") {
			
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
									toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 1);
									
									leftPlaneChecked = planeIndex;
									
								}
								
							} else {
								elapsedTimeLeft = reset;
								leftShowing = false;
								if (leftPlaneChecked != -1) {
									GameObject tempModelHolder = (GameObject) modelsMixedLeft[leftPlaneChecked];
									if (GameObject.FindGameObjectWithTag(tempModelHolder.name+"Left").renderer.enabled == true) {
										toggleRenderer(GameObject.FindGameObjectWithTag(tempModelHolder.name+"Left"), 0);
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
							if (GameObject.FindGameObjectWithTag(tempModelHolder.name+"Left").renderer.enabled == true) {
								toggleRenderer(GameObject.FindGameObjectWithTag(tempModelHolder.name+"Left"), 0);
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
									toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 1);
									
									rightPlaneChecked = planeIndex;
								}
								
							} else {
								elapsedTimeRight = reset;
								rightShowing = false;
								if (rightPlaneChecked != -1) {
									GameObject tempModelHolder = (GameObject) modelsMixedRight[rightPlaneChecked];
									if (GameObject.FindGameObjectWithTag(tempModelHolder.name+"Right").renderer.enabled == true) {
										toggleRenderer(GameObject.FindGameObjectWithTag(tempModelHolder.name+"Right"), 0);
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
							if (GameObject.FindGameObjectWithTag(tempModelHolder.name+"Right").renderer.enabled == true) {
								toggleRenderer(GameObject.FindGameObjectWithTag(tempModelHolder.name+"Right"), 0);
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
							
							toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 0);
							toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 0);
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
									toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 1);
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
									
									toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 1);
									
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
							
							toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 0);
							toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 0);
							
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
							
							toggleRenderer(GameObject.FindGameObjectWithTag(leftModelShowing.name+"Left"), 0);
							toggleRenderer(GameObject.FindGameObjectWithTag(rightModelShowing.name+"Right"), 0);
							
							GameObject.Find(currentLeft).renderer.material.color = Color.white;
							GameObject.Find(currentRight).renderer.material.color = Color.white;
							
						}	
					}
				}
				
			////////////////////////////////////END OF GAME LOGIC FOR MEMORY VERSION////////////////////////////
			
			}
		}
		
		if (matchesMade == numPlanes) {
			gameFinished = true;
			string toShow = "CONGRATULATIONS! \nYou finished the game with a time of: " + totalTime.ToString("f1") 
				+ "\nYour number of tries: " + numberOfTries;
			finalText.guiText.fontSize = 30;
			finalText.guiText.text = toShow;
		}
	}
	
	void OnDisable() {
		
		if (!gameFinished) {
			for (int i = 0; i < numPlanes; i++) {
				
				if (leftMemoryPlanes[i] != null) {
					leftMemoryPlanes[i].renderer.enabled = false;
				} else {
					print("DISABLE: Left object is null..");
				}
				if (rightMemoryPlanes[i] != null) {
					rightMemoryPlanes[i].renderer.enabled = false;
				} else {
					print("DISABLE: Right object is null..");
				}
				
			}
		}
		
		if (finalText != null) {
			if (finalText.active == true) {
				finalText.guiText.text = "";
			}
		}
		
		if (timeDisplay != null) {
			timeDisplay.guiText.enabled = false;
		}
		
		resetVariables();
		
		modelsMixedLeft.RemoveRange(0, modelsMixedLeft.Count - 1);
		modelsMixedRight.RemoveRange(0, modelsMixedLeft.Count - 1);
		
	}
	
	void resetVariables() {
		this.totalTime = 0.0f;
		numberOfTries = 0;
		gameFinished = false;
		matchesMade = 0;
	}
}


