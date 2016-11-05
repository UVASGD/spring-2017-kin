using UnityEngine;
using System.Collections;


[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Rigidbody2D))]
public class MeleeMinionAnimationController : MonoBehaviour
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
		lastMove = rb.velocity;
		animator.SetBool ("Moving", lastMove.magnitude > 0);
		if (dying) {
			animator.SetBool ("Dying", true);
			dying = false;
		} else if (animator.GetCurrentAnimatorStateInfo (0).IsTag ("Dying")) {
			animator.SetBool ("Dying", false);
		}
		if (attacking) {
			animator.SetTrigger ("Attack2");
			attacking = false;
		}
	}
}

