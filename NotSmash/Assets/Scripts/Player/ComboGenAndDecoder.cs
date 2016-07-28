using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboGenAndDecoder : MonoBehaviour{

	private ComboTree comboTree;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		GenerateCombos ();
	}

	void Update () { 

		//key checker
	}

	public void GenerateCombos () {

		List<string> combos = new List<string> ();
		//add combos

		comboTree = new ComboTree (combos);
	}
}