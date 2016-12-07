using UnityEngine;
//using UnityEditor;
using System.Collections;

public class MeleeAttackHitBox:MonoBehaviour
{
    private int damage;
    void OnTriggerEnter2D(Collider2D col)
    {


		Vector3 direction = col.transform.position - transform.position;
		Debug.Log ("direction" + direction);
		Debug.Log ("direction for " + Vector3.Dot(transform.forward, direction));
		/*if (Vector3.Dot (transform.forward, direction) > 0) {
			print ("Back");
		} 
		if (Vector3.Dot (transform.forward, direction) < 0) {
			print ("Front");
		} 
		if (Vector3.Dot (transform.forward, direction) == 0) {
			print ("Side");
		}
        //Debug.Log("meme");*/
        if (col.gameObject.tag == "enemy" || col.gameObject.tag == "Boss")
        {
            //Debug.Log("Collided");
            col.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
			//Get the current enemy
			//Vector3 temp = new Vector3(1.0f,0,0);
			//col.gameObject.transform.position += temp;
        }
    }

    public void setDamage(int d)
    {
        damage = d;
    }
}
