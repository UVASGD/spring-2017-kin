using UnityEngine;
using System.Collections;

public class HitboxController : MonoBehaviour {

	public bool hitbox = true;

	//All of the sprites for the animation

	//Idle_Down Sprites
	public PolygonCollider2D Character_0;
	public PolygonCollider2D Character_1;
	public PolygonCollider2D Character_2;
	public PolygonCollider2D Character_3;
	public PolygonCollider2D Character_4;
	public PolygonCollider2D Character_5;


	//Array of colliders for animation to switch from
	private PolygonCollider2D[] allHitboxes;
	//Current collider being used in the animation
	private PolygonCollider2D curHitbox;

	//All the available hitboxes
	public enum hitBoxes
	{
		Character_0,
		Character_1,
		Character_2,
		Character_3,
		Character_4,
		Character_5,
		clear // special case to remove all boxes
	}


	// Use this for initialization
	void Start () {
		allHitboxes = new PolygonCollider2D[]{Character_0,Character_1,Character_2,Character_3,Character_4,Character_5};

		//Create the collider
		curHitbox = gameObject.AddComponent<PolygonCollider2D>();
		curHitbox.isTrigger = true; //Won't collide with environ
		curHitbox.pathCount = 0; //clear auto-generated
	}

	// On Trigger event for collider
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Collider hit something!");
	}

	// Update is called once per frame
	void Update () {

	}

	//Function called by the animation, to set the hitbox, required to preset in the enum hitBoxes
	public void setHitBox(hitBoxes hb)
	{
		if(hb != hitBoxes.clear)
		{
			Debug.Log("Changing hitbox!");
			curHitbox.SetPath(0, allHitboxes[(int)hb].GetPath(0));
			Debug.Log ("value is " + hb);
			return;
		}
		curHitbox.pathCount = 0;
	}
}
