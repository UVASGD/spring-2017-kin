using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttractor : MonoBehaviour {

	GameObject target;

	ParticleSystem.Particle[] partArrayArray;
	Vector3[] posArray;

	public float finalSpeed = 1.0f;

	public ParticleEmit emit;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (lerping) {
			if (PosTimedLerp()) {
				emit.GetParticleSys().Clear();
				Destroy(emit.gameObject);
				Destroy(this);
			}
		}
	}

	public void AddPartArray(ParticleSystem.Particle[] array) {
		partArrayArray = array;
		posArray = new Vector3[array.Length];
	}

	public void StartLerp(GameObject tar) {
		for (int i = 0; i < partArrayArray.Length; i++) {
			posArray[i] = partArrayArray[i].position;
			partArrayArray[i].velocity = Vector3.zero;
		}
		lerping = true;
		target = tar;
	}

	bool lerping = false;
	public const float LERP_TIME = 1.25f;
	float transTime = 0.0f;

	public bool PosTimedLerp() {

		float timerVal = transTime / LERP_TIME;

		for (int i = 0; i < partArrayArray.Length; i++) {
			partArrayArray[i].position = Vector3.Lerp(posArray[i], target.transform.position, timerVal);
		}

		emit.GetParticleSys().SetParticles(partArrayArray, partArrayArray.Length);

		if (transTime > LERP_TIME) {
			transTime = 0.0f;
			return true;
		}  else {
			transTime += Time.deltaTime;
			return false;
		}
	}

	public bool GetLerping() {
		return lerping;
	}



}
