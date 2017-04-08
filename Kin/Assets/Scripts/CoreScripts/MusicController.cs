using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent(typeof(DayNightController))]
public class MusicController : MonoBehaviour {

	AudioSource aud;

	public AudioSource getAudioSource(){
		return aud;
	}

	string Bossname;
	bool introed;

	//day tracks
	ArrayList dayTracks;
	AudioClip day1;
	AudioClip day2;
	AudioClip day3;

	//night tracks
	ArrayList nightTracks;
	AudioClip night1;
	AudioClip night2;
	AudioClip night3;

	//monastery
	AudioClip monastery;

	AudioClip chacjam;
	AudioClip ixtabintro;
	AudioClip ixtabjam;

	public AudioMixerGroup AMG;

	DayNightController DNC;

	public GameObject mona;
	public GameObject chime;

	public float timeLeft;
	public float timer;
	public bool fading;
	public bool up;
	public bool isPlaying; //use me instead of the other one...
	public MusicState state;

	public float buffer;
	public const float bufferTime = 300;

	// Use this for initialization
	void Start () {
		buffer = 0;
		Bossname = "";
		introed = false;

		state = MusicState.World;
		dayTracks = new ArrayList ();
		day1 = Resources.Load ("Sounds/Music/Day Music Brian 1") as AudioClip;
		day2 = Resources.Load ("Sounds/Music/Overworld Day Christian 1") as AudioClip;
		day3 = Resources.Load ("Sounds/Music/Day Music Brian 2") as AudioClip;
		dayTracks.Add (day1);
		dayTracks.Add (day2);
		dayTracks.Add (day3);

		nightTracks = new ArrayList ();
		night1 = Resources.Load ("Sounds/Music/Night Music Brian 1") as AudioClip;
		night2 = Resources.Load ("Sounds/Music/Night Music Brian 2") as AudioClip;
		night2 = Resources.Load ("Sounds/Music/Night Music Brian 3") as AudioClip;
		nightTracks.Add (night1);
		nightTracks.Add (night2);
		nightTracks.Add (night3);

		monastery = Resources.Load ("Sounds/Music/Monastary Background Music Christian 1") as AudioClip;

		chacjam = Resources.Load ("Sounds/Music/Boss Music Chac Brian 1") as AudioClip;
		ixtabintro = Resources.Load ("Sounds/Music/Boss Music Ixtab (Intro) Brian 1") as AudioClip;
		ixtabjam = Resources.Load ("Sounds/Music/Boss Music Ixtab (Loop) Brian 1") as AudioClip;

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
		if (state == MusicState.World) {
			buffer -= Time.deltaTime;
		}

		int index = 0;

		if(dayTracks != null)
			index = Random.Range (0, dayTracks.Count);

		if (timer > timeLeft) {
			isPlaying = false;
			up = false;
		}
			
		if (!isPlaying) {
			up = true;
			fading = true;
			if (state == MusicState.World) {
				aud.loop = false;
				//int index = Random.Range (0, dayTracks.Count - 1);
				//Debug.Log (index);
				if (DNC.currentPhase == DayNightController.DayPhase.Day || DNC.currentPhase == DayNightController.DayPhase.Dawn) {
					aud.clip = (AudioClip)dayTracks [index];
				} else if (DNC.currentPhase == DayNightController.DayPhase.Night || DNC.currentPhase == DayNightController.DayPhase.Dusk) {
					aud.clip = (AudioClip)nightTracks [index];
				}
			} else if (state == MusicState.Boss) {
				if (Bossname == "Chac") {
					aud.clip = chacjam;
					aud.loop = true;
				} else if (Bossname == "Ixtab") {
					//intro then loop
					if (!introed) {
						aud.clip = ixtabintro;
						timeLeft = aud.clip.length;
						timer = 0.0f;
						introed = true;
					}
					if (introed && timeLeft != 10000000.0f && timer >= timeLeft) {
						aud.clip = ixtabjam;
						aud.loop = true;
						timeLeft = 10000000.0f;
					}
				} else {
					Debug.Log ("Invalid boss name in music controller");
				}
			} else if (state == MusicState.Monastery) {
				aud.clip = monastery;
				aud.loop = true;
			}

			if (aud != null && aud.clip != null) {
				timeLeft = aud.clip.length;
			} else {
				if (DNC.currentPhase == DayNightController.DayPhase.Day) {
					aud.clip = (AudioClip)dayTracks [index];
				} else if (DNC.currentPhase == DayNightController.DayPhase.Night) {
					aud.clip = (AudioClip)nightTracks [index];
				} else {
					timeLeft = 20.0f;
				}
			}

			aud.Play ();
			timer = 0.0f;
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

	public void InterruptForBoss(string bossname){
		isPlaying = false;
		aud.volume = 1.00f;
		up = false;
		state = MusicState.Boss;
		Bossname = bossname;
	}

	public void InteruptForMonastery(){
		isPlaying = false;
		up = false;
		aud.volume = 1.00f;
		mona.GetComponent<AudioSource> ().Play ();
		if (buffer < 0) {
			chime.GetComponent<AudioSource> ().Play ();
			buffer = bufferTime;
		}
		state = MusicState.Monastery;
		Bossname = "";
	}

	public void InterruptForWorld(){
		mona.GetComponent<AudioSource> ().Stop ();
		chime.GetComponent<AudioSource> ().Stop ();
		isPlaying = false;
		aud.volume = 1.00f;
		up = false;
		state = MusicState.World;
		timeLeft = 0.0f;
		introed = false;
		Bossname = "";
	}

	public enum MusicState
	{
		World = 0,
		Boss = 1,
		Monastery = 2,
	}
}
