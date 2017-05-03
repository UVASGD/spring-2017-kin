using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonastery : MonoBehaviour {

	public GameObject DNH;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Player" && DNH.GetComponent<MusicController>().state != MusicController.MusicState.Monastery) {
			DNH.GetComponent<MusicController> ().InteruptForMonastery ();
			PlayerHealth PH = player.GetComponent<PlayerHealth> ();
			PH.setNumPotions(PH.maxNumPotions);
			PH.setCurrentHealth ((int)PH.getMaxHealth());
			PlayerStamina PS = player.GetComponent<PlayerStamina> ();
			PS.setCurrentStamina ((int)PS.getMaxStamina());
		}
		if (coll.tag == "Player") {
			SaveController.s_instance.Save();
		}

	}

	public void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "Player" && DNH.GetComponent<MusicController>().state != MusicController.MusicState.World) {
			DNH.GetComponent<MusicController> ().InterruptForWorld ();
		}
		if (coll.tag == "Player") {
			SaveController.s_instance.Save();
		}
	}
}
