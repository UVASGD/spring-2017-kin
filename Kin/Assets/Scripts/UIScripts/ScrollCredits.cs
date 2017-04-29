using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScrollCredits : MonoBehaviour {

    public GameObject Title;
    public GameObject exitButton;
    private RectTransform rTrans;
	private double beginPos;
    private double endPos;
    private bool notDone;
    //private float maskTop;
	public float timer;
	public float length;

    // Use this for initialization
    void Start () {
        rTrans = GetComponent<RectTransform>();
		beginPos = rTrans.anchoredPosition.y;
        endPos = -rTrans.anchoredPosition.y;
        notDone = true;
        //RectTransform pTrans = transform.parent.GetComponent<RectTransform>();
        //maskTop = pTrans.rect.yMin;
		timer = 0.0f;
		length = (this.gameObject.GetComponent<AudioSource>() as AudioSource).clip.length;
    }
	
	// Update is called once per frame
	void Update () {
		if (notDone && Input.anyKeyDown) {
			timer = length;
			rTrans.anchoredPosition = new Vector3 (0.0f, (float)endPos+400, 0.0f);
		}

		timer += Time.deltaTime;
        //print(rTrans.anchoredPosition);
        if (rTrans.anchoredPosition.y < endPos) {
			rTrans.anchoredPosition = new Vector3 (0.0f, (float)(Mathf.Lerp ((float)beginPos,(float)endPos,(float)(timer/length))), 0.0f);
        }
		if(notDone && timer + 20.0f >= length) {
			Title.SetActive(true);
            exitButton.SetActive(true); 
            notDone = false; 
        }
	}

    public void exitCredits() {
        SceneManager.LoadScene("Main Menu");
    }
}
