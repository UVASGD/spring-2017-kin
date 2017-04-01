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

    public float stageRadius = 4.0f;
    protected float offset = 0.3f;

	public GameObject wallCreator;


	// Use this for initialization
	void Start ()
    {
        CloseArena();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CloseArena()
    {
        /*
        //Create Circle Arena
        for(int x = 0; x < 360; x+= 5)
        {
            float rad = Mathf.PI / 180 * x;
            GameObject newMinion = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x + stageRadius* Mathf.Cos(rad), gameObject.transform.position.y + stageRadius * Mathf.Sin(rad)), Quaternion.identity);
        }
        */

        //Create Square Arena
        //top wall
        for(int x = 0; x < 14; x++)
        {
            GameObject wallPart1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x + offset * x, gameObject.transform.position.y + stageRadius), Quaternion.identity);
            wallPart1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            GameObject wallPart2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x - offset * x, gameObject.transform.position.y + stageRadius), Quaternion.identity);
            wallPart2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        //bottom wall
        for (int x = 0; x < 14; x++)
        {
            GameObject wallPart1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x + offset * x, gameObject.transform.position.y - stageRadius), Quaternion.identity);
            wallPart1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            GameObject wallPart2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x - offset * x, gameObject.transform.position.y - stageRadius), Quaternion.identity);
            wallPart2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        //left wall
        for (int x = 0; x < 14; x++)
        {
            GameObject wallPart1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x + stageRadius, gameObject.transform.position.y + offset * x), Quaternion.identity);
            wallPart1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            GameObject wallPart2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x + stageRadius, gameObject.transform.position.y - offset * x), Quaternion.identity);
            wallPart2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        //right wall
        for (int x = 0; x < 14; x++)
        {
            GameObject wallPart1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x - stageRadius, gameObject.transform.position.y + offset * x), Quaternion.identity);
            wallPart1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            GameObject wallPart2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Brain the Hotdog", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x - stageRadius, gameObject.transform.position.y - offset * x), Quaternion.identity);
            wallPart2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
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
