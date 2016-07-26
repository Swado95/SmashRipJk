using UnityEngine;
using System.Collections;

public class RangedAttack : PlayerAttack {

	public float timeSpeedMultiplyer = 1;
	public GameObject arrow;

	void FixedUpdate () {

		if(timeOfStartAttack > timeOfEndAttack){
			GetComponent<Animator>().SetInteger ("animState", 3);
		}
	}

	public void Charge() {

		if (Time.time - timeOfEndAttack > cooldown) {
			timeOfStartAttack = Time.time;
		}
	}

	public void Fire() {

		if (timeOfStartAttack > timeOfEndAttack) {
			timeOfEndAttack = Time.time;
			attackDuration = timeOfEndAttack - timeOfStartAttack;
			Attack();
		}
	}

	public override void Attack () {

		Vector3 vec = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		GameObject arr = (GameObject)Instantiate(arrow, transform.position,
			Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(vec.y - transform.position.y, vec.x - transform.position.x)));

		float magnitude = Mathf.Sqrt(Mathf.Pow (vec.x - transform.position.x, 2) + Mathf.Pow (vec.y - transform.position.y, 2));

		arr.GetComponent<Arrow> ().SetDamageVelocity (damage, 
			new Vector2(vec.x - transform.position.x, vec.y - transform.position.y) * 
			attackDuration * timeSpeedMultiplyer / magnitude);
	}
}
