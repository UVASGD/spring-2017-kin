using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResumeGame() {
		SceneManager.LoadScene("Main");
	}

	public void MainMenu() {
		SceneManager.LoadScene("Main Menu");
	}
}
