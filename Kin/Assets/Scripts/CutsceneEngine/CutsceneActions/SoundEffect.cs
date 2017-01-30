using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : Action {

	protected string soundFile;

	public SoundEffect(string sound) : base("SoundFX") {
		soundFile = sound;
	}

	override public void Run() {

	}
}
