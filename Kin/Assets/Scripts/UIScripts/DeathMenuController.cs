using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuController : MonoBehaviour {

	private static DeathMenuController s_instance;

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

	public static DeathMenuController Instance
	{
		get { return s_instance; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
