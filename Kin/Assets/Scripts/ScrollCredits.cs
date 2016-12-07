using UnityEngine;
using System.Collections;

public class ScrollCredits : MonoBehaviour {

    public float speed = 1;
    private RectTransform rTrans;
    private float maxY;

	// Use this for initialization
	void Start () {
        rTrans = GetComponent<RectTransform>();
        RectTransform pTrans = transform.parent.GetComponent<RectTransform>();
        maxY = pTrans.rect.yMax;
    }
	
	// Update is called once per frame
	void Update () {
        if (rTrans.rect.yMax < maxY)
        {
            transform.position = transform.position + Vector3.up * speed;
        }
	}
}
