using UnityEngine;
using System.Reflection;
using System;

public class ObjectLinker : MonoBehaviour { 

	public string methodToCall;

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

		return true;
	}

	public bool HealthLevel() {

		return true;
	}

	public bool StaminaLevel() {

		return true;
	}

	public bool StrengthLevel() {

		return true;
	}

	public bool SmithLevel() {

		return true;
	}

	public bool NoneLevel() {

		return true;
	}
}
