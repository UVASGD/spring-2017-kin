using UnityEngine;
using System.Collections;

public class ScrollCredits : MonoBehaviour {

    public float speed = 1;
    private RectTransform rTrans;
    //private float maskTop;

    // Use this for initialization
    void Start () {
        rTrans = GetComponent<RectTransform>();
        //RectTransform pTrans = transform.parent.GetComponent<RectTransform>();
        //maskTop = pTrans.rect.yMin;
    }
	
	// Update is called once per frame
	void Update () {
        if (rTrans.anchoredPosition.y < 10000)
        {
            rTrans.anchoredPosition = rTrans.anchoredPosition + Vector2.up * speed;
            //Debug.Log("Moving!");
        }
	}
}
