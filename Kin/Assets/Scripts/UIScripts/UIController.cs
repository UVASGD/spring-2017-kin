using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    public Slider health;
    public Slider stamina;
    public Canvas optionsCanvas;
    // Add clock
    // Add Boss Health
	
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleOptionsCanvas();
        }

    }

    public void setMaxHealth(int maxHealth)
    {
        health.maxValue = maxHealth;
    }

    public void setMaxStamina(int maxStam)
    {
        stamina.maxValue = maxStam;
    }

    public void setStamina(int val)
    {
        stamina.value = val;
    }

    public int getStamina()
    {
        return (int)stamina.value;
    }

    public int getHealth()
    {
        return (int)health.value;
    }

    public void setHealth(int val)
    {

        health.value = val;
    }

    public void setClock(int time)
    {
        Debug.Log(@"No clock yet. ¯\_(ツ)_/¯");
    }

    public int getClock()
    {
        Debug.Log(@"No clock yet.¯\_(ツ)_/¯");
        return 0;
    }

    public void setBossHealth()
    {
        Debug.Log(@"No boss health yet.¯\_(ツ)_/¯");
    }

    public int getBossHealth()
    {
        Debug.Log(@"No boss health yet.¯\_(ツ)_/¯");
        return 0;
    }

    public void toggleOptionsCanvas()
    {
        optionsCanvas.enabled = !optionsCanvas.enabled;
    }
}
