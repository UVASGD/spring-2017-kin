using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    // Main Health Controller
    private int maxHealth; 
    private int currentHealth;
    public float restartDelay = 5f;
	float restartTimer;
    // Put damage audio here if we have that
    // public AudioClip damageClip;
    // public AudioClip deathClip;
    bool isDead;

    AvatarMvmController playerMvmController;
    // Reference to animator for death animation
    Animator anim;
    // Reference to audio source for damage audio
    AudioSource playerAudio;

    void Awake()
    {
        maxHealth = 100;
    }

	void Start()
    {
		anim = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
		playerMvmController = GetComponent<AvatarMvmController>();
        maxHealth = GetComponent<StatController>().getHealth();
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
        anim.SetBool("Dying", true);
        // Play death audio clip
        playerMvmController.enabled = false;
        // Go to UI Screen
    }

    public void setMaxHealth(int max)
    {
        this.maxHealth = max;
    }

    public void setCurrentHealth(int current)
    {
        this.currentHealth = current;
    }

    public int getMaxHealth()
    {
        return this.maxHealth;
    }

    public int getCurrentHealth()
    {
        return this.currentHealth;
    }

    void Update()
    {
        // For Testing
        //Debug.Log("Max: " + maxHealth + ", Current: " + currentHealth);
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    TakeDamage(30);
        //}
        // Timer set up so you can do something once they've been dead a certain amount of time
        if (isDead) {
            restartTimer += Time.deltaTime;
        }
        if (restartTimer >= restartDelay)
        {
            isDead = false;
            restartTimer = 0;
            anim.SetBool("Dying", false);
            // Move character?
            // Go back to scene?
        }
    }


}
