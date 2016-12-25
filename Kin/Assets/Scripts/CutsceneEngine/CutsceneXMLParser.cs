using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class CutsceneXMLParser : MonoBehaviour {

	XmlDocument xmlDoc;

	// Use this for initialization
	void Start () {
		xmlDoc = new XmlDocument();
		TextAsset textAsset = (TextAsset) Resources.Load("XML/Cutscenes");  
		xmlDoc.LoadXml ( textAsset.text );
		Debug.Log(textAsset.text);
	}

	// Update is called once per frame
	void Update () {

	}

	public List<Action> RequestCutscene(string cutName) {

		return null;
	}
}
