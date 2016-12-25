using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : Action {

	protected string charName;
	protected float totalTime;
	protected Vector3 finalPosition;

	public CharMove(string character, float time, Vector3 position) : base("CharMov"){
		charName = character;
		totalTime = time;
		finalPosition = position;
	}
}
