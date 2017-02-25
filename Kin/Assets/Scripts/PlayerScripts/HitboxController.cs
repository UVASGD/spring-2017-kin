using UnityEngine;
using System.Collections;

public class HitboxController : MonoBehaviour {

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

	//Idle_SideR Sprites
	public PolygonCollider2D Character_152R;
	public PolygonCollider2D Character_153R;
	public PolygonCollider2D Character_154R;
	public PolygonCollider2D Character_155R;
	public PolygonCollider2D Character_156R;
	public PolygonCollider2D Character_157R;

	//Walk_SideR Sprites
	public PolygonCollider2D Character_161R;
	public PolygonCollider2D Character_162R;
	public PolygonCollider2D Character_163R;
	public PolygonCollider2D Character_164R;
	public PolygonCollider2D Character_165R;
	public PolygonCollider2D Character_166R;

	//Idle_SideL Sprites
	public PolygonCollider2D Character_152L;
	public PolygonCollider2D Character_153L;
	public PolygonCollider2D Character_154L;
	public PolygonCollider2D Character_155L;
	public PolygonCollider2D Character_156L;
	public PolygonCollider2D Character_157L;

	//Walk_SideL Sprites
	public PolygonCollider2D Character_161L;
	public PolygonCollider2D Character_162L;
	public PolygonCollider2D Character_163L;
	public PolygonCollider2D Character_164L;
	public PolygonCollider2D Character_165L;
	public PolygonCollider2D Character_166L;


	//Array of colliders for animation to switch from
	private PolygonCollider2D[] allHitboxes;
	//Current collider being used in the animation
	private PolygonCollider2D curHitbox;

	private AnimationControl ac;

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
		Character_152R,
		Character_153R,
		Character_154R,
		Character_155R,
		Character_156R,
		Character_157R,
		Character_161R,
		Character_162R,
		Character_163R,
		Character_164R,
		Character_165R,
		Character_166R,
		Character_152L,
		Character_153L,
		Character_154L,
		Character_155L,
		Character_156L,
		Character_157L,
		Character_161L,
		Character_162L,
		Character_163L,
		Character_164L,
		Character_165L,
		Character_166L,
		clear // special case to remove all boxes
	}


	// Use this for initialization
	void Start () {
		// NOTE: this is a TERRIBLE way to program, just saying
		// pls fix so this doesn't break (if it hasn't already)
		allHitboxes = new PolygonCollider2D[]{
			Character_0,Character_1,Character_2,Character_3,Character_4,Character_5,
			Character_9,Character_10,Character_11,Character_12,Character_13,Character_14,
			Character_76,Character_77,Character_78,Character_79,Character_80,Character_81,
			Character_85,Character_86,Character_87,Character_88,Character_89,Character_90,
			Character_152R,Character_153R,Character_154R,Character_155R,Character_156R,Character_157R,
			Character_161R,Character_162R,Character_163R,Character_164R,Character_165R,Character_166R,
			Character_152L,Character_153L,Character_154L,Character_155L,Character_156L,Character_157L,
			Character_161L,Character_162L,Character_163L,Character_164L,Character_165L,Character_166L
			};

		//Create the collider
		curHitbox = gameObject.AddComponent<PolygonCollider2D>();
        curHitbox.isTrigger = true;
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
			bool isFlipped = gameObject.GetComponent<SpriteRenderer>().flipX;
			if (isFlipped) {
				string newhb = hb.ToString().Replace ("R", "L");
				hitBoxes flippedhb = (hitBoxes) System.Enum.Parse( typeof( hitBoxes ), newhb );
				curHitbox.SetPath(0, allHitboxes[(int)flippedhb].GetPath(0));
			} else {
				curHitbox.SetPath(0, allHitboxes[(int)hb].GetPath(0));
				//Debug.Log ("value is " + hb);
			} 
				
			return;
		}
		curHitbox.pathCount = 0;
	}
}
