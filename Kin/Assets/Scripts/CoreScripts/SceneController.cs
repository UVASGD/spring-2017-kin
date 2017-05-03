using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	//private IEnumerator coroutine;

	private static SceneController s_instance;

	void Awake()
	{
		if (s_instance == null)
		{
			DontDestroyOnLoad(gameObject); // save object on scene mvm
			s_instance = this;
		}
		else if (s_instance != this)
		{
			Destroy(gameObject);
		}
	}

	public IEnumerator Fade2Death() {
		//float fadeTime = gameObject.GetComponent<Faded>().BeginFade(1);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("Death");
	}

	public IEnumerator DeathFade() {
		//float fadeTime = gameObject.GetComponent<Faded>().BeginFade(-1);
		yield return new WaitForSeconds(1.5f);
	}

	public IEnumerator Fade2Main() {
		//float fadeTime = gameObject.GetComponent<Faded>().BeginFade(1);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("Main_with_HLD");
	}

	public IEnumerator MainFade() {
		//float fadeTime = gameObject.GetComponent<Faded>().BeginFade(-1);
		yield return new WaitForSeconds(1.5f);
	}

	public void FadeToDeath() {
		//coroutine = Fade2Death();
		StartCoroutine(Fade2Death());
		//SceneManager.LoadScene("Death");
	}

	public void FadeFromDeath() {
		StartCoroutine(DeathFade());
	}

	public void FadeToMain() {
		StartCoroutine(Fade2Main());
		//SceneManager.LoadScene("Main");
	}

	public void FadeFromMain() {
		StartCoroutine(MainFade());
	}
}
