using UnityEngine;
using UnityEditor;
using System.Collections;

public class BaseMinionAI : MonoBehaviour {


	float projectileSpeed; //Projectile Speed
	float rangedCurrCd; //Remaining cooldown for ranged attack
	float fireProjDelay; //Delay between proj attacks
	bool rangedOnCd; //Is ranged attack on cooldown
	float projectileRange; //Decay range for projectile decay

	float meleeDamage; //Melee attack damage
	float attackMeleeDelay; //Pause between minion attacks
	float attackRange; //Range to melee attack from
	bool meleeOnCd; //Is melee attack on cooldown
	float meleeCurrCd; //Remaining cooldown for melee attack

	public float awarenessRadius; //Range to change idle->detected
	public GameObject targetObject; //Player target
	public float speed = 1.0f; //Movement speed
	protected Rigidbody2D rb; //Minion Rigidbody

	//Set of AI behavior states
	protected enum AIStates {
		IdleState,
		DetectedState
	}
	AIStates curState; //Current AI behavior state


	protected void Start () {
		//Establish rigid body for minion
		rb = gameObject.GetComponent<Rigidbody2D>(); 
		if (rb == null) {
			Debug.LogError("AI has no RigidBody. AI name is " + gameObject.name + "!");
		}
		if (targetObject == null) {
			Debug.LogError("AI has no target. AI name is " + gameObject.name + "!");
		}
		//Initialize melee/ranged attack timers
		rangedOnCd = false;
		rangedCurrCd = 0.0f;
		projectileSpeed = 1.0f;
		meleeOnCd = false;
		meleeCurrCd = 0.0f;

		//Set initial state
		//detected for testing, will normally be idle until awarenessRadius is reached
		curState = AIStates.DetectedState;
		//prefab = AssetDatabase.LoadAssetAtPath("Assets/prefabs.MinionProj", typeof(GameObject));

	
	}
	
	// Update is called once per frame
	protected void Update () {

		//If aggressive, attack player
		//Eventually will attacktype based on minion type
		//If minions will be limited to one type of attack we can remove redundant Cd variables
		if (curState == AIStates.DetectedState) {
			MoveTowardsTarget ();
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
	//Fires projectile at player loc based on parameter speed
	protected void fireProj(float projSpeed)
	{
		//Instantiate projectile from prefab Instantiate(prefab,minionposition,no rotation)
		GameObject newProj = (GameObject) GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
		newProj.GetComponent<Rigidbody2D> ().velocity = (targetObject.transform.position - gameObject.transform.position).normalized * projSpeed;
		//Debug.Log (newProj.transform.position);


	}
	//Attack in arc based on circlecast
	protected void attackMelee(){
	}

	//Move linearly towards target
	protected void MoveTowardsTarget() {
		rb.velocity = ((Vector2)(targetObject.transform.position - gameObject.transform.position)).normalized * speed;
	}



}
