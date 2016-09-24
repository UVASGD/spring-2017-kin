using UnityEngine;
using System.Collections;

public class AvatarMvmController : MonoBehaviour {

    public float speed = 1.5f;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		gameObject.GetComponent<Animator>().SetBool("Moving", move.magnitude > 0);
		gameObject.GetComponent<Animator>().SetFloat("Horizontal", Input.GetAxis("Horizontal"));
		gameObject.GetComponent<Animator>().SetFloat("Vertical", Input.GetAxis("Vertical"));
		gameObject.GetComponent<Rigidbody2D> ().velocity = move * speed;
		Debug.Log (gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude);
    }

    public void playWalkSound()
    {
        audio.Play();
    }
}
