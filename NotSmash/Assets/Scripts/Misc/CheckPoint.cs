using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public GameObject playerPrefab;

	private GameObject player;
	private bool playerPassed;

	void FixedUpdate () {
	
		if(player == null && playerPassed){
			Instantiate (playerPrefab, transform.position, Quaternion.identity);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {

		if(col.tag.Equals("Player")){
			player = col.gameObject;
			playerPassed = true;
		}
	}
}
