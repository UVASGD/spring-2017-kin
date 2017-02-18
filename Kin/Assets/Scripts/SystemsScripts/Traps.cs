using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {

    BoxCollider2D collider;
    int damage;

	// Use this for initialization
	void Start () {
        collider = gameObject.GetComponent<BoxCollider2D>();
        damage = 10;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boom, Trap expoded.");
        GameObject target = other.gameObject;
        PlayerHealth pHealth;
        EnemyHealth eHealth;
        if(target.tag == "Player")
        {
            pHealth = target.GetComponent<PlayerHealth>();
            pHealth.TakeDamage(damage);
        }
        else if(target.tag == "enemy") {
            eHealth = target.GetComponent<EnemyHealth>();
            eHealth.takeDamage(damage);
        }

        
    }
}
