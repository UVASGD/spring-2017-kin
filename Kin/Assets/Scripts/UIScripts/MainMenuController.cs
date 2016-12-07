using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

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

    }

    public void Credits() {
        SceneManager.LoadScene(3);
    }

    public void Quit() {
        Application.Quit();
    }

}
