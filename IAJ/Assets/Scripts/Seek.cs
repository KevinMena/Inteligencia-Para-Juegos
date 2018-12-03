using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Behaviours {
	float maxAcc = 50f;

	void Start() {
		base.Init();
	}

	public float getNewOrientation (float currentOrientation, Vector3 velocity) {

		if (velocity.magnitude > 0.0f) {
			return Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
		} else {
			return currentOrientation;
		}
	}
	
	public override Steering getSteering() {
		steering.linear = target.transform.position - this.transform.position;
		steering.linear.Normalize();
		steering.linear *= maxAcc;

		steering.angular = 0.0f;
		return steering;
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
