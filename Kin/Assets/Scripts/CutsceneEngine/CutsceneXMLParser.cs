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
		XmlNodeList cutsceneList = xmlDoc.GetElementsByTagName("Cutscenes");
		XmlNode requestedCutscene = null;
		List<Action> actionList = new List<Action>();
		foreach (XmlNode cutscene in cutsceneList) {
			if (cutscene.Name == cutName) {
				requestedCutscene = cutscene;
				break;
			}
		}
		if (requestedCutscene == null) {
			Debug.LogError("ERROR: CUTSCENE " + cutName + " NOT FOUND.");
			return null;
		} else {
			XmlNodeList cutActList = requestedCutscene.ChildNodes;
			foreach (XmlNode node in cutActList) {
				actionList.Add(ActionReader(node));
			}
		}
		return actionList;
	}

	public Action ActionReader(XmlNode act) {
		Action action = null;
		XmlNodeList optList = act.ChildNodes;
		switch (act.Name) 
			{
			case "CamMov":
			float time = float.Parse(act.Attributes["time"].Value);
			float endPosX = float.Parse(optList[0].ChildNodes[0].InnerText);
			float endPosY = float.Parse(optList[0].ChildNodes[1].InnerText);
			float endPosZ = float.Parse(optList[0].ChildNodes[2].InnerText);
			Vector3 endPos = new Vector3(endPosX, endPosY, endPosZ);
			float size = float.Parse(optList[0].Attributes["size"].Value);
			return new CameraMove(time, endPos, 0);
			}

		return action;
	}
}
