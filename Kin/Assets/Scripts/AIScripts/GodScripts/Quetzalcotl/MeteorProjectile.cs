using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody2D))]
public class MeteorProjectile : MonoBehaviour {

	public float speed = 10.0f;

	public int damage = 10;

    public float despawnTimer = 5.0f;

	private Rigidbody2D rb;

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
	}

    public void ReverseVelocity()
    {
        rb.velocity = new Vector2(-1*rb.velocity.x, -1 * rb.velocity.y);
    }

    public void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("Collide");
        if (coll.gameObject.tag == "Boss")
        {
            coll.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
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
    }

    public void onCollisionEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            col.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
            // Create explody animation prefab here
            Destroy(gameObject);
        }
    }
}
