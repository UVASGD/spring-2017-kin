using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuetzalcotlAI : MonoBehaviour {

	public const float MAX_ATTACK_CD = 5.0f;
	public const float MAX_STALAG_CD = 10.0f;
	public const float MAX_SNAKE_CD = 15.0f;
	public const float MAX_METEOR_CD = 2.5f;

	public const float ANGERY_ACTIVATE = 60.0f;

	private bool dead = false;

	private float curAttackCD = 0.0f;
	private float curStalagCD = 0.0f;
	private float curSnakeCD = 0.0f;
	private float curMeteorCD = 0.0f;

	public int attackDamage = 10;
	public int stalagDamage = 5;
	public int meteorDamage = 8;

	public float aggroRadius = 4.0f;

	public GameObject wallCreator;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CloseArena() {

	}

	public void SpawnStalagtites() {

	}

	public void SpawnSnakes() {

	}

	public void ShootMeteor() {

	}

	public void SwipeFist() {

	}

	public void SlamFist() {

	}

	public void BiteHead() {

	}
}
