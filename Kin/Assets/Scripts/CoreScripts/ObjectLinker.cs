using UnityEngine;
using System.Reflection;
using System;

public class ObjectLinker : MonoBehaviour {

    UIController ui;
    public string methodToCall;

    void Start() {
        ui = FindObjectOfType<UIController>();
    }

	public object Run () 
	{
		object obj = typeof(ObjectLinker)
			.GetMethod(methodToCall, BindingFlags.Instance |BindingFlags.NonPublic | BindingFlags.Public)
			.Invoke(this, new object[0]);
		return obj;
	}

	public void Test() {
		Debug.Log("Testing");
	}

	public bool TestBool() {
		Debug.Log("Testing with boolean!");
		return true;
	}

	public bool WisdomLevel() {
		ui.turnOnStatsMenu((int)DialogueBox.TrainerType.WisdomTrainer);
		return true;
	}

	public bool HealthLevel() {
		ui.turnOnStatsMenu((int)DialogueBox.TrainerType.HealthTrainer);
		return true;
	}

	public bool StaminaLevel() {
		ui.turnOnStatsMenu((int)DialogueBox.TrainerType.StaminaTrainer);
		return true;
	}

	public bool StrengthLevel() {
		ui.turnOnStatsMenu((int)DialogueBox.TrainerType.StaminaTrainer);
		return true;
	}

	public bool SmithLevel() {

		return true;
	}

	public bool NoneLevel() {

		return true;
	}
}
