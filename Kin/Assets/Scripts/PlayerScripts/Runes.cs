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

    public void setIxtabActive() {
        GetComponent<PlayerHealth>().ixtabRune = true;
    }

    public void setIxtabInactive() {
        GetComponent<PlayerHealth>().ixtabRune = false;
    }

    public void setMaizeActive() { }
    public void setMaizeInactive() { }
    public void setMoonActive() { }
    public void setMoonInactive() { }
    public void setChacActive() { }
    public void setChacInactive() { }
    public void setHuntActive() { }
    public void setHuntInactive() { }
    public void setMirrorActive() { }
    public void setMirrorInactive() { }
    public void setSunActive() { }
    public void setSunInactive() { }
    public void setTwinsActive() { }
    public void setTwinsInactive() { }


}
