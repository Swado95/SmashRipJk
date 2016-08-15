using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public int health = 100;
	public float speed = 5;
	public float jumpF = 365;
	public float wallJumpDelay = .25f;

	private Text healthMeter;
	private Text heightMeter;

	private int startHealth;
	private float baseSpeed;
	private float baseJumpF;

	private Rigidbody2D rb2d;

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

		healthMeter = GameObject.Find ("Health Meter").GetComponent<Text> ();
		heightMeter = GameObject.Find ("Height Meter").GetComponent<Text> ();

		anim = GetComponent<Animator> ();

		SetHealthText ();
	}

	void FixedUpdate () {

		if (!stunned) {

			anim.SetInteger ("animState", 0);

		} else {
			stunned = Time.time - timeOfStun < stunDuration;
		}

		heightMeter.text = "Height: " + Mathf.Round(transform.position.y);

//		if(transform.eulerAngles.z > 45 && !(transform.eulerAngles.z > 315)){
//				transform.eulerAngles = new Vector3(0, 0, 45);
//		}
	}

	void Update () {

		if (Time.time - lastTimeWallJump > wallJumpDelay) {
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		}

		if (Input.GetKey ("a") && !leftWall && Time.time - lastTimeWallJump > wallJumpDelay) {
			rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
			anim.SetInteger ("animState", 1);
			transform.localScale = new Vector3 (-1, 1, 1);
		}

		if (Input.GetKey ("d") && !rightWall && Time.time - lastTimeWallJump > wallJumpDelay) {
			rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
			anim.SetInteger ("animState", 1);
			transform.localScale = new Vector3 (1, 1, 1);
		}
				
		if (Input.GetButtonDown ("Jump")) {
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x - .7f, transform.position.y - .9f), 
				                    Vector2.right, 1.5f);

			if (hit.collider != null && (hit.collider.tag.Equals ("Ground") || hit.collider.tag.Equals ("Enemy"))) {
				rb2d.AddForce (new Vector2 (0, jumpF));
			}
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

		if (Input.GetMouseButtonDown (0)) {
			GetComponent<MeleeAttack> ().Attack ();
		}

		if (Input.GetKeyDown ("f")) {
			GetComponent<RangedAttack> ().Charge ();
		}

		if (Input.GetKeyUp ("f")) {
			GetComponent<RangedAttack> ().Fire ();
		}

		if (Input.GetMouseButtonDown (1)) {
			GetComponent<PowerAttack> ().Charge ();
		}

		if (Input.GetMouseButtonUp (1)) {
			GetComponent<PowerAttack> ().Attack ();
		}
	}

	public void TakeDamage (int damage, Vector2 knockback, float stunDuration) {

		health -= damage;
		rb2d.AddForce (knockback);

		if (stunDuration > 0) {
			timeOfStun = Time.time;
			this.stunDuration = stunDuration;
			stunned = true;
		}

		SetHealthText ();
	}

	void OnCollisionEnter2D (Collision2D col) {

		if (col.gameObject.tag.Equals ("Wall")) {
			
			rb2d.gravityScale = .5f;
			rightWall = col.transform.position.x > transform.position.x ? true : false;
			leftWall = col.transform.position.x > transform.position.x ? false : true;
			if (rb2d.velocity.y > 0) {
				rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
			}
		}

//		if(col.gameObject.tag.Equals("Ground")){
//			transform.eulerAngles = col.transform.eulerAngles;
//		}
	}

	void OnCollisionExit2D (Collision2D col) {

		if (col.gameObject.tag.Equals ("Wall")) {
			rb2d.gravityScale = 1;
			leftWall = false;
			rightWall = false;
		}
	}

	void SetHealthText () {
		healthMeter.text = health + " / " + startHealth + " HP";
	}
}