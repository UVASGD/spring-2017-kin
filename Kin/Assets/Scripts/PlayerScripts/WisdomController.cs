using UnityEngine;
using System.Collections;

public class WisdomController : MonoBehaviour {
    private int wisdom;
    private bool firstTier = false;
    private bool secondTier = false;
    private bool thirdTier = false;
    private bool fourthTier = false;

    void Start()
    {
        wisdom = GetComponent<StatController>().getWisdom();
    }
    
	
	// Update is called once per frame
	void Update () {
        if (wisdom >= 80)
        {
            fourthTier = true;
        }
        else if (wisdom >= 60)
        {
            thirdTier = true;
        }
        else if (wisdom >= 40)
        {
            secondTier = true;
        }
        else if (wisdom >= 20)
        {
            firstTier = true;
        }
	}

    public int HealthPotionStrength()
    {
        int strength = wisdom * 10 + 3; // Need an actual function
        return strength;
    }
}
