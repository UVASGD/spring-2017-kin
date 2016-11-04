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
	
	}
}

