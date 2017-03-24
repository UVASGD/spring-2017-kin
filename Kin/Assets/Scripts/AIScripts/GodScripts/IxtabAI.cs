using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(EnemyHealth), (typeof(Animator)), (typeof(Rigidbody2D)))]
public class IxtabAI : MonoBehaviour, BaseAI {

	public GameObject player;
	public Slider UIHealth;
	public Text UIName;
	public float detectRange;
	public float speed;
	private EnemyHealth enemyHealth;
	private Animator anim;
	private Rigidbody2D rb2;
	public bool calm;

	private float slamCooldown;
	private float swipeCooldown;
	private float invisCooldown;

	// Use this for initialization
	void Start () {
		enemyHealth = this.gameObject.GetComponent<EnemyHealth> ();
		anim = this.gameObject.GetComponent<Animator> ();
		rb2 = this.gameObject.GetComponent<Rigidbody2D> ();
		calm = true;

		slamCooldown = 0.0f;
		swipeCooldown = 0.0f;
		invisCooldown = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ptp = player.transform.position;
		float dfp = Vector2.Distance (ptp, this.gameObject.transform.position);
		Debug.Log ("Distance: "+dfp);
		anim.SetFloat("DistanceFromPlayer", dfp);

		slamCooldown -= Time.deltaTime;
		swipeCooldown -= Time.deltaTime;
		invisCooldown -= Time.deltaTime;

		if (calm && dfp <= detectRange) { //activate!
			calm = false;
			UIHealth.enabled = true;
			UIName.enabled = true;
		}


		if (!calm) {
			if (dfp > detectRange + 0.5) { //deactivate!
				calm = true;
				UIHealth.enabled = false;
				UIName.enabled = false;
			} else if (dfp > 0.8) { //active & approaching
				rb2.velocity = ((Vector2)ptp - (Vector2)this.gameObject.transform.position).normalized * speed;
			} else { //active and within range
				rb2.velocity = Vector2.zero;

				// attack logic with cooldown resets




			}
		} else {
			rb2.velocity = Vector2.zero;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.15f);
		Gizmos.DrawSphere(gameObject.transform.position, detectRange);
		Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.10f);
		Gizmos.DrawSphere(gameObject.transform.position, detectRange + 0.5f);
	}

	void BaseAI.recoil(){
		anim.SetBool ("Recoiling", true);
	}
}
