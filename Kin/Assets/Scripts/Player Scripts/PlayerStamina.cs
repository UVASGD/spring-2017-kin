using UnityEngine;
using System.Collections;

public class PlayerStamina : MonoBehaviour {

	public float maxStamina; 
	public float currentStamina;
    private float staminaRegen = .15f;
    bool hasStamina;
    float resetLevel = 30;
    

    void Awake()
    {
        maxStamina = GetComponent<StatController>().getStamina();
        hasStamina = true;
    }

	void Start()
    {
		currentStamina = maxStamina;
	}

	public void TakeDamage(float amount)
    {
		currentStamina -= amount;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            hasStamina = false;
        }
	}

    void NoStamina()
    {
        hasStamina = false;
    }

	void Update()
	{
        if (currentStamina + staminaRegen <= maxStamina)
        {
            currentStamina += staminaRegen;
        }
        else
        {
            currentStamina = maxStamina;
        }
        //print("hasStamina: " + hasStamina + " Stamina: " + currentStamina);
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    TakeDamage(30);
        //}
        if (currentStamina >= resetLevel)
        {
            hasStamina = true;
        }
    }
}
