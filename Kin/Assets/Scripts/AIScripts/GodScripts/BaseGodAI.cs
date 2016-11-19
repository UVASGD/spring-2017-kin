using UnityEngine;
using UnityEditor;
using System.Collections;

public class BaseGodAI : MonoBehaviour {

	public float awarenessRadius; //Range to change idle->detected
	public GameObject targetObject; //Player target
	public float speed = 1.0f; //Movement speed
	protected Rigidbody2D rb; //God Rigidbody
	public string minionType;
	protected bool spawnOnCd;
	protected float spawnCurrCd;
	public float spawnCd;

	protected virtual void Start()
	{
		targetObject = GameObject.Find("Player");
		//Establish rigid body for minion
		rb = gameObject.GetComponent<Rigidbody2D>();
		if (rb == null)
		{
			Debug.LogError("AI has no RigidBody. AI name is " + gameObject.name + "!");
		}
		if (targetObject == null)
		{
			Debug.LogError("AI has no target. AI name is " + gameObject.name + "!");
		}

		spawnOnCd = true;

		//prefab = AssetDatabase.LoadAssetAtPath("Assets/prefabs.MinionProj", typeof(GameObject));
	}

	protected virtual void Update()
	{

	}

	// Update is called once per frame
	protected virtual void Spawn()
	{
		if (!spawnOnCd)
		{
			SpawnMinion(gameObject.transform.position);
			spawnCurrCd = spawnCd;
			spawnOnCd = true;
		}
		else
		{ //Decrease remaining cooldown
			spawnCurrCd -= Time.deltaTime;
			if (spawnCurrCd <= 0.0f)
				spawnOnCd = false;
		}
	}
	//Move linearly towards target
	protected void MoveTowardsTarget()
	{
		rb.velocity = ((Vector2)(targetObject.transform.position - gameObject.transform.position)).normalized * speed;
	}

	protected void MoveAwayFromTarget()
	{
		//Debug.Log ("in moveaway");
		rb.velocity = ((Vector2)(gameObject.transform.position - targetObject.transform.position)).normalized * speed;
	}

	//Move linearly towards target
	protected void MoveTowardsPosition(Vector2 pos)
	{
		rb.velocity = (pos - (Vector2)gameObject.transform.position).normalized;
	}

	protected void SpawnMinion(Vector2 pos)
	{
		Debug.Log("Spawn");
		GameObject newMinion = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/" + minionType,typeof(GameObject)),
			(Vector3)Random.insideUnitCircle + targetObject.transform.position,Quaternion.identity);
		newMinion.GetComponent<BaseMinionAI>().targetObject = targetObject;
	}
}