using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAlpha : Action {

	protected string charName;
	protected float finalAlpha;
	protected float totalTime;

	public ObjectAlpha(string character, float alpha, float time) : base("ObjAlpha") {
		charName = character;
		finalAlpha = alpha;
		totalTime = time;
	}

	override public void Run() {

	}
}
