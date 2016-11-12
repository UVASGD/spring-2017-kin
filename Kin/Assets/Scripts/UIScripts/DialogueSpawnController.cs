using UnityEngine;
using System.Collections;

public class DialogueSpawnController : MonoBehaviour {

	public float writeSpeed;

	string dialogueString;

	public enum PersonType {

	}

	public PersonType person;

	DialogueXMLParser parser;
	public GameObject xmlParser;

	bool interacted = false;

	public GameObject speaker;

	// Use this for initialization
	void Start () {
		parser = xmlParser.GetComponent<DialogueXMLParser>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Y)) {
			Debug.Log(parser.RequestDialogue("WisdomTrainer", "Init", 0));
		}
	}
}
