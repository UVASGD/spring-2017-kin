using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacTimer : MonoBehaviour {
	public float maxTime;
	public float currentTime;
	// Use this for initialization
	void Start () {
		currentTime = 0.0f;
		maxTime = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime >= maxTime) {
			currentTime = 0;	 
		} 

		else {
			currentTime += Time.deltaTime;
		}
			
}
}
