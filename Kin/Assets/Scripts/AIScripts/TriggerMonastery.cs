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
			player.GetComponent<PlayerHealth> ().setNumPotions(player.GetComponent<PlayerHealth> ().maxNumPotions);
		}

	}

	public void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "Player" && DNH.GetComponent<MusicController>().state != MusicController.MusicState.World) {
			DNH.GetComponent<MusicController> ().InterruptForWorld ();
		}
	}
}
