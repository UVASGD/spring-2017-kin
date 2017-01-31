using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEmit : MonoBehaviour {

	ParticleSystem particles;

	public GameObject target;

	float curCooldown;
	public float maxCooldown = 1.5f;

	bool setToEmit = false;

	ParticleAttractor attr;

	public bool testMode = false;

	// Use this for initialization
	void Start () {
		particles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (testMode) {
			if (Input.GetKeyDown(KeyCode.Comma) && !setToEmit) {
				if (attr == null) {
					attr = gameObject.AddComponent<ParticleAttractor>();
					if (!attr.GetLerping()) {
						XPEmit(30);
					}
				} else if (!attr.GetLerping()) {
					XPEmit(30);
				}
			}
		}
		if (setToEmit && curCooldown < maxCooldown) {
			curCooldown += Time.deltaTime;
		} else if (setToEmit) {
			curCooldown = 0.0f;
			ParticleSystem.Particle[] partArray = new ParticleSystem.Particle[particles.particleCount];
			particles.GetParticles(partArray);
			attr.AddPartArray(partArray);
			attr.StartLerp(target);
			setToEmit = false;
		}
	}

	public void UpdateParticles() {
		particles = GetComponent<ParticleSystem>();
	}

	public void XPEmit(int count) {
		particles.Emit(count);
		if (attr == null) {
			attr = gameObject.AddComponent<ParticleAttractor>();
		}
		attr.emit = this;
		setToEmit = true;
	}

	public ParticleSystem GetParticleSys() {
		return particles;
	}
}
