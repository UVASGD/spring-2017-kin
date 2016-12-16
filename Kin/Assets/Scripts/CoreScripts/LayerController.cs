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
				obj.GetComponent<SpriteRenderer>().sortingLayerName = "Front_Player";
			} else {
				obj.GetComponent<SpriteRenderer>().sortingLayerName = "Behind_Player";
            }
		}
	}

	/// <summary>
	/// Updates the list.
	/// </summary>
	void UpdateList() {
		layeringObjectList = GameObject.FindObjectsOfType<LayerParameter> () as LayerParameter[];
	}
}
