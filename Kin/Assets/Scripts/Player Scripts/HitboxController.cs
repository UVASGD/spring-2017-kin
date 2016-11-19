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

	//Walk_Down Sprites
	public PolygonCollider2D Character_9;
	public PolygonCollider2D Character_10;
	public PolygonCollider2D Character_11;
	public PolygonCollider2D Character_12;
	public PolygonCollider2D Character_13;
	public PolygonCollider2D Character_14;

	//Idle_Up Sprites
	public PolygonCollider2D Character_76;
	public PolygonCollider2D Character_77;
	public PolygonCollider2D Character_78;
	public PolygonCollider2D Character_79;
	public PolygonCollider2D Character_80;
	public PolygonCollider2D Character_81;

	//Walk_Up Sprites
	public PolygonCollider2D Character_85;
	public PolygonCollider2D Character_86;
	public PolygonCollider2D Character_87;
	public PolygonCollider2D Character_88;
	public PolygonCollider2D Character_89;
	public PolygonCollider2D Character_90;

	//Idle_Side Sprites
	public PolygonCollider2D Character_152;
	public PolygonCollider2D Character_153;
	public PolygonCollider2D Character_154;
	public PolygonCollider2D Character_155;
	public PolygonCollider2D Character_156;
	public PolygonCollider2D Character_157;

	//Walk_Side Sprites
	public PolygonCollider2D Character_161;
	public PolygonCollider2D Character_162;
	public PolygonCollider2D Character_163;
	public PolygonCollider2D Character_164;
	public PolygonCollider2D Character_165;
	public PolygonCollider2D Character_166;


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
		Character_9,
		Character_10,
		Character_11,
		Character_12,
		Character_13,
		Character_14,
		Character_76,
		Character_77,
		Character_78,
		Character_79,
		Character_80,
		Character_81,
		Character_85,
		Character_86,
		Character_87,
		Character_88,
		Character_89,
		Character_90,
		Character_152,
		Character_153,
		Character_154,
		Character_155,
		Character_156,
		Character_157,
		Character_161,
		Character_162,
		Character_163,
		Character_164,
		Character_165,
		Character_166,
		clear // special case to remove all boxes
	}


	// Use this for initialization
	void Start () {
		allHitboxes = new PolygonCollider2D[]{
			Character_0,Character_1,Character_2,Character_3,Character_4,Character_5,
			Character_9,Character_10,Character_11,Character_12,Character_13,Character_14,
			Character_76,Character_77,Character_78,Character_79,Character_80,Character_81,
			Character_85,Character_86,Character_87,Character_88,Character_89,Character_90,
			Character_152,Character_153,Character_154,Character_155,Character_156,Character_157,
			Character_161,Character_162,Character_163,Character_164,Character_165,Character_166};

		//Create the collider
		curHitbox = gameObject.AddComponent<PolygonCollider2D>();
		curHitbox.pathCount = 0; //clear auto-generated
	}

	// On Trigger event for collider
	void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log("Collider hit something!");
	}

	// Update is called once per frame
	void Update () {

	}

	//Function called by the animation, to set the hitbox, required to preset in the enum hitBoxes
	public void setHitBox(hitBoxes hb)
	{
		if(hb != hitBoxes.clear)
		{
			//Debug.Log("Changing hitbox!");
			curHitbox.SetPath(0, allHitboxes[(int)hb].GetPath(0));
			//Debug.Log ("value is " + hb);
			return;
		}
		curHitbox.pathCount = 0;
	}
}
