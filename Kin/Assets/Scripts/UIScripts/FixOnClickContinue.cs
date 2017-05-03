using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FixOnClickContinue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Button> ().onClick.AddListener(() => this.ResumeGame ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ResumeGame() {
		//sceneCont.GetComponent<SceneController>().FadeToMain();
		SceneManager.LoadScene("Main_with_HLD");
	}
}
