using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempGameController : MonoBehaviour {

    public Canvas canv;
	public GameObject save;
    private SaveController saveCont;
    private UIController ui;

	// Use this for initialization
	void Start () {
		saveCont = save.GetComponent<SaveController>();
        ui = canv.GetComponent<UIController>();
        ui.setMaxHealth((int)saveCont.health);
        ui.setMaxStamina((int)saveCont.stamina);
	}
	
	// Update is called once per frame
	void Update () {
        ui.setHealth((int)saveCont.health);
        ui.setStamina((int)saveCont.stamina);
	}
}
