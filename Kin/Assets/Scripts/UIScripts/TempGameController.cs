using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempGameController : MonoBehaviour {

	public GameObject Player;
    public Camera cam;
    public UIController ui;
    private GameObject[] bosses;
    private Plane[] planes;


	// Use this for initialization
	void Start () {
		ui.setMaxHealth(Player.GetComponent<PlayerHealth>().getMaxHealth());
		ui.setMaxStamina((int)Player.GetComponent<PlayerStamina>().getMaxStamina());
	}
	
	// Update is called once per frame
	void Update () {
		ui.setHealth(Player.GetComponent<PlayerHealth>().getCurrentHealth());
		ui.setStamina((int)Player.GetComponent<PlayerStamina>().getCurrentStamina());
        bossCheck();
	}


    public void bossCheck() {
        bosses = GameObject.FindGameObjectsWithTag("Boss");
        GameObject nearestBoss = null;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        BoxCollider2D collider;
        float minDist = Mathf.Infinity;
        float dist;
        bool bossNearby = true;

        // 0 is left, 1 is right, 2 is down, 3 is up 
        //planes[0].normal = planes[0].normal + Vector3.left * bossSearchOffset;
        //planes[1].normal = planes[1].normal + Vector3.right * bossSearchOffset;
        //planes[2].normal = planes[2].normal + Vector3.up * bossSearchOffset;
        //planes[3].normal = planes[3].normal + Vector3.down * bossSearchOffset;
        if (bosses.Length > 0)
        {
            foreach (GameObject boss in bosses)
            {
                dist = Vector3.Distance(boss.transform.position, Player.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestBoss = boss;
                }
            }
            collider = nearestBoss.GetComponent<BoxCollider2D>();
            bossNearby = GeometryUtility.TestPlanesAABB(planes, collider.bounds);
        }
        ui.bossHealth.SetActive(bossNearby);
        ui.bossName.enabled = bossNearby;
        if (bossNearby)
        {
            ui.setBossName(nearestBoss.name);
            ui.setBossMax(nearestBoss.GetComponent<EnemyHealth>().maxHealth);
            ui.setBossHealth(nearestBoss.GetComponent<EnemyHealth>().getHp());
        }
    }
}
