using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody2D))]
public class MeteorProjectile : MonoBehaviour {

	public float speed = 10.0f;

	public int damage = 10;

    public float despawnTimer = 2.0f;

	private Rigidbody2D rb;

	bool reversed = false;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		//rb.drag = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (despawnTimer <= 0.0f)
            Destroy(gameObject);
        else
            despawnTimer -= Time.deltaTime;
		
	}

	public void SetVelocity(float angle) {
		rb.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
		GetComponent<SpriteRenderer>().flipX = rb.velocity.x > 0;
	}

    public void ReverseVelocity()
    {
        rb.velocity = new Vector2(-1*rb.velocity.x, -1 * rb.velocity.y);
		GetComponent<SpriteRenderer>().flipY = true;
		reversed = true;
    }

    public void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Boss" && reversed)
        {
            coll.gameObject.GetComponent<EnemyHealth>().takeDamage(damage * 5);
            // Create explody animation prefab here
            Destroy(gameObject);
        }
        if (coll.gameObject.name == "RightAttackBox" || coll.gameObject.name == "LeftAttackBox" || coll.gameObject.name == "UpperAttackBox" || coll.gameObject.name == "LowerAttackBox")
        {
            ReverseVelocity();
        }
        if (coll.CompareTag("Player")) {
			coll.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
			// Create explody animation prefab here
			Destroy(gameObject);
		}
		if (coll.gameObject.CompareTag("enemy")) {
			coll.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
			// Create explody animation prefab here
			Destroy(gameObject);
		}
    }

    public void onCollisionEnter2D(Collider2D col)
    {
        Debug.Log("Hit");
        if (col.gameObject.tag == "Boss")
        {
            col.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
            // Create explody animation prefab here
            Destroy(gameObject);
        }
    }
}
