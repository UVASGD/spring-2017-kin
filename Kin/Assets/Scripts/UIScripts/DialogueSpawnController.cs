using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueSpawnController : MonoBehaviour {

	public float writeSpeed;
	public int periodMult;

	public GameObject diaNameBox;
	public GameObject diaTextBox;
	public GameObject soundCreator;

	float writeTimer;
	int curLength = 0;

	string dialogueString;
	string dialogueName;
	string curDiaStr;

	DialogueXMLParser parser;
	public GameObject xmlParser;

	bool interacted = false;
	bool initialized = false;
	bool finished = true;

	public GameObject speaker;

	// Use this for initialization
	void Start () {
		diaNameBox.GetComponent<Text>().text = dialogueName;
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
				if (Random.Range(0.0f, 1.0f) > 0.3) {
					soundCreator.GetComponent<AudioSource>().Play();
				}
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
		Debug.Log("Parser Initialize");
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
		if (parser == null) {
			Debug.Log("Parser is null");
		}
		dialogueString = parser.RequestDialogue(person, label, index);
		curLength = 0;
	}

	public void UpdateWithNewName(string person, string label) {
		dialogueName = parser.RequestName(person, label);
	}

	public void UpdateDiaMan(string diaStr) {
		dialogueString = diaStr;
	}

	public void UpdateNameMan(string nameStr) {
		dialogueName = nameStr;
	}

	public void Complete() {
		curDiaStr = dialogueString;
		curLength = dialogueString.Length;
		finished = true;
	}
}
