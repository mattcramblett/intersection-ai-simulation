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

	private Vector3 target;
	public float speed = 2f;
	Vector3 priorInt = new Vector3(0,0,0);
	bool moving = true;
	int intersectionPause = 0;
	bool atIntersection = false;
	bool ignoreNextIntersection = false;

	/// <summary>
	/// Senses other cars close enough in front.
	/// </summary>
	/// <returns><c>true</c>, if cars are sensed, <c>false</c> otherwise.</returns>
	bool senseCars(){
		// can probably use visionTest() from ai-agent-simulation but this would require looping through all other vehicles?
		// seems inefficient. Maybe a raycast and return the distance to the first object it hits?

		//possible code:
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if(Physics.Raycast(transform.position, fwd, 1)){
			print("something in front of car");
		}
		return false;
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

	void AssignTarget(){
		Vector3 pos = transform.position;
		if (pos == hor1Top || pos == hor1Bot) {
			target = intLeft; //Intersection: First and Center
		} else if (pos == hor2Bot || pos == hor2Top) {
			target = intMid; //Intersection: Second and Center
		} else if (pos == hor3Top || pos == hor3Bot) {
			target = intRight; //Intersection: Third and Center
		} else if (pos == verLeft) {
			target = intLeft;
		} else if (pos == verRight) {
			target = intRight;
		} else if(pos == intLeft){
			//Intersection Decision case: First and Center
			int randpos = Random.Range (0, 3);
			switch (randpos) {
			case 0:
				target = intMid; //Intersection: Second and Center
				if (priorInt == intMid || priorInt == intLeft) {
					target = hor1Bot;
				}
				priorInt = intLeft;
				break;
			case 1: 
				target = hor1Bot;
				priorInt = intLeft;
				break;
			case 2:
				target = verLeft;
				if (priorInt == verLeft || priorInt == intLeft) {
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
				if (priorInt == intLeft || priorInt == intMid) {
					target = intRight;
				}
				priorInt = intMid;
				break;
			case 1: 
				target = intRight; //Intersection: Third and Center
				if (priorInt == intRight || priorInt == intMid) {
					target = intLeft;
				}
				priorInt = intMid;
				break;
			case 2:
				target = hor2Top;
				priorInt = intMid;
				break;
			}
		}else if(pos == intRight){
			//Intersection Decision case: Third and Center
			int randpos = Random.Range (0, 3);
			switch (randpos) {
			case 0:
				target = intMid; //Intersection: Second and Center
				if (priorInt == intMid || priorInt == intRight) {
					target = hor3Bot;
				}
				priorInt = intRight;
				break;
			case 1: 
				target = hor3Bot;
				priorInt = intRight;
				break;
			case 2:
				target = verRight;
				if (priorInt == verRight || priorInt == intRight) {
					target = hor3Bot;
				}
				priorInt = intRight;
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
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
				//if (!ignoreNextIntersection) {
					//moving = false;
				//}
				//ignoreNextIntersection = !ignoreNextIntersection;
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
		if (reset) {
			int randpos = Random.Range (0, 5);
			switch (randpos) {
			case 0:
				transform.position = hor1Top; //1
				break;
			case 1:
				transform.position = hor2Bot; //2
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
				break;
			}
		}
		AssignTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		if (!atIntersection || intersectionPause > 15 ) {
			if (atIntersection) {
				ignoreNextIntersection = true;
			}
			atIntersection = false;
			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
			AssignTarget ();
			intersectionPause = 0;
		} else {
			intersectionPause++;
		}
		CheckIntersection ();
		CheckReplace ();
	}
}
