using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ChacLocation : MonoBehaviour
{
	public Vector3 distanceFromPlayer;
	public float awakenDistance = 10.0f;
	public GameObject player;
	private Animator stateMachine;

	// Use this for initialization
	void Start ()
	{
		stateMachine = gameObject.GetComponent<Animator> ();
		distanceFromPlayer = player.transform.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		distanceFromPlayer = player.transform.position - gameObject.transform.position;

		if(distanceFromPlayer.magnitude < awakenDistance) {
			
			stateMachine.SetBool ("closeEnough", true); 
		} 

		// update postion?
	}
}

