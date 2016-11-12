using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MeleeMinion : BaseMinionAI
{
    public int meleeDamage; //Melee attack damage
    public float attackRange; //Range to melee attack from
    bool meleeOnCd; //Is melee attack on cooldown
	bool dealtDamage;
    float meleeCurrCd; //Remaining cooldown for melee attack
	float despawnTimer;
	bool dying;
	RigidbodyConstraints2D origConstraints;

    protected new void Start()
    {
		despawnTimer = 0.0f;
		dying = false;
        base.Start();

        //Establish rigid body for minion
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("AI has no RigidBody. AI name is " + gameObject.name + "!");
        }
        if (targetObject == null)
        {
            Debug.LogError("AI has no target. AI name is " + gameObject.name + "!");
        }
		origConstraints = gameObject.GetComponent<Rigidbody2D> ().constraints;
		curState = AIStates.PatrolState;
		meleeOnCd = false;
        awarenessRadius = 1.0f;
		dealtDamage = false;
	
    }

    protected new void Update()
	{
		float distanceToPlayer = Vector2.Distance ((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
		if (!dying) {
			if (curState == AIStates.DetectedState) {
				if (distanceToPlayer < attackRange && !meleeOnCd) {
					meleeCurrCd = 1.2f;
					meleeOnCd = true;
					dealtDamage = false;
					gameObject.GetComponent<MeleeMinionAnimationController> ().attacking = true;
					gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
				} else if (!meleeOnCd && distanceToPlayer < awarenessRadius) {
					MoveTowardsTarget ();
				} else {
					curState = AIStates.PatrolState;
					rb.velocity = Vector2.zero;
				}
			}
			if (curState == AIStates.PatrolState) {
				Patrol ();
				if (distanceToPlayer < awarenessRadius)
					curState = AIStates.DetectedState;
			}
			meleeCurrCd -= Time.deltaTime;
			if (meleeCurrCd <= 0.0f) {
				meleeOnCd = false;
				gameObject.GetComponent<Rigidbody2D> ().constraints = origConstraints;
			} else if (meleeCurrCd <= 0.5f && !dealtDamage) {
				attackInRadius (targetObject.transform.position.x > rb.transform.position.x, attackRange);
				dealtDamage = true;
			}
			if (gameObject.GetComponent<EnemyHealth> ().getHp () <= 0)
				death ();

		} else {
			if (despawnTimer >= 10.0f)
				Destroy (gameObject);
			else
				despawnTimer += Time.deltaTime;
		}

	}

    private GameObject[] ObjectsInAttackArea(bool direction /* false==left, true==right */, float attackRadius)
    {
  		Collider2D[] allCollidersInRadius = Physics2D.OverlapCircleAll (rb.transform.position, attackRadius*1.2f);
  		List<GameObject> matches = new List<GameObject> ();
  		for (int i = 0; i < allCollidersInRadius.Length; i++) {
			float xDifference = allCollidersInRadius [i].attachedRigidbody.transform.position.x - rb.transform.position.x;
			if (direction) { //Right Side
				if (xDifference >= 0) {
					matches.Add (allCollidersInRadius [i].gameObject);
				}
			} else { //Left Side
				if (xDifference <= 0) {
					matches.Add (allCollidersInRadius [i].gameObject);
				}
			}
  		}
  		return matches.ToArray();
  	}

    void attackInRadius(bool direction, float radius)
    {
        //Debug.Log("Attacking");
        GameObject[] thingsToAttack = ObjectsInAttackArea(direction,radius);
        //Attack Everything In This List
        for(int i = 0; i < thingsToAttack.Length; i++){
			if (thingsToAttack[i].tag == "Player")
			{
				Debug.Log ("attacking");
					targetObject.GetComponent<PlayerHealth> ().TakeDamage (meleeDamage);
			}
        }
    }

	void death()
	{
		gameObject.GetComponent<MeleeMinionAnimationController> ().dying = true;
		dying = true;
	}
}