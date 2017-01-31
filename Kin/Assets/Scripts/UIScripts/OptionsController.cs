using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	public Slider MasterVolume;
	public Slider MusicVolume;
	public Slider PlayerVolume;
	public Slider DialogVolume;
	public Slider EnemyVolume;
	public Slider WorldVolume;

	public AudioMixer Mixer;

	// Use this for initialization
	void Start () {
		bool b = true;
		float f = 0.00f;
		b = Mixer.GetFloat ("masterVol", out f);
		if (b) {
			MasterVolume.value = f;
		} else {
			Debug.Log ("could not find masterVol");
		}

		b = Mixer.GetFloat ("musicVol", out f);
		if (b) {
			MusicVolume.value = f;
		} else {
			Debug.Log ("could not find musicVol");
		}

		b = Mixer.GetFloat ("playerVol", out f);
		if (b) {
			PlayerVolume.value = f;
		} else {
			Debug.Log ("could not find playerVol");
		}

		b = Mixer.GetFloat ("dialogVol", out f);
		if (b) {
			DialogVolume.value = f;
		} else {
			Debug.Log ("could not find dialogVol");
		}

		b = Mixer.GetFloat ("enemyVol", out f);
		if (b) {
			EnemyVolume.value = f;
		} else {
			Debug.Log ("could not find enemyVol");
		}

		b = Mixer.GetFloat ("worldVol", out f);
		if (b) {
			WorldVolume.value = f;
		} else {
			Debug.Log ("could not find worldVol");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void adjustMaster(){
		Mixer.SetFloat ("masterVol", MasterVolume.value);
	}

	public void adjustMusic(){
		Mixer.SetFloat ("musicVol", MusicVolume.value);
	}

	public void adjustPlayerFX(){
		Mixer.SetFloat ("playerVol", PlayerVolume.value);
	}

	public void adjustDialogFX(){
		Mixer.SetFloat ("dialogVol", DialogVolume.value);
	}

	public void adjustEnemyFX(){
		Mixer.SetFloat ("enemyVol", EnemyVolume.value);
	}

	public void adjustWorldFX(){
		Mixer.SetFloat ("worldVol", WorldVolume.value);
	}

	public void quit(){
		Application.Quit ();
		//UnityEditor.EditorApplication.isPlaying = false; //for in the editor *may need to be deleted*
	}
}
