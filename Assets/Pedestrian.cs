using UnityEngine;
using System.Collections;

public class Pedestrian : MonoBehaviour {

	public string orientation;
	public string position;
	public bool wishToCross = false;
	public bool moving = true;
	public GameObject body;
	//sidewalk position based on grid of blocks
	//5 6 7 8
	//1 2 3 4
	//Each block has left and/or right and a short side
	//Also an orientation (left/right or up/down)

	//1 Right: (-10.75 , .15 , -19.75) - (-10.75 , .15 , -0.75)
	//1 Short: (-10.75 , .15 , -0.75) - (-19.75 , .15f , -0.75)
	//5 Short: (-19.75 , .15 , 0.75) - (-10.75 , .15 , 0.75)
	//5 Right: (-10.75 , .15 , 0.75) - (-10.75 , .15 , 19.75)
	//2 Left: (-9.25 , .15 , -19.75) - (-9.25 , .15 , -0.75)
	//2 Short: (-9.25 , .15 , -0.75) - (-.75 , .15 , -0.75)
	//2 Right: (-0.75 , .15 , -0.75) - (-0.75, .15 , -19.75)
	//6 Left: (-9.25 , .15, 0.75) - (-9.25 , .15 , 19.75)
	//6 Short: (-9.25 , .15 , 0.75) - (-0.75 , .15 , 0.75)
	//6 Right: (-0.75 , .15 , 0.75) - (-0.75 , .15 , 19.75)
	//7 Left: (0.75 , .15 , 19.75) - (0.75 , .15 , 0.75)
	//7 Short: (0.75 , .15 , 0.75) - (9.25 , .15 , 0.75)
	//7 Right: (9.25 , .15 , 0.75) - (9.25 , .15 , 19.75)
	//3 Left: (0.75 , .15 , -0.75) - (0.75 , .15 , -19.75)
	//3 Short: (0.75 , .15 , -0.75) - (9.25 , .15 , -0.75) 
	//3 Right: (9.25 , .15 , -19.75) - (9.25 , .15 , -0.75)
	//4 Left: (10.75 , .15 , -0.75) - (-19.75 , .15 , 19.75)
	//4 Short: (10.75 , .15 , -0.75) - (19.75 , .15 , -0.75)
	//8 Short: (10.75 , .15, 0.75) - (19.75 , .15, -0.75)
	//8 Left: (10.75 , .15 , 0.75) - (10.75 , .15 , -19.75)

	string setInitialOrientation(string op1 , string op2){
		string orient = "";
		int randorient = Random.Range (1, 3);
		switch (randorient) {
		case 1:
			orient = op1;
			break;
		case 2:
			orient = op2;
			break;
		}
		return orient;
	}

	public Pedestrian(){
		this.body = GameObject.CreatePrimitive(PrimitiveType.Cube); // used for testing positions: positions are good 
		body.AddComponent<Rigidbody> ();
		int randpos = Random.Range (0, 5);
		this.body.transform.localScale = new Vector3 (.5f, .5f, .5f);

		//generation locations are in front of some buildings that approach the road
		switch (randpos) {
		case 0:
			//building on 3 left 
			this.orientation = setInitialOrientation("up","down");
			this.position = "3left";
			this.body.transform.position = new Vector3 (0.75f, .15f, -14.25f);
			break;
		case 1:
			//building on 2 right towards bottom
			this.orientation = setInitialOrientation("up","down");
			this.position = "2right";
			this.body.transform.position = new Vector3 (-0.75f, .15f, -14.5f);
			break;
		case 2:
			//building on 3 right
			this.orientation = setInitialOrientation("up","down");
			this.position = "3right";
			this.body.transform.position = new Vector3 (9.25f, .15f, -17.5f);
			break;
		case 3:
			//building on 7 left
			this.orientation = setInitialOrientation("up","down");
			this.position = "7left";
			this.body.transform.position = new Vector3 (0.75f, .15f, 3.75f);
			break;
		case 4:
			//building on 6 short
			this.orientation = setInitialOrientation("left","right");
			this.position = "6short";
			this.body.transform.position = new Vector3 (-7.5f, .15f, 0.75f);
			break;
		}
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
