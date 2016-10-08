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
        //SceneManager.LoadScene("Main");
    }

    public void NewGame() {
        //Uncomment after the build settings have been changed
        //SceneManager.LoadScene("Main");
    }

    public void Options() {

    }

    public void Credits() {

    }

    public void Quit() {
        Application.Quit();
    }

}
