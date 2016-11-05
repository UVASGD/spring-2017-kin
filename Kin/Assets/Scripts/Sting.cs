using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Sting : MonoBehaviour {
	bool played; public float triggerDistance =5;
	public float resetDistance = 8; public bool canReset = true;
	AudioSource src;
	MusicController mC;
		GameObject player;
	// Use this for initialization
	void Start () {
		played = false;
		player = GameObject.Find ("Player");
		src = gameObject.GetComponent<AudioSource> ();
		mC = GameObject.Find ("DayNightHolder").GetComponent<MusicController>();
	}
	
	// Update is called once per frame
	void Update () {
		float d = new Vector2 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y)
			.magnitude;
		if (!played) {
			if (d <= triggerDistance) {
				src.Play ();
				played = true;
			}
		} else if (d >= resetDistance && canReset)
			played = false;

		if (src.isPlaying && mC.getAudio().isPlaying) {
			mC.getAudio().Pause ();
		} else if (!src.isPlaying && !mC.getAudio().isPlaying)
			mC.getAudio().Play ();
	}

	//void OnCollisionEnter(Collision col){
	//	if (!played) {
	//		gameObject.GetComponent<AudioSource> ().Play();
	//	}
	//}

}
