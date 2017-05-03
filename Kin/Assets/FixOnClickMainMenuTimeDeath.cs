using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixOnClickMainMenuTimeDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Button> ().onClick.AddListener(() => GameObject.FindObjectOfType<EndMenuController> ().MainMenu ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
