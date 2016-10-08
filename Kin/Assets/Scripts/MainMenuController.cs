using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResumeGame() {
        SaveController.s_instance.Load();
        SceneManager.LoadScene("Game UI");
    }

    public void NewGame() {
        SceneManager.LoadScene("Game UI");
    }

    public void Options() {

    }

    public void Credits() {

    }

    public void Quit() {
        Application.Quit();
    }

}
