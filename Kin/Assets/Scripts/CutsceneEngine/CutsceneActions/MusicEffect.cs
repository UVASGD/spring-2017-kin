using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEffect : Action {

	protected string musicFile;
	protected bool looping;

	public MusicEffect(string music, bool loop) : base("MusicFX") {
		musicFile = music;
		looping = loop;
	}

	override public void Run() {

	}
}
