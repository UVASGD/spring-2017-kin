using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempGameController : MonoBehaviour {

    public Canvas canv;
    private SaveController save = SaveController.s_instance;
    private UIController ui;

	// Use this for initialization
	void Start () {
        ui = canv.GetComponent<UIController>();
        ui.setMaxHealth((int)save.health);
        ui.setMaxStamina((int)save.stamina);
	}
	
	// Update is called once per frame
	void Update () {
        ui.setHealth((int)save.health);
        ui.setStamina((int)save.stamina);
	}
}
