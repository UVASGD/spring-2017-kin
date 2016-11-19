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

	DialogueXMLParser parser;
	public GameObject xmlParser;

	bool interacted = false;
	bool initialized = false;
	bool finished = true;

	public GameObject speaker;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogueString != null && dialogueString.Length > 0) {
			if (writeTimer < writeSpeed) {
				writeTimer += Time.deltaTime;
			} else if (curLength < dialogueString.Length) {
				curLength++;
				curDiaStr = dialogueString.Substring(0, curLength);
				writeTimer = (curDiaStr.EndsWith(".") ? periodMult * -writeSpeed : 0);
				diaTextBox.GetComponent<Text>().text = curDiaStr;
				finished = false;
			} else {
				finished = true;
			}
			if (Input.GetKeyDown(KeyCode.Space)) {
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

	public void Initialize() {
		parser = xmlParser.GetComponent<DialogueXMLParser>();
		initialized = true;
	}

	public bool GetInit() {
		return initialized;
	}

	public bool GetFinished() {
		return finished;
	}

	public void UpdateWithNewDia(string person, string label, int index) {
		dialogueString = parser.RequestDialogue(person, label, index);
		curLength = 0;
	}
}
