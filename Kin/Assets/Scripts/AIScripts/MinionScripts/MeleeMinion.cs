using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(AudioSource))]
public class MeleeMinion : BaseMinionAI
{
    public int meleeDamage; //Melee attack damage
    public float attackRange; //Range to melee attack from
    public int experience = 200;
    bool meleeOnCd; //Is melee attack on cooldown
	bool dealtDamage;
    float meleeCurrCd; //Remaining cooldown for melee attack
	float despawnTimer;
	bool dying;
    

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

        curState = AIStates.IdleState;
		meleeOnCd = false;
        awarenessRadius = 3.0f;
		dealtDamage = false;
	
    }

    protected new void Update()
    {

        float distanceToPlayer = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
		if (!dying && GetComponent<Animator>().GetBool("Spawned")) {
			if (curState == AIStates.DetectedState) {
                if (distanceToPlayer >= awarenessRadius)
                {
                    curState = AIStates.IdleState;
					StopCoroutine ("FollowPath");
                    rb.velocity = Vector2.zero;
					return;
                }
                if (distanceToPlayer < attackRange || meleeOnCd) {
					if (!meleeOnCd) {
						//Need to choose 1 of 4 major directions to face and then call attack
						meleeCurrCd = 1.2f;
						meleeOnCd = true;
						dealtDamage = false;
						gameObject.GetComponent<MeleeMinionAnimationController>().attacking = true;
						gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
					} else {
						meleeCurrCd -= Time.deltaTime;
						if (meleeCurrCd <= 0.0f) {
							meleeOnCd = false;
							gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
						}
					}

				} else if (!meleeOnCd && curState == AIStates.DetectedState) {
					if (timeDelay > .5f)
					{
						RequestPathManager.Request(transform.position, targetObject.transform.position, OnPathFound);
						timeDelay = 0;
					}
                    else
                        timeDelay += Time.deltaTime;
				}
			} else {
				Patrol ();
				if (distanceToPlayer < awarenessRadius)
                {
					curState = AIStates.DetectedState;
                    isWaiting = false;
                }
			}
			if (gameObject.GetComponent<EnemyHealth> ().getHp () <= 0)
				death ();
		}
		if (dying) {
			if (despawnTimer >= 10.0f)
				Destroy (gameObject);
			else
				despawnTimer += Time.deltaTime;
		}
    }

    public override void applyDamage() {
        float distanceToPlayer = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
        if (!dealtDamage) {
            if (distanceToPlayer < attackRange) {
                attackInRadius(targetObject.transform.position.x > rb.transform.position.x, attackRange);
            }
            dealtDamage = true;
        }
    }

    private GameObject[] ObjectsInAttackArea(bool direction /* false==left, true==right */, float attackRadius)
    {
  		Collider2D[] allCollidersInRadius = Physics2D.OverlapCircleAll (rb.transform.position, attackRadius*1.2f);
  		List<GameObject> matches = new List<GameObject> ();
  		for (int i = 0; i < allCollidersInRadius.Length; i++) {
            GameObject target = allCollidersInRadius[i].gameObject;
            if (target.tag == "Player")
            {
                float xDifference = target.GetComponent<Rigidbody2D>().transform.position.x - rb.transform.position.x;
                if (direction)
                { //Right Side
                    if (xDifference >= 0)
                    {
                        matches.Add(allCollidersInRadius[i].gameObject);
                    }
                }
                else { //Left Side
                    if (xDifference <= 0)
                    {
                        matches.Add(allCollidersInRadius[i].gameObject);
                    }
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
		if (thingsToAttack.Length > 0){
			if (thingsToAttack[0].tag == "Player")
			{
				targetObject.GetComponent<PlayerHealth> ().TakeDamage (meleeDamage);
			}
		}
    }

	void death()
	{
		gameObject.GetComponent<MeleeMinionAnimationController> ().dying = true;
		gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		dying = true;
        Experience(experience);
	}
}