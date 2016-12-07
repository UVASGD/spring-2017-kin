﻿using UnityEngine;
using System.Collections;

public class ChacAI : BaseGodAI
{
	public float maxInvincCd;
	protected float invincCurrCd;
	float meleeRange;
	public float maxMeleeCd;
	protected float meleeCurrCd;
	protected bool meleeCd;
	public float maxRangedCd;
	protected float rangedCurrCd;
	protected bool rangedCd;
	float fireBoltCd = 0;
	bool boltOnCd = false;
	float accuracy = 100;
	float boltSpeed = 2.0f;
	const float ANGLE_THRESHOLD = Mathf.PI/4;

	//Set of AI behavior states
	protected enum AIStates
	{
		InvincibleState, MeleeState, IdleState
	}
	protected AIStates curState; //Current AI behavior state

	protected new void Start()
	{
		base.Start();

		invincCurrCd = maxInvincCd;

		curState = AIStates.IdleState;
	}

	protected new void Update()
	{
		switch(curState)
		{
		case AIStates.IdleState:
			if (true)//some condition
				curState = AIStates.InvincibleState;
			break;
		case AIStates.InvincibleState:
			int health = gameObject.GetComponent<EnemyHealth>().getHp();
			int maxHealth = gameObject.GetComponent<EnemyHealth>().maxHealth;
			if(health < maxHealth/3) //final invincible phase
			{
				//lightning and geysers
			}
			else if(health < (2/3) * maxHealth) //2nd invincible phase
			{
				//lightning
			}
			else //first invincibility phase
			{
				//geysers
			}

			if(invincCurrCd <= 0) //change to melee attacks after some time
			{
				curState = AIStates.MeleeState;
				invincCurrCd = maxInvincCd;
				//come down and shockwave
			}
			else
				invincCurrCd -= Time.deltaTime;
			break;
		case AIStates.MeleeState:
			float distance = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
			if(distance > meleeRange) //if too far for melee, then ranged attack
			{
				if (!rangedCd)
				{
					//attack ranged
					rangedCd = true;
				}
				else //cooldown after shooting
				{
					if (rangedCurrCd <= 0)
					{
						rangedCurrCd = maxRangedCd;
						rangedCd = false;
					}
					else
						rangedCurrCd -= Time.deltaTime;
				}
			}
			else
			{
				if(!meleeCd)
				{
					//attack melee
					meleeCd = true;
				}
				else //cooldown after melee
				{
					if (meleeCurrCd <= 0)
					{
						meleeCurrCd = maxMeleeCd;
						meleeCd = false;
					}
					else
						meleeCurrCd -= Time.deltaTime;
				}
			}
			break;
		default:
			Debug.Log("Unknown state. Something be wrong mudda poop");
			break;
		}
	}
	void fireBolt(int type){
		GameObject newProj1, newProj2, newProj3, newProj4, newProj5, newProj6;
		float angle = predictLocation();
		switch (type) {
		case 1:
			//Instantiate projectile from prefab Instantiate(prefab,minionposition,no rotation)
			newProj1 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle-.5f), boltSpeed * Mathf.Sin (angle-.5f));
			newProj2 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle), boltSpeed * Mathf.Sin (angle));
			newProj3 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj3.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle+.5f), boltSpeed * Mathf.Sin (angle+.5f));
			break;
		case 2:
			newProj1 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle), boltSpeed * Mathf.Sin (angle));
			break;
		case 3:
			newProj1 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle - 1.0f), boltSpeed * Mathf.Sin (angle - 1.0f));
			newProj2 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle - .5f), boltSpeed * Mathf.Sin (angle - .5f));
			newProj3 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj3.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle), boltSpeed * Mathf.Sin (angle));
			newProj4 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj4.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle + .5f), boltSpeed * Mathf.Sin (angle + .5f));
			newProj5 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj5.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle + 1.0f), boltSpeed * Mathf.Sin (angle + 1.0f));
			newProj6 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj6.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle + 1.5f), boltSpeed * Mathf.Sin (angle + 1.5f));
			break;

		default:
			return;
		} 

	}

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

	protected float predictLocation(){
		float C2 = (gameObject.transform.position.x-targetObject.transform.position.x);
		float C3 = (targetObject.transform.position.y-gameObject.transform.position.y);
		float C1 = ((targetObject.GetComponent<Rigidbody2D> ().velocity.y*C2 + targetObject.GetComponent<Rigidbody2D> ().velocity.x*C3)/boltSpeed);
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

}
