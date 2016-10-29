using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Rigidbody2D))]
public class KamikazeAnimationController : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	public Vector2 lastMove;
	Animator animator;

	public bool recoiling;
	public bool dying;
	public bool charging;

	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb = gameObject.GetComponent<Rigidbody2D>();
		lastMove = new Vector2 (0, 0);

		animator.logWarnings = false;
	}

	void Update () {

		updateDirection ();
		if (dying) {
			animator.SetBool ("Dying", true);
			dying = false;
		}else if (!dying && animator.GetCurrentAnimatorStateInfo (0).IsTag ("Dying")) {
			animator.SetBool ("Dying", false);
		}
		//Debug.Log ("charging" + !charging);
		//Debug.Log("in charge" + animator.GetCurrentAnimatorStateInfo (0).IsTag ("Charge"));

		if (charging) {
			animator.SetBool ("Charging", true);
			charging = false;
		} //else{ //if (!charging && animator.GetCurrentAnimatorStateInfo (0).IsTag ("Charge")) {
			//animator.SetBool ("Charging", false);
			//Debug.Log ("set bool false");
		//}

		if (recoiling) {
			animator.SetBool ("Recoiling", true);
			recoiling = false;
		} else if (!recoiling && animator.GetCurrentAnimatorStateInfo (0).IsTag ("Recoil")) {
			animator.SetBool ("Recoiling", false);
		}




		var move = rb.velocity;
		// include check if animator has each parameter
		//if dying has completed - switch bool to dead
		animator.SetBool ("Dead", animator.GetCurrentAnimatorStateInfo (0).IsTag ("Dead"));
		animator.SetBool("Moving", move.magnitude > 0);
		//animator.SetFloat("Direction", direction);

		// Save Vector2 of last movement
		if (!(System.Math.Abs(move.x) < 0.01f && System.Math.Abs(move.y) < 0.01f))
		{
			lastMove = move;
		}
	}

	void setRecoil()
	{
		
	}

	/// <summary>
	/// controls animation based off direction of last saved velocity
	/// </summary>
	/// <returns>The direction.</returns>
	void updateDirection(){
		int direction = 1; bool facingRight = true;
		if (lastMove.x <= 0)
			facingRight = false;  
	
		sr.flipX = !facingRight;
	}
}

