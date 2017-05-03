using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOverrideController : MonoBehaviour {

	bool normalControl;

	// Use this for initialization
	void Start () {
		normalControl = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NormalOn(){
		normalControl = true;
	}

	public void NormalOff(){
		normalControl = false;
	}

	public bool IsNormal(){
		return normalControl;
	}
}
