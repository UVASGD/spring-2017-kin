using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {

    BoxCollider2D collider;

	// Use this for initialization
	void Start () {
        collider = gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider thing)
    {

        print("HELP I WAS STEPPED ON");
    }
}
