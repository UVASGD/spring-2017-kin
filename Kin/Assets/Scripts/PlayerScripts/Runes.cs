using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Runes : MonoBehaviour
{

    enum runeModes { locked, craftable, unequipped, equipped };

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "rune") {
            if (other.gameObject.name.Contains("ixtab"))
            {
                Debug.Log("Ixtab rune!");
                ixtabRune = (int)runeModes.equipped; // eventually, just set to craftable
                setIxtabActive(true);
            }
            else if (other.gameObject.name.Contains("maize"))
            {
                Debug.Log("Maize rune!");
                maizeRune = (int)runeModes.equipped; // eventually, just set to craftable
                setMaizeActive(true);
            }
            else if (other.gameObject.name.Contains("moon"))
            {
                Debug.Log("Moon rune!");
                moonRune = (int)runeModes.equipped; // eventually, just set to craftable
                setMoonActive(true);
            }
            else if (other.gameObject.name.Contains("chac"))
            {
                Debug.Log("Chac rune!");
                chacRune = (int)runeModes.equipped; // eventually, just set to craftable
                setChacActive(true);
            }
            else if (other.gameObject.name.Contains("hunt"))
            {
                Debug.Log("Hunt rune!");
                huntRune = (int)runeModes.equipped; // eventually, just set to craftable
                setHuntActive(true);
            }
            else if (other.gameObject.name.Contains("mirror"))
            {
                Debug.Log("Mirror rune!");
                mirrorRune = (int)runeModes.equipped; // eventually, just set to craftable
                setMirrorActive(true);
            }
            else if (other.gameObject.name.Contains("sun"))
            {
                Debug.Log("Sun rune!");
                sunRune = (int)runeModes.equipped; // eventually, just set to craftable
                setSunActive(true);
            }
            else if (other.gameObject.name.Contains("twin"))
            {
                Debug.Log("Twin rune!");
                twinsRune = (int)runeModes.equipped; // eventually, just set to craftable
                setTwinsActive(true);
            }
            Destroy(other.gameObject);
        }
    }

}


