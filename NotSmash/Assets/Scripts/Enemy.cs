using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float emy_health = 100;
    public float emy_dmg = 5;
    public bool emy_isAgro = false;
    public float emy_dmgMuti = 1;
    public float emy_speed = 4;
    public GameObject target;



	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
	
       if(emy_isAgro == true)
        {
            transform.position = new Vector2(Vector2.MoveTowards(transform.position, 
                target.transform.position, emy_speed * Time.deltaTime).x , transform.position.y);

        }
        

	}

    public void emyAttack(float __dmg, float __dmgMuti)
    {
        __dmg = emy_dmg;
        __dmgMuti = emy_dmgMuti;
        float rad = 1.0f;

        RaycastHit2D bCast = Physics2D.CircleCast(transform.position, rad, Vector2.right);
        if(bCast.transform.tag == "Player")
        {
            //player health script   
        }
        
    }
}
