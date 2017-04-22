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
}
