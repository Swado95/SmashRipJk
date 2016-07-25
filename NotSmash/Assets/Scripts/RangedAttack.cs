using UnityEngine;
using System.Collections;

public class RangedAttack : PlayerAttack {

	public GameObject arrow;
	
	void FixedUpdate () {

		if(timeOfStartAttack > timeOfEndAttack){
			GetComponent<Animator>().SetInteger ("animState", 3);

			if(Time.time - timeOfStartAttack > attackDuration){
				timeOfEndAttack = Time.time;
			}
		}
	}

	public override void Attack () {

		if(Time.time - timeOfStartAttack > attackDuration && Time.time - timeOfEndAttack > cooldown){
			timeOfStartAttack = Time.time;

			GameObject arr = (GameObject)Instantiate(arrow, transform.position, Quaternion.identity);
			arr.GetComponent<Arrow> ().SetDamageVelocity (damage, new Vector2(5 * transform.localScale.x, 0));
		}
	}
}
