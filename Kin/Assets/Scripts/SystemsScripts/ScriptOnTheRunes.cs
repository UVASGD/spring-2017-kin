﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptOnTheRunes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Runes>().ActivateRune(gameObject.name);
            Destroy(gameObject);
        }
    }
}
