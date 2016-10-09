﻿using UnityEngine;
using System.Collections;

public class AvatarMvmController : MonoBehaviour {

    public float speed = 1.0f;
    AudioSource audio;

	Rigidbody2D rb;

    void Start()
    {
        audio = GetComponent<AudioSource>();
		rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		gameObject.GetComponent<Animator>().SetBool("Moving", move.magnitude > 0);
		gameObject.GetComponent<Animator>().SetBool("FacingRight", move.x >= 0);
		gameObject.GetComponent<Animator>().SetFloat("Horizontal", Input.GetAxis("Horizontal"));
		gameObject.GetComponent<Animator>().SetFloat("Vertical", Input.GetAxis("Vertical"));
        //transform.position += move * speed * Time.deltaTime;
		rb.velocity = ((Vector2) move) * speed;
    }

    public void playWalkSound()
    {
        audio.Play();
    }
}
