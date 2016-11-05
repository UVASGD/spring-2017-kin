using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempGameController : MonoBehaviour {

	public GameObject Player;
    public UIController ui;

	// Use this for initialization
	void Start () {
		ui.setMaxHealth(Player.GetComponent<PlayerHealth>().getMaxHealth());
		ui.setMaxStamina((int)Player.GetComponent<PlayerStamina>().getMaxStamina());
	}
	
	// Update is called once per frame
	void Update () {
		ui.setHealth(Player.GetComponent<PlayerHealth>().getCurrentHealth());
		ui.setStamina((int)Player.GetComponent<PlayerStamina>().getCurrentStamina());
	}
    
}
