using UnityEngine;
using System.Collections;

public class CalendarController : MonoBehaviour {

	int bigBoiCount = 0;
	//20
	public GameObject bigBoi;

	int mediumBoiCount = 0;
	//18
	public GameObject mediumBoi;

	int smallBoiCount = 0;
	//13
	public GameObject smallBoi;

	int miniBoiCount = 0;
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

	Quaternion bigBoiRotation;
	Quaternion mediumBoiRotation;
	Quaternion smallBoiRotation;
	Quaternion miniBoiRotation;

	Quaternion bigBoiFinalRot;
	Quaternion mediumBoiFinalRot;
	Quaternion smallBoiFinalRot;
	Quaternion miniBoiFinalRot;


	// Use this for initialization
	void Start () {
		/*bigBoiRotation = Quaternion.identity;
		mediumBoiRotation = Quaternion.identity;
		smallBoiRotation = Quaternion.identity;
		miniBoiRotation = Quaternion.identity;*/

		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.B)) {
			CalendarIncrement();
		}
		if (lerping) {
			
			CalendarLerp();
		}
	}

	public void CalendarUpdate() {
		bigBoiInit = bigBoi.transform.localRotation;
		mediumBoiInit = mediumBoi.transform.localRotation;
		smallBoiInit = smallBoi.transform.localRotation;
		miniBoiInit = miniBoi.transform.localRotation;

		transTime = 0.0f;

		source.Play();

		lerping = true;
	}

	public void CalendarSet(int kin) {
		bigBoiCount = miniBoiCount = kin;
		mediumBoiCount = smallBoiCount = kin / 20;
		//Debug.Log("Big Boi Count: " + bigBoiCount + ", Medium Boi Count: " + mediumBoiCount +
			//", Small Boi Count: " + smallBoiCount + ", Mini Boi Count: " + miniBoiCount);
		bigBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (360/20) * bigBoiCount);
		mediumBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (-360/18) * mediumBoiCount);
		smallBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (360/13) * smallBoiCount);
		miniBoiFinalRot = Quaternion.Euler(0.0f, 0.0f, (-360/9) * miniBoiCount);
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
