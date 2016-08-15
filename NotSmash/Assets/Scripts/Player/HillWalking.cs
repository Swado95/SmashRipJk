using UnityEngine;
using System.Collections;

public class HillWalking : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float rayDis = 1.0f;
    public float airLerp = 0.02f;
    public float groundSlerp = 0.02f;


	// Use this for initialization
	void Start () 
	{
		
		rb2d = GetComponent<Rigidbody2D> ();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, rayDis, 1 << 10);
        Debug.DrawLine(transform.position,  new Vector2(transform.position.x, transform.position.y + -rayDis));

		if(hit.collider != null && hit.collider.tag == "Ground")
		{
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal), groundSlerp);
            Debug.Log("hit ground");
        }
        else if(hit.collider == null)
        {
            Debug.Log("doing lerp stuff");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0), airLerp);
        }

	}
}
