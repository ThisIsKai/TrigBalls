using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// --> / <--  = start / end stuff I want to get working

// ~~~ 		= old stuff I want to keep just in case

public class BallScript : MonoBehaviour {

	[SerializeField]																	// makes it editable in the inspector
	float forceValue = 4.5f; 															// so we can edit this more easily
	public GameObject newBall; 															// the balls that will be added
	Rigidbody2D myBody;																	// the rigidbody attached to the gameobject
	GoalScript gs;																		// this is a ref to the goalscript
	public PaddleScript[] paddles;
	public float radius;																// the radius
	private bool rotating;																// is the ball rotating
	private float angle;																// the angle
	public float rotationSpeed;															// how fast it will rotate
	public Transform paddle1, paddle2, topWall, bottomWall;
	float wallBounceVelocity;
	float paddleBounceVelocity;
	float wallVelocityMod;
	float paddleVelocityMod;


	[Range(-0.9f, 0.9f)]																// limiter range for the inspector
	public float dotProdLimiterMinY;													// dot prod minY value

	[Range(-0.9f, 0.9f)]																// limiter range for the inspector
	public float dotProdLimiterMaxY;													// dot prod maxY value

	[Range(-0.9f, 0.9f)]																// limiter range for the inspector
	public float dotProdLimiterMinX;													// dot prod minX value

	[Range(-0.9f, 0.9f)]																// limiter range for the inspector
	public float dotProdLimiterMaxX;													// dot prod maxX value

	[Range(-0.9f, 0.9f)]																// modification range for the inspector (will be added to velocity)
	public float dotProdPaddleMod;														// paddle modifier value

	[Range(-0.9f, 0.9f)]																// modification range for the inspector (will be added to velocity)
	public float dotProdWallMod;														// wall modifier value






	// public int [] limiterArray;
	// ~~~ public KeyCode newBall = KeyCode.Space;  //assigning the key for the ball respawn



	void Start () {																								// Start function
																												//~~~	paddles = players.GetComponentsInChildren<PaddleScript> ();
	}//END START
		
	void Update(){																								// Update function
		if (rotating)																							// if bool is set to rotate
			Rotate ();																							// then run the rotate function
	}//END UPDATE

	public void Init(){																							// Initialize function
		myBody = GetComponent<Rigidbody2D> ();																	// set the rb to my body
		float randomAngle = Random.Range (0, 360) * Mathf.Deg2Rad;												// set the random angle
		transform.localPosition = new Vector2 (Mathf.Cos (randomAngle), Mathf.Sin (randomAngle)) * radius; 		// set the local posistion 
		rotating = true;																						// set rotate to true
		angle = randomAngle;																					// set the angle to the random angle we defined
	}//END ININT


	void Rotate(){ 																								//Rotate function
		angle += rotationSpeed * Time.deltaTime; 
		transform.localPosition = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * radius;
	}//END ROTATE

