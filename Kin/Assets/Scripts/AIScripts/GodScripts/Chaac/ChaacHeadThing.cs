using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaacHeadThing : MonoBehaviour {

    public float angle;

    GameObject player, head;
    Animator anim;
    SpriteRenderer sr, p_sr;

	void Start () {
        player = GameObject.Find("Player");
        head = GameObject.Find("ChaacHead");
        if (head != null) {
            anim = head.GetComponent<Animator>();
            sr = head.GetComponent<SpriteRenderer>();
            p_sr = player.GetComponent<SpriteRenderer>();
            hideHead();
        }
	}
	
	void Update () {
        if (player != null) {
            angle = 180+StaticMethods.AngleBetweenVec2(player.transform.position, transform.position);
            anim.SetFloat("Angle", angle);
            sr.flipX = p_sr.flipX;
        }
	}

    void showHead() {
        head.SetActive(true);
        
    }

    void hideHead() {
        head.SetActive(false);
    }
}
