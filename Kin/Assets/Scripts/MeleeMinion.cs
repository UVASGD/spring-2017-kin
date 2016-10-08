using UnityEngine;
using UnityEditor;
using System.Collections;

public class MeleeMinion : BaseMinionAI
{
    float meleeDamage; //Melee attack damage
    float attackMeleeDelay; //Pause between minion attacks
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

        awarenessRadius = 1.0f;
    }

    protected new void Update()
    {
        float distanceToPlayer = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);

        if (curState == AIStates.DetectedState)
        {
            if (distanceToPlayer < attackRange)
            {
                if(!meleeOnCd)
                {
                    //attack
                    meleeCurrCd = .5f;
                    meleeOnCd = true;
                }
                else
                {
                    meleeCurrCd -= Time.deltaTime;
                    if (meleeCurrCd <= 0.0f)
                        meleeOnCd = false;
                }
            }
        }
        else
        {
            if (distanceToPlayer < awarenessRadius)
                curState = AIStates.DetectedState;
        }
    }

    void attackInRadius(float direction,float angleOfAttack,float radius)
    {
        Collider2D[] everythingInRange = Physics2D.OverlapCircleAll(rb.transform.position, radius);
        ArrayList matches = new ArrayList();
        for(int i = 0; i < everythingInRange.Length; i++)
        {
            Vector2 objPosition = everythingInRange[i].GetComponent<Rigidbody2D>().transform.position;
            float angleToObject = (Mathf.Atan2(objPosition.y - rb.position.y, objPosition.x - rb.position.x) + 2*Mathf.PI) % 2*Mathf.PI;
            if (angleToObject > direction - (angleOfAttack / 2) && angleToObject < direction + (angleOfAttack / 2)) //We can check tag of the object to avoid friendly fire if deisred here
            {
                matches.Add(everythingInRange[i].gameObject);
            }
        }
        //Attack Everything In This List

    }
}