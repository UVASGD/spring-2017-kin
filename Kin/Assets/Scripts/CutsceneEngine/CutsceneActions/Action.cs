using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action {

	protected CutsceneController cont;

	protected string actionName;
	protected bool inProgress;

	public Action(string name) {
		actionName = name;
		cont = GameObject.Find("Cutscene Controller").GetComponent<CutsceneController>();
	}

	abstract public void Run();

	public string getActionName() {
		return actionName;
	}

	public bool isInProgress() {
		return inProgress;
	}
}
