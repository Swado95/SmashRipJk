using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public int health = 100;
    public int damage = 5;
    public bool isAggro = false;
    public bool keepDistance = false;
    public float damageMulti = 1;
    public int speed = 4;
    public int jForce = 5;
    public float attackRate = 3;
    public float hugDistance = 1;
    public float attackDistance = 1;
    public float runDistance = 1;


    private Rigidbody2D rb2d;
    private GameObject target;
    private float jumpCol = 0;
    private float attackCol = 0;
    private int startHealth = 0;
    private BoxCollider2D col2d;
    private float aS = 1f;
    private float timeOfStun;
    private float stunDuration;
    private bool stunned;


    void Start()
    {

        startHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        col2d = GetComponent<BoxCollider2D>();
        aS = col2d.size.x * rb2d.transform.lossyScale.x;
    }

    void FixedUpdate()
    {

        Attack(5, 1);


        if (isAggro)
        {
            Movement(keepDistance);
        }
    }

    void Attack(int dmg, float dmgMulti)
    {

        RaycastHit2D bCast = Physics2D.CircleCast(transform.position, (aS / 2) + attackDistance, Vector2.zero, (aS / 2) + attackDistance, ~8);

        if (bCast.collider != null && bCast.collider.tag.Equals("Player") && attackCol < Time.time)
        {
            target.GetComponent<PlayerController>().TakeDamage(dmg, Vector2.zero, 0);
            Debug.Log(target.GetComponent<PlayerController>().health);
            attackCol = Time.time + attackRate;
        }
    }

    //__________________AI Movement_________________________
    void Movement(bool fleeDis)
    {
        float dis = Mathf.Abs(target.transform.position.x - transform.position.x);

        Debug.Log(aS + " " + dis);




        if (fleeDis == false)
        {
            if (dis < aS + hugDistance)
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
        else
        {
            if (dis > runDistance)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            else
            {
                //if true go to -1 else go to 1                      V    V
                rb2d.velocity = new Vector2(speed * (transform.position.x > target.transform.position.x ? 1 : -1), rb2d.velocity.y);
            }
        }

    }


    public void EnemyTakeDamage(int damage, Vector2 knockback, float stunDuration)
    {

        health -= damage;
        rb2d.AddForce(knockback);

        if (stunDuration > 0)
        {
            timeOfStun = Time.time;
            this.stunDuration = stunDuration;
            stunned = true;
        }

    }
}