using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Rigidbody2D))]
public class AnimationControl : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	Vector2 lastMove;

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
		sr = gameObject.GetComponent<SpriteRenderer> ();
		rb = gameObject.GetComponent<Rigidbody2D>();
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
		Animator animator = gameObject.GetComponent<Animator> ();
		int direction = updateDirection ();

		var move = rb.velocity;
		// include check if animator has each parameter
		animator.SetBool ("Dead", animator.GetCurrentAnimatorStateInfo (0).IsTag ("Dead"));
		animator.SetBool("Moving", move.magnitude > 0);
		animator.SetFloat("Direction", direction);

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
}
