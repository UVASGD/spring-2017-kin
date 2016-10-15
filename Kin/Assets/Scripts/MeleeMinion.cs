﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MeleeMinion : BaseMinionAI
{
    public float meleeDamage; //Melee attack damage
    float attackRange; //Range to melee attack from
    bool meleeOnCd; //Is melee attack on cooldown
    float meleeCurrCd; //Remaining cooldown for melee attack

    protected new void Start()
    {
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
		attackRange = .3f;
        awarenessRadius = 1.0f;
    }

    protected new void Update()
    {
        float distanceToPlayer = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);

        if (curState == AIStates.DetectedState)
        {
			if (distanceToPlayer < attackRange) {
				if (!meleeOnCd) {
					//attack
					//Need to choose 1 of 4 major directions to face and then call attack
					meleeCurrCd = .5f;
					meleeOnCd = true;
				} else {
					meleeCurrCd -= Time.deltaTime;
					if (meleeCurrCd <= 0.0f)
						meleeOnCd = false;
				}
			} else {
				MoveTowardsTarget ();
			}
        }
        else
        {
            if (distanceToPlayer < awarenessRadius)
                curState = AIStates.DetectedState;
        }
    }

    private GameObject[] ObjectsInAttackArea(float direction, float angleOfAttack, float attackRadius)
    {
  		Collider2D[] allCollidersInRadius = Physics2D.OverlapCircleAll (rb.transform.position, attackRadius);
  		List<GameObject> matches = new List<GameObject> ();
  		for (int i = 0; i < allCollidersInRadius.Length; i++) {
			float theta = Mathf.Rad2Deg * Mathf.Atan2 (allCollidersInRadius [i].transform.position.x - rb.transform.position.x, allCollidersInRadius [i].transform.position.y - rb.transform.position.y);
  			if (theta > direction - (angleOfAttack / 2) && theta < direction + (angleOfAttack / 2)) {
  				matches.Add (allCollidersInRadius [i].gameObject);
  			}
  		}
  		return matches.ToArray();
  	}

    void attackInRadius(float direction,float angleOfAttack,float radius)
    {
        GameObject[] thingsToAttack = ObjectsInAttackArea(direction,angleOfAttack,radius);
        //Attack Everything In This List
        for(int i = 0; i < thingsToAttack.Length; i++){
			if (thingsToAttack[i].tag == "Player")
			{
				targetObject.GetComponent<PlayerHealth> ().TakeDamage (meleeDamage);
			}
        }
    }
}