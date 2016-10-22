using UnityEngine;
using System.Collections;

public class LevelUpController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//getters
	int getPlayerLevel()
	{
		return playerLevel;
	}

	int getOrderLevel()
	{
		return orderLevel;
	}

	//setters
	void setPlayerLevel(int inputPlayerLevel)
	{
		playerLevel = inputPlayerLevel;
	}

	void setOrderLevel(int inputOrderLevel)
	{
		orderLevel = inputOrderLevel;
	}


	//level up
	void playerLevelUp()
	{
		playerLevel++;
	}

	void orderLeveUp()
	{
		orderLevel++;
	}
}
