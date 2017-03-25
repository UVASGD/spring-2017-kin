using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RunesMenu : MonoBehaviour {

    public GameObject hiltRune;
    public GameObject bladeRune;
    public GameObject tipRune;
    public GameObject[] runeSprites;
    public GameObject player;
    private Runes runes;
    private Image hiltImg;
    private Image bladeImg;
    private Image tipImg;
    

	// Use this for initialization
	void Start () {
        hiltImg = hiltRune.GetComponent<Image>();
        bladeImg = bladeRune.GetComponent<Image>();
        tipImg = tipRune.GetComponent<Image>();
        runes = player.GetComponent<Runes>();
        updateRunes();
        
}

    // Update is called once per frame
    public void updateRunes()
    {
        Debug.Log("UPDATE");
        if (runes.ixtabRune == 3)
        {
            hiltImg.color = Color.blue;
            hiltImg.sprite = Resources.Load<Sprite>("Sprites/Runes/Rune (1)");
        }
        else
        {
            hiltImg.sprite = Resources.Load<Sprite>("Sprites/Runes/Rune (0)");
        }

        if(runes.chacRune == 3)
        {

            tipImg.sprite = Resources.Load<Sprite>("Sprites/Runes/Rune (10)");
        }
        else
        {
            tipImg.sprite = Resources.Load<Sprite>("Sprites/Runes/Rune (0)");
        }
    }
}
