using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {

    [HideInInspector]
    public GameObject source, target;
    public Sprite sprite;

    private List<GameObject> links;
    private float life = 0;
    
	void Start () {
	}

    public void forceStart() {
        gameObject.transform.parent = source.transform;
        links = new List<GameObject>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
            links.Add(gameObject.transform.GetChild(i).gameObject);
        
        if (sprite != null) {
            // create as many pieces of lightning as necessary to fill gap
            float distance = StaticMethods.Distance((Vector2)source.transform.position,
                (Vector2)target.transform.position);
            float margin = distance * StaticMethods.PPU - sprite.rect.size.y;
            //Debug.Log("M: " + margin);
            if (margin > 0) {
                for (int i = 0; i < distance % sprite.rect.size.y - 1; i++) {
                    GameObject cur = Instantiate(links[links.Count - 1]) as GameObject;
                    links.Add(cur);
                    cur.transform.parent = gameObject.transform;

                    lookAt(cur, source, target);
                    float theta = StaticMethods.AngleBetweenVec2((Vector2)source.transform.position,
                        (Vector2)target.transform.position);
                    cur.transform.position = source.transform.position + new Vector3(sprite.rect.size.x * Mathf.Cos(theta),
                        sprite.rect.size.y * Mathf.Sin(theta)) * (i + .5f) / (StaticMethods.PPU);
                }
                // partially fill final lightning
                mask(links[links.Count-1], distance - (distance % sprite.rect.size.y) * sprite.rect.size.y);
            } else { // can only fill once, partially
                mask(links[0], margin);
            }
        } else {
            Debug.Log("Must have a source sprite to create chain.");
        }
    }

    private void mask(GameObject go, float len) {
        //Rect r = go.GetComponent<SpriteRenderer>().sprite.rect;
        //r.Set(r.x,r.y,r.width, len);
    }

    public void kill() {
        //foreach (GameObject g in lightning) Destroy(g);
        Destroy(gameObject);
    }

    void Update() {
        life += Time.deltaTime;
        if (life > .5f) kill();
        lookAt(gameObject, source, target);

        if (sprite != null) {
            float distance = StaticMethods.Distance((Vector2)source.transform.position,
                (Vector2)target.transform.position);
            float margin = distance * StaticMethods.PPU - sprite.rect.size.y;
            if (margin > 0) {
                float theta = StaticMethods.AngleBetweenVec2((Vector2)source.transform.position,
                    (Vector2)target.transform.position)/Mathf.Rad2Deg;

                for (int i = 1; i < links.Count; i++) {
                    GameObject cur = links[i];
                    lookAt(cur, source, target);
                    cur.transform.position = source.transform.position + new Vector3(sprite.rect.size.y * Mathf.Cos(theta),
                        sprite.rect.size.y * Mathf.Sin(theta)) * (i + .5f) / (StaticMethods.PPU);
                }
                // partially fill final lightning
                mask(links[links.Count-1], distance - (distance % sprite.rect.size.y) * sprite.rect.size.y);
            } else { // can only fill once, partially
                mask(links[0], margin);
            }
        }
    }

    private void lookAt(GameObject rot, GameObject source, GameObject target) {
        Vector3 diff = source.transform.position - target.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rot.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
