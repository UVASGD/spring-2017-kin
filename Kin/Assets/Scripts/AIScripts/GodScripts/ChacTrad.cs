using UnityEngine;
using System.Collections;

public class ChacTrad : BaseGodAI {

	float fireBoltCd = 0;
	bool boltOnCd = false;
	float accuracy = 100;
	float boltSpeed = 2.0f;
	const float ANGLE_THRESHOLD = Mathf.PI/4;
	// Use this for initialization
	protected override void Start () {
		
	}

	// Update is called once per frame
	protected override void Update () {
		if (!boltOnCd) {
			fireBolt (2);
			boltOnCd = true;
			fireBoltCd = 1.0f;
		} else {
			fireBoltCd -= Time.deltaTime;
			if (fireBoltCd < 0.0f)
				boltOnCd = false;
		}
	
	}

	void fireBolt(int type){
		GameObject newProj1, newProj2, newProj3, newProj4, newProj5, newProj6;
		float angle = predictLocation();
		switch (type) {
		case 1:
			//Instantiate projectile from prefab Instantiate(prefab,minionposition,no rotation)
			newProj1 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle-.5f), boltSpeed * Mathf.Sin (angle-.5f));
			newProj2 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle), boltSpeed * Mathf.Sin (angle));
			newProj3 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj3.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle+.5f), boltSpeed * Mathf.Sin (angle+.5f));
			break;
		case 2:
			newProj1 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle), boltSpeed * Mathf.Sin (angle));
			break;
		case 3:
			newProj1 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle - 1.0f), boltSpeed * Mathf.Sin (angle - 1.0f));
			newProj2 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle - .5f), boltSpeed * Mathf.Sin (angle - .5f));
			newProj3 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj3.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle), boltSpeed * Mathf.Sin (angle));
			newProj4 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj4.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle + .5f), boltSpeed * Mathf.Sin (angle + .5f));
			newProj5 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj5.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle + 1.0f), boltSpeed * Mathf.Sin (angle + 1.0f));
			newProj6 = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj6.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boltSpeed * Mathf.Cos (angle + 1.5f), boltSpeed * Mathf.Sin (angle + 1.5f));
			break;
			
		default:
			return;
		} 
		
	}

	protected float accuracyRand(){
		//Requirements for spread calculation
		//accuracy = 0 -> spread = .5
		//accuracy = infin -> spread = 0	
		//so maybe use spread = .5/(accuracy+1)

		float spread = 0.5f/(accuracy/100+1);
		float mxval = 0.5f+spread;
		float mnval = 0.5f-spread;
		return Random.value*(mxval-mnval)+mnval;
	}

	protected float predictLocation(){
		float C2 = (gameObject.transform.position.x-targetObject.transform.position.x);
		float C3 = (targetObject.transform.position.y-gameObject.transform.position.y);
		float C1 = ((targetObject.GetComponent<Rigidbody2D> ().velocity.y*C2 + targetObject.GetComponent<Rigidbody2D> ().velocity.x*C3)/boltSpeed);
		float leading = 2*Mathf.Atan((C2+Mathf.Sqrt(-1*(C1*C1)+C2*C2+C3*C3))/(C1+C3));
		float still = Mathf.Atan2(C3,-C2);
		float difference = leading-still;
		float lagangle;
		float overleadangle;
		if(Mathf.Abs(difference)<0.1){//Player is standing still relative to enemy
			lagangle = still + ANGLE_THRESHOLD;
			overleadangle = still - ANGLE_THRESHOLD;
		}
		else {
			lagangle = still;
			//difference = difference>3?difference-2*Mathf.PI:difference; //Attempting to fix bug in low accuracy shots when crossing -x axis with respect to enemy.
			//difference = difference<-3?difference+2*Mathf.PI:difference;
			overleadangle = leading+difference;
		}
		float angle = accuracyRand()*(overleadangle-lagangle)+lagangle;
		//Debug.Log("("+lagangle+","+leading+","+overleadangle+")->"+angle+","+difference);
		return angle;
	}

}
