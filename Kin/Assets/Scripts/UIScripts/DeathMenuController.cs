using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour {

	public GameObject sceneCont;

	// Use this for initialization
	void Start () {
		sceneCont.GetComponent<SceneController>().FadeFromDeath();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResumeGame() {
		//sceneCont.GetComponent<SceneController>().FadeToMain();
		SceneManager.LoadScene("Main");
	}

	public void MainMenu() {
		SceneManager.LoadScene("Main Menu");
	}
}
