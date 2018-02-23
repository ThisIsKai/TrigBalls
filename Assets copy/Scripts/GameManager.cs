using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {

	int playerOneScore, playerTwoScore;
	//Text ScoreText;
	private List<BallScript> balls;
	public GameObject ballPrefab;
	public Vector3 ballSpawnPoint;
	public float zoomOutFactor;
	public Transform walls;
	private float paddleDistance;
	public PaddleScript[] paddles;
	private GoalScript[] goals;
//	public  SceneManager[] sm;
	public float amountZoomedOut;
	private float baseOrthoSize;
	public BoxCollider2D topWall;
	public BoxCollider2D bottomWall;

	public static GameManager instance = null;
	public int score = 0;
	public int highScore = 0;

	void Awake(){
		instance = this;
	}

	void Start () {
//	sm = SceneManager;  //assign a abbriviation for scene manager
//		ScoreText; // the text component
		goals = walls.GetComponentsInChildren<GoalScript> (); //find the goals by finding the objects with the goal script attached to them
		paddleDistance = paddles [0].transform.position.x - goals [0].GetComponent<BoxCollider2D> ().bounds.max.x; //find the distance between the paddles and the goal walls
		balls = new List<BallScript> (); //make a list to keep all the new balls in
		baseOrthoSize = Camera.main.orthographicSize;
		if (PlayerPrefs.HasKey ("highScoreOnDisk")) {
			highScore + PlayerPrefs.GetInt ("highScoreOnDisk");

			StartGame (); //run the start function

//
//		if (instance == null) {
//			instance = this;
//			DontDestroyOnLoad(gameObject);
//		}//end if
//		else {
//			instance.playerOneScore = 0;
//			instance.playerTwoScore = 0;
//			Destroy(gameObject); 
//		}//end else
		
		}

		}//END START



		void Update () {
		ScoreText = "Score" + "Player One" + playerOneScore + "Player Two" + playerTwoScore; //tell the text what to display
		if (Input.GetKeyDown (KeyCode.Space)){ //if the spacebar is pressed then
		StartGame ();//start the game
			}
	}//END UPDATE

	void GameOver()	{

		PlayerPrefs.SetInt ("highScoreOnDisk", highScore);
		PlayerPrefs.Save ();

			if (playerOneScore > playerTwoScore) {//if playerone's score is greater than playertwo's
			playerOneScore = highScore;
					SceneManager.LoadScene("PlayerOneWinner"); //then load the 'PlayerOneWinner' scene 
		}//end if1
	
			else {
					SceneManager.LoadScene("PlayerTwoWinner"); //then load the 'PlayerTwoWinner's scene 
			playerTwoScore = highScore;

			}//end if2
			PlayerPrefs.SetInt ("highScoreOnDisk", highScore);
			PlayerPrefs.Save ();
	}//END GAMEOVER

	void StartGame(){
		for (int i = balls.Count -1; i >= 0; i--) {  //standard for loop but shrinking because of the flux in ball list size
			DestroyBall (balls [i]);
		}//end for ball loop
//		playerOneScore = 0; //set player one's starting score to 0
//		playerTwoScore = 0;//set player two's starting score to 0
		balls = new List<BallScript>();  //new list of balls
		BallScript startingBall = Instantiate (ballPrefab).GetComponent<BallScript> (); //instantiate the starting ball?
		balls.Add (startingBall); //add to the starting ball?
		amountZoomedOut = 1; //how much is it zoomed out
		SetZoom (); //run set zoom function
		startingBall.Reset (); //find the starting ball and reset it

	}//END STARTGAME

	public void GoalScored(int playerNumber, BallScript scoringBall) { // increase the score for whichever player scored
		AddBall(scoringBall);  //run add ball function on the scoring ball
		if (playerNumber == 1)
			GameManager.instance.playerOneScore++;
		else if (playerNumber == 2)
			GameManager.instance.playerTwoScore++;
		// then check if the player has won
		if (playerOneScore >= 10)
			GameOver ();
			
		else if (playerTwoScore >= 10) 
			GameOver ();

	}//END GOAL SCORED


	void AddBall (BallScript parentBall) {

		GameObject newBallObj = Instantiate(ballPrefab, parentBall.transform); //am i actually instantiating this shit?????*?*?*
		//newBallObj.transform.position = CircleUtility.PointOnCircle (bigBall.transform.position.x, bigBall.transform.position.y);
		BallScript newBall = newBallObj.GetComponent<BallScript>();  
		newBall.Init ();

		balls.Add (newBall);
		ZoomOut ();
		Debug.Log ("adding ball");

	}//END IF NEW BALL

	void DestroyBall(BallScript ball){
		balls.Remove (ball);
		Destroy (ball.gameObject);
	}//END DESTROY BALL

	void ZoomOut(){
		amountZoomedOut *= zoomOutFactor;
		SetZoom ();
	}

	void SetZoom(){
		Camera.main.orthographicSize = baseOrthoSize * amountZoomedOut;
		walls.localScale = amountZoomedOut * Vector3.one;
		float paddleXPos1 = goals [0].GetComponent<BoxCollider2D> ().bounds.max.x + paddleDistance;
		paddles [0].transform.position = new Vector3 (paddleXPos1, paddles [0].transform.position.y, paddles [0].transform.position.z);
		float paddleXPos2 = goals [1].GetComponent<BoxCollider2D> ().bounds.min.x - paddleDistance;
		paddles [1].transform.position = new Vector3 (paddleXPos2, paddles [1].transform.position.y, paddles [1].transform.position.z);
//		for (int i = 0; i < paddles.Length; i++) {
//			float xPos;
//			if(i == 0){
//				xPos = goals [0].GetComponent<BoxCollider2D> ().bounds.max.x + paddleDistance;
//			}
//			else{
//				xPos = goals [1].GetComponent<BoxCollider2D> ().bounds.min.x - paddleDistance;
//			}
//			paddles [i].transform.position = new Vector3 (xPos, paddles [i].transform.position.y, paddles [i].transform.position.z);
//		}
	}//END ZOOM OUT
		
}//END SCRIPT

