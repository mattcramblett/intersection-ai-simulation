using UnityEngine;
using System.Collections;

public class Pedestrian : MonoBehaviour {

	public string orientation;
	public bool wishToCross = false;
	public bool moving = true;

	public Pedestrian(){
	
	}

	/// <summary>
	/// Turn at corner
	/// </summary>
	void Turn(){
	}

	/// <summary>
	/// Checks if safe to cross or not
	/// </summary>
	bool Cross(){
		return false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
