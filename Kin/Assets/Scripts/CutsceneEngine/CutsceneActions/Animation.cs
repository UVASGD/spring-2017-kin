using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : Action {

	protected string animation;
	protected string charName;

	public Animation(string anim, string character) : base("Anim") {
		animation = anim;
		charName = character;
	}
}
