using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    public Slider health;
    public Slider stamina;
    private Slider bossHealthSlider;
    public GameObject bossHealth;
    public GameObject statsMenu;
	public GameObject options;
    public GameObject runesMenu;
	public GameObject player;
    public Text bossName;
    // Add clock
	
	void Start () {
        bossHealthSlider = bossHealth.GetComponent<Slider>();
		health.value = (player.GetComponent<PlayerHealth> () as PlayerHealth).maxHealth;
		stamina.value = (player.GetComponent<PlayerStamina> () as PlayerStamina).maxStamina;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Stats"))
        {
            toggleStatsMenu();
        }
        //TODO Change Later, something that' not stats
		if (Input.GetButtonDown ("Stats")) {
			options.GetComponent<Canvas> ().enabled = !options.GetComponent<Canvas> ().enabled;
		}
        if (Input.GetKeyDown(KeyCode.R))
        {
            //updateRunes();
            runesMenu.GetComponent<Canvas>().enabled = !runesMenu.GetComponent<Canvas>().enabled;
        }
    }
		

    public void setMaxHealth(int maxHealth)
    {
		if (health.value > maxHealth) {
			health.value = maxHealth;
		}
		health.maxValue = maxHealth;
    }

    public void setMaxStamina(int maxStam)
    {
		if (stamina.value > maxStam) {
			stamina.value = maxStam;
		}
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

    public void setBossName(string name)
    {
        bossName.text = name;
    }

    public string getBossName()
    {
        return bossName.text;
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

    public void setBossHealth(int val)
    {
        bossHealthSlider.value = val;
    }

    public int getBossHealth()
    {
        return (int)bossHealthSlider.value;
    }

    public void setBossMax(int val)
    {
        bossHealthSlider.maxValue = val;
    }

    public int getBossMax()
    {
        return (int)bossHealthSlider.maxValue;
    }

    public void toggleStatsMenu()
    {
		if (statsMenu) {
			statsMenu.SetActive (!statsMenu.activeSelf);
		}
    }

    
}
