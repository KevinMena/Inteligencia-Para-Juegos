using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : Behaviours {

	float maxAngularAcc= 5f;
	float maxRotation = 50f;
	float targetRadius = 0.5f;

	float slowRadius = 1f;

	float timeToTarget = 0.1f;

	void Start() {
		base.Init();
	}

	public float mapToRange (float rotation) {
		rotation %= 360.0f;
		if (Mathf.Abs(rotation) > 180.0f) {
			if (rotation < 0.0f)
				rotation += 360.0f;
			else
				rotation -= 360.0f;
		}
		return rotation;
	}

	public override Steering getSteering() {
		float rotation = target.transform.eulerAngles.y - agente.orientacion;
		rotation = mapToRange(rotation);
		float rotationSize = Mathf.Abs(rotation);
		float targetRotation = 0.0f;
		
		if (rotationSize < targetRadius) {
			return new Steering();
		}

		if (rotationSize > slowRadius) {
			targetRotation = maxRotation;
		} else {
			targetRotation = maxRotation * rotationSize / slowRadius;
		}

		targetRotation *= rotation / rotationSize;

		steering.angular = targetRotation - agente.rotacion;
		steering.angular /= timeToTarget;

		float angularAcc = Mathf.Abs(steering.angular);
		if (angularAcc > maxAngularAcc) {
			steering.angular /= angularAcc;
			steering.angular *= maxAngularAcc;
		}

		steering.linear = Vector3.zero;
		return steering;
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
