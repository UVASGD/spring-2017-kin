using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : Action {

	protected float totalTime;
	protected Vector3 finalPosition;
	protected float finalSize;

	GameObject mainCam;

	// LERP Variables
	Vector3 initPos;
	float initSize;
	float transTime = 0.0f;

	public CameraMove(float time, Vector3 position, float size) : base("CamMov"){
		totalTime = time;
		finalPosition = position;
		finalSize = size;
	}

	override public void Run() {
		if (inProgress) {
			if (totalTime != 0) {
				if (PosTimedLerp()) {
					inProgress = false;
				}
			} else {
				cont.getMainCamera().transform.position = finalPosition;
			}
		}
	}

	void InitiateLERPVars() {
		mainCam = cont.getMainCamera().gameObject;
		initPos = mainCam.transform.position;
		initSize = cont.getMainCamera().orthographicSize;
	}

	public bool PosTimedLerp() {

		float timerVal = transTime / totalTime;

		Camera.main.transform.position = Vector3.Lerp(initPos, finalPosition, timerVal);
		Camera.main.orthographicSize = Mathf.Lerp(initSize, finalSize, timerVal);

		if (transTime > (totalTime - Time.deltaTime)) {
			mainCam.transform.position = finalPosition;
			cont.getMainCamera().orthographicSize = finalSize;
			return true;
		}  else {
			transTime += Time.deltaTime;
			return false;
		}
	}


}
