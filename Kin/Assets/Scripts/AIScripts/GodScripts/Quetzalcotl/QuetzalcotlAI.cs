using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuetzalcotlAI : MonoBehaviour {

	public const float MAX_ATTACK_CD = 5.0f;
	public const float MAX_STALAG_CD = 10.0f;
    public const float MAX_SNAKE_CD = 15.0f; //15.0f;
	public const float MAX_METEOR_CD = 6.0f;

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
    private float offset = 0.3f;
    public GameObject targetObject;
    private float baseX;
    private float changeX;
    private float baseY;
    private float changeY;
    private float radius = 1.0f;
    private int numStalagSpots = 3;
    private int numStalag = 4;

	public GameObject wallCreator;


	// Use this for initialization
	void Start ()
    {
        baseX = gameObject.transform.position.x;
        baseY = gameObject.transform.position.y;

        changeX = 2.0f;
        changeY = 2.0f;
        //CloseArena();		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if (curMeteorCD <= 0.0f)
        {
            ShootMeteor();
            curMeteorCD = MAX_METEOR_CD;
        }
        else
            curMeteorCD -= Time.deltaTime;
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
        //currently, square around boss location, need to change once moved off cliff
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

	public void SpawnStalagtites()
    {
        for(int x = 0; x < numStalagSpots; x++)
        {
            float centerX = Random.Range(gameObject.transform.position.x - changeX, gameObject.transform.position.x + changeX);
            float centerY = Random.Range(gameObject.transform.position.y, gameObject.transform.position.y + changeY);

            for(int y = 0; y < numStalag; y++)
            {
                float ang = Random.Range(0, 2*Mathf.PI);
                float rad = Random.Range(0, radius);
                GameObject proj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Projectiles/LightningBolt", typeof(GameObject)),
                    new Vector3(centerX + rad * Mathf.Cos(ang), gameObject.transform.position.y + rad * Mathf.Sin(ang)), Quaternion.identity);
            }
        }
    }

    public void SpawnSnakes()
    {
        foreach (Vector2 vec in calculateAngles(4, 0.5f))
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Snake", typeof(GameObject)) as GameObject,
                vec, Quaternion.identity) as GameObject;
        }
    }

    private List<Vector2> calculateAngles(int num, float distanceAway)
    {
        if (num == 0)
        {
            //return new List<Vector2>();
            num = 3;
        }
        int angleDisplacement = 120 / num;
        int curAngle = -60;
        List<Vector2> vecList = new List<Vector2>();
        for (int i = 0; i < num; i++)
        {
            vecList.Add((Vector2)gameObject.transform.position +
                new Vector2(Mathf.Cos(Mathf.Deg2Rad * curAngle), Mathf.Sin(Mathf.Deg2Rad * curAngle)).normalized * distanceAway);
            curAngle += angleDisplacement;
        }
        return vecList;
    }

    public void ShootMeteor()
    {
        float angle = Mathf.Atan2(targetObject.transform.position.y - gameObject.transform.position.y, targetObject.transform.position.x - gameObject.transform.position.x);
        GameObject proj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Projectiles/Meteor", typeof(GameObject)),
            new Vector3(gameObject.transform.position.x + 0.1f * Mathf.Cos(angle), gameObject.transform.position.y + 0.1f * Mathf.Sin(angle)), Quaternion.identity);
        float speed = proj.GetComponent<MeteorProjectile>().speed;
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));

        //proj.GetComponent<MeteorProjectile>().SetVelocity(angle);
    }

	public void SwipeFist() {

	}

	public void SlamFist() {

	}

	public void BiteHead() {

	}
}
