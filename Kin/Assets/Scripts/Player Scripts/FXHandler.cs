﻿using UnityEngine;
using System.Collections;

public class FXHandler : MonoBehaviour {

	private AudioSource aud;
	private AudioClip right_dirt;
	private AudioClip left_dirt;
	private AudioClip right_grass;
	private AudioClip left_grass;

	// Use this for initialization
	void Start () {
		aud = gameObject.AddComponent<AudioSource>();
		aud.volume = 0.4f;
		right_dirt = Resources.Load ("Sounds/Player_FX/Footsteps_Dirt_Right") as AudioClip;
		left_dirt = Resources.Load("Sounds/Player_FX/Footsteps_Dirt_Left") as AudioClip;
		right_grass = Resources.Load("Sounds/Player_FX/Footsteps_Grass_Right") as AudioClip;
		left_grass = Resources.Load("Sounds/Player_FX/Footsteps_Grass_Left") as AudioClip;
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


}
