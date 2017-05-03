using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuetzalcotlInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnStalagtites()
	{
		transform.GetComponentInChildren<QuetzalcotlAI>().SpawnStalagtites();
	}

	public void SpawnSnakes()
	{
		transform.GetComponentInChildren<QuetzalcotlAI>().SpawnSnakes();
	}

	public void ShootMeteor() {
		transform.GetComponentInChildren<QuetzalcotlAI>().ShootMeteor();
	}
}
