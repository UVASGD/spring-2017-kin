using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeForward : Action {

	// This could potentially be a string or an enum.
	protected float finalTime;

	public TimeForward(float time) : base("TimeFor") {
		finalTime = time;
	}
}
