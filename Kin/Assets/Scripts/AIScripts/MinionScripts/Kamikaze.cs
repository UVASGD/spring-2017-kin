using UnityEngine;
using UnityEditor;
using System.Collections;

public class Kamikaze : BaseMinionAI
{
    
    float explodeRadius; //radius where kamikaze stops and explodes
    bool isExploding;
    float explodeDelay;  //time delay between stopping and exploding
    float timeToExplode; //buildup to the explosion
	public int explodeDamage;
	float decayTime;
	bool exploded;
	bool dying;

    protected new void Start()
    {
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

        curState = AIStates.PatrolState;

        explodeRadius = .4f;
        timeToExplode = 0.0f;
        explodeDelay = .9f;
        awarenessRadius = 1.0f;
		decayTime = 10.0f;
		exploded = false;
        speed = 1.2f;
		dying = false;
    }

    protected new void Update()
    {
        float distanceToPlayer = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);


		if (dying) {
			return;
		}

		if (gameObject.GetComponent<EnemyHealth> ().getHp () <= 0) {
			death ();
			dying = true;
		}

        if (curState == AIStates.DetectedState)
        {
            if (distanceToPlayer >= awarenessRadius)
            {
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
                }
                else
                    MoveTowardsTarget();
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
                curState = AIStates.DetectedState;
        }
    }

	void death()
	{
		gameObject.GetComponent<KamikazeAnimationController> ().dying = true;
		dying = true;
	}

	public void makeNoise(/*string sound*/){
		//AudioClip clip = Resources.Load ("Sounds/Attack SFX/"+sound) as AudioClip;
		GetComponent<AudioSource>().Play();
	}
}