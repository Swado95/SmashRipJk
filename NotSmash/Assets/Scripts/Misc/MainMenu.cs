using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string[] levelNames;
	public GameObject play;
	public GameObject exit;
	public GameObject button;
	public GameObject canvas;

	void Start () {
	
		play.GetComponent<Button> ().onClick.AddListener (PlayClick);
		exit.GetComponent<Button> ().onClick.AddListener (ExitClick);
	}

	void PlayClick() {

		play.SetActive (false);
		exit.SetActive (false);

		for (int i = 0; i < levelNames.Length; i++) {

			GameObject but = (GameObject)Instantiate (button);
			but.transform.SetParent (canvas.transform);
			but.transform.localPosition = new Vector3(0, -i * 50, 0);

			but.transform.FindChild ("Text").GetComponent<Text> ().text = levelNames[i];

			int level = i;
			but.GetComponent<Button> ().onClick.AddListener (() => LevelSelect(level));
		}
	}

	void ExitClick() { 
		Application.Quit ();
	}

	void LevelSelect(int level) {

		SceneManager.LoadScene (levelNames[level]);
	}
}
