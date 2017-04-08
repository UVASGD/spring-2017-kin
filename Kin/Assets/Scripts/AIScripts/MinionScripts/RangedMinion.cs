using UnityEngine;
using System.Collections;

public class RangedMinion : BaseMinionAI {

	public float projectileSpeed; //Speed of the projectile. Targetting messes up if projectile speed < player speed away from the minion (because shots hit player at t=infinity)
	float rangedCurrCd; //Remaining cooldown for ranged attack
	float fireProjDelay; //Delay between proj attacks
	bool rangedOnCd; //Is ranged attack on cooldown
	float projectileRange; //Decay range for projectile decay
	float fireRadius; //Ideal firing range
	public float accuracy;//Accuracy of shots, higher is better

	public float attackCooldown;
	const float ANGLE_THRESHOLD = Mathf.PI/4; //For targetting when standing still

	//Fires projectile at player loc based on parameter speed
	protected void fireProj()
	{
		float angle = predictLocation();
		//Instantiate projectile from prefab Instantiate(prefab,minionposition,no rotation)
		GameObject newProj = (GameObject) GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
		newProj.GetComponent<Rigidbody2D> ().velocity = new Vector2(projectileSpeed*Mathf.Cos(angle),projectileSpeed*Mathf.Sin(angle));
		//Debug.Log (newProj.transform.position);
	}
	
	protected float predictLocation(){
		float C2 = (gameObject.transform.position.x-targetObject.transform.position.x);
		float C3 = (targetObject.transform.position.y-gameObject.transform.position.y);
		float C1 = ((targetObject.GetComponent<Rigidbody2D> ().velocity.y*C2 + targetObject.GetComponent<Rigidbody2D> ().velocity.x*C3)/projectileSpeed);
		float leading = 2*Mathf.Atan((C2+Mathf.Sqrt(-1*(C1*C1)+C2*C2+C3*C3))/(C1+C3));
		float still = Mathf.Atan2(C3,-C2);
		float difference = leading-still;
		float lagangle;
		float overleadangle;
		if(Mathf.Abs(difference)<0.1){//Player is standing still relative to enemy
			lagangle = still + ANGLE_THRESHOLD;
			overleadangle = still - ANGLE_THRESHOLD;
		}
		else {
			lagangle = still;
			//difference = difference>3?difference-2*Mathf.PI:difference; //Attempting to fix bug in low accuracy shots when crossing -x axis with respect to enemy.
			//difference = difference<-3?difference+2*Mathf.PI:difference;
			overleadangle = leading+difference;
		}
		float angle = accuracyRand()*(overleadangle-lagangle)+lagangle;
		//Debug.Log("("+lagangle+","+leading+","+overleadangle+")->"+angle+","+difference);
		return angle;
	}

	/*
	function accuracyRand
	This function returns a random number such that:
		the max value is >=0.5, <=1, and decreases as accuracy increases
		the min value is <=0.5, >=0, and increases as accuracy increases
		(max+min)/2 = 0.5, ie the two values are equidistant from 0.5
	A returned value of 0.5 is a perfectly led shot.
	*/
	protected float accuracyRand(){
		//Requirements for spread calculation
		//accuracy = 0 -> spread = .5
		//accuracy = infin -> spread = 0	
		//so maybe use spread = .5/(accuracy+1)

		float spread = 0.5f/(accuracy/100+1);
		float mxval = 0.5f+spread;
		float mnval = 0.5f-spread;
		return Random.value*(mxval-mnval)+mnval;
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
		fireRadius = .8f;
		//Initialize melee/ranged attack timers
		rangedOnCd = false;
		rangedCurrCd = 0.0f;
		projectileSpeed = 2.0f;
		Debug.Log ("Sub start");

        awarenessRadius = 1.0f;
        curState = AIStates.PatrolState;
    }
	
	// Update is called once per frame

	 protected override void Update () {
        //If aggressive, attack player
        //Eventually will attacktype based on minion type
        //If minions will be limited to one type of attack we can remove redundant Cd variables
        float distanceToPlayer = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
        if (curState == AIStates.DetectedState) {
            if(distanceToPlayer >= awarenessRadius)
            {
                curState = AIStates.IdleState;
                return;
            }
            //Check distance to player, move towards if beyond a certain radius, fire in the middle, away if too close
            if (distanceToPlayer > fireRadius + .1f)
			MoveTowardsTarget ();
		else if (distanceToPlayer < fireRadius + .1f && distanceToPlayer > fireRadius - .1f) {
			rb.velocity = Vector2.zero;
			//Fire and set to cooldown
			if (!rangedOnCd) {
				fireProj ();
				rangedCurrCd = attackCooldown;
				rangedOnCd = true;
			} else { //Decrease remaining cooldown
				rangedCurrCd -= Time.deltaTime;
				if (rangedCurrCd <= 0.0f)
					rangedOnCd = false;
			}
		} else {
			MoveAwayFromTarget ();
			//Fire and set to cooldown
			if (!rangedOnCd) {
				fireProj ();
				rangedCurrCd = attackCooldown;
				rangedOnCd = true;
			} else { //Decrease remaining cooldown
				rangedCurrCd -= Time.deltaTime;
				if (rangedCurrCd <= 0.0f)
					rangedOnCd = false;
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
}
