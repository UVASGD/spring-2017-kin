using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject playerChar;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = Vector3.Lerp(getLerpA(), 
            StaticMethods.ChangeZ(playerChar.transform.position, this.gameObject.transform.position.z), 
            /*vTot(playerChar.GetComponent<Rigidbody2D>().velocity.magnitude)*/1);
	}
	/// <summary>
	/// Vs the tot.
	/// </summary>
	/// <returns>The tot.</returns>
	/// <param name="v">V.</param>
    float vTot (float v)
    {
        return 1 / (v + 1);
    }

	/// <summary>
	/// Gets the lerp a for moving the camera.
	/// </summary>
	/// <returns>The lerp a.</returns>
    Vector3 getLerpA()
    {
        return StaticMethods.ChangeZ(playerChar.transform.position, this.gameObject.transform.position.z)
            - StaticMethods.V2toV3(playerChar.GetComponent<Rigidbody2D>().velocity, this.gameObject.transform.position.z);
    }
}
