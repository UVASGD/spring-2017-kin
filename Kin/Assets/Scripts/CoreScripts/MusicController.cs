using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour {

	AudioSource aud;

	public AudioSource getAudioSource(){
		return aud;
	}

	//day tracks
	ArrayList dayTracks;
	AudioClip day1;
	AudioClip day2;

	//night tracks
	ArrayList nightTracks;
	AudioClip night1;

	//monastery
	AudioClip monastery;

	public AudioMixerGroup AMG;

	DayNightController DNC;

	float timeLeft;
	float timer;
	public bool fading;
	public bool up;
	public bool isPlaying; //use me instead of the other one...
	public MusicState state;

	// Use this for initialization
	void Start () {
		state = MusicState.World;
		dayTracks = new ArrayList ();
		day1 = Resources.Load ("Sounds/Music/Day Music Brian 1") as AudioClip;
		day2 = Resources.Load ("Sounds/Music/Overworld Day Christian 1") as AudioClip;

		dayTracks.Add (day1);
		dayTracks.Add (day2);

		nightTracks = new ArrayList ();
		night1 = Resources.Load ("Sounds/Music/Night Music Brian 1") as AudioClip;
		nightTracks.Add (night1);

		monastery = Resources.Load ("Sounds/Music/Monastary Background Music Christian 1") as AudioClip;

		if (gameObject.GetComponent<AudioSource> ()) {
			aud = gameObject.GetComponent<AudioSource> ();
		} else {
			aud = gameObject.AddComponent<AudioSource> ();
		}

		aud.outputAudioMixerGroup = AMG;

		DNC = this.gameObject.GetComponent<DayNightController> ();

		timer = 0;
		timeLeft = 0;
		fading = false;
		isPlaying = false;
		up = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > timeLeft) {
			isPlaying = false;
			up = false;
		}
			
		if (!isPlaying) {
			up = true;
			fading = true;
			if (state == MusicState.World) {
				if (DNC.currentPhase == DayNightController.DayPhase.Day) {
					aud.clip = (AudioClip)dayTracks [Random.Range (0, dayTracks.Count - 1)];
				} else if (DNC.currentPhase == DayNightController.DayPhase.Night) {
					aud.clip = (AudioClip)nightTracks [Random.Range (0, nightTracks.Count - 1)];
				}
			} else if (state == MusicState.Boss) {



			} else if (state == MusicState.Monastery) {
				aud.clip = monastery;
				aud.loop = true;
			}
			timeLeft = aud.clip.length;
			aud.Play ();
			isPlaying = true;
		}

		if (fading) {
			if (!up) {
				aud.volume -= 0.02f * Time.deltaTime;

				if (aud.volume <= 0.01f) {
					aud.volume = 0.00f;
				}

				if (aud.volume <= 0.00f) {
					//aud.Pause ();
					isPlaying = false;
					fading = false;
				}
			} else {
				if (aud.volume < 1.00f) {
					aud.volume += 0.05f * Time.deltaTime;
				}
			}
		}
	}

	public AudioSource getAudio(){
		return aud;
	}

	public void InterruptForBoss(){
		isPlaying = false;
		aud.volume = 1.00f;
		state = MusicState.Boss;
	}

	public void InteruptForMonastery(){
		isPlaying = false;
		aud.volume = 1.00f;
		state = MusicState.Monastery;
	}

	public enum MusicState
	{
		World = 0,
		Boss = 1,
		Monastery = 2,
	}
}
