using UnityEngine;
using UnityEditor;
using System.Collections;

public class Kamikaze : BaseMinionAI
{
    float explodeRadius; //radius where kamikaze stops and explodes
    bool isExploding;
    float explodeDelay;  //time delay between stopping and exploding
    float timeToExplode; //buildup to the explosion

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

        this.isRanged = false;

        curState = AIStates.DetectedState;

        explodeRadius = .2f;
        timeToExplode = 0.0f;
        explodeDelay = .8f;

    }

    protected new void Update()
    {
        float distanceToPlayer = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);

        if (curState == AIStates.DetectedState)
        {
            if (distanceToPlayer < explodeRadius)
            {
                isExploding = true;
                rb.velocity = Vector2.zero;
            }
            if (!isExploding)
                MoveTowardsTarget();
            else
            { 
                if(timeToExplode > explodeDelay)
                    Destroy(gameObject);
                else
                {
                    timeToExplode += Time.deltaTime;
                }
            }
        }
    }
}