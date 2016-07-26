using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	
	void FixedUpdate () {
	
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else {
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
		}
	}
}