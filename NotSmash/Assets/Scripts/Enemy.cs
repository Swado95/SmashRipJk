using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int health = 100;
	public int damage = 5;
    public bool isAggro = false;
	public int damageMulti = 1;
	public int speed = 4;
    public GameObject target;
		
	void Update () {
	
       if(isAggro == true)
        {
            transform.position = new Vector2(Vector2.MoveTowards(transform.position, 
                target.transform.position, speed * Time.deltaTime).x , transform.position.y);
        }
	}

    public void emyAttack(int dmg, int dmgMulti)
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
}
