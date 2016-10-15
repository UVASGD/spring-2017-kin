#License

Under the current constitution of Student Game Developers at the University of Virginia and the insistance of our current Vice President, Jay Sebastian, the game scripts, scenes, assets, resources, documents and all other content in this repository not otherwise owned or liscened by another company or organization are intended for public use and consumption free of charge and are licensed under Creative Commons Attribution-ShareAlike 4.0. Creative Commons 

![alt tag](https://camo.githubusercontent.com/e170e276291254896665fa8f612b99fe5b7dd005/68747470733a2f2f692e6372656174697665636f6d6d6f6e732e6f72672f6c2f62792d73612f342e302f38387833312e706e67)


# Kin

##Systems Plan

1 Big Controller per System

####1) Combat

	hitboxes [DONE, Laura: implemented combat]
  
	numbers [WAITING: need information from Design to set]
  
	animation [DONE (kind of), Reid: waiting on sprites for animation]
  
	HealthController (OnDeath = reset stats, etc./Call the UI Element/Timing/Mostly comments atm) [DONE?, Laura]
	
	FUTURE: draw off Level Up, (health and stamina determined by Level Up)
  
####2a) Time

	just keeping track of days, timedelta, literally just time [IN PROGRESS/Skylar, Lauren, Kathy]

  
####2b) Level Up

	level controller [TODO/Reid?]
  
	stats (4 stats for Player, 4 stat for Order, Getters and Setters)[IN PROGRESS/Nathan, Paul]
  
	XP/Lumee/Luminescence/Spend Time
  
	Singleton Class [TODO/Laura]
  
####3) Day/Night and Weather

	Stuff
	
	
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
