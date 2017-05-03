using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class QuoteHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {

		List<string> deathQuotes = new List<string>();
		deathQuotes.Add("\"Where men can't live, gods fare no better.\"");
		deathQuotes.Add("\"A person has learned much who has learned how to die.\"");
		deathQuotes.Add("\"Death keeps no calendar.\"");
		deathQuotes.Add("\"Six feet of earth makes us all equal.\"");
		deathQuotes.Add("\"Death is nothing, but to live defeated and inglorious is to die daily.\"");
		deathQuotes.Add("\"Knowledge forbidden?\nSuspicious, reasonless.\nWhy should their Lord\nEnvy them that!\nCan it be a sin to know?\nCan it be death?”\"");
		deathQuotes.Add("\"Freely they stood who stood, and fell who fell.\"");
		deathQuotes.Add("\"There is no God and we are his prophets.\"");
		deathQuotes.Add("\"To die will be an awfully big adventure.\"");
		deathQuotes.Add("\"Death\nAs a dark\nShadow\nBeckons his prey\nInto the unknown\nBy a soft whisper\nIn the soul\"");
		deathQuotes.Add("\"Death is when the monsters get you.\"");

		this.GetComponent<Text>().text = deathQuotes[Random.Range(0, deathQuotes.Count)];

		AudioSource aSource = Camera.main.GetComponent<AudioSource>();

		if (aSource == null) {
			aSource = new AudioSource ();
		}

		string clipName = "Sounds/Player SFX/Death Sound Brian " + Random.Range(1, 3);
		AudioClip c = Resources.Load(clipName) as AudioClip;
		aSource.clip = c;
		if (!aSource.isPlaying) {
			aSource.GetComponent<AudioSource> ().Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
