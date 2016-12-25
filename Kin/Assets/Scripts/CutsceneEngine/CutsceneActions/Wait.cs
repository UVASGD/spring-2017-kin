using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Action {

	public float totalTime;

	public Wait (float time) : base("Wait") {
		totalTime = time;
	}
}
