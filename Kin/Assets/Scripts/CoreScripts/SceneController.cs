using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public IEnumerator FadeToDeath() {
		float fadeTime = gameObject.GetComponent<Faded>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene("Death");
	}

	public IEnumerator FadeFromDeath() {
		float fadeTime = gameObject.GetComponent<Faded>().BeginFade(-1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene("Main");
	}
}
