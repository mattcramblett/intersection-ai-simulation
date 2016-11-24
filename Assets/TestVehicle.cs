using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TestVehicle : MonoBehaviour {

	//Ripping off the Catmull Rom algorithm from lab 5
	const int NumberOfPoints = 5;
	Vector3[] controlPoints;
	
	float time = 0f;
	const float DT = 0.0015f;
	public static int segmentCount = 2;
	public static float tau = 0.1f;

	/* Returns a point on a cubic Catmull-Rom/Blended Parabolas curve
	 * u is a scalar value from 0 to 1
	 * segment_number indicates which 4 points to use for interpolation
	 */
	Vector3 ComputePointOnCatmullRomCurve(float u, int segmentNumber) {
    	Vector3 point = new Vector3();

    	Vector3 p0 = controlPoints[(segmentNumber - 2) % NumberOfPoints];
    	Vector3 p1 = controlPoints[(segmentNumber - 1) % NumberOfPoints];
    	Vector3 p2 = controlPoints[segmentNumber % NumberOfPoints];
    	Vector3 p3 = controlPoints[(segmentNumber + 1) % NumberOfPoints];

    	Vector3 c3 = new Vector3(); 
    	Vector3 c2 = new Vector3();
    	Vector3 c1 = new Vector3();
    	Vector3 c0 = new Vector3();

    	//x components of c values:
    	c3.x = (-1f*tau*p0.x) + (2f-tau)*p1.x + (tau-2f)*p2.x + tau*p3.x;
    	c2.x = 2f*tau*p0.x + (tau-3f)*p1.x + (3f-(2f*tau))*p2.x + tau*(-1f)*p3.x;
    	c1.x = -1f*tau*p0.x + tau*p2.x;
    	c0.x = p1.x;

    	//y components of c values:
    	c3.y = (-1f*tau*p0.y) + (2f-tau)*p1.y + (tau-2f)*p2.y + tau*p3.y;
    	c2.y = 2f*tau*p0.y + (tau-3f)*p1.y + (3f-(2f*tau))*p2.y + tau*(-1f)*p3.y;
    	c1.y = -1f*tau*p0.y + tau*p2.y;
    	c0.y = p1.y;

    	//z components of c values:
    	c3.z = (-1f*tau*p0.z) + (2f-tau)*p1.z + (tau-2f)*p2.z + tau*p3.z;
    	c2.z = 2f*tau*p0.z + (tau-3f)*p1.z + (3f-(2f*tau))*p2.z + tau*(-1f)*p3.z;
    	c1.z = -1f*tau*p0.z + tau*p2.z;
    	c0.z = p1.z;

    	//Parabolic curve equation:
    	point.x = c3.x*u*u*u + c2.x*u*u + c1.x*u + c0.x;
    	point.y = c3.y*u*u*u + c2.y*u*u + c1.y*u + c0.y;
    	point.z = c3.z*u*u*u + c2.z*u*u + c1.z*u + c0.z;
    	
    	return point;
	}

	void Start () {
		controlPoints = new Vector3[NumberOfPoints];
		controlPoints[0] = new Vector3(-20, 0.15f, 0);
		controlPoints[1] = new Vector3(-10, 0.15f, 0);
		controlPoints[2] = new Vector3(-10, 0.15f, 20);
    controlPoints[3] = new Vector3(-10, 0.15f, 0);
    controlPoints[4] = new Vector3(-20, 0.15f, 0);
	}
	
	// Update is called once per frame
	void Update () {
	   	if(time >= 1f){
   			segmentCount++;
   			time = 0f;
   		}else{
   			time += DT;
   		}
   		Vector3 temp = ComputePointOnCatmullRomCurve(time, segmentCount);
   		transform.LookAt(temp);
   		transform.position = temp;
	}
}
