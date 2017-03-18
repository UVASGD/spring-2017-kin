using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void setChacActive(bool active) { }
    public void setHuntActive(bool active) { }
    public void setMirrorActive(bool active) { }
    public void setSunActive(bool active) { }
    public void setTwinsActive(bool active) { }


}
