using UnityEngine;
using System.Collections;

public class PlayerStamina : MonoBehaviour {

	public float maxStamina = 100; // Get maxHealth from Stat Controller
	public float currentStamina;
	// Put damage audio here if we have that
	// public AudioClip damageClip;
	// public AudioClip deathClip;

	void Start(){
		currentStamina = maxStamina;
	}

	public void TakeDamage(int amount)
	{
		currentStamina -= amount;
	}

	void Update()
	{
		
	}
}
