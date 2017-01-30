using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : Action {

	protected string charName;
	protected float totalTime;
	protected Vector3 finalPosition;

	GameObject character;

	// LERP Variables
	Vector3 initPos;
	float transTime = 0.0f;

	public CharMove(string character, float time, Vector3 position) : base("CharMov"){
		charName = character;
		totalTime = time;
		finalPosition = position;
	}

	override public void Run() {
		if (inProgress) {
			if (totalTime != 0) {
				// Play walking animation
				if (PosTimedLerp()) {
					inProgress = false;
					// Stop walking animation
				}
			} else {
				cont.getGameObject(charName).transform.position = finalPosition;
			}
		}
	}

	void InitiateLERPVars() {
		character = cont.getGameObject(charName);
		initPos = character.transform.position;
	}

	public bool PosTimedLerp() {

		float timerVal = transTime / totalTime;

		Camera.main.transform.position = Vector3.Lerp(initPos, finalPosition, timerVal);

		if (transTime > (totalTime - Time.deltaTime)) {
			character.transform.position = finalPosition;
			return true;
		}  else {
			transTime += Time.deltaTime;
			return false;
		}
	}
}
