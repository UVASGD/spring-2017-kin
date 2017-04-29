using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Runes : MonoBehaviour
{

    public enum runeModes { locked, craftable, unequipped, equipped };

    public int ixtabRune = (int)runeModes.locked;
    public int maizeRune = (int)runeModes.locked;
    public int moonRune = (int)runeModes.locked;
    public int chacRune = (int)runeModes.locked;
    public int huntRune = (int)runeModes.locked;
    public int mirrorRune = (int)runeModes.locked;
    public int sunRune = (int)runeModes.locked;
    public int twinsRune = (int)runeModes.locked;
    //Temporary Rune-sprite cheat-sheet
    //Ixtab - 1, Fire - 9, Chac - 10
    public Sprite[] ixtabRuneSprites;
    public Sprite[] maizeRuneSprites;
    public Sprite[] moonRuneSprites;
    public Sprite[] chacRuneSprites;
    public Sprite[] huntRuneSprites;
    public Sprite[] mirrorRuneSprites;
    public Sprite[] sunRuneSprites;
    public Sprite[] twinsRuneSprites;

    public void setIxtabActive(bool active) {
        GetComponent<PlayerHealth>().ixtabRune = active;
    }

    public void setMaizeActive(bool active) { }
    public void setMoonActive(bool active) { }
    public void setChacActive(bool active)
    {
        GetComponent<PlayerMelee>().chacRuneActivated = active;
    }
    public void setHuntActive(bool active) { }
    public void setMirrorActive(bool active) { }
    public void setSunActive(bool active) {
		if (active) {
            GetComponent<PlayerMelee>().setDamage(30);
		} else {
            GetComponent<PlayerMelee>().setDamage(20);
		}
	}
    public void setTwinsActive(bool active) { }

    //Translates string, should be able to be used for multiple runes at once...
    public void ActivateRune(string name)
    {
        if (name.Contains("ixtab"))
        {
            Debug.Log("Ixtab rune!");
            ixtabRune = (int)runeModes.equipped; // eventually, just set to craftable
            setIxtabActive(true);
        }
        if (name.Contains("maize"))
        {
            Debug.Log("Maize rune!");
            maizeRune = (int)runeModes.equipped; // eventually, just set to craftable
            setMaizeActive(true);
        }
        if (name.Contains("moon"))
        {
            Debug.Log("Moon rune!");
            moonRune = (int)runeModes.equipped; // eventually, just set to craftable
            setMoonActive(true);
        }
        if (name.Contains("chac"))
        {
            Debug.Log("Chac rune!");
            chacRune = (int)runeModes.equipped; // eventually, just set to craftable
            setChacActive(true);
        }
        if (name.Contains("hunt"))
        {
            Debug.Log("Hunt rune!");
            huntRune = (int)runeModes.equipped; // eventually, just set to craftable
            setHuntActive(true);
        }
        if (name.Contains("mirror"))
        {
            Debug.Log("Mirror rune!");
            mirrorRune = (int)runeModes.equipped; // eventually, just set to craftable
            setMirrorActive(true);
        }
        if (name.Contains("sun"))
        {
            Debug.Log("Sun rune!");
            sunRune = (int)runeModes.equipped; // eventually, just set to craftable
            setSunActive(true);
        }
        if (name.Contains("twin"))
        {
            Debug.Log("Twin rune!");
            twinsRune = (int)runeModes.equipped; // eventually, just set to craftable
            setTwinsActive(true);
        }
    }

}


