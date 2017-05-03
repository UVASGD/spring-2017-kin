using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndMenuController : MonoBehaviour {

	private static EndMenuController s_instance;

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

	void Start() {
		AudioSource aSource = Camera.main.GetComponent<AudioSource>();
		string clipName = "Sounds/Player SFX/Death Sound Brian " + Random.Range(1, 3);
		AudioClip c = Resources.Load(clipName) as AudioClip;
		aSource.clip = c;
		aSource.GetComponent<AudioSource>().Play();
	}

	public static EndMenuController Instance
	{
		get { return s_instance; }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MainMenu() {
		SceneManager.LoadScene("Main Menu");
	}
}
