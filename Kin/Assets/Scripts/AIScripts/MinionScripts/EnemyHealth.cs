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
        Debug.Log(currentHealth);
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
}