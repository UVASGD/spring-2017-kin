using UnityEngine;
using System.Collections;

public class Geyser : MonoBehaviour {

	public float delay;
	public int damage;
	public float duration;
	public float radiusOfEffect;
	public float damageCooldown;
	private float age;
	private float currentCooldown;

	private enum States {
		tell,
		damage,
		fade
	}
	private States currState = States.tell;

	// Use this for initialization
	void Start () {
		age = 0;
		currentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currState) {
		default:
			//Stuff we always gotta do
			age += Time.deltaTime;
		case States.tell:
			//Stuff we do during the "tell" phase before damage
			if (age > delay) {
				currState = States.damage;
				age = 0;
				//Set animator to go to shooting-up stage here
			}
			break;
		case States.damage:
			//Stuff we do during the damaging phase
			currentCooldown -= Time.deltaTime;
			if (age > duration) {
				currState = States.fade;
				//Set animator to falling-down stage here
			} else if(currentCooldown <= 0) {
				Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll (gameObject.transform.position, radiusOfEffect);
				for (int i = 0; i < nearbyObjects.Length; i++) {
					if (nearbyObjects [i].tag == "Player") {
						nearbyObjects [i].gameObject.GetComponent<PlayerHealth> ().TakeDamage (damage);
						currentCooldown = damageCooldown;
					}
				}
			}
			break;
		case States.fade:
			//Stuff we do while fading
			//is there anything to do here? the world may never know
			break;
		}
	}
}
