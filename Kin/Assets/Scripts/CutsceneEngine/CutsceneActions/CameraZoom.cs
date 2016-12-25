using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : Action {

	protected float finalSize;

	public CameraZoom(float size) : base("CamZoom") {
		finalSize = size;
	}
}
