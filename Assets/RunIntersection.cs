using UnityEngine;
using System.Collections;

public class RunIntersection : MonoBehaviour {

	public ArrayList pedestrians;
	public ArrayList vehicles;

	// Use this for initialization
	void Start () {
		pedestrians = new ArrayList ();
		vehicles = new ArrayList ();
		Pedestrian s = new Pedestrian ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Pedestrian p in pedestrians) {
		
		}

		foreach (Vehicle v in vehicles) {
		
		}
	}
}
