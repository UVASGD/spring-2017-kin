using UnityEngine;
using System.Collections;

public class Nightlight : MonoBehaviour {

	DayNightController DNC;
	Light NL;
	public float speed = 0.01f;

	// Use this for initialization
	void Start (){

		/*if (!GameObject.Find("DayNightHolder") ()) {
			Camera.main.gameObject.AddComponent<DayNightController> ();
		}*/
		DNC = GameObject.Find("DayNightHolder").gameObject.GetComponent<DayNightController>();
		if (!this.GetComponent<Light> ()) {
			this.gameObject.AddComponent<Light> ().type = LightType.Spot;
		}
		NL = this.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (DNC.currentPhase.ToString ());
		if (DNC.currentPhase == DayNightController.DayPhase.Dusk) {
			if (NL.intensity < 1.00f) {
				NL.intensity += speed;
			}
		} else if (DNC.currentPhase == DayNightController.DayPhase.Night) {
			if (NL.intensity < 1.00f) {
				NL.intensity = 1.00f;
			}
		} else if (DNC.currentPhase == DayNightController.DayPhase.Dawn) {
			if (NL.intensity > 0.00f) {
				NL.intensity -= speed;
			}
		} else {
			if (NL.intensity > 0.00f) {
				NL.intensity = 0.00f;
			}
		}
	}
}
