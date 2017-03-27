using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSTCHAIN : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Interact") && target!=null)
            shoot(gameObject.transform.position);
	}

    void shoot(Vector2 source) {
        // draw arc from source to this enemy
        GameObject arc = Instantiate(Resources.Load("Prefabs/Projectiles/Chain Lightning", typeof(GameObject)) as GameObject,
           source, Quaternion.identity);

        Chain chain = arc.GetComponent<Chain>();
        chain.source = gameObject; chain.target = target;
        chain.forceStart();
    }
}
