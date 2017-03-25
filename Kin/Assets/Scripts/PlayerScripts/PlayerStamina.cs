using UnityEngine;
using System.Collections;

public class PlayerStamina : MonoBehaviour {

	public int maxStamina; 
	private int currentStamina;
    private int staminaRegen = 4;
    public bool hasStamina;
    float resetLevel = 500;
    bool pauseRegen = false;
    float pauseLength = 0.6f; // seconds it should pause for
    float pauseTimer = 0;
    
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
        maxStamina = GetComponent<StatController>().getStamina();
        hasStamina = true;
    }

	void Start()
    {
		currentStamina = maxStamina;
	}

	public void TakeDamage(int amount)
    {
		currentStamina -= amount;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
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
            currentStamina += staminaRegen;
        }
        else if (!(currentStamina + staminaRegen <= maxStamina) && !pauseRegen)
        {
            currentStamina = maxStamina;
        } 
        //print("hasStamina: " + hasStamina + " Stamina: " + currentStamina);
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    TakeDamage(300);
        //}
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
    }
}
