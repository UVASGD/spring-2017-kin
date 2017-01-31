using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : Action {

	protected string charName;

	public Delete(string character) : base("Del") {
		charName = character;
	}

	override public void Run() {

	}
}
