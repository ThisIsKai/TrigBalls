using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
	
	int playerOneScore, playerTwoScore; 					// player one score and player two score
// --> Text ScoreText;										// the text that displays the code
	private List<BallScript> balls;							// list of balls
	public GameObject ballPrefab;							// the ball prefab
	public Vector3 ballSpawnPoint;							// where the ball spawns
	public float zoomOutFactor; 							// the amount that the camera zooms out each time
	public Transform walls; 								// the walls posistions
	public PaddleScript[] paddles; 							// the paddles script
	private float paddleDistance; 							// the distance that the paddles are from the left and right walls
	private GoalScript[] goals; 							// the goal script
	public float amountZoomedOut;							// the total amount that the camera is zoomed out
	private float baseOrthoSize; 							// the original start size of the camera frame
	public BoxCollider2D topWall; 							// the top wall collider
	public BoxCollider2D bottomWall;						// the bottom wall collider
	public static GameManager instance = null;				// ***?*?*?***
	public int score = 0;									// the starting score
	public int highScore = 0;								// the starting high score


	void Awake(){																										// Awake function
		instance = this;																								// this instance of the Awake function
	}//END AWAKE

	void Start () {																										// Start function
// -->	ScoreText; 																										// the text component
		goals = walls.GetComponentsInChildren<GoalScript> (); 															// define 'goals' as the components in the parent object 'walls' that have the goal script attached												// find the goals by finding the objects with the goal script attached to them
		paddleDistance = paddles [0].transform.position.x - goals [0].GetComponent<BoxCollider2D> ().bounds.max.x; 		// find the distance between the paddles and the goal walls
		balls = new List<BallScript> ();																				// make a list to keep all the new balls in
		baseOrthoSize = Camera.main.orthographicSize;																	// define 'baseOrthoSize' as whatever the main camera orthographic size is at the start ofthe game (hence it being in the 'Start' function) 
		if (PlayerPrefs.HasKey ("highScoreOnDisk")) {																	// ***?*?*?***
//	-->		highScore + PlayerPrefs.GetInt ("highScoreOnDisk");															// ***?*?*?***
		}//end playerprefs for high score
			StartGame ();																								// and run the start function

			// ~~~	if (instance == null) {
			//	~~~		instance = this;
			//	~~~		DontDestroyOnLoad(gameObject);
			//	~~~	}//end if
			//	~~~	else {
			//	~~~		instance.playerOneScore = 0;
			//	~~~		instance.playerTwoScore = 0;
			//	~~~		Destroy(gameObject); 
			//	~~~	} //end else

		}//END START



		void Update () { 																						// Update function
// -->	ScoreText = "Score" + "Player One" + playerOneScore + "Player Two" + playerTwoScore; 					// --> tell the text what to display
		if (Input.GetKeyDown (KeyCode.Space)){ 																	// if the spacebar is pressed then
		StartGame ();																							// start the game
			} //end if spacebar pressed
	}//END UPDATE

	void GameOver()	{																							// GameOver function

			if (playerOneScore > playerTwoScore) {																// if playerone's score is greater than player two's
			playerOneScore = highScore;																			// then the high score value is defined by player one's score
// -->		SceneManager.LoadScene("PlayerOneWinner"); 															// --> and then load the 'PlayerOneWinner' scene 
		}//end if1
	
			else {
				playerTwoScore = highScore;																		// then the high score value is defined by player two's score
// -->			SceneManager.LoadScene("PlayerTwoWinner");														// --> and then load the 'PlayerTwoWinner's scene 

			}//end if2
	
		PlayerPrefs.SetInt ("highScoreOnDisk", highScore);														// set the high score
		PlayerPrefs.Save ();																					// save the new high score 

	}//END GAMEOVER

	void StartGame(){ 																							// StartGame function
		for (int i = balls.Count -1; i >= 0; i--) {																// standard for loop but shrinking because of the flux in ball list size
			DestroyBall (balls [i]);																			// Destroy the ball defined in the array
		}//end for ball loop

// -->		playerOneScore = 0; 																				// -->set player one's starting score to 0
// -->		playerTwoScore = 0;																					// -->set player two's starting score to 0
		balls = new List<BallScript>(); 																		// new list of balls
		BallScript startingBall = Instantiate (ballPrefab).GetComponent<BallScript> (); 						// instantiate the starting ball?
		balls.Add (startingBall); 																				// add to the starting ball?
		amountZoomedOut = 1; 																					// how much is it zoomed out
		SetZoom ();																								// run set zoom function
		startingBall.Reset ();																					// find the starting ball and reset it

	}//END STARTGAME

	public void GoalScored(int playerNumber, BallScript scoringBall) { 											// increase the score for whichever player scored
		AddBall(scoringBall); 																					// run add ball function on the scoring ball
		if (playerNumber == 1)																					// if it's player one
			GameManager.instance.playerOneScore++;																// then add 1 to player one's score
		else if (playerNumber == 2)																				// if it's player two
			GameManager.instance.playerTwoScore++;																// then add 1 to player two's score
		// then check if the player has won
	
		if (playerOneScore >= 10)																				// if player one's score is 10 or more
			GameOver ();																						// then run the GameOver function
			
		else if (playerTwoScore >= 10)																			// if player two's score is 10 or more
			GameOver ();																						// then run the GameOver function

	}//END GOAL SCORED


	void AddBall (BallScript parentBall) { 																		// AddBall function

		GameObject newBallObj = Instantiate(ballPrefab, parentBall.transform);									// instantiate the ball and pair it to the parentBall's transform posistion
// ~~~	newBallObj.transform.position = CircleUtility.PointOnCircle (bigBall.transform.position.x, bigBall.transform.position.y);
		BallScript newBall = newBallObj.GetComponent<BallScript>();  											// ***?*?*?***
		newBall.Init (); 																						// and run the Init function on the new ball
		balls.Add (newBall); 																					// add a new ball
		ZoomOut (); 																							// run the ZoomOut function
		Debug.Log ("AddBall works"); 																			// Debug, "AddBall works"
	}//END ADDBALL

	void DestroyBall(BallScript ball){ 																			// DestroyBall function
		balls.Remove (ball);																					// remove the ball?
		Destroy (ball.gameObject);																				// destroy the ball?
	}//END DESTROY BALL

	void ZoomOut(){ 																							// ZoomOut function
		amountZoomedOut *= zoomOutFactor;																		// ***?*?*?***
		SetZoom ();																								// run the SetZoom function
	}// END ZOOMOUT

	void SetZoom(){ 																							// SetZoom function
		Camera.main.orthographicSize = baseOrthoSize * amountZoomedOut;											// take the base orto size of the camera and then multiply it by the amout we want to zoom
		walls.localScale = amountZoomedOut * Vector3.one;														// 
		float paddleXPos1 = goals [0].GetComponent<BoxCollider2D> ().bounds.max.x + paddleDistance;				// 
		paddles [0].transform.position = new Vector3 (paddleXPos1, paddles [0].transform.position.y, paddles [0].transform.position.z);
		float paddleXPos2 = goals [1].GetComponent<BoxCollider2D> ().bounds.min.x - paddleDistance;
		paddles [1].transform.position = new Vector3 (paddleXPos2, paddles [1].transform.position.y, paddles [1].transform.position.z);
							// ~~~	for (int i = 0; i < paddles.Length; i++) {
							// ~~~	float xPos;
							// ~~~	if(i == 0){
							// ~~~	xPos = goals [0].GetComponent<BoxCollider2D> ().bounds.max.x + paddleDistance;
							// ~~~	}
							// ~~~	else{
							// ~~~	xPos = goals [1].GetComponent<BoxCollider2D> ().bounds.min.x - paddleDistance;
							// ~~~	}
							// ~~~	paddles [i].transform.position = new Vector3 (xPos, paddles [i].transform.position.y, paddles [i].transform.position.z);
							// ~~~	}
	}//END ZOOM OUT
		
}//END SCRIPT

