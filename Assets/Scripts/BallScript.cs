using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// --> / <--  = start / end broken stuff I want to get working

// ~~~ 		= old stuff I want to keep just in case

public class BallScript : MonoBehaviour {

	[SerializeField]																	// makes it editable in the inspector
	float forceValue = 4.5f; 															// so we can edit this more easily

	public GameObject newBall; 															// the balls that will be added
	Rigidbody2D myBody;																	// the rigidbody attached to the gameobject
	GoalScript gs;																		// this is a ref to the goalscript
	public PaddleScript[] paddles;														// this a ref to the paddles
	public float radius;																// the radius
	private bool rotating;																// is the ball rotating
	private float angle;																// the angle
	public float rotationSpeed;															// how fast it will rotate

	public Transform paddle1, paddle2, topWall, bottomWall;								// the transforms of the walls and player paddles
	public float wallBounceVelocity;													// ball velocity is multiplied by this number when it hits it 
	public float paddleBounceVelocity;													// ball velocity is multiplied by this number when it hits it 
	public float wallVelocityMod;														// the additional force multiplied to the velocity
	public float paddleVelocityMod;														// the additional force multiplied to the velocity


	[Range(-1.9f, 1.9f)]																// limiter range for the inspector
	public float dotProdLimiterMinY;													// dot prod minY value

	[Range(-1.9f, 1.9f)]																// limiter range for the inspector
	public float dotProdLimiterMaxY;													// dot prod maxY value

	[Range(-1.9f, 1.9f)]																// limiter range for the inspector
	public float dotProdLimiterMinX;													// dot prod minX value

	[Range(-1.9f, 1.9f)]																// limiter range for the inspector
	public float dotProdLimiterMaxX;													// dot prod maxX value

	[Range(-1.9f, 1.9f)]																// modification range for the inspector (will be added to velocity)
	public float dotProdPaddleMod;														// paddle modifier value

	[Range(-1.9f, 1.9f)]																// modification range for the inspector (will be added to velocity)
	public float dotProdWallMod;														// wall modifier value






