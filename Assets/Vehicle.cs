using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {
	//ROAD LOCATIONS (for generating vehicles):
	//First/left horizontal road: (-10, 20) - (-10, -20)
		//Middle of intersection at (-10, 0)
	//Middle horizontal road: (0,20)-(0,-20)
		//Middle of intersection at (0,0)
	//Top/right horizontal road: (10,20)-(10,-20)
		//middle of intersection at (10,0)
	//Vertical road: (-20,0)-(20,0)
		//intersections: (-10,0) , (0,0) , (10,0)
	//Vehicles should be randomly generated at following points:
	//(-10,20): orientation should be towards lower part of screen
	//(0,-20): orientation should be towards upper part of screen
	//(10,20): orientation should be towards lower part of screen

	//Stopping points at intersections are 1.5 units from middle of intersection (in whichever direction car is coming from)
	//Catmull-Rom curve should be from this place to 

	public Vector3 target;
	int frameCount = 0;
	public float speed = 2f;
	Vector3 priorInt = new Vector3(0,0,0);
	bool moving = true;
	int intersectionPause = 0;
	public bool atIntersection = false;
	bool ignoreNextIntersection = false;
	bool replace = false;
	int numberOfCars = 11;
	int carNum;
	List<GameObject> carList = new List<GameObject> ();
	Vector3 previousPosition = new Vector3 (-1, -1, -1);
	int previousPositionCount = 0;

	/// <summary>
	/// Senses other cars close enough in front.
	/// </summary>
	/// <returns><c>true</c>, if cars are sensed, <c>false</c> otherwise.</returns>
	public string objectInWay;
	bool senseCars(){
		Vector3 pos = transform.position;
		foreach (GameObject car in carList) {
			if (car != null) {
				Vector3 heading = car.transform.position - pos;
				Vector3 standardizedHeading = heading / heading.magnitude;
				float distance = heading.magnitude;
			//	print ("Distance between car " + carNum + " and " + car.name + ": " + distance);
				if (distance < 1.5 && distance > 0 /*&& standardizedHeading == forward*/){
					/*if (Mathf.Abs (Vector3.Distance (pos, hor1Bot)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor1Top)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, verLeft)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, verRight)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor2Bot)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor2Top)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor3Bot)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor3Top)) < 1.5f) {
						return false;
					} else {
						return true;
					}*/
					
					if (standardizedHeading == forward) {
						clearPath = false;
						objectInWay = car.name;
					} else {
						objectInWay = "";
						clearPath = true;
					}
					return true;
					//print ("standardized heading: " + standardizedHeading + " , forward: " + forward);
				}
			}
		}
		return false;
	}

	void checkAhead(){
		Vector3 pos = transform.position;
		foreach (GameObject car in carList) {
			if (car != null) {
				Vector3 heading = pos - car.transform.position;
				Vector3 standardizedHeading = heading / heading.magnitude;
				float distance = heading.magnitude;
				if (distance < 1.5 && distance > 0 && standardizedHeading == forward){
					/*if (Mathf.Abs (Vector3.Distance (pos, hor1Bot)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor1Top)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, verLeft)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, verRight)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor2Bot)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor2Top)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor3Bot)) < 1.5f || Mathf.Abs (Vector3.Distance (pos, hor3Top)) < 1.5f) {
						return false;
					} else {
						return true;
					}*/
					clearPath = true;
					//print ("standardized heading: " + standardizedHeading + " , forward: " + forward);
					//print ("car " + carNum + " is approaching " + car.name + " at distance " + distance + " and heading " + heading + " , forward: " + forward );
				}
			}
		}
	}

	/// <summary>
	/// Turn at intersection.
	/// </summary>
	void Turn(){
		//catmull-rom curve interpolation from current point, which should be intersection centers +/- 1.5 to the next intersection
	}

	Vehicle generate(){
		Vehicle v = new Vehicle ();
		return v;
	}

	/// <summary>
	/// Chooses what to do at an intersection (turn or continue forwards)
	/// </summary>
	/// <returns>The decision.</returns>
	string IntersectionDecision(){
		var number = Random.Range (0, 3);//[x,y)
		string decision = "";
		switch (number) {
		case 0:
			decision = "straight";
			break;
		case 1:
			decision = "left";
			break;
		case 2:
			decision = "right";
			break;
		}
		return decision;
	}


	Vector3 hor1Bot = new Vector3 (-10, .15f, -20);
	Vector3 hor1Top = new Vector3 (-10, .15f, 20);
	Vector3 hor2Bot = new Vector3 (0, .15f, -20);
	Vector3 hor2Top = new Vector3 (0, .15f, 20);
	Vector3 hor3Bot = new Vector3 (10, .15f, -20);
	Vector3 hor3Top = new Vector3 (10, .15f, 20);
	Vector3 verLeft = new Vector3 (-20, .15f, 0);
	Vector3 verRight = new Vector3 (20, .15f, 0);
	Vector3 intLeft = new Vector3 (-10, 0.15f, 0);
	Vector3 intMid = new Vector3 (0, 0.15f, 0);
	Vector3 intRight = new Vector3 (10, 0.15f, 0);
	static Vector3 intLeftAbove = new Vector3 (-10, 0.15f, 1.5f);
	static Vector3 intLeftLeft = new Vector3 (-11.5f, 0.15f, 0);
	static Vector3 intLeftRight = new Vector3 (-8.5f, 0.15f, 0);
	static Vector3 intLeftBelow = new Vector3(-10,0.15f,-1.5f);
	static Vector3 intMidAbove = new Vector3 (0, .15f, 1.5f);
	static Vector3 intMidLeft = new Vector3 (-1.5f, 0.15f, 0);
	static Vector3 intMidRight = new Vector3 (1.5f, 0.15f, 0);
	static Vector3 intMidBelow = new Vector3 (0, .15f, -1.5f);
	static Vector3 intRightAbove = new Vector3 (10, 0.15f, 1.5f);
	static Vector3 intRightLeft = new Vector3 (8.5f, 0.15f, 0f);
	static Vector3 intRightRight = new Vector3 (11.5f, 0.15f, 0f);
	static Vector3 intRightBelow = new Vector3 (10, 0.15f, -1.5f);
	Vector3[] nearInts = new Vector3[12] {
		intLeftAbove,
		intLeftBelow,
		intLeftLeft,
		intLeftRight,
		intMidAbove,
		intMidBelow,
		intMidLeft,
		intMidRight,
		intRightAbove,
		intRightBelow,
		intRightLeft,
		intRightRight
	};
	public Vector3 forward = new Vector3(-1,-1,-1);

	void AssignTarget(){
		Vector3 pos = transform.position;
		if (pos == hor1Top || pos == hor1Bot) {
			target = intLeft; //Intersection: First and Center
			forward = new Vector3(0,0,-1);
		} else if (pos == hor2Bot || pos == hor2Top) {
			target = intMid; //Intersection: Second and Center
			forward = new Vector3(0,0,1);
		} else if (pos == hor3Top || pos == hor3Bot) {
			target = intRight; //Intersection: Third and Center
			forward = new Vector3(0,0,-1);
		} else if (pos == verLeft) {
			forward = new Vector3(1,0,0);
			target = intLeft;
		} else if (pos == verRight) {
			forward = new Vector3(-1,0,0);
			target = intRight;
		} else if(pos == intLeft){
			//Intersection Decision case: First and Center
			int randpos = Random.Range (0, 3);
			switch (randpos) {
			case 0:
				target = intMid; //Intersection: Second and Center
				forward = new Vector3(1,0,0);
				if (priorInt == intMid || priorInt == intLeft) {
					forward = new Vector3(0,0,-1);
					target = hor1Bot;
				}
				priorInt = intLeft;
				break;
			case 1: 
				forward = new Vector3(0,0,-1);
				target = hor1Bot;
				priorInt = intLeft;
				break;
			case 2:
				target = verLeft;
				forward = new Vector3(-1,0,0);
				if (priorInt == verLeft || priorInt == intLeft) {
					forward = new Vector3(0,0,-1);
					target = hor1Bot;
				}
				priorInt = intLeft;
				break;
			}
		} else if(pos == intMid){
			//Intersection Decision case: Second and Center
			int randpos = Random.Range (0, 3);
			switch (randpos) {
			case 0:
				target = intLeft; //Intersection: First and Center
				forward = new Vector3(-1,0,0);
				if (priorInt == intLeft || priorInt == intMid) {
					target = intRight;
					forward = new Vector3(1,0,0);
				}
				priorInt = intMid;
				break;
			case 1: 
				target = intRight; //Intersection: Third and Center
				forward = new Vector3(1,0,0);
				if (priorInt == intRight || priorInt == intMid) {
					target = intLeft;
					forward = new Vector3(-1,0,0);
				}
				priorInt = intMid;
				break;
			case 2:
				target = hor2Top;
				forward = new Vector3(0,0,1);
				priorInt = intMid;
				break;
			}
		}else if(pos == intRight){
			//Intersection Decision case: Third and Center
			int randpos = Random.Range (0, 3);
			switch (randpos) {
			case 0:
				target = intMid; //Intersection: Second and Center
				forward = new Vector3(-1,0,0);
				if (priorInt == intMid || priorInt == intRight) {
					target = hor3Bot;
					forward = new Vector3(0,0,-1);
				}
				priorInt = intRight;
				break;
			case 1: 
				target = hor3Bot;
				forward = new Vector3(0,0,-1);
				priorInt = intRight;
				break;
			case 2:
				target = verRight;
				forward = new Vector3(1,0,0);
				if (priorInt == verRight || priorInt == intRight) {
					target = hor3Bot;
					forward = new Vector3(0,0,-1);
				}
				priorInt = intRight;
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {

		for (int i = 1; i <= numberOfCars; i++) {
			string thisnum = this.ToString ();
			thisnum = thisnum.Substring (3,1);
			if (i.ToString () != thisnum) {
				carList.Add (GameObject.Find ("car" + i.ToString ()));
			} else {
				carNum = i;
			}
		}
			
		int randpos = Random.Range (0, 5);
		switch (randpos) {
		case 0:
			transform.position = hor1Top; //1
			break;
		case 1:
			transform.position = hor2Bot; //2
			break;
		case 2:
			transform.position = verLeft; //3
			priorInt = verLeft;
			break;
		case 3:
			transform.position = verRight; //1
			priorInt = verRight;
			break; 
		case 4:
			transform.position = hor3Top; //3
			break;
		}
		AssignTarget();
	}

	void CheckIntersection(){
		Vector3 pos = transform.position;
		for (int i = 0; i < nearInts.Length; i++) {	
			if(Mathf.Abs(Vector3.Distance(pos,nearInts[i])) <= .1f){
				if ((nearInts [i] == intLeftAbove || nearInts [i] == intLeftBelow || nearInts [i] == intLeftLeft || nearInts [i] == intLeftRight) && priorInt == intLeft) {
				} else if ((nearInts [i] == intMidAbove || nearInts [i] == intMidBelow || nearInts [i] == intMidLeft || nearInts [i] == intMidRight) && priorInt == intMid) {
				} else if ((nearInts [i] == intRightAbove || nearInts [i] == intRightBelow || nearInts [i] == intRightLeft || nearInts [i] == intRightRight) && priorInt == intRight) {
				} else {
					atIntersection = true;
				}
			}
		}
	}

	void CheckReplace(){
		bool reset = false;
		Vector3 pos = transform.position;
		if (pos == hor1Bot || pos == hor1Top) {
			reset = true;
		} else if (pos == hor2Bot || pos == hor2Top) {
			reset = true;
		} else if (pos == hor3Bot || pos == hor3Top) {
			reset = true;
		} else if (pos == verLeft || pos == verRight) {
			reset = true;
		}
		if (reset || replace) {
			ignoreNextIntersection = false;
			replace = false;
			int randpos = Random.Range (0, 5);
			switch (randpos) {
			case 0:
				transform.position = hor1Top; //1
				priorInt = new Vector3(-1,-1,-1);
				break;
			case 1:
				transform.position = hor2Bot; //2
				priorInt = new Vector3(-1,-1,-1);
				break;
			case 2:
				priorInt = verLeft;
				transform.position = verLeft; //3
				break;
			case 3:
				priorInt = verRight;
				transform.position = verRight; //1
				break; 
			case 4:
				transform.position = hor3Top; //3
				priorInt = new Vector3(-1,-1,-1);
				break;
			}
		}
		AssignTarget ();
	}

	List<GameObject> carsAtIntersection = new List<GameObject>();
	int CheckOthers(){
		Vector3 pos = transform.position;
		foreach (GameObject car in carList) {
			if (car != null) {
				Vector3 carPos = car.transform.position;
				if (carPos == intLeftAbove || carPos == intLeftBelow || carPos == intLeftLeft || carPos == intLeftRight || Mathf.Abs(Vector2.Distance(carPos,intLeft)) < 1) {
					if (pos == intLeftAbove || pos == intLeftBelow || pos == intLeftLeft || pos == intLeftRight) {
						print ("wait!!");
					}
				} else if(carPos == intMidAbove || carPos == intMidBelow || carPos == intMidLeft || carPos == intMidRight || Mathf.Abs(Vector2.Distance(carPos,intMid)) < 1){
					if (pos == intMidAbove || pos == intMidBelow || pos == intMidLeft || pos == intMidRight) {
						print ("wait!!");
					}
				} else if(carPos == intRightAbove || carPos == intRightBelow || carPos == intRightLeft || carPos == intRightLeft || Mathf.Abs(Vector2.Distance(carPos,intRight)) < 1){
					if (pos == intRightAbove || pos == intRightBelow || pos == intRightLeft || pos == intRightRight) {
						print ("wait!!");
					}
				}
			}
		}
		return 0;
	}

	int WaitTime(int carNumber){
		return carNumber*30;
		if (carNumber == 1) {
			return 20;
		} else if (carNumber == 2) {
			return 40;
		} else if (carNumber == 3) {
			return 60;
		} else if (carNumber == 4) {
			return 80;
		} else {
			return carNumber * carNumber * carNumber;
		}
	}
		
	float pauseLength = 0;
	public bool clearPath = true;
	public bool stuck = false;
	// Update is called once per frame
	void Update () {
		bool moved = false;
		//if current position is starting point, check distance to other cars. if close, don't move. otherwise move normal
		Vector3 pos = transform.position;
		if (pauseLength <= 1) {
			transform.LookAt (target);
			if (!atIntersection || intersectionPause > 10) {
				if (atIntersection) {
					ignoreNextIntersection = true;
					//CheckOthers ();
				}
				atIntersection = false;
				bool sensor = senseCars ();
				if (!sensor || (clearPath && frameCount > 30)) {
					transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
					moved = true;
				} else {
				}
				AssignTarget ();
				intersectionPause = 0;
			} else {
				intersectionPause++;
			}
			CheckIntersection ();
			CheckReplace ();
		} else {
			pauseLength = pauseLength - 1;
		}

		foreach (GameObject car in carList) {
			if (car != null) {
				if (Mathf.Abs(Vector3.Distance(car.transform.position,pos)) < 2) {
					if (pauseLength == 0) {
						pauseLength = WaitTime(carNum);
					}
				}
			}
		}
		if (this.transform.position == previousPosition) {
			previousPositionCount++;
		} else {
			previousPositionCount = 0;
			stuck = false;
		}
		previousPosition = this.transform.position;
		if (previousPositionCount >= 150) {
			stuck = true;
			/*if (stuck) {
				if (transform.position.x > 9) {
					target = hor3Bot;
				} else if (transform.position.x < 5 && transform.position.x > -5) {
					target = hor2Top;
				} else if (transform.position.x < -5) {
					target = hor1Bot;
				}
			}*/
		}

		frameCount++;
	}
}
