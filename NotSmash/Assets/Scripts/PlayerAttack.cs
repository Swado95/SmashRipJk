using UnityEngine;
using System.Collections;

public abstract class PlayerAttack : MonoBehaviour{

	public int damage;
	public Vector2 knockback;
	public float stunDuration;
	public float cooldown;
	public float attackDuration;

	public float timeOfStartAttack;
	public float timeOfEndAttack;

	public abstract void Attack();
}