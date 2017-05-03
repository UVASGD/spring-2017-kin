using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagtites : MonoBehaviour
{

    public float range; //Range at which proj will decay
    private float distTravelled; //Total distance travelled
    Vector2 previous;//Location last frame

    // Use this for initialization
    void Start ()
    {
        //Initialize distance, range and pos
        range = 5.0f;
        distTravelled = 0.0f;
        previous = (Vector2)transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        distTravelled += Vector2.Distance(((Vector2)transform.position), previous);
        previous = (Vector2)transform.position;
        //Debug.Log (distTravelled);
        if (distTravelled >= range)
            Destroy(gameObject);
    }

    //Handle player collisions
    void OnTriggerEnter2D(Collider2D obj)
    {
        //Debug.Log ("in trigger");
        if (obj.tag == "Player")
        {
            Destroy(gameObject);
            //deals damage
            obj.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
        }// else if (obj.tag == "Terrain") {
         //Destroy (gameObject);
         //} 
    }
}
