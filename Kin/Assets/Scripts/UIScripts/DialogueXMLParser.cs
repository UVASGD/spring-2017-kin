using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class DialogueXMLParser : MonoBehaviour {

	XmlDocument xmlDoc;

	// Use this for initialization
	void Start () {
		xmlDoc = new XmlDocument();
		TextAsset textAsset = (TextAsset) Resources.Load("XML/Duologue");  
		xmlDoc.LoadXml ( textAsset.text );
	}

	// Update is called once per frame
	void Update () {

	}

	public List<string> RequestDialogue(string person, string label, int index) {
		XmlNodeList personList = xmlDoc.GetElementsByTagName(person);
		List<string> list = new List<string>();
		foreach (XmlNode node in personList) {
			XmlNodeList diaList = node.ChildNodes;
			foreach (XmlNode childNode in diaList) {
				if (childNode.Name == label) {
					int num = 0;
					if (childNode.Attributes ["flag"] != null && childNode.Attributes ["flag"].Value == "random") {
						num = Random.Range (0, childNode.ChildNodes.Count);
					}else if (childNode.Attributes["flag"] != null && childNode.Attributes["flag"].Value == "ordered"){
						num = index;
					} else {
						num = 0;
					}
					XmlNode diaNode = childNode.ChildNodes[num];
					list.Add(childNode.ChildNodes[num].Attributes["dialogue"].Value);
					if (diaNode.ChildNodes.Count > 0) {
						list.Add(childNode.ChildNodes[num].ChildNodes[0].Attributes["a"].Value);
						list.Add(childNode.ChildNodes[num].ChildNodes[0].Attributes["b"].Value);
					}
					return list;
				}
			}
		}
		return null;
	}

	public string RequestName(string person, string label) {
		XmlNodeList personList = xmlDoc.GetElementsByTagName("Characters");
		personList = personList.Item(0).ChildNodes;
		foreach (XmlNode type in personList) {
			if (type.Name == person){
				foreach (XmlNode node in type.ChildNodes){
					if (node.Name == label) {
						return node.Attributes["name"].Value;
					}
				}
			}
		}
		return null;
	}
}
