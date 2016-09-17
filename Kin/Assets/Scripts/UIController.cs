using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderController : MonoBehaviour {

    public Slider health;
    public Slider stamina;
    // Add clock
    // Add Boss Health
	
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void setMaxHealth(int maxHealth)
    {
        health.maxValue = maxHealth;
    }

    void setMaxStamina(int maxStam)
    {
        stamina.maxValue = maxStam;
    }

    void setStamina(int val)
    {
        stamina.value = val;
    }

    void setHealth(int val)
    {

        health.value = val;
    }
}
