using UnityEngine;
using System.Collections;

public class MeleeAttack : PlayerAttack {

	private BoxCollider2D bc2d;

	void Start () {
		bc2d = GetComponent<BoxCollider2D>();
		bc2d.enabled = false;
	}

	void FixedUpdate () {

		if(timeOfStartAttack > timeOfEndAttack){
			GetComponent<Animator>().SetInteger ("animState", 2);

			if(Time.time - timeOfStartAttack > attackDuration){
				timeOfEndAttack = Time.time;
				bc2d.enabled = false;
			}
		}
	}

	public override void Attack () {

		if(Time.time - timeOfStartAttack > attackDuration && Time.time - timeOfEndAttack > cooldown){
			timeOfStartAttack = Time.time;
			bc2d.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col){

//		Debug.Log("hit an enemy");

		if(col.tag.Equals("Enemy")){
		}
	}
}