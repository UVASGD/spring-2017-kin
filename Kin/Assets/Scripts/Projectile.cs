using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {


	public float range; //Range at which proj will decay
	private float distTravelled; //Total distance travelled
	Vector2 previous;//Location last frame

	public float damage; //Damage on hit

	void Start () {
		//Initialize distance, range and pos
		range = 1.0f;
		distTravelled = 0.0f;
		previous = (Vector2) transform.position;
	
	}
	
	//Handle projectile should decay
	void Update () {
		distTravelled += Vector2.Distance (((Vector2) transform.position), previous);
		previous = (Vector2) transform.position;
		//Debug.Log (distTravelled);
		if (distTravelled >= range)
			Destroy (gameObject);
	
	}

	//Handle player collisions
	void OnTriggerEnter2D(Collider2D obj)
	{
		Debug.Log ("in trigger");
		if (obj.tag == "Player") {
			//deal damage
			Destroy (gameObject);
		}// else if (obj.tag == "Terrain") {
		//Destroy (gameObject);
		//} 

	}
		
}
