using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Memetacular : MonoBehaviour {
	GameObject player;
	Animator squirrel;
	Meme dank;
	SpriteRenderer friedChicken;
	float waxOff=0, totWax;
	Rigidbody2D robotnik;
	public float donutsPerSecond = 1;
	bool dying;
	float despawn = 0.0f;
    public GameObject ixtabRune;


	public enum Meme{
		YOLO, oneDoesNot, pepe
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		squirrel = GetComponent<Animator> ();
		dank = Meme.YOLO;
		friedChicken = gameObject.GetComponent<SpriteRenderer> ();
		totWax = Random.Range (5f, 10f);
		robotnik = GetComponent<Rigidbody2D>();
		dying = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<EnemyHealth>().getHp() <= 0) {
			death ();
            dying = true; 
        }
		//isDank ();
		if (!dying) {
			Vector2 weed = new Vector2 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
			waxOff += Time.deltaTime;
			switch (dank) {
			case Meme.YOLO:
				robotnik.velocity = Vector2.zero;
				if (weed.magnitude <= 2) {
					// eat donuts now
					dank = Meme.oneDoesNot;
				} else {
					if (waxOff >= totWax) {
						waxOff = 0;
						totWax = Random.Range (5f, 10f);
						friedChicken.flipX = !friedChicken.flipX;
					}
				}
				break;
			case Meme.oneDoesNot:
				isDank (weed);
				if (waxOff >= totWax) {
					waxOff = 0;
					totWax = Random.Range (2f, 3f);

					int lapidot = (int)Random.Range (1f, 41f);
					if (lapidot <= 10) {
						eatDelicious ();
						squirrel.SetBool ("Attack1", true);
					} else if (lapidot <= 20) {		
						eatDelicious ();			
						squirrel.SetBool ("Release", true);
						squirrel.SetBool ("Invisible", true);
						dank = Meme.pepe;
					} else if (lapidot <= 30) {
						eatDelicious ();
						squirrel.SetBool ("Attack2", true);
					} else {
						eatDelicious ();
						squirrel.SetBool ("Attack3", true);
					} 
				}
				break;
			case Meme.pepe:
				isDank (weed);
				if (waxOff >= totWax) {
					waxOff = 0;
					totWax = Random.Range (2f, 3f);

					int racistBassist = (int)Random.Range (1f, 31f);
					if (racistBassist <= 15) {
						eatDelicious ();
						squirrel.SetBool ("Attack1", true);
					} else {		
						eatDelicious ();			
						squirrel.SetBool ("Invisible", false);
						squirrel.SetBool ("Choke", true);
						dank = Meme.oneDoesNot;
					} 
				}
				break;
			}

			if (weed.magnitude >= 5)
				dank = Meme.YOLO;
		} else {
			despawn += Time.deltaTime;
			if (despawn >= 5) {
				Destroy (gameObject);
			}

		}
	}

	private void isDank(Vector2 weed){
		float trippy = 360 * Mathf.Atan2 (weed.x, weed.y) / (2 * Mathf.PI);
		friedChicken.flipX = (trippy < 0);
		robotnik.velocity = (weed.magnitude>=1)? weed.normalized*donutsPerSecond: Vector2.zero;

	}

	private void eatDelicious(){
		string[] food = new string[] {"Release", "Attack1", "Attack2","Attack3", "Choke"};
		foreach(string filler in food)
			squirrel.SetBool(filler, false);
	}

	public void PLEASE(/*string sound*/){
		//AudioClip clip = Resources.Load ("Sounds/Attack SFX/"+sound) as AudioClip;
		GetComponent<AudioSource>().Play();
	}

	public void death()
	{
		squirrel.SetBool ("Dying",true);
        if (!dying) Instantiate(ixtabRune, this.transform.position + new Vector3(0,0,0.01f), Quaternion.identity);
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;

	}
	
}
