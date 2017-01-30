using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : Action {

	protected float totalTime;
	protected bool dir;

	public Fade(float time, bool direction) : base("Fade") {
		totalTime = time;
		dir = direction;
	}

	override public void Run() {

	}
}
