﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour {

	public GameObject sceneCont;

	// Use this for initialization
	void Start () {
		sceneCont.GetComponent<SceneController>().FadeFromDeath();
		AudioSource aSource = Camera.main.GetComponent<AudioSource>();
		string clipName = "Sounds/Player SFX/Death Sound Brian " + Random.Range(1, 3);
		AudioClip c = Resources.Load(clipName) as AudioClip;
		aSource.clip = c;
		aSource.GetComponent<AudioSource>().Play();
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
