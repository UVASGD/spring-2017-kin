using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    public Slider health;
	public Slider underHealth;
    public Slider stamina;
	public Slider underStamina;
    private Slider bossHealthSlider;
    public GameObject bossHealth;
    public GameObject statsMenu;
	public GameObject options;
    public GameObject runesMenu;
	public GameObject player;
    public Text bossName;
    // Add clock

	private float healthlerpA;
	private float healthlerpB;
	private float healthlerpT;
	private bool lerpingHealth;

	private float staminalerpA;
	private float staminalerpB;
	private float staminalerpT;
	private bool lerpingStamina;
	
	void Start () {
        bossHealthSlider = bossHealth.GetComponent<Slider>();
		health.value = (player.GetComponent<PlayerHealth> () as PlayerHealth).maxHealth;
		underHealth.value = (player.GetComponent<PlayerHealth> () as PlayerHealth).maxHealth;
		stamina.value = (player.GetComponent<PlayerStamina> () as PlayerStamina).maxStamina;
		underStamina.value = (player.GetComponent<PlayerStamina> () as PlayerStamina).maxStamina;
		healthlerpT = 0;
		lerpingHealth = false;
		staminalerpT = 0;
		lerpingStamina = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (healthlerpT < 0.5f)
			healthlerpT += Time.deltaTime;
		else
			lerpingHealth = false;

		if (staminalerpT < 0.5f)
			staminalerpT += Time.deltaTime;
		else
			lerpingStamina = false;
		//Debug.Log (staminalerpT / 0.5);

		if (Input.GetButtonDown("Stats") && statsMenu != null)
        {
            toggleStatsMenu();
        }
        //TODO Change Later, something that' not stats
		if (Input.GetButtonDown ("Cancel")) {
			options.GetComponent<Canvas> ().enabled = !options.GetComponent<Canvas> ().enabled;
		}
        if (Input.GetKeyDown(KeyCode.P))
        {
            //updateRunes();
            runesMenu.GetComponent<Canvas>().enabled = !runesMenu.GetComponent<Canvas>().enabled;
            runesMenu.GetComponent<RunesMenu>().updateRunes();
        }
		if(lerpingHealth)
			health.value = Mathf.Lerp (healthlerpA, healthlerpB, healthlerpT/0.5f);
		if(lerpingStamina)
			stamina.value = Mathf.Lerp (staminalerpA, staminalerpB, staminalerpT/0.5f);
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
		underStamina.value = val;
		staminalerpA = health.value;
		staminalerpB = val;
		staminalerpT = 0;
		lerpingStamina = true;
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
		underHealth.value = val;
		healthlerpA = health.value;
		healthlerpB = val;
		healthlerpT = 0;
		lerpingHealth = true;
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
