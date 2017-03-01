using UnityEngine;
//using UnityEditor;
using System.Collections;

public class EnemyHealth:MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    bool isDead;
    bool isInvinc;

    public float recoilDist;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
    public void takeDamage(int amount)
    {
        if(!isInvinc)
        {
            currentHealth -= amount;
            recoil();
        }
        //Debug.Log("Took Damage");
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
        Vector2 dir = ((Vector2)(gameObject.transform.position - GameObject.FindGameObjectsWithTag("Player")[0].transform.position)).normalized * recoilDist;
        gameObject.GetComponent<Rigidbody2D>().velocity = dir;
    }

    // For Lighting Chain Attack from Chac's Rune
    public void chainDamage(int damage, int distance)
    {
        takeDamage(damage);
        int jumpDistance = 20;
        if(distance > 1)
        {
            //chain damage nearby enemy
            GameObject[] nearbyEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearestEnemy = null;
            float minDist = Mathf.Infinity;
            float dist;
            if (nearbyEnemies.Length > 0)
            {
                foreach (GameObject enemy in nearbyEnemies)
                {
                    dist = Vector3.Distance(enemy.transform.position, this.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestEnemy = enemy;
                    }
                }
                
            }
            if(Vector3.Distance(nearestEnemy.transform.position, this.transform.position) < jumpDistance){
                EnemyHealth nearbyHealth = nearestEnemy.GetComponent<EnemyHealth>();
                nearbyHealth.chainDamage(damage, distance - 1);
            }
        }
    }
}