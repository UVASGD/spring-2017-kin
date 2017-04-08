using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusicController : MonoBehaviour {

	AudioClip loop;
	public float introTime;
	public float timer;
	public bool flipped;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		introTime = this.gameObject.GetComponent<AudioSource> ().clip.length - 13.5f;
		flipped = false;
		loop = Resources.Load ("Sounds/Music/Main Menu Music Looped Part") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (!flipped && timer >= introTime) {
			this.gameObject.GetComponent<AudioSource> ().clip = loop;
			this.gameObject.GetComponent<AudioSource> ().loop = true;
			flipped = true;
			this.gameObject.GetComponent<AudioSource> ().Play ();
		}
	}
}
