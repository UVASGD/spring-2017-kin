using UnityEngine;
using System.Collections;

public class AvatarMvmController : MonoBehaviour {

    public float speed;
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
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		if(!animator.GetBool("Dead")) rb.velocity = ((Vector2) move.normalized) * speed;


		lastMove = gameObject.GetComponent<AnimationControl>().lastMove;

        // Save Vector2 of last movement
        //if (!(System.Math.Abs(move.x) < 0.01f && System.Math.Abs(move.y) < 0.01f))
        //{
        //    lastMove = move;
        //}
    }

    public void playWalkSound()
    {
        audio.Play();
    }
}
