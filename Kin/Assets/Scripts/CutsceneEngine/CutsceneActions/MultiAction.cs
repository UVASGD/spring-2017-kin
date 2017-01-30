using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAction : Action {

	protected List<Action> actions;

	public MultiAction(List<Action> acts) : base("MultiAction") {
		actions = acts;
	}

	override public void Run() {

	}
}
