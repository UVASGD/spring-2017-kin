using UnityEngine;
//using UnityEditor;
using System.Collections;

public class Kamikaze : BaseMinionAI
{
    public int experience = 100;
    float explodeRadius; //radius where kamikaze stops and explodes
    bool isExploding;
    float explodeDelay;  //time delay between stopping and exploding
    float timeToExplode; //buildup to the explosion
	public int explodeDamage;
	float decayTime;
	bool exploded;
	bool dying;
	float timeSinceDeath;

    protected new void Start()
    {
        base.Start();

        //Establish rigid body for minion
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("AI has no RigidBody. AI name is " + gameObject.name + "!");
        if (targetObject == null)
            Debug.LogError("AI has no target. AI name is " + gameObject.name + "!");

        curState = AIStates.PatrolState;

        explodeRadius = 0.4f;
        timeToExplode = 0.0f;
		timeSinceDeath = 0.0f;
        explodeDelay = .9f;
		if(awarenessRadius == 0.0f) awarenessRadius = 2.0f; //3.0f;
		decayTime = 10.0f;
		exploded = false;
        speed = 1.0f;
		dying = false;
    }

    protected new void Update() {
        base.Update();
        if (!gameObject.GetComponent<Animator>().GetBool("Spawned")) return;
        float distanceToPlayer = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);

		if (dying)
		{
			timeSinceDeath += Time.deltaTime;
			if (timeSinceDeath > decayTime)
				Destroy (gameObject);
			return;
		}
		if (gameObject.GetComponent<EnemyHealth> ().getHp () <= 0) {
			killed ();
			dying = true;
		}

        if (curState == AIStates.DetectedState)
        {
            if (distanceToPlayer >= awarenessRadius)
            {
                StopCoroutine("FollowPath");
                rb.velocity = Vector2.zero;
                curState = AIStates.IdleState;
                return;
            }
            if (!isExploding)
            {
                if (distanceToPlayer < explodeRadius)
                {
                    isExploding = true;
					gameObject.GetComponent<KamikazeAnimationController> ().charging = true;
                    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    StopCoroutine("FollowPath");
                    rb.velocity = Vector2.zero;
                }
                else{
                    if (timeDelay > 1.0f)
                      {
                          RequestPathManager.Request(transform.position, targetObject.transform.position, OnPathFound);
                          timeDelay = 0;
                      }
					//MoveTowardsTarget ();
                    }
            }
            else
            {
                if (timeToExplode > explodeDelay)
                {
                        //Debug.Log("Hurt");
						if (!exploded) {
							if (distanceToPlayer < explodeRadius)
								targetObject.GetComponent<PlayerHealth> ().TakeDamage (explodeDamage);
							exploded = true;
                        GetComponent<Collider2D>().enabled = false;
						}
                    //Debug.Log("Explode");
					timeToExplode+= Time.deltaTime;
					if (timeToExplode > decayTime)
                   		Destroy(gameObject);
                }
                else
                {
                    timeToExplode += Time.deltaTime;
                }
            }
        }
        else
        {
            Patrol();
            if (distanceToPlayer < awarenessRadius)
            {
                curState = AIStates.DetectedState;
                isWaiting = false;
            }
        }
    }

	void killed()
	{
		gameObject.GetComponent < KamikazeAnimationController>().killed = true;
		GetComponent<Collider2D>().enabled = false;
		dying = true;
        Experience(experience);
	}

	public bool getExploded() {
		return exploded;
	}
		

	public void makeNoise(/*string sound*/){
		//AudioClip clip = Resources.Load ("Sounds/Attack SFX/"+sound) as AudioClip;
		GetComponent<AudioSource>().Play();
	}
}