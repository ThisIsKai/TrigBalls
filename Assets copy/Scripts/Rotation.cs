using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour { // **SCRIPT TO MAKE AN OBJECT ROTATE AROUND ANOTHER ONE**
	public float radius; //radius value
	[Range(0f, 2f * Mathf.PI)] //make a slider 
	public float angle; //angle value
	public float randomrange;



	// Use this for initialization
	void Start () {
		randomrange = Random.Range (1f, 1.05f); //setting the range for the random number
	}// END START

	// Update is called once per frame
	void Update () {
		// **option 1**
		//			transform.RotateAround(    expensive, may obscure the accuracy 
		// **option 2**
		//			Vector3 newpos = Vector3.zero;  //create an empty vector 3 for us to manipulate
		//			newpos.x = radius * Mathf.Cos(angle);
		//			newpos.y = radius * Mathf.Sin(angle);
		//			this.transform.position = newpos;
		// **option 3**
		Vector3 newpos = PointOnCircle (radius + randomrange, Mathf.Rad2Deg * Time.time + Time.time); //***???***
		this.transform.position = newpos; //***???***


	}// END UPDATE



	public Vector3 PointOnCircle(float radius, float angle) { //***???***
		float angleInRadians = angle * Mathf.Deg2Rad; //mathf.deg2rad is a constant value of 180/pi, (stands for degrees 2 radians)
		return new Vector3 ( GameManager.instance.amountZoomedOut* radius * Mathf.Cos (angleInRadians), //x value
			radius * Mathf.Sin (angleInRadians), // y value
			0f); //z value




	}
} //END SCRIPT


// making the ai enemies spawn in a circle around the player:
// float enemySpawnAngle = Random.Range (-Mathf.PI, Mathf.PI);
