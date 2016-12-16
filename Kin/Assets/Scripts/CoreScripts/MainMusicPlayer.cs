using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MainMusicPlayer : MonoBehaviour {

	AudioSource aus;
	AudioSource ausLoop;
	AudioSource bkgd;
	AudioClip intro;
	AudioClip loop;
	AudioClip fire;

	public float timer;
	public float introTimer;
	bool playedIntro;

	public AudioMixerGroup AMG;
	public AudioMixerGroup WorldFX;

	// Use this for initialization
	void Start () {
		aus = gameObject.AddComponent<AudioSource>();
		ausLoop = gameObject.AddComponent<AudioSource>();
		bkgd = gameObject.AddComponent<AudioSource>();
		aus.volume = 0.7f;
		aus.outputAudioMixerGroup = AMG;
		ausLoop.outputAudioMixerGroup = AMG;
		bkgd.outputAudioMixerGroup = WorldFX;
		intro = Resources.Load ("Sounds/Music/Main Menu Music Intro") as AudioClip;
		loop = Resources.Load ("Sounds/Music/Main Menu Music Looped Part") as AudioClip;
		fire = Resources.Load ("Sounds/Environment SFX/Fire_(Soft)") as AudioClip;

		timer = 0.0f;
		playedIntro = false;
		introTimer = intro.length - 13.65f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (!playedIntro) {
			playedIntro = true;
			aus.clip = intro;
			aus.Play ();
			bkgd.clip = fire;
			bkgd.loop = true;
			bkgd.PlayDelayed (1.2f);
		}

		if (timer >= introTimer && !ausLoop.loop) {
			ausLoop.clip = loop;
			ausLoop.loop = true;
			ausLoop.Play ();
		}
	}
}
