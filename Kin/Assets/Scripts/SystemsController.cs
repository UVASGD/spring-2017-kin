using UnityEngine;
using System.Collections;

public class SystemsController : MonoBehaviour {

	public bool hitbox = true;
	public PolygonCollider2D Forward1;
	public PolygonCollider2D WalkingRight1;


	private PolygonCollider2D[] colliders;
	private PolygonCollider2D coll;


	Rigidbody2D rb;

	public enum hitBoxes
	{
		Forward1,
		WalkingRight1,
		clear // special case to remove all boxes
	}


	// Use this for initialization
	void Start () {
		colliders = new PolygonCollider2D[]{Forward1,WalkingRight1};

		rb = gameObject.GetComponent<Rigidbody2D>();
		coll = gameObject.AddComponent<PolygonCollider2D>();
		coll.isTrigger = true;
	}

	// Trigger 2D
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Collider hit something!");
		col.GetComponent<Collider2D> ().enabled = false;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		Debug.Log ("Collider left something!");
		col.GetComponent<Collider2D> ().enabled = true;
		coll.enabled = true;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		Debug.Log ("Collider is staying in something!");
		col.GetComponent<Collider2D> ().enabled = false;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void setHitBox(hitBoxes val)
	{
		if(val != hitBoxes.clear)
		{
			Debug.Log("Changing hitbox!");
			coll.SetPath(0, colliders[(int)val].GetPath(0));
			Debug.Log ("value is " + val);
			return;
		}
		coll.pathCount = 0;
	}
}
