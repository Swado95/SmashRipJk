using UnityEngine;
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
    private float tStamp = 0;
    private int startHealth = 0;

    void Start(){

        startHealth = health;
		rb2d = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag("Player");
    }

	void FixedUpdate () {

		if (isAggro)
        {
            Movement(); 
        }
    }

    void Attack(int dmg, float dmgMulti)
    {
        damage = dmg;
        damageMulti = dmgMulti;
        float rad = 1.0f;

        RaycastHit2D bCast = Physics2D.CircleCast(transform.position, rad, Vector2.right);

        if(bCast.transform.tag == "Player")
        {
            //player health script   
        }        
    }

    //__________________AI Movement_________________________
    void Movement()
	{
		rb2d.velocity = new Vector2(speed * (transform.position.x < target.transform.position.x ? 1 : -1), rb2d.velocity.y);

        //enemy only jumps when near target and target is higher then enemy
		float dis = Vector2.Distance(target.transform.position, transform.position);
        if (dis < 10 && (target.transform.position.y > transform.position.y + 5) && tStamp < Time.time)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jForce);
            tStamp = Time.time + 3;
        }
    }
}
