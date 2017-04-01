using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRune : MonoBehaviour {

    public GameObject rune;

    public void dropRune() {
        Instantiate(rune, this.transform.position + new Vector3(0, 0, 0.01f), Quaternion.identity);
		Debug.Log("Drop Rune");
    }

}
