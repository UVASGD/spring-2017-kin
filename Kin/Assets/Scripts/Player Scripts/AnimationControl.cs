using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Rigidbody2D))]
public class AnimationControl : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	public Vector2 lastMove;
	Animator animator;

	float timer;

	private bool isRolling;

	HitboxController hbCont;

	/// <summary> ability to face 4 directions	/// </summary>
	public bool MultiDirectional = false;
	/// <summary> direction to face upon spawn	/// </summary>
	public Direction InitialDirection = Direction.Right;
	public enum Direction {
		Up,
		Left,
		Right,
		Down
	};

	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb = gameObject.GetComponent<Rigidbody2D>();
		isRolling = false;

		timer = 0.00f;

		animator.logWarnings = false;
		switch (InitialDirection) {
		case Direction.Up:
			lastMove = MultiDirectional ? new Vector2 (0, 1) : new Vector2 (-1, 0);
			break;
		case Direction.Left:
			lastMove = new Vector2 (-1, 0);
			break;
		case Direction.Down:
			lastMove = MultiDirectional ? new Vector2 (0, -1) : new Vector2 (1, 0);
			break;
		case Direction.Right:
			lastMove = new Vector2 (1, 0);
			break;
		}
	}

	void Update () {
		int direction = updateDirection ();
		updateRoll ();

		timer += Time.deltaTime;

		var move = rb.velocity;
		// include check if animator has each parameter
		animator.SetBool ("Dead", animator.GetCurrentAnimatorStateInfo (0).IsTag ("Dead"));


		animator.SetBool("Moving", move.magnitude > 0);


		animator.SetFloat("Direction", direction);
		/*
		if (animator.GetBool ("Rolling") && timer >= 3.0f) {
			animator.SetBool ("Rolling", false);
			animator.SetBool("Moving", move.magnitude > 0);
			timer = 0.00f;
		} else if (!animator.GetBool ("Rolling")) {
			if (Input.GetButtonDown ("Roll")) {
				animator.SetBool ("Rolling", true);
				animator.SetBool ("Moving", false);
			}
			animator.SetBool("Moving", move.magnitude > 0);
		} */



		// Save Vector2 of last movement
		if (!(System.Math.Abs(move.x) < 0.01f && System.Math.Abs(move.y) < 0.01f))
		{
			lastMove = move;
		}
	}

	/// <summary>
	/// controls animation based off direction of last saved velocity
	/// </summary>
	/// <returns>The direction.</returns>
	int updateDirection(){
		float angle = 360*Mathf.Atan2(lastMove.x,lastMove.y)/(2*Mathf.PI);
		int direction = 0; bool facingRight = true;
		// simplify angle to an integer from 0 to 2
		if (MultiDirectional) { // can face four directions
			if (angle >= -45 && angle < 45)
				direction = 2;  // facing up
		else if ((angle > 45 && angle <= 135) || ((angle < -45 && angle >= -135))) {
				direction = 1;
				if (angle < 0) // facing side
				facingRight = false;
			}
		} else { // only can face two directions
			direction = 1;
			if (angle <= 0)
				facingRight = false;  
		}

		sr.flipX = !facingRight;
		return direction;
	}

	public void updateRoll(){
		if (Input.GetButtonDown ("Roll") && !isRolling) {
			animator.SetBool ("Rolling", true);
			gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
			//animator.SetBool ("Moving", false);
		}
	}

	public void setRolling(bool roll) {
		isRolling = roll;
	}
}
