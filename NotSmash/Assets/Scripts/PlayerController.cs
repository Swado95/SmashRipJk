using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 5;
    public float jumpF = 365;
	public float wallJumpDelay = .25f;

	private Rigidbody2D rb2d;
	private bool isGrounded;

	private bool leftWall;
	private bool rightWall;
	private float lastTimeWallJump;

	void Start () {

		rb2d = GetComponent<Rigidbody2D>();
		rb2d.freezeRotation = true;
	}
	
	void FixedUpdate () {

		if (Input.GetAxis ("Horizontal") != 0 && !leftWall && !rightWall && Time.time - lastTimeWallJump > wallJumpDelay) {
			rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
		}

        if (isGrounded && Input.GetButtonDown("Jump")) {
			rb2d.AddForce(new Vector2(0, jumpF));
        	isGrounded = false;
        }

		if(leftWall && Input.GetButtonDown("Jump")){
			rb2d.AddForce(new Vector2(200, jumpF));
			leftWall = false;
			lastTimeWallJump = Time.time;
		}

		if(rightWall && Input.GetButtonDown("Jump")){
			rb2d.AddForce(new Vector2(-200, jumpF));
			leftWall = false;
			lastTimeWallJump = Time.time;
		}
    }

    void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Equals("Ground")) {
            isGrounded = true;
        }

		if (col.gameObject.tag.Equals ("LeftWall")) {
			leftWall = true;
		}

		if (col.gameObject.tag.Equals ("RightWall")) {
			rightWall = true;
		}
    }

	void OnCollisionExit2D(Collision2D col){

		if(col.gameObject.tag.Equals("LeftWall")){
			leftWall = false;
		}

		if(col.gameObject.tag.Equals("RightWall")){
			rightWall = false;
		}
	}
}