using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	AudioSource aud;

	AudioClip day1;
	AudioClip day2;

	AudioClip night1;

	ArrayList dayTracks;
	ArrayList nightTracks;

	public float dayCycleLength;
	public float time;
	public bool decided;
	public bool playing;

	public bool day;

	public float trackLength;
	public float trackLengths;

	// Use this for initialization
	void Start () {
		dayCycleLength = this.gameObject.GetComponent<TimeController> ().getDayLength ();
		time = 0.0f;
		trackLength = 0.0f;
		trackLengths = 0.0f;
		decided = false;
		playing = true;
		day = true;

		dayTracks = new ArrayList ();
		nightTracks = new ArrayList ();

		aud = gameObject.AddComponent<AudioSource>();

		day1 = Resources.Load ("Sounds/Music/Day Music Brian 1") as AudioClip;
		day2 = Resources.Load ("Sounds/Music/Overworld Day Christian 1") as AudioClip;
		dayTracks.Add (day1);
		dayTracks.Add (day2);

		night1 = Resources.Load ("Sounds/Music/Night Music Brian 1") as AudioClip;
		nightTracks.Add (night1);
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time >= dayCycleLength/2) {
			time = 0.0f;
			decided = false;
			day = !day;
		}

		if (!decided) {
			if (day) {
				//day
				int trackIndex = Random.Range (0, dayTracks.Count - 1);
				aud.clip = dayTracks [trackIndex] as AudioClip;
				trackLength = aud.clip.length;
				trackLengths = trackLength;
				decided = true;
				playing = false;

			} else {
				//night
				int trackIndex = Random.Range (0, nightTracks.Count - 1);
				aud.clip = nightTracks [trackIndex] as AudioClip;
				trackLength = aud.clip.length;
				trackLengths = trackLength;
				decided = true;
				playing = false;
			}
		}

		if (time > trackLengths && (dayCycleLength/2 - time > trackLength)) {
			playing = false;
			trackLengths += trackLength;
		}

		if (!playing) {
			playing = true;
			aud.Play ();
		}
	}
}
