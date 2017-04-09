using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuetzyArm : MonoBehaviour {

	public GameObject hand;
	GameObject parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = gameObject.name.Contains("B") ? new Vector2(0.5f, 0.0f) : new Vector2(-0.5f, 0.0f);
		Vector2 bodyToHand = ((Vector2)parent.transform.position - (Vector2)hand.transform.position) + offset; 
		gameObject.transform.localScale = new Vector3(transform.localScale.x, bodyToHand.magnitude, transform.localScale.z);
		Quaternion rot = Quaternion.FromToRotation(Vector3.down, bodyToHand);
		gameObject.transform.rotation = rot;
	}
}
