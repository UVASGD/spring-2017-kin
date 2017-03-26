using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {

    public int damage;

	// Use this for initialization
	void Start () {

/**
 * proposed change:
 * 
 * I feel like the glyph should be a solid color
 * and when colided with should change colors,
 * and then a second later the particles and damage should happen,
 * that way the player has a second to get out of the way.
 * 
 * We should probably also have different types...
 * **/

	}
	
	// Update is called once per frame
	void Update () {
	}


    void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Boom, Trap expoded.");

		GameObject target = other.gameObject;
		PlayerHealth pHealth;
		EnemyHealth eHealth;
		if (target.tag == "Player") {
			pHealth = target.GetComponent<PlayerHealth> ();
			pHealth.TakeDamage (damage);
		} else if (target.tag == "enemy") {
			eHealth = target.GetComponent<EnemyHealth> ();
			eHealth.takeDamage (damage);
		}
		GameObject part = (GameObject)(Resources.Load ("Prefabs/TrapParticles", typeof(GameObject)));
		GameObject instPart = Instantiate (part, transform.position, Quaternion.Euler(-90,0,0));
		instPart.GetComponent<ParticleSystem>().Emit(40);
        GetComponent<AudioSource>().Play();
        Destroy (gameObject);

	}
}