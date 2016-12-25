using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : Action {

	protected float totalTime;
	protected Vector3 finalPosition;
	protected float finalSize;

	public CameraMove(float time, Vector3 position, float size) : base("CamMov"){
		totalTime = time;
		finalPosition = position;
		finalSize = size;
	}
}
