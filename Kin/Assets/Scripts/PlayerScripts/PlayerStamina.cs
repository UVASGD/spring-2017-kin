using UnityEngine;
using System.Collections;

public class PlayerStamina : MonoBehaviour {

	public int maxStamina; 
	public int currentStamina;
    private int staminaRegen = 5;
    public bool hasStamina;
    public float resetLevel = 500;
    public bool pauseRegen = false;
    public float pauseLength = 1.6f; // seconds it should pause for
    public float pauseTimer = 0;
    
    public int getMaxStamina()
    {
        return maxStamina;
    }
    
    public int getCurrentStamina()
    {
        return currentStamina;
    }

    public void setMaxStamina(int max)
    {
        maxStamina = max;
    }

    public void setCurrentStamina(int current)
    {
        currentStamina = current;
    }

    void Awake()
    {
		
    }

	void Start()
    {
        setMaxStamina(GetComponent<StatController>().getStamina());
        hasStamina = true;
        setCurrentStamina(getMaxStamina());
	}

	public void TakeDamage(int amount)
    {
		setCurrentStamina(getCurrentStamina() - amount);
		pauseTimer = 0;
		if (getCurrentStamina() <= 0)
        {
			setCurrentStamina(0);
            hasStamina = false;
        }
        pauseRegen = true;
	}

    void NoStamina()
    {
        hasStamina = false;
    }

	void Update()
	{
        if (currentStamina + staminaRegen <= maxStamina && !pauseRegen)
        {
			setCurrentStamina(getCurrentStamina() + staminaRegen);
        }
        else if (!(currentStamina + staminaRegen <= maxStamina) && !pauseRegen)
        {
			setCurrentStamina(getMaxStamina());
        } 
        if (currentStamina >= resetLevel)
        {
            hasStamina = true;
        }
        if (pauseRegen)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer > pauseLength)
            {
                pauseRegen = false;
                pauseTimer = 0;
            }
        }
        setMaxStamina(GetComponent<StatController>().getStamina());
    }
}
