using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 5;

	private Rigidbody2D rb2d;

	void Start () {
	
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
	
		if (Input.GetAxis ("Horizontal") != 0) {
			rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
		} else {
			rb2d.velocity = new Vector2(0, rb2d.velocity.y);
		}
	}
}
