using UnityEngine;
using System.Collections;

public class RangedMinion : BaseMinionAI {

	float projectileSpeed; //Projectile Speed
	float rangedCurrCd; //Remaining cooldown for ranged attack
	float fireProjDelay; //Delay between proj attacks
	bool rangedOnCd; //Is ranged attack on cooldown
	float projectileRange; //Decay range for projectile decay
	float fireRadius; //Ideal firing range
	public bool isRanged; //Is ranged minion


	//Fires projectile at player loc based on parameter speed
	protected void fireProj(float projSpeed)
	{
		//Instantiate projectile from prefab Instantiate(prefab,minionposition,no rotation)
		GameObject newProj = (GameObject) GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
		newProj.GetComponent<Rigidbody2D> ().velocity = (targetObject.transform.position - gameObject.transform.position).normalized * projSpeed;
		//Debug.Log (newProj.transform.position);


	}

	// Use this for initialization
	void Start () {
		fireRadius = .8f;
		//Initialize melee/ranged attack timers
		rangedOnCd = false;
		rangedCurrCd = 0.0f;
		projectileSpeed = 1.0f;
		isRanged = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		//If aggressive, attack player
		//Eventually will attacktype based on minion type
		//If minions will be limited to one type of attack we can remove redundant Cd variables
		if (curState == AIStates.DetectedState) {
			//Determine if ranged attacker
			if (isRanged) {
				//Check distance to player, move towards if beyond a certain radius, fire in the middle, away if too close
				float distanceToPlayer = Vector2.Distance ((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
				if (distanceToPlayer > fireRadius + .1f)
					MoveTowardsTarget ();
				else if (distanceToPlayer < fireRadius + .1f && distanceToPlayer > fireRadius - .1f) {
					rb.velocity = Vector2.zero;
					//Fire and set to cooldown
					if (!rangedOnCd) {
						fireProj (projectileSpeed);
						rangedCurrCd = .5f;
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
						fireProj (projectileSpeed);
						rangedCurrCd = .5f;
						rangedOnCd = true;
					} else { //Decrease remaining cooldown
						rangedCurrCd -= Time.deltaTime;
						if (rangedCurrCd <= 0.0f)
							rangedOnCd = false;
					}
				}
			}
		}
	
	}
}
