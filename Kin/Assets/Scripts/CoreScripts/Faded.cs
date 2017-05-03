using UnityEngine;
using System.Collections;

public class Faded : MonoBehaviour {

	public Texture2D black;
	public float fadeSpeed = 1.5f;

	int drawDepth = -1000;
	float alpha = 1.0f;
	int fadeDir = -1;

	void Update () {
	}

	/// <summary>
	/// Begins the fade for transitions.
	/// </summary>
	/// <returns>The fade.</returns>
	/// <param name="dir">Dir.</param>
	public float BeginFade(int dir) {
		return 1;
	}
}
