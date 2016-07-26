using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	private int damage;
	private float startTime;
	private float activeTime = 3;
	private Rigidbody2D rb2d;

	void Awake(){
		Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), 
			GetComponent<BoxCollider2D>());
	}

	void Start () {
		startTime = Time.time;
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){

		if(Time.time - startTime > activeTime){
			Destroy(gameObject);
		}

		if(rb2d != null){
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x));
		}
	}

	public void SetDamageVelocity(int damage, Vector2 velocity){

		this.damage = damage;
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D col){

		if(col.gameObject.tag.Equals("Enemy")){
			//do enemy damage
		}

		Destroy (rb2d);
	}
}
