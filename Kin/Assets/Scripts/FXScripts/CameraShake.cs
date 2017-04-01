///Daniel Moore (Firedan1176) - Firedan1176.webs.com/
///26 Dec 2015
///
///Shakes camera parent object

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public bool debugMode = false;//Test-run/Call ShakeCamera() on start

    public float shakeAmount;//The amount to shake this frame.
    public float shakeDuration;//The duration this frame.

    //Readonly values...
    float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.

    bool isRunning = false; //Is the coroutine running right now?
    private GameObject cam;
    public bool smooth;//Smooth rotation?
    public float smoothAmount = 5f;//Amount to smooth
    private float curAmnt, curTime;

    void Start() {
        cam = GameObject.Find("Main Camera");
        if (debugMode) ShakeCamera();
    }
    
    void ShakeCamera() {
        curAmnt = shakeAmount;//Set default (start) values
        curTime = shakeDuration;//Set default (start) values
        
        //Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
        if (!isRunning) StartCoroutine(Shake());
    }

    public void ShakeCamera(float amount, float duration) {
        shakeAmount = amount;//Add to the current amount.
        curAmnt += shakeAmount;//Reset the start amount, to determine percentage.
        shakeDuration = duration;//Add to the current time.
        curTime += shakeDuration;//Reset the start time.

        if (!isRunning) StartCoroutine(Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }
    
    IEnumerator Shake() {
        isRunning = true;

        while (curTime > 0.01f) {
            Vector3 rotationAmount = Random.insideUnitSphere * curAmnt;//A Vector3 to add to the Local Rotation
            rotationAmount.z = 0;//Don't change the Z; it looks funny.

            shakePercentage = curTime / shakeDuration;//Used to set the amount of shake (% * startAmount).

            curAmnt = shakeAmount * shakePercentage;//Set the amount of shake (% * startAmount).
            curTime = Mathf.Lerp(curTime, 0, Time.deltaTime);//Lerp the time, so it is less and tapers off towards the end.
            
            if (smooth)
                cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, 
                    Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
            else
                cam.transform.localRotation = Quaternion.Euler(rotationAmount);//Set the local rotation the be the rotation amount.
            yield return null;
        }

        transform.localRotation = Quaternion.identity;//Set the local rotation to 0 when done, just to get rid of any fudging stuff.
        isRunning = false;
        Debug.Log("we have a winner folks");    
    }
}