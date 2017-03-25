using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Virtuoso : MonoBehaviour {

	public GameObject player;
	public float distance;

	// Use this for initialization
	void Start () {
		distance = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector2.Distance (this.gameObject.transform.position, player.transform.position);
		if (distance <= 10) 
			this.GetComponent<AudioSource> ().volume = 1 - distance / 10;
		else
			this.GetComponent<AudioSource> ().volume = 0.0f;
	}
}
