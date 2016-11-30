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
		int rando = Random.Range(0, 4);
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
		else if(pos == b) { //non endpoints
			switch(rando){
				case 0: target = endOneB; break;
				case 1: target = a; break;
				case 2: target = j; break;
				case 3: target = c; break;
			}} //non-endpoints
		else if(pos == c) {
			switch(rando){
				case 0: target = endOneC; break;
				case 1: target = b; break;
				case 2: target = k; break;
				case 3: target = d; break;
			}}
		else if(pos == d) {
			switch(rando){
				case 0: target = endTwoD; break;
				case 1: target = l; break;
				case 2: target = e; break;
				case 3: target = c; break;
			}}
		else if(pos == e) {
			switch(rando){
				case 0: target = endTwoE; break;
				case 1: target = d; break;
				case 2: target = f; break;
				case 3: target = m; break;
			}}
		else if(pos == f) {
			switch(rando){
				case 0: target = endThreeF; break;
				case 1: target = g; break;
				case 2: target = e; break;
				case 3: target = n; break;
			}}
		else if(pos == g) {
			switch(rando){
				case 0: target = endThreeG; break;
				case 1: target = h; break;
				case 2: target = f; break;
				case 3: target = o; break;
			}}
		else if(pos == j) {
			switch(rando){
				case 0: target = endFourJ; break;
				case 1: target = i; break;
				case 2: target = b; break;
				case 3: target = k; break;
			}}
		else if(pos == k) {
			switch(rando){
				case 0: target = endFourK; break;
				case 1: target = j; break;
				case 2: target = c; break;
				case 3: target = l; break;
			}}
		else if(pos == l) {
			switch(rando){
				case 0: target = endFiveL; break;
				case 1: target = k; break;
				case 2: target = m; break;
				case 3: target = d; break;
			}}
		else if(pos == m) {
			switch(rando){
				case 0: target = endFiveM; break;
				case 1: target = l; break;
				case 2: target = e; break;
				case 3: target = n; break;
			}}
		else if(pos == n) {
			switch(rando){
				case 0: target = endSixN; break;
				case 1: target = o; break;
				case 2: target = f; break;
				case 3: target = m; break;
			}}
		else if(pos == o) {
			switch(rando){
				case 0: target = endSixO; break;
				case 1: target = p; break;
				case 2: target = g; break;
				case 3: target = n; break;
			}}
		//note: any changes to the above pattern should avoid infinite loops
	}

	//sidewalk position based on grid of blocks:
	//	   end1   end2    end3
	//	    ||	   ||	   ||
	// a   b c    d e     f g    h
	//   -- || --  || ---  || -- 
	// i   j k    l m     n o    p
	//		||	   ||	   ||
	//     end4   end5    end6

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
