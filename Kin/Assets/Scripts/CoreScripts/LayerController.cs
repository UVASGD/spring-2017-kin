using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LayerController : MonoBehaviour {

	LayerParameter[] layeringObjectList;
	float playerY;

	public GameObject player;

	// Use this for initialization
	void Start () {
		UpdateList ();
	}
	
	// Update is called once per frame
	void Update () {
		playerY = player.transform.position.y;
		foreach (LayerParameter lay in layeringObjectList) {
			GameObject obj = lay.gameObject;
			if (obj.transform.position.y + lay.yOffset < playerY) {
				// In front of player
			} else {
				// Behind player
			}
		}
	}

	void UpdateList() {
		layeringObjectList = GameObject.FindObjectsOfType<LayerParameter> () as LayerParameter[];
	}
}