	// public int [] limiterArray;
	// ~~~ public KeyCode newBall = KeyCode.Space;  //assigning the key for the ball respawn ----USED FOR TESTING BALL RESPAWN



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
		angle += rotationSpeed * Time.deltaTime; 																//rotate it over time
		transform.localPosition = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * radius;					//based on it's previous loctation
	}//END ROTATE

	void OnCollisionEnter2D (Collision2D other) { 																// collision function, change velocity force of ball depending on what it hits
		float dotprodPaddle1 = Vector3.Dot (transform.position.normalized, paddle1.position.normalized); 		// calc of the dot prod of player1 paddle and the ball velocity 
		float dotprodPaddle2 = Vector3.Dot (transform.position.normalized, paddle2.position.normalized);		// calc of the dot prod of player2 paddle and the ball velocity 
		float dotprodTopWall = Vector3.Dot(transform.position.normalized, topWall.position.normalized);			// calc of the dot prod of top wall and the ball velocity 
		float dotprodBottomWall = Vector3.Dot(transform.position.normalized, bottomWall.position.normalized);	// calc of the dot prod of top wall and the ball velocity 

	
		if (other.transform.name == "WallTop") { 																// if the name of the object is 'WallTop' ...

			if (dotprodTopWall < dotProdLimiterMinY 															// if the dot prod is less than the min set Y value 
				|| dotprodTopWall > dotProdLimiterMaxY){														// ...OR is greater than the max set Y value
				wallBounceVelocity = -1* (wallBounceVelocity + dotProdWallMod);									// then the wallBounceVelocity = (itself + the dotproductmodifier) * -1
			}//end top wall dot prod
			myBody.velocity = myBody.velocity * wallBounceVelocity * wallVelocityMod;							// and now the ball's velocity = (itself) * (wallBounceVelocity)* (Velocitymodifier)
			if (rotating) {																						// if it's rotating and it hits a paddle
				rotating = false;																				// set the bool to false
				transform.parent = null;																		// and un-parent it's transform
			}//end if rotating
		}//end wall top bounce force


		if (other.transform.name == "WallBottom") {																// if the name of the object is 'WallBottom' 
		
			if (dotprodBottomWall < dotProdLimiterMinY 															// if the dot prod is less than the min set Y value 
				|| dotprodBottomWall > dotProdLimiterMaxY) {													// ...OR is greater than the max set Y value
				wallBounceVelocity = wallBounceVelocity + dotProdWallMod;										// then the wallBounceVelocity = (itself) + (the dotproductmodifier) 
			}//end bottom wall dot prod
			myBody.velocity = myBody.velocity * wallBounceVelocity * wallVelocityMod;							// and now the ball's velocity = (itself) * (wallBounceVelocity)* (Velocitymodifier)
			if (rotating) {																						// if it's rotating and it hits a paddle
				rotating = false;																				// set the bool to false
				transform.parent = null;																		// and un-parent it's transform
			}//end if rotating
		}//end wall bottom bounce force


		if (other.gameObject.name == "Player1") {																// if the name is player1 
			
			if (dotprodPaddle1 < dotProdLimiterMinX 															// if the dot prod is less than the min set X value 
				|| dotprodPaddle1> dotProdLimiterMaxX) {														// ...OR is greater than the max set X value
				paddleBounceVelocity = paddleBounceVelocity + dotProdPaddleMod;									// then the paddleBounceVelocity = (itself) + (the dotproductmodifier) 								
			}//end paddle1 dot prod
			myBody.velocity = myBody.velocity * paddleBounceVelocity * paddleVelocityMod;						// and now the ball's velocity = (itself) * (wallBounceVelocity)* (Velocitymodifier)
			if (rotating) {																						// if it's rotating and it hits a paddle
				rotating = false;																				// set the bool to false
				transform.parent = null;																		// and un-parent it's transform
			}//end if rotating
		}//end paddle1 bounce force
	
				
		if (other.gameObject.name == "Player2") {																// if the name is player2

			if (dotprodPaddle2 < dotProdLimiterMinX 															// if the dot prod is less than the min set X value 
				|| dotprodPaddle2> dotProdLimiterMaxX) {														// ...OR is greater than the max set X value
				paddleBounceVelocity = -1*(paddleBounceVelocity + dotProdPaddleMod);							// then the paddleBounceVelocity = (itself + the dotproductmodifier) * -1
			}//end paddle2 dot prod
			myBody.velocity = myBody.velocity * paddleBounceVelocity * paddleVelocityMod;						// and now the ball's velocity = (itself) * (wallBounceVelocity)* (Velocitymodifier)
				if (rotating) {																					// if it's rotating and it hits a paddle
				rotating = false;																				// set the bool to false
				transform.parent = null;																		// and un-parent it's transform
			}//end if rotating
		}//end paddle bounce force
	}//END ON COLLISION ENTER 2D


		
	public void Reset() {																						// Restart function,  to set the ball position and restart the ball movement
		myBody = GetComponent<Rigidbody2D>();																	// again set the rb to mybody
		transform.position = new Vector2(0,0);																	// and again, no force or movement

		bool right = Random.value > 0.5f; 																		// basic 50/50 chances
		float startingAngle;																					// the starting angle for the reset ball
		if (right) {																							// if it's going right
			startingAngle = Random.Range (-30, 30);																// then set the range for the angle is goes off from the starting point if it's going right
		} else {																								// else (aka, if its going left)
			startingAngle = Random.Range (150, 210);															// set the range for the angle is goes off from the starting point if it's going left
		}//end else
	
		startingAngle *= Mathf.Deg2Rad;																			// starting angle
		myBody.velocity = new Vector2 (Mathf.Cos (startingAngle), Mathf.Sin (startingAngle)) * forceValue; 		// ***?*?*?***

//	--->	myBody.velocity = new Vector2 (Mathf.Cos (startingAngle), Mathf.Sin (startingAngle)) * forceValue * GameManager.instance.amountZoomedOut; //nul ref exception, not set to inistance of object

	}//END RESET
}//END SCRIPT
