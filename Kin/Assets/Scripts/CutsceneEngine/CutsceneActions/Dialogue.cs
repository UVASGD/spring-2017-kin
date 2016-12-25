using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Action {

	protected Sprite sprite;
	protected string diaName;
	protected string diaStr;

	public Dialogue(Sprite spr, string name, string str) : base("Dia") {
		sprite = spr;
		diaName = name;
		diaStr = str;
	}
}
