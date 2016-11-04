using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class LevelUpController : MonoBehaviour {

	public static StatController statCont;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//level up
	void levelUpHealth()
	{
		int levelOrder = statCont.getHealthOrder ();
		int currentLevel = statCont.getHealth ();
		//GIVE DESIRED LEVEL

		bool agreeToLevel = true;
		float daysRequired = levelUpFunction(levelOrder,currentLevel,currentLevel+1);
		if (agreeToLevel) {
			//Subtract Days

			//Level Up Stat By Pursued
			statCont.setHealth(currentLevel+1);
		}	
	}

	//level up
	void levelUpStamina()
	{
		int levelOrder = statCont.getStaminaOrder ();
		int currentLevel = statCont.getStamina ();
		//GIVE DESIRED LEVEL

		bool agreeToLevel = true;
		float daysRequired = levelUpFunction(levelOrder,currentLevel,currentLevel+1);
		if (agreeToLevel) {
			//Subtract Days

			//Level Up Stat By Pursued
			statCont.setStamina(currentLevel+1);
		}
	}

	//level up
	void levelUpStrength()
	{
		int levelOrder = statCont.getStrengthOrder ();
		int currentLevel = statCont.getStrength ();
		//GIVE DESIRED LEVEL

		bool agreeToLevel = true;
		float daysRequired = levelUpFunction(levelOrder,currentLevel,currentLevel+1);
		if (agreeToLevel) {
			//Subtract Days

			//Level Up Stat By Pursued
			statCont.setStrength(currentLevel+1);
		}	
	}

	//level up
	void levelUpWisdom()
	{
		int levelOrder = statCont.getWisdomOrder ();
		int currentLevel = statCont.getWisdom ();
		//GIVE DESIRED LEVEL

		bool agreeToLevel = true;
		float daysRequired = levelUpFunction(levelOrder,currentLevel,currentLevel+1);
		if (agreeToLevel) {
			//Subtract Days

			//Level Up Stat By Pursued
			statCont.setWisdom(currentLevel+1);
		}	
	}

	IEnumerable<int> availableLevels(int levelOrder, int currentLevel){
		int maxLevel = 0;
		switch (levelOrder) {
		case 0:
			maxLevel = 0;
			//Cannot level up
			break;
		case 1:
			maxLevel = 35;
			break;
		case 2:
			maxLevel = 55;
			break;
		case 3:
			maxLevel = 75;
			break;
		case 4:
			maxLevel = 90;
			break;
		case 5:
			maxLevel = 100;
			break;
		}
		IEnumerable<int> range = Enumerable.Range (currentLevel+1, maxLevel);
		return range;
	}


	float levelUpFunction(int levelOrder, int currentLevel, float levelPursued)
	{
		//get levelPursused from available levels
		float nFunc = 0f;
		if (levelOrder > 1 || levelOrder <= 3) {
			nFunc = 0.5f + 0.5f * levelOrder;
		}
		if (levelOrder > 3) {
			nFunc = levelOrder - 1;
		}
		float daysRequired = (450 - 50 * nFunc) * (1 - Mathf.Pow (2, -levelPursued / 20f)) + Mathf.Pow (1.5f, levelPursued / (2f * nFunc));
		return daysRequired;
	}
}
