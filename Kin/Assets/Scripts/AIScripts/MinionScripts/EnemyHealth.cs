using UnityEngine;
//using UnityEditor;
using System.Collections;

public class EnemyHealth:MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
    public void takeDamage(int amount)
    {
        currentHealth -= amount;
    }

	public int getHp()
	{
		return currentHealth;
	}


}