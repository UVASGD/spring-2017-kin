using UnityEngine;
using UnityEditor;
using System.Collections;

public class BaseMinionAI : MonoBehaviour {


	public float awarenessRadius; //Range to change idle->detected
	public GameObject targetObject; //Player target
	public float speed = 1.0f; //Movement speed
	protected Rigidbody2D rb; //Minion Rigidbody
	public bool meleeOnCd;
	public float meleeCurrCd;

	//Set of AI behavior states
	protected enum AIStates {
		IdleState,
		DetectedState
	}
	protected AIStates curState; //Current AI behavior state


	protected void Start () {
		//Establish rigid body for minion
		rb = gameObject.GetComponent<Rigidbody2D>(); 
		if (rb == null) {
			Debug.LogError("AI has no RigidBody. AI name is " + gameObject.name + "!");
		}
		if (targetObject == null) {
			Debug.LogError("AI has no target. AI name is " + gameObject.name + "!");
		}


		meleeOnCd = false;
		meleeCurrCd = 0.0f;



		//Set initial state
		//detected for testing, will normally be idle until awarenessRadius is reached
		curState = AIStates.DetectedState;

		//prefab = AssetDatabase.LoadAssetAtPath("Assets/prefabs.MinionProj", typeof(GameObject));

	
	}
	
	// Update is called once per frame
	protected virtual void Update () {


	}

	//Attack in arc based on circlecast
	protected void attackMelee(){
	}

	//Move linearly towards target
	protected void MoveTowardsTarget() {
		rb.velocity = ((Vector2)(targetObject.transform.position - gameObject.transform.position)).normalized * speed;
	}

	protected void  MoveAwayFromTarget(){
		Debug.Log ("in moveaway");
		rb.velocity = ((Vector2)(gameObject.transform.position - targetObject.transform.position)).normalized * speed;
	}



}
