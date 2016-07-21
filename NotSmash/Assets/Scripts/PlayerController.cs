using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float speed = 5;
	public float jumpF = 365;
	public float wallJumpDelay = .25f;

	private Rigidbody2D rb2d;
	private bool isGrounded;

	public bool leftWall;
	public bool rightWall;
	private float lastTimeWallJump;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.freezeRotation = true;
	}

	void FixedUpdate ()
	{

		if (Time.time - lastTimeWallJump > wallJumpDelay) {
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		}
		if (Input.GetAxis ("Horizontal") < 0 && !leftWall && Time.time - lastTimeWallJump > wallJumpDelay) {
			rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
		}

		if (Input.GetAxis ("Horizontal") > 0 && !rightWall && Time.time - lastTimeWallJump > wallJumpDelay) {
			rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
		}

		if (isGrounded && Input.GetButtonDown ("Jump")) {
			rb2d.AddForce (new Vector2 (0, jumpF));
			isGrounded = false;
		}

		if (leftWall && Input.GetButtonDown ("Jump")) {
			rb2d.AddForce (new Vector2 (200, jumpF));
			rb2d.gravityScale = 1;
			leftWall = false;
			lastTimeWallJump = Time.time;
		}

		if (rightWall && Input.GetButtonDown ("Jump")) {
			rb2d.AddForce (new Vector2 (-200, jumpF));
			rb2d.gravityScale = 1;
			rightWall = false;
			lastTimeWallJump = Time.time;
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag.Equals ("Ground")) {
			isGrounded = true;
		}

		if (col.gameObject.tag.Equals ("Wall")) {
			
			rb2d.gravityScale = .5f;
			rightWall = col.transform.position.x > transform.position.x ? true : false;
			leftWall = col.transform.position.x > transform.position.x ? false : true;
			if(rb2d.velocity.y > 0){
				rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
			}

		}
	}

	void OnCollisionExit2D (Collision2D col){

		if (col.gameObject.tag.Equals ("Wall")) {
			rb2d.gravityScale = 1;
			leftWall = false;
			rightWall = false;
		}
	}
}