	void OnCollisionEnter2D (Collision2D other) { 	
		float dotprodPaddle1 = Vector3.Dot (transform.position.normalized, paddle1.position.normalized);
		float dotprodPaddle2 = Vector3.Dot (transform.position.normalized, paddle2.position.normalized);
		float dotprodTopWall = Vector3.Dot(transform.position.normalized, topWall.position.normalized);
		float dotprodBottomWall = Vector3.Dot(transform.position.normalized, bottomWall.position.normalized);

		// collision function, change velocity force of ball depending on what it hits
		if (other.transform.name == "WallTop") { 																// if the name of the object is 'WallTop' 
			myBody.velocity = myBody.velocity * wallBounceVelocity;	
			if (dotprodTopWall < dotProdLimiterMaxY || dotprodTopWall > dotProdLimiterMinY){
				wallBounceVelocity = wallVelocityMod * -1;
			}// then multiply the ball's velocity by x
			if (rotating) {																						// if it's rotating and it hits a paddle
				rotating = false;																				// set the bool to false
				transform.parent = null;																		// and un-parent it's transform
			}
		}//end wall top bounce force
		if (other.transform.name == "WallBottom") {																// if the name of the object is 'WallBottom' 
			myBody.velocity = myBody.velocity * wallBounceVelocity;															// then multiply the ball's velocity by x
			if (dotprodBottomWall < dotProdLimiterMinY || dotprodBottomWall > dotProdLimiterMaxY) {
				wallBounceVelocity = wallVelocityMod;
			}
			if (rotating) {																						// if it's rotating and it hits a paddle
				rotating = false;																				// set the bool to false
				transform.parent = null;																		// and un-parent it's transform
			}//end if rotating
		}//end wall bottom bounce force
		if (other.gameObject.name == "Player1") {																	// if the name is player1 
			myBody.velocity = myBody.velocity * 1.1f;	
			if (dotprodPaddle1 < dotProdLimiterMinX || dotprodPaddle1> dotProdLimiterMaxX) {
				paddleBounceVelocity = paddleVelocityMod;													// --> then multiply the ball's velocity by x : (NullReferenceException: Object reference not set to an instance of an object)
			if (rotating) {																						// if it's rotating and it hits a paddle
				rotating = false;																				// set the bool to false
				transform.parent = null;																		// and un-parent it's transform
			}//end if rotating
		}//end paddle bounce force
			if (other.gameObject.name == "Player2") {															// if the name is player2
			if (dotprodPaddle2 < dotProdLimiterMinX || dotprodPaddle2> dotProdLimiterMaxX) {
				paddleBounceVelocity = paddleVelocityMod * -1;
			}
			myBody.velocity = myBody.velocity * 1.1f;															// --> then multiply the ball's velocity by x : (NullReferenceException: Object reference not set to an instance of an object)
			if (rotating) {																						// if it's rotating and it hits a paddle
				rotating = false;																				// set the bool to false
				transform.parent = null;																		// and un-parent it's transform
			}//end if rotating
		}//end paddle bounce force
	}
//// -->  GET THIS WORKING


//
//		if (Vector3.Dot(bodyA.position.normalized, transform.forward.normalized) > 0.5f) {
//			bodyA.gameObject.SetActive(true);
//		} else {
//			bodyA.gameObject.SetActive(false);
//		}
//	}

//		if ((paddle.transform.position, ball.transform.position * dotprodPaddle) > dotProdLimiterMinX){
//				myBody.velocity = myBody.velocity + dotProdPaddleMod;
//				}//end dot prod paddle limiter minX
//		if ((paddle.transform.position, ball.transform.position * dotprodPaddle) > dotProdLimiterMaxX){
//				myBody.velocity = myBody.velocity + dotProdPaddleMod;
//				}//end dot prod paddle limiter maxX
//		if ((paddle.transform.position, ball.transform.position * dotprodPaddle) > dotProdLimiterMinY){
//				myBody.velocity = myBody.velocity + dotProdPaddleMod;
//				}//end dot prod paddle limiter minY
//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMaxY);{
//				myBody.velocity = myBody.velocity + dotProdPaddleMod));
//				}//end dot prod paddle limiter maxY
//			}//end dotprod paddle
//		
//		
//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMinX);{
//				myBody.velocity = myBody.velocity +dotProdWallMod;
//				}//end dot prod wall limiter minX
//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMaxX);{
//				myBody.velocity = myBody.velocity +dotProdWallMod;
//				}//end dot prod wall limiter maxX
//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMinY);{
//				myBody.velocity = myBody.velocity + dotProdWallMod));
//				}//end dot prod wall limiter minY
//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMaxY);{
//				myBody.velocity = myBody.velocity + dotProdWallMod));
//				}//end dot prod wall limiter maxY
//
//			}//end dotprod wall
//// <-- UNTIL HERE
	

		/// //// -->  GET THIS WORKING
		//		float dotprodPaddle = Vector3.Dot (ball.position.normalized, paddle.position.normalized);
		//		if ((paddle.transform.position, ball.transform.position * dotprodPaddle) > dotProdLimiterMinX);{
		//				myBody.velocity = myBody.velocity + dotProdPaddleMod;
		//				}//end dot prod paddle limiter minX
		//		if ((paddle.transform.position, ball.transform.position * dotprodPaddle) > dotProdLimiterMaxX);{
		//				myBody.velocity = myBody.velocity + dotProdPaddleMod;
		//				}//end dot prod paddle limiter maxX
		//		if ((paddle.transform.position, ball.transform.position * dotprodPaddle) > dotProdLimiterMinY);{
		//				myBody.velocity = myBody.velocity + dotProdPaddleMod;
		//				}//end dot prod paddle limiter minY
		//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMaxY);{
		//				myBody.velocity = myBody.velocity + dotProdPaddleMod));
		//				}//end dot prod paddle limiter maxY
		//			}//end dotprod paddle
		//		
		//		float dotprodWall = Vector3.Dot (ball.position.normalized, paddle.position.normalized);
		//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMinX);{
		//				myBody.velocity = myBody.velocity +dotProdWallMod;
		//				}//end dot prod wall limiter minX
		//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMaxX);{
		//				myBody.velocity = myBody.velocity +dotProdWallMod;
		//				}//end dot prod wall limiter maxX
		//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMinY);{
		//				myBody.velocity = myBody.velocity + dotProdWallMod));
		//				}//end dot prod wall limiter minY
		//		if ((wall.transform.position, ball.transform.position * dotprodWall) > dotProdLimiterMaxY);{
		//				myBody.velocity = myBody.velocity + dotProdWallMod));
		//				}//end dot prod wall limiter maxY
		//
		//			}//end dotprod wall
		//// <-- UNTIL HERE
	
	} // END ON COLLISION
		
	public void Reset() {																								// Restart function,  to set the ball position and restart the ball movement
		myBody = GetComponent<Rigidbody2D>();																			// again set the rb to mybody
		transform.position = new Vector2(0,0);																			// and again, no force or movement

		bool right = Random.value > 0.5f; 																				// basic 50/50 chances
		float startingAngle;																							// the starting angle for the reset ball
		if (right) {																									// if it's going right
			startingAngle = Random.Range (-30, 30);																		// then set the range for the angle is goes off from the starting point if it's going right
		} else {																										// else (if its going left)
			startingAngle = Random.Range (150, 210);																	// set the range for the angle is goes off from the starting point if it's going left
		}//end else 
	
		startingAngle *= Mathf.Deg2Rad;																					// starting angle
		myBody.velocity = new Vector2 (Mathf.Cos (startingAngle), Mathf.Sin (startingAngle)) * forceValue * GameManager.instance.amountZoomedOut;

	}//END RESET
}//END SCRIPT
