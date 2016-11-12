using UnityEngine;
using System.Collections;
using System.Xml;

public class DialogueXMLParser : MonoBehaviour {

	XmlDocument xmlDoc;

	// Use this for initialization
	void Start () {
		xmlDoc = new XmlDocument();
		TextAsset textAsset = (TextAsset) Resources.Load("XML/MyXMLFile");  
		xmlDoc.LoadXml ( textAsset.text );
	}

	// Update is called once per frame
	void Update () {

	}

	public string RequestDialogue(string person, string label) {
		XmlNodeList personList = xmlDoc.GetElementsByTagName(person);
		foreach (XmlNode node in personList) {
			XmlNodeList diaList = node.ChildNodes;
			foreach (XmlNode childNode in diaList) {
				if (childNode.Name == label) {
					int num;
					if (childNode.Attributes["flag"] && childNode.Attributes["flag"].Value == "random")
						num = Random.Range(0, childNode.ChildNodes.Count);
					else {
						num = 0; // TODO: Figure this out later.
					}
					return childNode.ChildNodes[num].InnerText;
				}
			}
		}
		return null;
	}
}
