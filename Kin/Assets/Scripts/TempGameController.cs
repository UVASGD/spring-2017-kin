using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempGameController : MonoBehaviour {

    public Canvas canv;
	public GameObject Player;
    private UIController ui;

	// Use this for initialization
	void Start () {
        ui = canv.GetComponent<UIController>();
		ui.setMaxHealth((int)Player.GetComponent<PlayerHealth>().maxHealth);
		ui.setMaxStamina((int)Player.GetComponent<PlayerStamina>().maxStamina);
	}
	
	// Update is called once per frame
	void Update () {
		ui.setHealth((int)Player.GetComponent<PlayerHealth>().currentHealth);
		ui.setStamina((int)Player.GetComponent<PlayerStamina>().currentStamina);
	}
}
