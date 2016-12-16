using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour {

	public GameObject options;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResumeGame() {
        PreLoader.Instance.preLoad("", false);
        SceneManager.LoadScene(1);
    }

    public void NewGame() {
        SceneManager.LoadScene(1);
    }

    public void Options() {
		options.GetComponent<Canvas>().enabled = !options.GetComponent<Canvas> ().enabled;
	}

    public void Credits() {
        SceneManager.LoadScene(3);
    }

    public void Quit() {
        Application.Quit();
    }

}
