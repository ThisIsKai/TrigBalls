using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleScript : MonoBehaviour {
	
	[SerializeField] 																// makes it editable in the inspector
	bool isPlayerTwo;																// is it player two or not

	[SerializeField] 																// makes it editable in the inspector
	float speed = 0.2f;      	 													// paddle speed

	Transform myTransform;															// reference to the object's transform
	int direction = 0; 																// 0 = not moving, 1= up, -1 = down
	float previousPositionY;														// previous y pos of paddle
	private Rigidbody2D rb; 														// rigidbody is rb now
	private CapsuleCollider2D col;
	public float minDistFromWall;

// ~~~ public KeyCode upKey;														// making it variable more easily editable in the inspector
// ~~~ public KeyCode downKey;														// making it variable more easily editable in the inspector
		

	void Start () {																	// Start function
		myTransform = transform; 													// define myTransform
		rb = GetComponent<Rigidbody2D>(); 											// get that rigidbody
		col = GetComponent<CapsuleCollider2D>();									// get that collider
	}//END START


	void FixedUpdate () {															// FixedUpdate is called once per physics tick/frame
		if (isPlayerTwo) {															// is this player 2?
			if (Input.GetKey ("o"))													// make o the up key for player2
				MoveUp (); 															// call move up
			else if (Input.GetKey ("l")) 											// make l the down key for player2
				MoveDown (); 														// call move down
			else {																	// else
				rb.velocity = Vector2.zero; 										// otherwise don't move
			} //end else not moving
		} //end player 2 control scheme
	
		else { 																		// if it's not player 2 (making it player1)
			if (Input.GetKey ("q")) 												// make q the up key for player1
				MoveUp ();															// call move up
			else if (Input.GetKey ("a"))											// make a the down key for player2
				MoveDown (); 														// call move down
			else {																	// else
				rb.velocity = Vector2.zero;											// otherwise don't move
			} //end else not moving
		} //end player 1 control scheme

		ClampYPos();																// and run clamp y function

	}//END FIXED UPDATE
		
	void MoveUp() { 																// MoveUp function, to move paddle up, effected by 'speed'
		rb.velocity = new Vector2(0, speed);										// simplified upwards movement
	}//END MOVE UP

	void MoveDown() {																// MoveDown function, to move paddle down,effected by 'speed'
		rb.velocity = new Vector2(0, -speed); 										// simplified downwards momvement
	}//END MOVE DOWN

	void ClampYPos(){
		float maxYPos = GameManager.instance.topWall.bounds.min.y - col.bounds.extents.y - minDistFromWall;				// find the distance from top wall
		float minYPos = GameManager.instance.bottomWall.bounds.max.y + col.bounds.extents.y + minDistFromWall; 			// find the distance from the bottom wall
		transform.position = new Vector2 (transform.position.x, Mathf.Clamp (transform.position.y, minYPos, maxYPos));	// make those the bounds for paddle movement
	}//END CLAMP Y POS

}//END SCRIPT