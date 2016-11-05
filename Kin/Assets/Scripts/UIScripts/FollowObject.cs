using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

	public GameObject target;

	public Vector3 offset;

	public bool objUI;

	public int setZ;

	// Use this for initialization
	void Start () {
		if (target == null) {
			Debug.LogError("Follow Object target is null on " + gameObject.name);
		}
	}

	void OnEnable() {
		Update();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 followVec;
		if (!objUI)
			followVec = StaticMethods.ChangeZ(target.transform.position, setZ) + offset;
		else {
			followVec = StaticMethods.ChangeZ(target.transform.position, setZ) + offset;
			followVec = Camera.main.WorldToScreenPoint(followVec);
		}
		this.gameObject.transform.position = followVec;
	}
}
