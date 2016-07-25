using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	private int damage;
	private Vector2 velocity;
	private float startTime;
	private float activeTime = 2;

	void Awake(){
		Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), 
			GetComponent<BoxCollider2D>());
	}

	void Start () {
		startTime = Time.time;
	}

	void FixedUpdate(){

		if(Time.time - startTime > activeTime){
			Destroy(gameObject);
		}
	}

	public void SetDamageVelocity(int damage, Vector2 velocity){

		this.damage = damage;
		this.velocity = velocity;

		if(velocity.x < 0){
			transform.localScale = new Vector3(-1, 1, 1);
		}

		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D col){

		if(col.gameObject.tag.Equals("Enemy")){
			//do enemy damage
		}
	}
}
