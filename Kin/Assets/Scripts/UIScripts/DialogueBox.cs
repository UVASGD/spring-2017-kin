using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueBox : MonoBehaviour {

	/// <summary>
	/// The GameObject that indicates to the player that this NPC is ready to begin talking.
	/// </summary>
	public GameObject indicator;

	/// <summary>
	/// The GameObject that holds the dialogue canvas elements. Will be an empty GameObject with canvas elements for children.
	/// </summary>
	public GameObject dialogue;

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

	// Use this for initialization
	void Start () {
	
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
					dialogue.SetActive(true);
					indicator.SetActive(false);
				}
			}
		}
	}
}
