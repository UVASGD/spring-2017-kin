using UnityEngine;
using UnityEditor;
using System.Collections;

public class BaseMinionAI : BaseAI {

	//Ai Damage Values
	float projectileSpeed;
	float meleeDamage;

	//Pause between minion attacks
	float attackMeleeDelay;
	float fireProjDelay;

	float attackRange;
	float projectileRange;
	public Object prefab;
	float curCd;
	bool onCd;


	AIStates curstate;


	// Use this for initialization
	protected override void Start () {
		onCd = false;
		curCd = 0.0f;
		projectileSpeed = 1.0f;
		Debug.Log ("in sub start");
		base.rb = gameObject.GetComponent<Rigidbody2D> ();
		curstate = BaseAI.AIStates.DetectedState;
		//prefab = AssetDatabase.LoadAssetAtPath("Assets/prefabs.MinionProj", typeof(GameObject));

	
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (curstate == BaseAI.AIStates.DetectedState) {
			Debug.Log ("Calling move");
			MoveTowardsTarget ();
			if (!onCd) {
				fireProj (projectileSpeed);
				curCd = .5f;
				onCd = true;
			} else {
				curCd -= Time.deltaTime;
				if (curCd <= 0.0f)
					onCd = false;
			}
	
		}
	}

	protected void fireProj(float projSpeed)
	{
		Vector2 loc = new Vector2(targetObject.transform.position.x,targetObject.transform.position.y);
		GameObject newProj = (GameObject) GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
		newProj.GetComponent<Rigidbody2D> ().velocity = (targetObject.transform.position - gameObject.transform.position).normalized * projSpeed;
		//Debug.Log (newProj.transform.position);


	}

	protected void attackMelee(){
	}




}
