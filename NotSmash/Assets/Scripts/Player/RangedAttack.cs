using UnityEngine;
using System.Collections;

public class RangedAttack : PlayerAttack {

	public float maxChargeTime = 3;
	public float timeSpeedMultiplyer = 1;
	public GameObject arrow;

	private GameObject chargeBar;

	void Start () {
		chargeBar = transform.FindChild("Charge Bar").gameObject;
	}

	void FixedUpdate () {

		if(timeOfStartAttack > timeOfEndAttack){
			GetComponent<Animator>().SetInteger ("animState", 3);

			float charge = Time.time - timeOfStartAttack;
			if(charge > maxChargeTime){
				charge = maxChargeTime;
			}

			chargeBar.transform.localPosition = new Vector3 (-1, .25f * charge - .75f, 0);
			chargeBar.transform.localScale = new Vector3(1, 10.0f / 3.0f * charge, 1);
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
			chargeBar.transform.localScale = new Vector3(1, 0, 1);
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
