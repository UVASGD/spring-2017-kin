using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class FXHandler : MonoBehaviour {

	private AudioSource aud;
	private AudioClip right_dirt;
	private AudioClip left_dirt;
	private AudioClip right_grass;
	private AudioClip left_grass;
	private AudioClip roll;

	private List<AudioClip> atkLows;
	private List<AudioClip> hurtSounds;

	public AudioMixerGroup AMG;

	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<AudioSource> () == null) {
			aud = gameObject.AddComponent<AudioSource> ();
		} else {
			aud = gameObject.GetComponent<AudioSource> ();
		}
		aud.volume = 0.9f;
		right_dirt = Resources.Load ("Sounds/Player_FX/Footsteps_Dirt_Right") as AudioClip;
		left_dirt = Resources.Load("Sounds/Player_FX/Footsteps_Dirt_Left") as AudioClip;
		right_grass = Resources.Load("Sounds/Player_FX/Footsteps_Grass_Right") as AudioClip;
		left_grass = Resources.Load("Sounds/Player_FX/Footsteps_Grass_Left") as AudioClip;
		roll = Resources.Load("Sounds/Player_FX/Roll") as AudioClip;

		aud.outputAudioMixerGroup = AMG;

		atkLows = new List<AudioClip> ();
		for (int i = 1; i < 5; i++) {
			atkLows.Add (Resources.Load ("Sounds/Attack SFX/Attack Low " + i) as AudioClip);
		}

		hurtSounds = new List<AudioClip> ();
		for (int i = 1; i < 6; i++) {
			hurtSounds.Add (Resources.Load ("Sounds/Player_FX/Hurt Brian " + i) as AudioClip);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	private void playSound(){
		aud.Play ();
	}

	public void playRightGrass(){
		aud.clip = right_dirt;
		playSound ();
	}

	public void playLeftGrass(){
		aud.clip = left_dirt;
		playSound ();
	}

	public void playRightDirt(){
		aud.clip = right_dirt;
		playSound ();
	}

	public void playLeftDirt(){
		aud.clip = left_dirt;
		playSound ();
	}

	public void playRoll(){
		aud.clip = roll;
		playSound ();
	}

	public void playAtkLow(){
		int i = (int)Random.Range (0, atkLows.Count);
		aud.clip = atkLows [i];
		playSound ();
	}

	public void playHurt(){
		int i = (int)Random.Range (0, hurtSounds.Count);
		aud.clip = hurtSounds [i];
		playSound ();
	}

}
