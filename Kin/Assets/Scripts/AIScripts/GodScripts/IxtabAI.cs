using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(EnemyHealth), (typeof(Animator)), (typeof(Rigidbody2D)))]
public class IxtabAI : MonoBehaviour, BaseAI {

	public GameObject player;
	public GameObject UI;
	public float detectRange;
	public float speed;
	private EnemyHealth enemyHealth;
	private Animator anim;
	private Rigidbody2D rb2;
	public bool calm;

	// Use this for initialization
	void Start () {
		enemyHealth = this.gameObject.GetComponent<EnemyHealth> ();
		anim = this.gameObject.GetComponent<Animator> ();
		rb2 = this.gameObject.GetComponent<Rigidbody2D> ();
		calm = true;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ptp = player.transform.position;
		float dfp = Vector2.Distance (ptp, this.gameObject.transform.position);
		Debug.Log ("Distance: "+dfp);
		anim.SetFloat("DistanceFromPlayer", dfp);

		if (calm && dfp <= detectRange) {
			calm = false;
		}


		if (!calm) {
			if (dfp > detectRange + 1) {
				calm = true;
			} else if (dfp > 0.5) {
				rb2.velocity = ((Vector2)ptp - (Vector2)this.gameObject.transform.position).normalized * speed;
			}
		} else {
			rb2.velocity = Vector2.zero;
		}
	}

	void BaseAI.recoil(){
		anim.SetBool ("Recoiling", true);
	}
}
