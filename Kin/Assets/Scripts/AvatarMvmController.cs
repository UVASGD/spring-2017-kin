using UnityEngine;
using System.Collections;

public class AvatarMvmController : MonoBehaviour {

    public float speed = 1.5f;
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
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		gameObject.GetComponent<Animator>().SetBool("Moving", move.magnitude > 0);
		gameObject.GetComponent<Animator>().SetFloat("Horizontal", Input.GetAxis("Horizontal"));
		gameObject.GetComponent<Animator>().SetFloat("Vertical", Input.GetAxis("Vertical"));
        //transform.position += move * speed * Time.deltaTime;
		rb.velocity = ((Vector2) move) * speed;

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
