using UnityEngine;
using System.Collections;

public class HillWalking : MonoBehaviour {

	private Rigidbody2D rb2d;
    private float boxColWidth;
    private Vector2 avg;

	public float rayDis = 1.0f;
    public float airLerp = 0.02f;
    public float groundSlerp = 0.02f;
    public BoxCollider2D mainCollider;

	// Use this for initialization
	void Start () 
	{
		
		rb2d = GetComponent<Rigidbody2D> ();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        mainCollider = GetComponent<BoxCollider2D>();

        //gets side points for 2 raycasts
        boxColWidth = mainCollider.size.x / 2;

    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        //casts ray from 2 points down
		RaycastHit2D hit1 = Physics2D.Raycast (new Vector2(transform.position.x + boxColWidth, transform.position.y), 
            new Vector2(transform.position.x + boxColWidth, transform.position.y - rayDis), rayDis, 1 << 10);

        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - boxColWidth, transform.position.y), 
            new Vector2(transform.position.x - boxColWidth, transform.position.y - rayDis), rayDis, 1 << 10);

        //takes average of two normals to smooth out transitions
        avg =(hit2.normal + hit1.normal) / 2;
       


        Debug.DrawLine(new Vector2(transform.position.x + boxColWidth, transform.position.y), 
            new Vector2(transform.position.x + boxColWidth, transform.position.y - rayDis));

        Debug.DrawLine(new Vector2(transform.position.x - boxColWidth, transform.position.y), 
            new Vector2(transform.position.x - boxColWidth, transform.position.y - rayDis));


        if ((hit1.collider && hit2.collider != null) && (hit1.collider.tag == "Ground" && hit2.collider.tag == "Ground"))
		{
            // rotates transform to line up with avg normal vector and smooths transition again with slerp
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, avg), groundSlerp);
            Debug.DrawRay(transform.position, avg);
        }

        else if(hit1.collider == null && hit2.collider == null)
        {
            //rotates transform to up right poition if in air
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0), airLerp);
        }

	}
}
