using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunesMenu : MonoBehaviour {

    public GameObject hiltRune;
    public GameObject bladeRune;
    public GameObject tipRune;
    private Runes runes;
    public GameObject[] runeSprites;
    public GameObject player;

	// Use this for initialization
	void Start () {
        runes = player.GetComponent<Runes>();
        updateRunes();
	}

    // Update is called once per frame
    void updateRunes()
    {
        Debug.Log("UPDATE");
        if (runes.ixtabRune == 3)
        {
            Debug.Log("ON");
            Sprite hilt = hiltRune.GetComponent<Sprite>();
            hilt = (Sprite)Resources.Load("Sprites/Runes/Rune (1).png");
        }
        else
        {
            Debug.Log("OFF");
        }
    }
}
