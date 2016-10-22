using System;
using UnityEngine;
public class PatrolMinion : Kamikaze
{
    int currIndex;
    int size;
	Vector2 startPos;
	float patrolRad;
    Vector2[] positions;
    bool onWay;
    float timePause, currWait;
    bool isWaiting;

	protected new void Start()
	{
        startPos = gameObject.transform.position;
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

        //this.isRanged = false;

        size = 5;
		curState = AIStates.IdleState;
		positions = new Vector2[size + 1];
        patrolRad = 1.0f;

        float angleIncr = (float)(2 * Math.PI) / size;
        positions[0] = startPos;
        for(int x = 1; x < positions.Length; x++)
        {
            //float theta = UnityEngine.Random.Range(0.0f, (float)(2 * Math.PI));
            float theta = x * angleIncr;
            positions[x] = new Vector2(startPos.x + patrolRad * (float)Math.Cos(theta), startPos.y + patrolRad * (float)Math.Sin(theta));
            Debug.Log(positions[x]);
        }
       // Debug.Log(positions);
        currIndex = 0;

        onWay = false;

        timePause = 1.0f;
        currWait = 0.0f;

		//explodeRadius = .2f;
		//timeToExplode = 0.0f;
		//explodeDelay = .8f;
		awarenessRadius = 1.0f;
		//wanderRad = 2.0f;
	}

	protected new void Update()
	{
        Vector2 pos = (Vector2)gameObject.transform.position;
        if (Vector2.Distance(pos, positions[currIndex]) < .05f) //if reached destination
        {
            rb.velocity = Vector2.zero;
            onWay = false;
            isWaiting = true;
            if(currWait > timePause)
            {
                isWaiting = false;
                currWait = 0.0f;
                System.Random rand = new System.Random();
                currIndex = rand.Next(0, 4);
                MoveTowardsPosition(positions[currIndex]);
                onWay = true;
            }
            else
            {
                currWait += Time.deltaTime;
            }
        }
       else if (!isWaiting) //if currently moving
            MoveTowardsPosition(positions[currIndex]);
    }

    //Move linearly towards target
    protected void MoveTowardsPosition(Vector2 pos)
    {
        rb.velocity = (pos - (Vector2) gameObject.transform.position).normalized * speed;
    }


}