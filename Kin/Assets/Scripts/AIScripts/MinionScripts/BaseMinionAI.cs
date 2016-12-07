using UnityEngine;
//using UnityEditor;
using System.Collections;

public class BaseMinionAI : MonoBehaviour {

	public float awarenessRadius; //Range to change idle->detected
	public GameObject targetObject; //Player target
	public float speed = 1.0f; //Movement speed
    public float patrolSpeed = 0.5f; //Patroling Speed
	protected Rigidbody2D rb; //Minion Rigidbody

    int currIndex; //the index of the point the minion is currently heading toward
    int size; //number of points to patrol around
    Vector2 startPos; //starting position
    float patrolRad; //radius of circle for patrol points
    Vector2[] positions; //set of points to patrol
    bool onWay; //whether the minion is currently moving
    float timePause, currWait; //to have the minion pause at the point before going to the next one
    bool isWaiting; //whether the minion is currently waiting

    //Set of AI behavior states
    protected enum AIStates {
		IdleState,
		DetectedState,
		PatrolState
	}
	protected AIStates curState; //Current AI behavior state


	protected void Start () {
        targetObject = GameObject.Find("Player");
        startPos = gameObject.transform.position;
        //Establish rigid body for minion
        rb = gameObject.GetComponent<Rigidbody2D>(); 
		if (rb == null) {
			Debug.LogError("AI has no RigidBody. AI name is " + gameObject.name + "!");
		}
		if (targetObject == null) {
			Debug.LogError("AI has no target. AI name is " + gameObject.name + "!");
		}

        size = 5;
        curState = AIStates.PatrolState;
        positions = new Vector2[size + 1];
        patrolRad = 1.0f;

        float angleIncr = (float)(2 * Mathf.PI) / size;
        positions[0] = startPos;
        for (int x = 1; x < positions.Length; x++)
        {
            //float theta = UnityEngine.Random.Range(0.0f, (float)(2 * Math.PI));
            float theta = x * angleIncr;
            positions[x] = new Vector2(startPos.x + patrolRad * (float)Mathf.Cos(theta), startPos.y + patrolRad * (float)Mathf.Sin(theta));
            //Debug.Log(positions[x]);
        }
        //Debug.Log(positions);
        currIndex = 0;

        onWay = false;
        isWaiting = false;

        timePause = 1.0f;
        currWait = 0.0f;

        //Set initial state
        //detected for testing, will normally be idle until awarenessRadius is reached
        curState = AIStates.DetectedState;

		//prefab = AssetDatabase.LoadAssetAtPath("Assets/prefabs.MinionProj", typeof(GameObject));
	}
	
	// Update is called once per frame
	protected virtual void Update () {


	}
	//Move linearly towards target
	protected void MoveTowardsTarget() {
		rb.velocity = ((Vector2)(targetObject.transform.position - gameObject.transform.position)).normalized * speed;
	}

	protected void  MoveAwayFromTarget(){
		//Debug.Log ("in moveaway");
		rb.velocity = ((Vector2)(gameObject.transform.position - targetObject.transform.position)).normalized * speed;
	}

    protected void Patrol()
    {
        Vector2 pos = (Vector2)gameObject.transform.position;
        if (Vector2.Distance(pos, positions[currIndex]) < .05f) //if reached destination
        {
            rb.velocity = Vector2.zero;
            onWay = false;
            isWaiting = true;
            if (currWait > timePause)
            {
                isWaiting = false;
                currWait = 0.0f;
                System.Random rand = new System.Random();
                currIndex = rand.Next(0, 4);
                MoveTowardsPosition(positions[currIndex]);
                onWay = true;
            }
            else
            {
                currWait += Time.deltaTime;
            }
        }
        else if (!isWaiting) //if currently moving
            MoveTowardsPosition(positions[currIndex]);
    }

    //Move linearly towards target
    protected void MoveTowardsPosition(Vector2 pos)
    {
        rb.velocity = (pos - (Vector2)gameObject.transform.position).normalized * patrolSpeed;
    }

    protected void recoil(Vector2 v)
    {
        rb.velocity = v;

    }
}
