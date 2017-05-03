using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatScreenController : MonoBehaviour {

    public Slider pStrength, pStamina, pHealth, pWisdom, oStrength, oStamina, oHealth, oWisdom;
    public Text pExp, oExp;

    public string PlayerExp
    {
        get { return (string)pExp.text; }
        set { pExp.text = value; }
    }

    public int PlayerStrength
    {
        get { return (int)pStrength.value; }
        set { pStrength.value = value; }
    }

    public int PlayerStamina
    {
        get { return (int)pStamina.value; }
        set { pStamina.value = value; }
    }

    public int PlayerHealth
    {
        get { return (int)pHealth.value; }
        set { pHealth.value = value; }
    }

    public int PlayerWisdom
    {
        get { return (int)pWisdom.value; }
        set { pWisdom.value = value; }
    }


    public string OrderExp
    {
        get { return (string)oExp.text; }
        set { oExp.text = value; }
    }

    public int OrderStrength
    {
        get { return (int)oStrength.value; }
        set { oStrength.value = value; }
    }

    public int OrderStamina
    {
        get { return (int)oStamina.value; }
        set { oStamina.value = value; }
    }

    public int OrderHealth
    {
        get { return (int)oHealth.value; }
        set { oHealth.value = value; }
    }

    public int OrderWisdom
    {
        get { return (int)oWisdom.value; }
        set { oWisdom.value = value; }
    }

	void OnEnable(){
		GameObject.FindObjectOfType<InputOverrideController> ().NormalOff ();
		(this.gameObject.transform.FindChild ("Cancel").GetComponent<Button> () as Button).interactable = true;
	}

	void OnDisable(){
		GameObject.FindObjectOfType<InputOverrideController> ().NormalOn ();
	}
}
