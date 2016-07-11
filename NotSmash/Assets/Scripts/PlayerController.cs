using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 5;
    public float jumpF = 2;
    public bool isGrounded;

	private Rigidbody2D rb2d;

	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
	
		if (Input.GetAxis ("Horizontal") != 0) {
			rb2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), rb2d.velocity.y);
		}

        if (isGrounded == true && Input.GetButtonDown("Jump"))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpF);
                isGrounded = false;
            }
        }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "isGround")
        {
            isGrounded = true;
        }
    }
}

