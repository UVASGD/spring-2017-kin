using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {

    [HideInInspector]
    public Vector3 source, target;
    public Sprite sprite;

    private List<GameObject> lightning;
    
	void Start () {
        if (sprite != null) {
            // create as many pieces of lightning as necessary to fill gap
            float distance = StaticMethods.Distance((Vector2)source, (Vector2)target);
            float diff = distance - sprite.rect.size.y;

            if (diff > 0) {
                for (int i = 0; i < distance % sprite.rect.size.y - 1; i++) {

                }
            } else { // can only fill once, partially

            }
        } else {
            Debug.Log("Must have a source sprite to create chain.");
        }
	}

    public void kill() {
        foreach (GameObject g in lightning) Destroy(g);
    }
	
	void Update () {
		
	}
}
