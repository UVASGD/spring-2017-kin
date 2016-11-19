using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueSpawnController : MonoBehaviour {

	public float writeSpeed;
	public int periodMult;

	public GameObject diaTextBox;

	float writeTimer;
	int curLength = 0;

	string dialogueString;
	string curDiaStr;

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
			dialogueString = parser.RequestDialogue("HealthTrainer", "Init", 0);
			Debug.Log(dialogueString);
		}
		if (dialogueString != null && dialogueString.Length > 0) {
			if (writeTimer < writeSpeed) {
				writeTimer += Time.deltaTime;
			} else if (curLength < dialogueString.Length) {
				curLength++;
				curDiaStr = dialogueString.Substring(0, curLength);
				writeTimer = (curDiaStr.EndsWith(".") ? periodMult * -writeSpeed : 0);
				diaTextBox.GetComponent<Text>().text = curDiaStr;
			}
			if (Input.GetKeyDown(KeyCode.U)) {
				curLength = dialogueString.Length;
				curDiaStr = dialogueString.Substring(0, curLength);
				diaTextBox.GetComponent<Text>().text = curDiaStr;
				writeTimer = writeSpeed;
			}
		}
	}

	public string GetCurrentString() {
		return curDiaStr;
	}
}
