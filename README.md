#License

Under the current constitution of Student Game Developers at the University of Virginia and the insistance of our current Vice President, Jay Sebastian, the game scripts, scenes, assets, resources, documents and all other content in this repository not otherwise owned or liscened by another company or organization are intended for public use and consumption free of charge and are licensed under Creative Commons Attribution-ShareAlike 4.0. Creative Commons 

![alt tag](https://camo.githubusercontent.com/e170e276291254896665fa8f612b99fe5b7dd005/68747470733a2f2f692e6372656174697665636f6d6d6f6e732e6f72672f6c2f62792d73612f342e302f38387833312e706e67)


# Kin

## Future Ideas

Adding the ability to allocate points in stats at very start of game (i.e. start off with 5/5/5/5, given 20 points to put in)

##Systems Layout

HitboxController.cs = Holds all of the hitboxes for each player animations, and cycles through them based off of the animation controller.
ATTACH TO: Player
USES: ??

PlayerMelee.cs = The players UDLR (Up/Down/Left/Right) *attack* hitboxes.
ATTACH TO: Player Prefab
USES: ??

AvatarMvmController.cs = Simple script in charge of the Animation Controller and player movement (kind of, mostly for speed?).
ATTACH TO: Player Prefab
USES: ??

PlayerHealth.cs = In charge of the *CURRENT* Health of the player and in charge of taking damage, also checks for death.
ATTACH TO: Player Prefab
USES: ??

StatController.cs = Simple Getters and Setters for *CURRENT* Stats of player, will integrate into LevelUpController.
ATTACH TO: Player Prefab
USES: ??

AnimationControl.cs = More complicated controller for the animation and movement of the player.
ATTACH TO: Player Prefab
USES: ??

CameraController.cs = Gets position of the player and tracks them.
ATTACH TO: Main Camera
USES: ??

SaveController.cs = Able to save and load stats into serializables, updates the GUI?.
ATTACH TO: SaveController Prefab
USES: ??

TimeController.cs = Keeps track of game time (in days).
ATTACH TO: ??
USES: ??

UIController.cs = Sets the UI elements for the player health and stamina.
ATTACH TO: HUD Prefab
USES: ??

TempGameController.cs = Also used for setting UI elements?? 
ATTACH TO: HUDController Prefab
USES: ??
  
#### TODO

	Integrate Player to Enemy Combat
	
	Day and Night Cycle
	
	Weather
	
	Create all Prefabs
	
	Level-Up Controller
	
	Set Main Scene
	
	Get Left-Right Animation Hitboxes
	
	Rolls and Attack Animation
	
	
##Controls

Arrow = Movement

X = Attack

Circle = Roll

Square = Interact

Triangle = Use

Start/Middle = Pause

R1/L1 = switch through consumables


Level Up Change Idea

Will only show progress, summary of stat, where to go

Leveling up the mechanism requires going to the monastery

For balancing there will be different levels (i.e. overall level)




using UnityEngine;
using System.Collections;

public class levelupController : MonoBehaviour {
    public int playerLevel = 1;
    public int orderLevel = 1;

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

