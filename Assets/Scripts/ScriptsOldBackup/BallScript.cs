//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//
//public class BallScript : MonoBehaviour {
//
////	public KeyCode newBall = KeyCode.Space; //assigning the key for the ball respawn
//	[SerializeField] //makes it editable in the inspector
//	float forceValue = 4.5f; //so we can edit this more easily
//	public GameObject newBall; //the balls that will be added
//	Rigidbody2D myBody; //the rigidbody attached to the gameobject
//	GoalScript gs; //this is a ref to the goalscript
//	public PaddleScript[] paddles;
//	public float radius; //the radius
//	private bool rotating; //is the ball rotating
//	private float angle; //the angle
//	public float rotationSpeed; //how fast it will rotate
//
//
//	void Start () {
////		paddles = players.GetComponentsInChildren<PaddleScript> ();
//	}//END START
//		
//	void Update(){
//		if (rotating) //if bool is set to rotate
//			Rotate (); //then rotate
//	}//END UPDATE
//
//	public void Init(){ //initialize function
//		myBody = GetComponent<Rigidbody2D> (); //set the rb to my body
//		float randomAngle = Random.Range (0, 360) * Mathf.Deg2Rad; //set the random angle
//		transform.localPosition = new Vector2 (Mathf.Cos (randomAngle), Mathf.Sin (randomAngle)) * radius; //set the local posistion 
//		rotating = true; //set rotate to true
//		angle = randomAngle; //set the angle to the random angle we defined
//	}//END ININT
//
//	void Rotate(){ //rotation function
//		angle += rotationSpeed * Time.deltaTime; 
//		transform.localPosition = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * radius;
//	}//END ROTATE
//
//	void OnCollisionEnter2D (Collision2D other) { //collision function, change velocity force of ball depending on what it hits
//		if (other.transform.name == "WallTop") { //if the name of the object is 'WallTop' 
//			myBody.velocity = myBody.velocity * 1.0f;//then multiply the ball's velocity by x
//			if (rotating) { //if it's rotating and it hits a paddle
//				rotating = false; //set the bool to false
//				transform.parent = null; //and un-parent it's transform
//			}
//		}//end wall top bounce force
//		if (other.transform.name == "WallBottom") { //if the name of the object is 'WallBottom' 
//			myBody.velocity = myBody.velocity * 1.0f;//then multiply the ball's velocity by x
//			if (rotating) { //if it's rotating and it hits a paddle
//				rotating = false; //set the bool to false
//				transform.parent = null; //and un-parent it's transform
//			}
//		}//end wall bottom bounce force
//		if (other.gameObject.tag == "Paddle") { //if the tag on the object is paddle 
//			myBody.velocity = myBody.velocity * 1.1f;//then multiply the ball's velocity by x
//			if (rotating) { //if it's rotating and it hits a paddle
//				rotating = false; //set the bool to false
//				transform.parent = null; //and un-parent it's transform
//			}//end if rotating
//		}
////		float dotprodPaddle = Vector3.Dot (ball.position.normalized, paddle.position.normalized);
////			if ((paddle.transform.position, ball.transform.position * dotprodPaddle) < 1);{
////				myBody.velocity = myBody.velocity * (dotprodPaddle*Mathf.PI));
////			}//end dotprod paddle
////		float dotprodWall = Vector3.Dot (ball.position.normalized, paddle.position.normalized);
////			if ((paddle.transform.position, ball.transform.position * dotprodWall) < 1);{
//////				myBody.velocity = myBody.velocity * (dotprodWall*Mathf.PI));
////			}//end dotprod wall
//		//}//end if paddle bounce force
//
//	} // END ON COLLISION
//		
//	public void Reset() { // reset the ball position and restart the ball movement
//		myBody = GetComponent<Rigidbody2D>(); //again set the rb to mybody
//		transform.position = new Vector2(0,0); //and again, no force or movement
//		bool right = Random.value > 0.5f; 
//		float startingAngle;
//		if (right) { //if it's going right
//			startingAngle = Random.Range (-30, 30); //set the range for the angle is goes off from the starting point if it's going right
//		} else { //if its going left
//			startingAngle = Random.Range (150, 210); //set the range for the angle is goes off from the starting point if it's going left
//		}//end else 
//		startingAngle *= Mathf.Deg2Rad; //starting angle
//		myBody.velocity = new Vector2 (Mathf.Cos (startingAngle), Mathf.Sin (startingAngle)) * forceValue * GameManager.instance.amountZoomedOut;
//	}//END RESET
//}//END SCRIPT
