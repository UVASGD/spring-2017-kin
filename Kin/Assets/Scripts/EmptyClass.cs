using System;

public class EmptyClass : Kamikaze
{

	Vector2 curPos;
	Vector2 basePos;
	float patrolRad;

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

		curState = AIStates.IdleState;
		float[] positions = new float[2];
		explodeRadius = .2f;
		timeToExplode = 0.0f;
		explodeDelay = .8f;
		awarenessRadius = 1.0f;
		wanderRad = 2.0f;
	}


	public EmptyClass ()
	{

	}


	@Override
	Update()
	{
		float distanceToPlayer = Vector2.Distance ((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);

		if (curState == AIStates.PatrolState) {
			rb.velocity = ((Vector2)(targetObject.transform.position - gameObject.transform.position)).normalized * speed;
		}

		if (curState == AIStates.DetectedState) {
			if (distanceToPlayer < explodeRadius) {
				isExploding = true;
				rb.velocity = Vector2.zero;
			}
			if (!isExploding)
				MoveTowardsTarget ();
			else { 
				if (timeToExplode > explodeDelay)
					Destroy (gameObject);
				else {
					timeToExplode += Time.deltaTime;
				}
			}
		} else {
			if (distanceToPlayer < awarenessRadius)
				curState = AIStates.DetectedState;
		}
	}
}