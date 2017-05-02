using UnityEngine;
using System.Collections;

public class CalendarController : MonoBehaviour {

	float bigBoiCount = 0f;
	//20
	public GameObject bigBoi;

	float mediumBoiCount = 0f;
	//18
	public GameObject mediumBoi;

	float smallBoiCount = 0f;
	//13
	public GameObject smallBoi;

	float miniBoiCount = 0f;
	//9
	public GameObject miniBoi;
    
	AudioSource source;

	bool lerping = false;
	public const float LERP_TIME = 3.45f;
	float transTime = 0.0f;

	Quaternion bigBoiInit;
	Quaternion mediumBoiInit;
	Quaternion smallBoiInit;
	Quaternion miniBoiInit;

	Quaternion bigBoiFinalRot;
	Quaternion mediumBoiFinalRot;
	Quaternion smallBoiFinalRot;
	Quaternion miniBoiFinalRot;


	// Use this for initialization
	void Start () {
		bigBoi.transform.localRotation = Quaternion.identity;
		mediumBoi.transform.localRotation = Quaternion.identity;
		smallBoi.transform.localRotation = Quaternion.identity;
		miniBoi.transform.localRotation = Quaternion.identity;

		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindObjectOfType<InputOverrideController>().IsNormal() && Input.GetKeyDown(KeyCode.B)) {
			CalendarIncrement();
		}
		if (lerping) {
			
			CalendarLerp();
		}
	}

	public void CalendarUpdate() {
		if (bigBoi != null && mediumBoi != null && smallBoi != null && miniBoi != null) {
			bigBoiInit = bigBoi.transform.localRotation;
			mediumBoiInit = mediumBoi.transform.localRotation;
			smallBoiInit = smallBoi.transform.localRotation;
			miniBoiInit = miniBoi.transform.localRotation;
		} else {
			bigBoi = transform.GetChild(0).gameObject;
			mediumBoi = transform.GetChild(1).gameObject;
			smallBoi = transform.GetChild(2).gameObject;
			miniBoi = transform.GetChild(3).gameObject;
			bigBoiInit = bigBoi.transform.localRotation;
			mediumBoiInit = mediumBoi.transform.localRotation;
			smallBoiInit = smallBoi.transform.localRotation;
			miniBoiInit = miniBoi.transform.localRotation;
		}

		transTime = 0.0f;
		if (source != null) {
			source.Play();
		}

		lerping = true;
	}

	public void CalendarSet(int kin) {
		bigBoiCount = miniBoiCount = kin;
		mediumBoiCount = smallBoiCount = kin / 20.0f;
		//Debug.Log("Big Boi Count: " + bigBoiCount + ", Medium Boi Count: " + mediumBoiCount +
			//", Small Boi Count: " + smallBoiCount + ", Mini Boi Count: " + miniBoiCount);
		bigBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (360.0f/20.0f) * bigBoiCount);
		mediumBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (-360.0f/18.0f) * mediumBoiCount);
		smallBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (360.0f/13.0f) * smallBoiCount);
		miniBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (-360.0f/9.0f) * miniBoiCount);
		CalendarUpdate();
	}

	public void CalendarIncrement() {
		bigBoiCount++;  miniBoiCount++;
		if (bigBoiCount % 20 == 0) {
			mediumBoiCount++; smallBoiCount++;
		}
		//Debug.Log("Big Boi Count: " + bigBoiCount + ", Medium Boi Count: " + mediumBoiCount +
			//", Small Boi Count: " + smallBoiCount + ", Mini Boi Count: " + miniBoiCount);
		bigBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (360/20) * bigBoiCount);
		mediumBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (-360/18) * mediumBoiCount);
		smallBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (360/13) * smallBoiCount);
		miniBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (-360/9) * miniBoiCount);
		CalendarUpdate();
	}

	void CalendarLerp() {
		float timerVal = transTime / LERP_TIME;

		bigBoi.transform.rotation = Quaternion.Lerp(bigBoiInit, bigBoiFinalRot, timerVal);
		mediumBoi.transform.rotation = Quaternion.Lerp(mediumBoiInit, mediumBoiFinalRot, timerVal);
		smallBoi.transform.rotation = Quaternion.Lerp(smallBoiInit, smallBoiFinalRot, timerVal);
		miniBoi.transform.rotation = Quaternion.Lerp(miniBoiInit, miniBoiFinalRot, timerVal);

		if (transTime > LERP_TIME) {

			bigBoi.transform.rotation = bigBoiFinalRot;
			mediumBoi.transform.rotation = mediumBoiFinalRot;
			smallBoi.transform.rotation = smallBoiFinalRot;
			miniBoi.transform.rotation = miniBoiFinalRot;

			transTime = 0.0f;
			lerping = false;
		}  else {
			transTime += Time.deltaTime;
		}
	}
}
