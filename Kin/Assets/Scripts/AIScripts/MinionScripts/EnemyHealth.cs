using UnityEngine;
//using UnityEditor;
using System.Collections;

public class EnemyHealth:MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    bool isDead;
    bool isInvinc;
    public BaseAI AIsrc;
    private bool alreadyArced;
    Animator animator;

    public float recoilDist;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        isDead = false;
		AIsrc = gameObject.GetComponent<BaseAI>();
        alreadyArced = false;
    }
    public void takeDamage(int amount)
    {
        if(!isInvinc)
        {
            currentHealth -= amount;
            //recoil();
	           AIsrc.recoil ();
        }
        // Play damage audio clip
       // if (currentHealth <= 0 && !isDead)
        //{
          //  Death();
        //}
    }

    public void setInvinc(bool b)
    {
        isInvinc = b;
    }

	public int getHp()
	{
		return currentHealth;
	}

    public void recoil()
    {
        //Vector2 dir = ((Vector2)(gameObject.transform.position - GameObject.FindGameObjectsWithTag("Player")[0].transform.position)).normalized * recoilDist;
        //gameObject.GetComponent<Rigidbody2D>().velocity = dir;
        animator.SetBool("Recoiling", true);
    }

    // For Lighting Chain Attack from Chac's Rune
    public void chainDamage(int damage, int distance, Vector3 source)
    {

        // draw arc from source to this enemy
        GameObject arc = Instantiate(Resources.Load("Prefabs/Projectiles/Chain Lightning", typeof(GameObject)) as GameObject,
            source, Quaternion.identity);
        arc.transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation((source - transform.position).normalized), 1);
        Chain chain =  arc.GetComponent<Chain>();
        chain.source = source; chain.target = transform.position;

        alreadyArced = true;
        takeDamage(damage);
        float jumpDistance = 3;
        if (distance > 1)
        {
                //chain damage nearby enemy
                GameObject[] nearbyEnemies = GameObject.FindGameObjectsWithTag("enemy");
                GameObject nearestEnemy = null;
                float minDist = Mathf.Infinity;
                float dist;
                if (nearbyEnemies.Length > 0)
                {
                    foreach (GameObject enemy in nearbyEnemies)
                    {
                        dist = Vector2.Distance(enemy.transform.position, this.transform.position);
                        if (dist < minDist && !enemy.GetComponent<EnemyHealth>().alreadyArced)
                        {
                            minDist = dist;
                            nearestEnemy = enemy;
                        }
                    }

                }
                if (Vector2.Distance(nearestEnemy.transform.position, this.transform.position) < jumpDistance)
                {
                    nearestEnemy.GetComponent<EnemyHealth>().chainDamage(damage * 2 / 3, distance - 1,
                        gameObject.transform.position);
                }
            }

    }
}
