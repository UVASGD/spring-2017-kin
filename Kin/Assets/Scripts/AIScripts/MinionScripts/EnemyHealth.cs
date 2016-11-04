using UnityEngine;
using UnityEditor;
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
        Debug.Log("Took Damage");
        // Play damage audio clip
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject);
        //anim.SetBool("Dying", true);
        //play visual + audio death
    }
}
