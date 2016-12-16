using UnityEngine;
using System.Collections;

public class Faded : MonoBehaviour {

	public Texture2D black;
	public float fadeSpeed = 1.5f;

	int drawDepth = -1000;
	float alpha = 1.0f;
	int fadeDir = -1;

	void OnGUI () {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.g, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
	}

	/// <summary>
	/// Begins the fade for transitions.
	/// </summary>
	/// <returns>The fade.</returns>
	/// <param name="dir">Dir.</param>
	public float BeginFade(int dir) {
		fadeDir = dir;
		return fadeSpeed;
	}
}
