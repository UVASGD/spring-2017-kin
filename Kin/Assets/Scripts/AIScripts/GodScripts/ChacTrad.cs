using UnityEngine;
using System.Collections;

public class ChacTrad : MonoBehaviour {

	float fireBoltCd;
	float accuracy;
	float boltSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void fireBolt(int type){
		switch (type) {
		case 1:
			float angle = predictLocation();
			//Instantiate projectile from prefab Instantiate(prefab,minionposition,no rotation)
			GameObject newProj1 = (GameObject) GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj1.GetComponent<Rigidbody2D> ().velocity = new Vector2(boltSpeed*Mathf.Cos(angle),boltSpeed*Mathf.Sin(angle));
			GameObject newProj2 = (GameObject) GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj2.GetComponent<Rigidbody2D> ().velocity = new Vector2(boltSpeed*Mathf.Cos(angle),boltSpeed*Mathf.Sin(angle));
			GameObject newProj3 = (GameObject) GameObject.Instantiate (Resources.Load ("Prefabs/MinionProj", typeof(GameObject)), gameObject.transform.position, Quaternion.identity);
			newProj3.GetComponent<Rigidbody2D> ().velocity = new Vector2(boltSpeed*Mathf.Cos(angle),boltSpeed*Mathf.Sin(angle));
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
		float C1 = ((targetObject.GetComponent<Rigidbody2D> ().velocity.y*C2 + targetObject.GetComponent<Rigidbody2D> ().velocity.x*C3)/projectileSpeed);
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
