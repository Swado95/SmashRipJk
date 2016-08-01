using UnityEngine;
using System.Collections;

public class PowerAttack : PlayerAttack {

	public float maxChargeTime = 3;

	private GameObject chargeBar;
	private BoxCollider2D bc2d;
	private float timeOfRelease;
	private int frame = 4;

	void Start () {
		chargeBar = transform.FindChild("Charge Bar").gameObject;
		bc2d = transform.FindChild ("Sword").GetComponent<BoxCollider2D> ();
	}

	void FixedUpdate () {

		if(timeOfStartAttack > timeOfEndAttack){
			GetComponent<Animator>().SetInteger ("animState", frame);

			float charge = Time.time - timeOfStartAttack;
			if(charge > maxChargeTime){
				charge = maxChargeTime;
			}

			chargeBar.transform.localPosition = new Vector3 (-1, .25f * charge - .75f, 0);
			chargeBar.transform.localScale = new Vector3(1, 10.0f / 3.0f * charge, 1);

			if(frame == 5 && Time.time - timeOfRelease > attackDuration){
				chargeBar.transform.localScale = new Vector3(1, 0, 1);
				timeOfEndAttack = Time.time;
				bc2d.enabled = false;
			}
		}
	}

	public void Charge() {

		if (Time.time - timeOfEndAttack > cooldown) {
			timeOfStartAttack = Time.time;
			frame = 4;
		}
	}

	public override void Attack () {

		if (timeOfStartAttack > timeOfRelease) {
			timeOfRelease = Time.time;
			frame = 5;
			bc2d.enabled = true;
		}
	}
}
