using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {
	public float timeLeft;

	public float day = 1;
	//Text text;

	// Use this for initialization
	void Start () {
		timeLeft = 420.0f;
	}

	void Update(){
		UpdatebyTime ();
	}

	// Update is called once per frame
	private void UpdatebyTime () {
		timeLeft -= Time.deltaTime;
		Debug.Log (timeLeft);
		if(timeLeft < 0)
		{
			day += 1;
			timeLeft = 420.0f;
		}
		if (day == 12) {
			//game ends
		}

	}
}
