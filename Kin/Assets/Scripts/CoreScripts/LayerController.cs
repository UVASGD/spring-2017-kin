using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LayerController : MonoBehaviour {

	List<GameObject> layeringObjectList;
	float playerY;

	public GameObject player;

	// Use this for initialization
	void Start () {
		layeringObjectList = GameObject.FindGameObjectsWithTag("Layered");
	}
	
	// Update is called once per frame
	void Update () {
		playerY = player.transform.position.y;
		foreach (GameObject obj in layeringObjectList) {
			
			if (obj.transform.position.y < playerY) {
				// In front of player
			} else {
				// Behind player
			}
		}
	}
}
