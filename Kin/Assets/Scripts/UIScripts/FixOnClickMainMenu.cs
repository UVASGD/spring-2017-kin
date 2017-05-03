using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FixOnClickMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Button> ().onClick.AddListener (() => this.MainMenu ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MainMenu() {
		SceneManager.LoadScene("Main Menu");
	}
}
