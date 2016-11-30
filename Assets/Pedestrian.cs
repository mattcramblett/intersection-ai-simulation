using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pedestrian : MonoBehaviour {

	public int numberOfGuys = 8;
	public float speed = 1f;
	public int numberOfCars = 11;
	public Vector3 target;
	int guyNum;


	//sidewalk position based on grid of blocks:
	//	   end1   end2    end3
	//	    ||	   ||	   ||
	// a   b c    d e     f g    h
	//   -- || --   ||  --- || -- 
	// i   j k    l m     n o    p
	//		||	   ||	   ||
	//     end4   end5    end6	
	
	//orientation:
	// z ^
	// x >

	//control points:
	Vector3 a = new Vector3(-20f, 0f, .75f); //endpoint
	Vector3 b = new Vector3(-10.75f, 0f, .75f);
	Vector3 c = new Vector3(-9.35f, 0f, .75f);
	Vector3 d = new Vector3(-0.75f, 0f, 0.75f);
	Vector3 e = new Vector3(0.75f, 0f, 0.75f);
	Vector3 f = new Vector3(9.3f, 0f, 0.75f);
	Vector3 g = new Vector3(10.75f, 0f, 0.75f);
	Vector3 h = new Vector3(20f, 0f, 0.75f); //endpoint

	Vector3 i = new Vector3(-20f, 0f, -0.75f); //endpoint
	Vector3 j = new Vector3(-10.75f, 0f, -0.75f);
	Vector3 k = new Vector3(-9.35f, 0f, -0.75f);
	Vector3 l = new Vector3(-0.75f, 0f, -0.75f);
	Vector3 m = new Vector3(0.75f, 0f, -0.75f);
	Vector3 n = new Vector3(9.3f, 0f, -0.75f);
	Vector3 o = new Vector3(10.75f, 0f, -0.75f);
	Vector3 p = new Vector3(20f, 0f, -0.75f); //endpoint

	//each 'end' corresponds to the exit points in line with
	//the letter of that intersection 
	Vector3 endOneB = new Vector3(-10.75f, 0f, 20f);
	Vector3 endOneC = new Vector3(-9.35f, 0f, 20f);
	Vector3 endTwoD = new Vector3(-0.75f, 0f, 20f);
	Vector3 endTwoE = new Vector3(0.75f ,0f, 20f);
	Vector3 endThreeF = new Vector3(9.3f, 0f, 20f);
	Vector3 endThreeG = new Vector3(10.75f, 0f, 20f);

	Vector3 endFourJ = new Vector3(-10.75f, 0f, -20f);
	Vector3 endFourK = new Vector3(-9.35f, 0f, -20f);
	Vector3 endFiveL = new Vector3(-0.75f, 0f, -20f);
	Vector3 endFiveM = new Vector3(0.75f, 0f, -20f);
	Vector3 endSixN = new Vector3(9.3f, 0f, -20f);
	Vector3 endSixO = new Vector3(10.75f, 0f, -20f);

	List<Vector3> endpoints;
	List<Vector3> crosspoints;
	List<GameObject> guyList = new List<GameObject>();

	void Start(){
		crosspoints = new List<Vector3>{b,c,d,e,f,g,j,k,l,m,n,o};
		endpoints = new List<Vector3>{a,h,i,p,endOneB,endOneC,endTwoD,endTwoD,
				endTwoD, endTwoE, endThreeF, endThreeG, endFourK, endFourK,
				endFiveL, endSixN, endSixO};

		//collect list of other guys
		for (int z = 1; z <= numberOfGuys; z++) {
			string thisnum = this.ToString ();
			thisnum = thisnum.Substring (3,1);
			if (i.ToString () != thisnum) {
				guyList.Add (GameObject.Find ("guy" + z.ToString ()));
			} else {
				guyNum = z;
			}
		}

		//assign a random starting position at an endpoint
		int randpos = Random.Range(0, endpoints.Count);
		transform.position = endpoints[randpos];

		//assign new random position if another guy is already there
		foreach (GameObject guy in guyList){
			if(guy.transform.position == transform.position){
				int randp = Random.Range(0, endpoints.Count);
				transform.position = endpoints[randp];
			}
		}
		AssignTarget();
	}

	//Assigns target based on current position
	//hard coded, but the overall pattern will simulate random movement
	void AssignTarget(){
		Vector3 pos = transform.position;
		if(pos == a) {target = b;} //endpoints
		else if(pos == h) {target = g;}
		else if(pos == i) {target = j;}
		else if(pos == p) {target = o;}
		else if(pos == endOneB) {target = b;}
		else if(pos == endOneC) {target = c;}
		else if(pos == endTwoD) {target = d;}
		else if(pos == endTwoE) {target = e;}
		else if(pos == endThreeF) {target = f;}
		else if(pos == endThreeG) {target = g;}
		else if(pos == endFourJ) {target = j;}
		else if(pos == endFourK) {target = k;}
		else if(pos == endFiveL) {target = l;}
		else if(pos == endFiveM) {target = m;}
		else if(pos == endSixN) {target = n;}
		else if(pos == endSixO) {target = o;}
		else if(pos == b) {target = c;} //non-endpoints
		else if(pos == c) {target = k;}
		else if(pos == d) {target = e;}
		else if(pos == e) {target = f;}
		else if(pos == f) {target = n;}
		else if(pos == g) {target = f;}
		else if(pos == j) {target = endFourJ;}
		else if(pos == k) {target = j;}
		else if(pos == l) {target = d;}
		else if(pos == m) {target = e;}
		else if(pos == n) {target = m;}
		else if(pos == o) {target = endSixO;}
		//note: any changes to the above pattern should avoid infinite loops
	}

	//returns true if a car is nearby
	//this is meant for checking for nearby cars when assigning a new
	//target at an intersection
	bool CheckForCar(){
		bool result = false;
		for (int z = 1; z <= numberOfCars; z++) {
			Vector3 carPos = GameObject.Find ("car" + z.ToString()).transform.position;
			float distance = (transform.position - carPos).magnitude;
			if(distance < 1 && distance > 0){ //this might need changed for precision
				result = true;
				break;
			}
		}
		return result;
	}

	// Update is called once per frame
	void Update () {
		AssignTarget();
		transform.LookAt(target);
		transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);

		//Pause if there's a car nearby when at an intersection (not an endpoint)
		if(!endpoints.Contains(target) && CheckForCar() && crosspoints.Contains(transform.position)){
			speed = 0;
		}else{
			speed = 1f;
		}
	}
}
