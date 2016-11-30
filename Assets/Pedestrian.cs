using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pedestrian : MonoBehaviour {

	public int numberOfGuys = 8;
	int guyNum;

	//sidewalk position based on grid of blocks:
	//	   end1   end2    end3
	//	    ||	   ||	   ||
	// a   b c    d e     f g    h
	//   --	| --   ||  --- || -- 
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

	List<Vector3> endpoints = new List<Vector3>();
	List<GameObject> guyList = new List<GameObject>();

	void Start(){
		endpoints.Add(a);
		endpoints.Add(h);
		endpoints.Add(p);
		endpoints.Add(endOneB);
		endpoints.Add(endOneC);
		endpoints.Add(endTwoD);
		endpoints.Add(endTwoE);
		endpoints.Add(endThreeF);
		endpoints.Add(endThreeG);
		endpoints.Add(endFourK);
		endpoints.Add(endFourK);
		endpoints.Add(endFiveL);
		endpoints.Add(endSixN);
		endpoints.Add(endSixO);

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

		int randpos = Random.Range(0, endpoints.Count);
		transform.position = endpoints[randpos];

		//assign new position if another guy is already there
		foreach (GameObject guy in guyList){
			if(guy.transform.position == transform.position){
				int randp = Random.Range(0, endpoints.Count);
				transform.position = endpoints[randp];
			}
		}

	}

	void AssignTarget(){
		
	}

	// Update is called once per frame
	void Update () {
	
	}
}
