using UnityEngine;
using System.Collections;

public class AvatarMvmController : MonoBehaviour {

    public float speed = 1.0f;
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
        transform.position += move * speed * Time.deltaTime;
    }

    public void playWalkSound()
    {
        audio.Play();
    }
}
