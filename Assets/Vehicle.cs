using UnityEngine;
using System.Collections;

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

	void AssignTarget(){
		Vector3 pos = transform.position;
		if(pos == new Vector3(-10, 0.15f, -20) || pos == new Vector3 (-10, .15f, 20)){
			target = new Vector3(-10, 0.15f, 0); //Intersection: First and Center
		}else if(pos == new Vector3 (0, .15f, 20) || pos == new Vector3 (0, .15f, -20)){
			target = new Vector3(0, 0.15f, 0); //Intersection: Second and Center
		}else if(pos == new Vector3 (10, .15f, -20) || pos == new Vector3 (10, .15f, 20)){
			target = new Vector3(10, 0.15f, 0); //Intersection: Third and Center
		}else if(pos == new Vector3(-10, 0.15f, 0)){
			//Intersection Decision case: First and Center
			int randpos = Random.Range (0, 3);
			switch (randpos) {
				case 0:
					target = new Vector3(0, 0.15f, 0); //Intersection: Second and Center
					break;
				case 1: 
					target = new Vector3 (-10, .15f, -20);
					break;
				case 2:
					target = new Vector3 (-10, .15f, 20);
					break;
			}
		}else if(pos == new Vector3(0, 0.15f, 0)){
			//Intersection Decision case: Second and Center
			int randpos = Random.Range (0, 4);
			switch (randpos) {
				case 0:
					target = new Vector3(-10, 0.15f, 0); //Intersection: First and Center
					break;
				case 1: 
					target = new Vector3(10, 0.15f, 0); //Intersection: Third and Center
					break;
				case 2:
					target = new Vector3 (0, .15f, 20);
					break;
				case 3:
					target = new Vector3 (0, .15f, -20);
					break;
			}

		}else if(pos == new Vector3(10, 0.15f, 0)){
			//Intersection Decision case: Third and Center
			int randpos = Random.Range (0, 3);
			switch (randpos) {
				case 0:
					target = new Vector3(0, 0.15f, 0); //Intersection: Second and Center
					break;
				case 1: 
					target = new Vector3 (10, .15f, -20);
					break;
				case 2:
					target = new Vector3 (10, .15f, 20);
					break;
			}
		}
	}

	// Use this for initialization
	void Start () {
		int randpos = Random.Range (0, 5);
		switch (randpos) {
		case 0:
			transform.position = new Vector3 (-10, .15f, -20); //1
			break;
		case 1:
			transform.position = new Vector3 (0, .15f, 20); //2
			break;
		case 2:
			transform.position = new Vector3 (10, .15f, -20); //3
			break;
		case 3:
			transform.position = new Vector3 (-10, .15f, 20); //1
			break; 
		case 4:
			transform.position = new Vector3 (0, .15f, -20); //2
			break;
		case 5:
			transform.position = new Vector3 (10, .15f, 20); //3
			break;
		}
		AssignTarget();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
		AssignTarget();
	}
}
