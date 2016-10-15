using UnityEngine;
using System.Collections;

public class SpawnMinionController : MonoBehaviour {
	public int spawnRadius;
	public GameObject player;


	// Use this for initialization
	void Start () {
		
	
	}

	void spawnMinion(){
		int angle = ((int) Random.Range (0.0f, 360.0f));
		var rot = Quaternion.Euler (0, 0, angle);
		var position = rot * ((new Vector2 (1, 0)) * Random.Range (0.0f, 1.0f));

		GameObject newMinion= (GameObject) GameObject.Instantiate (Resources.Load ("Prefabs/RangedMinion", typeof(GameObject)), gameObject.transform.position + position, Quaternion.identity);
		newMinion.GetComponent<RangedMinion> ().targetObject = player;
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
}
