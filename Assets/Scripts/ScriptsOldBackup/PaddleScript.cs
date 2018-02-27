//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//
//public class PaddleScript : MonoBehaviour {
//	
//	[SerializeField] 	//makes it editable in the inspector
//	bool isPlayerTwo;	 //is it player two or not
//	[SerializeField] 	//makes it editable in the inspector
//	float speed = 0.2f;       // paddle speed
//	Transform myTransform;    // reference to the object's transform
//	int direction = 0; // 0 = not moving, 1= up, -1 = down
//	float previousPositionY; //previous y pos of paddle
////	public KeyCode upKey;   // making it variable more easily editable in the inspector
////	public KeyCode downKey; // making it variable more easily editable in the inspector
//	private Rigidbody2D rb; //rigidbody is rb now
//	private CapsuleCollider2D col;
//	public float minDistFromWall;
//
//
//
//	void Start () {
//		myTransform = transform; // define myTransform
////		previousPositionY = myTransform.position.y; //comparison of posistion
//		rb = GetComponent<Rigidbody2D>(); //get that rigidbody
//		col = GetComponent<CapsuleCollider2D>();
//	}//END START
//
//	// FixedUpdate is called once per physics tick/frame
//	void FixedUpdate () {
//		if (isPlayerTwo) { // is this player 2?
//			if (Input.GetKey ("o"))//make o the up key for player2
//				MoveUp (); // call move up
//			else if (Input.GetKey ("l")) //make l the down key for player2
//				MoveDown (); //call move down
//			else {
//				rb.velocity = Vector2.zero; //otherwise don't move
//			}
//		}	//end player 2 control scheme
//		else { // if it's not player 2 (making it player1)
//			if (Input.GetKey ("q")) //make q the up key for player1
//				MoveUp (); //call move up
//			else if (Input.GetKey ("a"))//make a the down key for player2
//				MoveDown (); //call move down
//			else {
//				rb.velocity = Vector2.zero;	//otherwise don't move
//			}
//		}//end player 1 control scheme
////
////		if (previousPositionY > myTransform.position.y) //indicating direction of movement based
////														//on the comparison of the two posistions
////			direction = -1; //move down
////		else if (previousPositionY < myTransform.position.y)
////			direction = 1; //move up
////		else
////			direction = 0; //elso no movement
//		ClampYPos();
//
//	}//END FIXED UPDATE
//		
//	void MoveUp() { // move the player's paddle down by an amount determined by 'speed'
//		//myTransform.position = new Vector2(myTransform.position.x, myTransform.position.y + speed); // move up
//		rb.velocity = new Vector2(0, speed);//simplified upwards movement
//	}//END MOVE UP
//
//	void MoveDown() {// move the player's paddle down by an amount determined by 'speed'
//		//myTransform.position = new Vector2 (myTransform.position.x, myTransform.position.y - speed); //move down
//		rb.velocity = new Vector2(0, -speed); //simplified downwards omvement
//	}//END MOVE DOWN
//
//	void ClampYPos(){
//		float maxYPos = GameManager.instance.topWall.bounds.min.y - col.bounds.extents.y - minDistFromWall;
//		float minYPos = GameManager.instance.bottomWall.bounds.max.y + col.bounds.extents.y + minDistFromWall;
//		transform.position = new Vector2 (transform.position.x, Mathf.Clamp (transform.position.y, minYPos, maxYPos));
//	}
//
////	void LateUpdate() {
////		previousPositionY = myTransform.position.y; //comparing last posistion
////	}//LATE UPDATE
//}//END SCRIPT