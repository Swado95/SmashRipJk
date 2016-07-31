using UnityEngine;
using System.Collections;

public class PowerAttack : PlayerAttack {

	public float maxChargeTime = 3;

	private GameObject chargeBar;

	void Start () {
		chargeBar = transform.FindChild("Charge Bar").gameObject;
	}

	void FixedUpdate () {

		if(timeOfStartAttack > timeOfEndAttack){
			GetComponent<Animator>().SetInteger ("animState", 4);

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

	}
}
