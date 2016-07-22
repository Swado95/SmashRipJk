﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int health = 100;
	public int damage = 5;
    public bool isAggro = false;
	public float damageMulti = 1;
	public int speed = 4;
    public int jForce = 5;

	private Rigidbody2D rb2d;
	private GameObject target;
    private float jumpCol = 0;
    private float attackCol = 0;
    private int startHealth = 0;
    private ParticleSystem par;


    void Start(){

        startHealth = health;
		rb2d = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag("Player");
        par = GetComponent<ParticleSystem>();
        
    }

	void FixedUpdate () {

        Attack(5, 1);


		if (isAggro)
        {
            Movement(); 
        }
    }





    void Attack(int dmg, float dmgMulti)
    {
        

        float rad = 1;

        RaycastHit2D bCast = Physics2D.CircleCast(transform.position, rad, Vector2.zero);

		if(bCast.collider != null && bCast.collider.tag.Equals("Player") && attackCol < Time.time)
        {
            target.GetComponent<PlayerController>().TakeDamage(dmg, Vector2.zero, 0);
            Debug.Log(target.GetComponent<PlayerController>().health);
            par.Play();
            attackCol = Time.time + 3;
        }        
    }





    //__________________AI Movement_________________________
    void Movement()
	{
		float dis = Mathf.Abs(target.transform.position.x - transform.position.x);

        if (dis < .1f)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        else
        {
           	rb2d.velocity = new Vector2(speed * (transform.position.x < target.transform.position.x ? 1 : -1), rb2d.velocity.y);
        }

        //enemy only jumps when near target and target is higher then enemy
		
        if (dis < 10 && (target.transform.position.y > transform.position.y + 5) && jumpCol < Time.time)
        {
            rb2d.AddForce(Vector2.up * jForce);
            jumpCol = Time.time + 3;
        }
    }
}
