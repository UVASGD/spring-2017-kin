using UnityEngine;
using System.Collections;

public class SkullCandy : MonoBehaviour {
    
    public int damage;
    public float duration, radiusOfEffect, damageCooldown, delay;

    private float age, currentCooldown;
    private bool killMePls = false;
    private Animator a;

    private enum States {
        tell,
        damage,
        fade
    }

    private States currState = States.tell;

    // Use this for initialization
    void Start() {
        a = gameObject.GetComponent<Animator>();
        age = 0;
        currentCooldown = 0;
    }

    // Update is called once per frame
    void Update() {
        //Stuff we always gotta do
        age += Time.deltaTime;

        switch (currState) {
            case States.tell:
                //Stuff we do during the "tell" phase before damage
                if (age > delay) {
                    currState = States.damage;
                    age = 0;
                    //Set animator to go to shooting-up stage here
                }
                break;
            case States.damage:
                //Stuff we do during the damaging phase
                currentCooldown -= Time.deltaTime;
                if (age > duration) {
                    currState = States.fade;
                    //Set animator to falling-down stage here
                } else if (currentCooldown <= 0) {
                    Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(gameObject.transform.position, radiusOfEffect);
                    for (int i = 0; i < nearbyObjects.Length; i++) {
                        if (nearbyObjects[i].tag == "Player") {
                            nearbyObjects[i].gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                            currentCooldown = damageCooldown;
                        }
                    }
                    currState = States.fade;
                }
                break;
            case States.fade:
                //Stuff we do while fading
                killMePls = true;
                break;
            default:
                break;
        }
    }

    // set direction based off spawn point
    public void setVelocity(Vector3 ownPos) {
        //gameObject.GetComponent<Rigidbody2D>().velocity = him8;
    }

    void justkillmenowthisworldSUX() {
        if (killMePls) {
            a.SetBool("Kill", true);
        }
    }

    void yee() {
        Destroy(gameObject);
    }

    void OnDrawGizmos() {
        // Detect Range
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.15f);
        Gizmos.DrawSphere(gameObject.transform.position, radiusOfEffect);
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.10f);
        Gizmos.DrawSphere(gameObject.transform.position, radiusOfEffect + 0.1f);
    }

}
