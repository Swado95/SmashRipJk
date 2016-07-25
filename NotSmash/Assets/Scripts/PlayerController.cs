using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public int health = 100;
	public float speed = 5;
	public float jumpF = 365;
	public float wallJumpDelay = .25f;

    private Text uIDisplay;

    private int startHealth;
	private float baseSpeed;
	private float baseJumpF;

	private Rigidbody2D rb2d;
	private bool isGrounded;

	private Animator anim;
	private bool attacking;

	private bool leftWall;
	private bool rightWall;
	private float lastTimeWallJump;

	private bool stunned;
	private float timeOfStun;
	private float stunDuration;
    

	void Start () {

		startHealth = health;
     	baseSpeed = speed;
		baseJumpF = jumpF;

		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.freezeRotation = true;
        uIDisplay = GameObject.Find("HealthMeter").GetComponent<Text>();
        
		anim = GetComponent<Animator> ();

        SetHealthText();
	}

	void FixedUpdate () {

        if (!stunned) {

			anim.SetInteger("animState", 0);

			if (Time.time - lastTimeWallJump > wallJumpDelay) {
				rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			}

			if (Input.GetKey ("a") && !leftWall && Time.time - lastTimeWallJump > wallJumpDelay) {
				rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
				anim.SetInteger("animState", 1);
				transform.localScale = new Vector3 (-1, 1, 1);
			}
	
			if (Input.GetKey ("d") && !rightWall && Time.time - lastTimeWallJump > wallJumpDelay) {
				rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
				anim.SetInteger("animState", 1);
				transform.localScale = new Vector3 (1, 1, 1);
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

			if (Input.GetMouseButtonDown(0)){
				GetComponent<PlayerAttack> ().Attack ();
//				anim.SetInteger("animState", 2);
			}

		} else {
			stunned = Time.time - timeOfStun < stunDuration;
		}

//		if(transform.eulerAngles.z > 45 && !(transform.eulerAngles.z > 315)){
//			transform.eulerAngles = new Vector3(0, 0, 45);
//		}
    }

	public void TakeDamage(int damage, Vector2 knockback, float stunDuration){

		health -= damage;
		rb2d.AddForce(knockback);

		if(stunDuration > 0){
			timeOfStun = Time.time;
			this.stunDuration = stunDuration;
			stunned = true;
		}

		SetHealthText ();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag.Equals ("Ground") || col.gameObject.tag.Equals("Enemy")) {
			isGrounded = true;
//			rb2d.freezeRotation = false;
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

		if (col.gameObject.tag.Equals ("Ground") || col.gameObject.tag.Equals("Enemy")) {
			isGrounded = false;
//			rb2d.freezeRotation = true;
		}
	}

    void SetHealthText () {
        uIDisplay.text = health + " / " + startHealth + " HP";
    }
}