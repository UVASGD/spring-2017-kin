using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCreation : Action {

	protected string prefabName;
	protected string charName;
	protected float scale;
	protected Vector3 position;
	protected Dictionary<string, string> optionalArguments;

	public PrefabCreation(string pref, string character, float scl, Vector3 pos, Dictionary<string, string> optArg) : 
	base("PrefabC") {
		prefabName = pref;
		charName = character;
		scale = scl;
		position = pos;
		optionalArguments = optArg;
	}
}
