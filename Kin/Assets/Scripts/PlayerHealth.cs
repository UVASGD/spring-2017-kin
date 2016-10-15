using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    // Main Health Controller

    public int maxHealth = 100; // Get maxHealth from Stat Controller
    public int currentHealth;
    public float restartDelay = 5f;
    // Put damage audio here if we have that
    // public AudioClip damageClip;
    // public AudioClip deathClip;
    bool isDead;
    float restartTimer;

    AvatarMvmController playerMvmController;
    // Reference to animator for death animation
    Animator anim;
    // Reference to audio source for damage audio
    AudioSource playerAudio;
    

	void Awake() {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMvmController = GetComponent<AvatarMvmController>();
        currentHealth = maxHealth;
	}
	
	public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        // Play damage audio clip
        if (currentHealth <= 0 && !isDead) {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        //anim.SetTrigger("Die");
        // Play death audio clip
        playerMvmController.enabled = false;
        // Go to UI Screen
    }

    void Update()
    {
        // Timer set up so you can do something once they've been dead a certain amount of time
        if (isDead) {
            restartTimer += Time.deltaTime;
        }
        if (restartTimer >= restartDelay)
        {
            isDead = false;
            // Move character?
            // Go back to scene?
        }
    }


}
