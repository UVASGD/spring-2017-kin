using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMinionAI : BaseMinionAI {

    public float biteRange;
    public int biteDamage;
    public float maxBiteCd;
    protected float biteCurrCd;
    protected bool biteCd;

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
        //Initialize bite/ranged attack timers
        rangedCd = false;
        rangedCurrCd = 0.0f;
        maxRangedCd = 10.0f;
        biteCd = false;
        biteCurrCd = 0.0f;
        maxBiteCd = 10.0f;
        projectileRange = 2.0f;
        projectileSpeed = 2.0f;
        Debug.Log("Sub start");
    }

    // Update is called once per frame
    protected override void Update()
    {
        /*int health = gameObject.GetComponent<EnemyHealth>().getHp();
        if (dying)
        {
            if (despawnTimer >= 10.0f)
                Destroy(gameObject);
            else
                despawnTimer += Time.deltaTime;
        }*/
        float distance = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
        if(distance < projectileRange){
            if (distance > biteRange) //if too far for bite, then ranged attack
            {

                if (!rangedCd) //attack ranged
                {
                    SnakeShoot();
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

                if (!biteCd)
                {
                    MoveTowardsTarget();
                }
            }
            else
            {
                if (!biteCd)
                {
                    //fix timing as animation comes in
                    biteCd = true;
                    SnakeBite();
                    Debug.Log("CoolDown");
                }
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

	public void SnakeShoot() {
        GameObject newProj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
        newProj.GetComponent<Rigidbody2D>().velocity = ((Vector2)(targetObject.transform.position - gameObject.transform.position)).normalized * projectileSpeed;
    }

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
