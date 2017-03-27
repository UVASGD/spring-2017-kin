using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    // Main Health Controller
    public int maxHealth; 
    private int currentHealth;
    public float restartDelay = 5f;
	float restartTimer;
    // Put damage audio here if we have that
    // public AudioClip damageClip;
    // public AudioClip deathClip;
    bool isDead = false;
    public bool ixtabRune = false;
    public bool invincible = true; // for debug ONLY!!!

    AvatarMvmController playerMvmController;
    // Reference to animator for death animation
    Animator anim;
    // Reference to audio source for damage audio
    AudioSource playerAudio;

	public GameObject sceneCont;

    void Awake()
    {
        maxHealth = GetComponent<StatController>().getHealth();
        /*if you want the ui controller to get this value, 
         * you need to set it here, not in start. 
         * Start is too late, and this is fine because start 
         * will only ever get called once whereas we will wnat 
         * max health to change as level increases*/
    }

	void Start()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMvmController = GetComponent<AvatarMvmController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!anim.GetBool("Rolling") && !anim.GetBool("Recoiling"))
        {
            if(!invincible) currentHealth -= amount;
            anim.SetBool("Recoiling", true);
            // Play damage audio clip
        }
        else if (anim.GetBool("Recoiling"))
        {
            anim.SetBool("Recoiling", false);
        }
        if (currentHealth <= 0 && !isDead)
        {
			if (ixtabRune) {
				setCurrentHealth(getMaxHealth() / 4);
				Debug.Log ("Ixtab rune activated");
                GetComponent<Runes>().ixtabRune = (int)Runes.runeModes.locked;
                GetComponent<Runes>().setIxtabActive(false);
			} else {
				setCurrentHealth(0);
				Death ();
			}
        }
        else
        {
			if (getCurrentHealth() < 0)
            {
				setCurrentHealth(0);
            }
        }

    }

    void Death()
    {
        //isDead = true;
        anim.SetBool("Dying", true);
        // Play death audio clip
        playerMvmController.enabled = false;

        // Go to UI Screen
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition; // Make sure to unfreeze before undying...

		sceneCont.GetComponent<SceneController>().FadeToDeath();
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
        //Debug.Log("Recoiling " + anim.GetBool("Recoiling"));
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

	public void playDeathSound() {
		Debug.Log("Death");
		AudioSource aSource = GetComponent<AudioSource>();
		string clipName = "Sounds/Player SFX/Death Sound Brian " + Random.Range(1, 3);
		Debug.Log(clipName);
		AudioClip c = Resources.Load(clipName) as AudioClip;
		aSource.clip = c;
		aSource.GetComponent<AudioSource>().Play();
	}


}
