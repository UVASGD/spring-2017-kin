using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody2D))]
public class MeteorProjectile : MonoBehaviour {

	public float speed = 2.0f;

	public int damage = 10;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.drag = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetVelocity(float x, float y) {
		rb.velocity = new Vector2(x, y);
	}

    public void ReverseVelocity()
    {
        rb.velocity = new Vector2(-1*rb.velocity.x, -1 * rb.velocity.y);
    }

    public void OnTriggerEnter2D(Collider2D coll) {
		if (coll.CompareTag("Player")) {
			coll.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
			// Create explody animation prefab here
			Destroy(gameObject);
		}
        if (coll.gameObject.name == "rightAttackBox" || coll.gameObject.name == "leftAttackBox" || coll.gameObject.name == "upperAttackBox" || coll.gameObject.name == "lowerAttackBox")
        {
            ReverseVelocity();
        }
    }
}
