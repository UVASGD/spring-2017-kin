using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMinionAI : BaseMinionAI {

    public float biteRange;
    public float tooCloseRange;
    public int biteDamage;
    public float maxBiteCd;
    protected float biteCurrCd;
    protected bool biteCd;
    protected float despawnTimer;

    public float maxRangedCd;
    protected float rangedCurrCd;
    protected bool rangedCd;
    protected float projectileRange;
    protected float fireRadius; //Ideal firing range
    public float projectileSpeed;

    SpriteRenderer render;

    // Use this for initialization
    new void Start () {
        base.Start();
        fireRadius = .8f;
        tooCloseRange = biteRange / 2;
        //Initialize bite/ranged attack timers
        rangedCd = false;
        rangedCurrCd = 0.0f;
        maxRangedCd = 10.0f;
        despawnTimer = 0.0f;
        biteCd = false;
        biteCurrCd = 0.0f;
        maxBiteCd = 1.0f;
        projectileRange = 2.0f;
        projectileSpeed = 2.0f;
        targetObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        int health = gameObject.GetComponent<EnemyHealth>().getHp();
		GetComponent<SpriteRenderer>().flipX = rb.velocity.x < 0;
        if (health <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			Destroy(this.gameObject);
        }
        float distance = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
        if(distance < biteRange)
        {
            if(distance < tooCloseRange)
            {
                MoveAwayFromTarget();
                return;
            }
            if (!biteCd)
            {
                //fix timing as animation comes in
                biteCd = true;
                SnakeBite();
            }
            if (biteCurrCd <= 0.0)
            {
                biteCurrCd = maxBiteCd;
                biteCd = false;
            }
            else
            {
                biteCurrCd -= Time.deltaTime;
            }
        }
        else
        {
            MoveTowardsTarget();
        }
    }

	/*public void SnakeShoot() {
        GameObject newProj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
        newProj.GetComponent<Rigidbody2D>().velocity = ((Vector2)(targetObject.transform.position - gameObject.transform.position)).normalized * projectileSpeed;
    }*/

	public void SnakeBite() {
        //Debug.Log("Attacking");
        GameObject[] thingsToAttack = ObjectsInAttackArea(targetObject.transform.position.x > rb.transform.position.x, biteRange);
        //Attack Everything In This List
        if (thingsToAttack.Length > 0)
        {
            if (thingsToAttack[0].tag == "Player")
            {
                targetObject.GetComponent<PlayerHealth>().TakeDamage(biteDamage);
            }
        }

    }

    private GameObject[] ObjectsInAttackArea(bool direction /* false==left, true==right */, float attackRadius)
    {
        Collider2D[] allCollidersInRadius = Physics2D.OverlapCircleAll(rb.transform.position, attackRadius * 1.2f);
        List<GameObject> matches = new List<GameObject>();
        for (int i = 0; i < allCollidersInRadius.Length; i++)
        {
            GameObject target = allCollidersInRadius[i].gameObject;
            if (target.tag == "Player")
            {
                float xDifference = target.GetComponent<Rigidbody2D>().transform.position.x - rb.transform.position.x;
                if (direction)
                { //Right Side
                    if (xDifference >= 0)
                    {
                        matches.Add(allCollidersInRadius[i].gameObject);
                    }
                }
                else
                { //Left Side
                    if (xDifference <= 0)
                    {
                        matches.Add(allCollidersInRadius[i].gameObject);
                    }
                }
            }
        }
        return matches.ToArray();
    }
}
