using UnityEngine;
using System.Collections;


[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Rigidbody2D))]
public class ChacAnimationController : MonoBehaviour
{

	Rigidbody2D rb;
	SpriteRenderer sr;
	public Vector2 lastMove;
	Animator animator;
	public bool attacking,dying,recoiling;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		lastMove = new Vector2 (0, 0);
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!dying)
			updateDirection ();
		//last nonzero move
		lastMove = rb.velocity.magnitude == 0 ? lastMove : rb.velocity;
		animator.SetBool ("Moving", rb.velocity.magnitude > 0);
		if (dying) {
			animator.SetTrigger ("Dying");
			gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			dying = false;
			Destroy(gameObject.GetComponent<MeleeMinionAnimationController>());
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsTag ("Dying")) {
			//animator.SetBool ("Dying", false);
			Debug.Log("In dying");
		}
		else if (attacking) {
			animator.SetTrigger ("Attack2");
			attacking = false;
		}
	}

	void updateDirection(){
		bool facingRight = true;
		if (lastMove.x <= 0)
			facingRight = false;  

		sr.flipX = !facingRight;
	}
}

