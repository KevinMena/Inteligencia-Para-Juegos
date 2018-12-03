using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : Behaviours {

	float maxAcc = 50f;
	float targetRadius = 0.1f;

	float slowRadius = 0.3f;

	float timeToTarget = 0.1f;

	void Start() {
		Init();
	}

	public override void Init() {
		base.Init();
	}

	public override Steering getSteering() {
		Vector3 direction = target.transform.position - character.transform.position;
		float distance = direction.magnitude;
		float targetSpeed = 0.0f;
		if (distance < targetRadius) {
			return new Steering();
		}

		if (distance > slowRadius) {
			targetSpeed = agente.maxSpeed;
		} else {
			targetSpeed = agente.maxSpeed * distance / slowRadius;
		}

		Vector3 targetVelocity = direction;
		targetVelocity.Normalize();
		targetVelocity *= targetSpeed;

		steering.linear = targetVelocity - agente.velocidad;
		steering.linear /= timeToTarget;

		if (steering.linear.magnitude > maxAcc) {
			steering.linear.Normalize();
			steering.linear *= maxAcc;
		}

		steering.angular = 0.0f;
		return steering;
	}

	 void Update() {
		agente.UpdateInfo(getSteering());
	}
}
