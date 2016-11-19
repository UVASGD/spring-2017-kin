﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueBox : MonoBehaviour {

	/// <summary>
	/// The GameObject that indicates to the player that this NPC is ready to begin talking.
	/// </summary>
	public GameObject indicator;

	/// <summary>
	/// The GameObject that holds the dialogue canvas elements. Will be an empty GameObject with canvas elements for children.
	/// </summary>
	public GameObject dialogue;

	DialogueSpawnController spawnCont;

	/// <summary>
	/// The range at which the player needs to be, centered around the NPC, in order to display its indicator.
	/// </summary>
	public float detectRange;

	/// <summary>
	/// The range at which the dialogue box will close itself if the player gets outside of this radius.
	/// </summary>
	public float decayRange;

	/// <summary>
	/// The player.
	/// </summary>
	public GameObject player;

	public enum PersonType {
		WisdomTrainer,
		HealthTrainer,
		StaminaTrainer,
		StrengthTrainer
	}

	public enum DiaType {
		Init,
		Greetings,
		Train
	}

	public PersonType persType;
	public DiaType diaType;

	int diaIndex = 0;

	string[] diaList;

	// Use this for initialization
	void Start () {
		spawnCont = dialogue.GetComponent<DialogueSpawnController>();
		string[] personList = System.Enum.GetNames(typeof(PersonType));
		string[] typeList = System.Enum.GetNames(typeof(DiaType));
		diaList = new string[personList.Length + typeList.Length];
		personList.CopyTo(diaList, 0);
		typeList.CopyTo(diaList, personList.Length);
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogue.activeInHierarchy) {
			if (Input.GetButtonDown("Interact")) 
				dialogue.SetActive(false);
			if (StaticMethods.Distance((Vector2)player.transform.position, (Vector2)gameObject.transform.position) > decayRange) 
				dialogue.SetActive(false);
		}
		else {
			if (StaticMethods.Distance((Vector2)player.transform.position, (Vector2)gameObject.transform.position) < detectRange)
				indicator.SetActive(true);
			else if (indicator.activeInHierarchy)
				indicator.SetActive(false);

			if (indicator.activeInHierarchy) {
				if (Input.GetButtonDown("Interact")) {
					if (!spawnCont.GetInit())
						spawnCont.Initialize();
					if (spawnCont.GetFinished())
						spawnCont.UpdateWithNewDia(diaList[(int)persType], diaList[(int)diaType + System.Enum.GetNames(typeof(PersonType)).Length], 0);
					dialogue.SetActive(true);
					indicator.SetActive(false);
				}
			}
		}
	}
}
