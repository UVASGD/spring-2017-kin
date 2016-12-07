using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScrollCredits : MonoBehaviour {

    public GameObject Title;
    public GameObject exitButton;
    public float speed = 1;
    private RectTransform rTrans;
    private double endPos;
    private bool notDone;
    //private float maskTop;

    // Use this for initialization
    void Start () {
        rTrans = GetComponent<RectTransform>();
        endPos = -rTrans.anchoredPosition.y;
        notDone = true;
        //RectTransform pTrans = transform.parent.GetComponent<RectTransform>();
        //maskTop = pTrans.rect.yMin;
    }
	
	// Update is called once per frame
	void Update () {
        //print(rTrans.anchoredPosition);
        if (rTrans.anchoredPosition.y < endPos)
        {
            rTrans.anchoredPosition = rTrans.anchoredPosition + Vector2.up * speed;
            //Debug.Log("Moving!");
        }
        else
        {
            if (notDone)
            {
                //RectTransform titleObject = Instantiate(Title);
                //titleObject.SetParent(transform.parent.transform.parent);
                //titleObject.position = Vector2.zero;
                Title.SetActive(true);
                exitButton.SetActive(true); 
                notDone = false;
            }
        }
	}

    public void exitCredits()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
