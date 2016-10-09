using UnityEngine;
using System.Collections;

public class AvatarMvmController : MonoBehaviour {

    public float speed = 1.0f;
    AudioSource audio;

	Rigidbody2D rb;

    public Vector2 lastMove = new Vector2(0, 0);

    void Start()
    {
        audio = GetComponent<AudioSource>();
		rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
	{
		Animator animator = gameObject.GetComponent<Animator> ();

		// direction and animation controls should be moved to new script
		float angle = 360*Mathf.Atan2(lastMove.x,lastMove.y)/(2*Mathf.PI);
		// simplify angle to an integer from 0 to 2
		int direction = 0; bool facingRight = true;
		if (angle >= -45 && angle < 45)
			direction = 2;  // facing up
		else if ((angle > 45 && angle <= 135) || ((angle < -45 && angle >= -135))) 
		{
			direction = 1;
			if (angle < 0) // facing side
				facingRight = false;
		}
		gameObject.GetComponent<SpriteRenderer> ().flipX = !facingRight;

		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		animator.SetBool ("Dead", animator.GetCurrentAnimatorStateInfo (0).IsTag ("Dead"));
		animator.SetBool("Moving", move.magnitude > 0);
		animator.SetFloat("Direction", direction);
		if(!animator.GetBool("Dead")) rb.velocity = ((Vector2) move) * speed;

        // Save Vector2 of last movement
        if (!(System.Math.Abs(move.x) < 0.01f && System.Math.Abs(move.y) < 0.01f))
        {
            lastMove = move;
        }
    }

    public void playWalkSound()
    {
        audio.Play();
    }
}
