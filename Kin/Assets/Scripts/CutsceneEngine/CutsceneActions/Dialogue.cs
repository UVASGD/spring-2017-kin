using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : Action {

	protected Sprite sprite;
	protected string diaName;
	protected string diaStr;

	bool init = false;

	GameObject diaObj;

	public Dialogue(Sprite spr, string name, string str) : base("Dia") {
		sprite = spr;
		diaName = name;
		diaStr = str;
	}

	override public void Run() {
		if (!init) {
			InitializeDialogue();
		} else {
			if (diaObj.activeInHierarchy) {
				if (Input.GetButtonDown("Interact")) {
					if (diaObj.GetComponent<DialogueSpawnController>().GetFinished()) {
						diaObj.SetActive(false);
						inProgress = false;
					} else {
						diaObj.GetComponent<DialogueSpawnController>().Complete();
					}
				}
			}
		}
	}

	void InitializeDialogue() {
		GameObject dialogue = (GameObject) Resources.Load("Prefabs/Dialogue Box", typeof(GameObject));
		diaObj = GameObject.Instantiate(dialogue);
		diaObj.SetActive(true);
		diaObj.GetComponent<DialogueSpawnController>().UpdateDiaMan(diaStr);
		diaObj.GetComponent<DialogueSpawnController>().UpdateNameMan(diaName);
		init = true;
	}
}
