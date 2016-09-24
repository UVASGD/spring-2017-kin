using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 5.0f;
	//Range at which proj will decay
	public float range = 500.0f;
	public float damage;
	private float distTravelled;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (distTravelled >= range)
			//Destroy (gameObject);
	
	}

	void OnTriggerEnter(Collider obj)
	{
		if (obj.tag == "Player") {
			//deal damage
		}
	}
}
