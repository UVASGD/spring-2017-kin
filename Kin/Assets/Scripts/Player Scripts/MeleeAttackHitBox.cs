using UnityEngine;
//using UnityEditor;
using System.Collections;

public class MeleeAttackHitBox:MonoBehaviour
{
    private int damage;
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("meme");
        if (col.gameObject.tag == "enemy")
        {
            //Debug.Log("Collided");
            col.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
        }
    }

    public void setDamage(int d)
    {
        damage = d;
    }
}
