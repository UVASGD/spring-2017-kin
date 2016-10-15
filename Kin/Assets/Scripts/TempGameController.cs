using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempGameController : MonoBehaviour {

    public Canvas canv;
    private SaveController save;
    private UIController ui;
    public Canvas options;

	// Use this for initialization
	void Start () {
		save = SaveController.s_instance;
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
