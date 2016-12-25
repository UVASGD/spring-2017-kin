using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherEffect : Action {

	protected string weather;

	public WeatherEffect(string wea) : base("WeatherFX") {
		weather = wea;
	}
}
