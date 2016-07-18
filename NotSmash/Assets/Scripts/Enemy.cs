using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int health = 100;
	public int damage = 5;
    public bool isAggro = false;
	public float damageMulti = 1;
	public int speed = 4;
    public GameObject target;
    public int jForce = 5;

    private float tStamp = 0;
    private int startHealth = 0;

    void Start(){

        startHealth = health;
    }

	void Update () {

        

        if (emyAggro() == true)
        {
            emyAiMovement(); 
        }

<<<<<<< HEAD
    }

    public void emyAttack(int dmg, float dmgMulti)
=======
    public void emyAttack(int dmg, int dmgMulti)
>>>>>>> origin/master
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
    public void emyAiMovement()
    {

        float dis = Vector2.Distance(target.transform.position, transform.position);

        transform.position = new Vector2(Vector2.MoveTowards(transform.position,
            target.transform.position, speed * Time.deltaTime).x, transform.position.y);

        //enemy only jumps when near target and target is higher then enemy
        if (dis < 10 && (target.transform.position.y > transform.position.y + 5) && tStamp < Time.time)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jForce);
            tStamp = Time.time + 3;
        }
    }

    public bool emyAggro()
    {
        if (health != startHealth)
        {
            isAggro = true;
        }

        return isAggro;
    }
}
