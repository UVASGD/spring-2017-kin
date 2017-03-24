using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ChacLocation : MonoBehaviour
{
	public float distanceFromPlayer;
	public float awakenDistance = 10.0f;
	public GameObject player;

	// Use this for initialization
	void Start ()
	{
		distanceFromPlayer = player.transform.position - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		distanceFromPlayer = player.transform.position - gameObject.transform.position;

		if(distanceFromPlayer < awakenDistance) {
			
			stateMachine.SetTrigger ("closeEnough"); 
		} 

		// update postion?
	}
}

