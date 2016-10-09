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
		float angle = 360*Mathf.Atan2(lastMove.x,lastMove.y)/(2*Mathf.PI);
		Animator animator = gameObject.GetComponent<Animator> ();

		int direction = 0; bool facingRight = true;
		if (angle >= -45 && angle <= 45)
			direction = 2;
		else if (Mathf.Abs (angle) > 45 && Mathf.Abs (angle) <= 135) 
		{
			direction = 1;
			if (angle < 0)
				facingRight = false;
		}


		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		animator.SetBool ("Dead", animator.GetCurrentAnimatorStateInfo (0).IsTag ("Dead"));
		animator.SetBool("Moving", move.magnitude > 0);
		animator.SetBool("FacingRight", facingRight);
		animator.SetFloat("Direction", direction);
        //transform.position += move * speed * Time.deltaTime;
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
