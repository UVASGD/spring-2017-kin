using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action {

	protected string actionName {get;}
	protected bool inProgress {get;}

	public Action(string name) {
		actionName = name;
	}
}
