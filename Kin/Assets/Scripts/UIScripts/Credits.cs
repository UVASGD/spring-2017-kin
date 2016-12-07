using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    public float speed = 0.2F;
    private bool crawling = false;

    private void Start()
    {


    }

    private void Update()
    {
        if (!crawling)
            return;
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (gameObject.transform.position.y > .8)
        {
            crawling = false;
        }
    }
}
