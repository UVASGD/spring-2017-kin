using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Rigidbody2D))]
public class AnimationControl : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sr;
	public Vector2 lastMove;
	Animator animator;

    public float jump = 0.2f;
	private bool isRolling;
	private bool isAttacking;
    private bool isRecoiling;


	/// <summary> ability to face 4 directions	/// </summary>
	public bool MultiDirectional = false;
	/// <summary> direction to face upon spawn	/// </summary>
	public Direction InitialDirection = Direction.Right;

	/// <summary>
	/// Direction enumeration.
	/// </summary>
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
		updateAttack ();

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
	public int updateDirection(){
		float angle = 360*Mathf.Atan2(lastMove.x,lastMove.y)/(2*Mathf.PI);
		int direction = 0; bool facingRight = true;
		// simplify angle to an integer from 0 to 2
		if (MultiDirectional) { // can face four directions
			if (angle > -45 && angle < 45)
				direction = 2;  // facing up
		else if ((angle >= 45 && angle <= 135) || ((angle <= -45 && angle >= -135))) {
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

	/// <summary>
	/// Updates the roll.
	/// </summary>
	public void updateRoll(){
		if (Input.GetButtonDown ("Roll") && !isRolling && rb.velocity != Vector2.zero) {
            if (canRoll()) 
            {
                animator.SetBool("Rolling", true);
                // If you want to turn the collider during roll
                //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
	}

	/// <summary>
	/// Updates the attack.
	/// </summary>
	public void updateAttack(){
		if (Input.GetButtonDown ("Attack") && !isAttacking) {
            if (canAttack())
            {
                animator.SetBool("Attacking", true);
                if (animator.GetBool("Moving"))
                {
                    GetComponent<Transform>().position += new Vector3(GetComponent<Rigidbody2D>().velocity.x*jump, GetComponent<Rigidbody2D>().velocity.y*jump, 0);
                }
            }
		}
	}


	public void setRolling(bool roll) {
		isRolling = roll;
	}

	public void setAttacking(bool attack) {
		isAttacking = attack;
	}

    public void setRecoiling(bool recoil)
    {
        isRecoiling = recoil;
    }

    public bool getRecoiling()
    {
        return isRecoiling;
    }

    bool canRoll()
    {
        return animator.GetBool("Moving") && !animator.GetBool("Attacking") && gameObject.GetComponent<PlayerStamina>().hasStamina && !animator.GetBool("Recoiling");
    }

    bool canAttack()
    {
        return !animator.GetBool("Rolling") && gameObject.GetComponent<PlayerStamina>().hasStamina && !animator.GetBool("Recoiling"); 
    }
}
