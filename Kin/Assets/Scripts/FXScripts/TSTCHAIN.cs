using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSTCHAIN : MonoBehaviour {

    //public GameObject target;
    public float radius = 4;
    public float minRadius = 1;
    public int spawnCount = 1;
    public List<GameObject> enemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Interact")/* && target != null*/)
            spawn();
            //shoot(gameObject.transform.position);
	}

    void spawn() {
        for(int i = 0; i<spawnCount; i++) {
            float d = Random.Range(minRadius, radius);
            float a = Random.Range(0, 360);
            int t = Random.Range(0, enemies.Count);
            GameObject en = Instantiate(enemies[t], transform.position + new Vector3(d * Mathf.Cos(a * Mathf.Deg2Rad),
                d * Mathf.Sin(a * Mathf.Deg2Rad),0), Quaternion.identity) as GameObject;
        }
    }

    void shoot(Vector2 source) {
        // draw arc from source to this enemy
        //GameObject arc = Instantiate(Resources.Load("Prefabs/Projectiles/Chain Lightning", typeof(GameObject)) as GameObject,
        //   source, Quaternion.identity);

        //Chain chain = arc.GetComponent<Chain>();
        //chain.source = gameObject; chain.target = target;
        //chain.forceStart();
    }
}
