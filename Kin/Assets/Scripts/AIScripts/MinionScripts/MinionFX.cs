using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MinionFX : MonoBehaviour {

	private AudioSource aud;
	private AudioClip staff;

	public AudioMixerGroup AMG;

	// Use this for initialization
	void Start () {
		aud = gameObject.AddComponent<AudioSource>();
		aud.volume = 1.0f;
		aud.outputAudioMixerGroup = AMG;
		staff = Resources.Load ("Sounds/Attack SFX/Enemy Staff Magic Attack") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void playSound(){
		aud.Play ();
	}

	public void playStaffHit(){
		aud.clip = staff;
		playSound ();
	}
}
