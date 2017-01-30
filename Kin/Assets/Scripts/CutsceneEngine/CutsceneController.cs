using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour {

	public GameObject canvas;

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Camera getMainCamera() {
		return Camera.main;
	}

	public GameObject getGameObject(string name) {
		return GameObject.Find(name);
	}

	public void DestroyGameObject(string name) {
		GameObject.Destroy(GameObject.Find(name));
	}

	public void SetToggle(string boolName) {

	}

	public AudioSource getAudioSource() {
		return audioSource;
	}

	public GameObject getMainCanvas() {
		return canvas;
	}
}
