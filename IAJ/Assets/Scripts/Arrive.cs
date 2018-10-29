using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour {

	float maxAcc = 2.5f;
	float targetRadius = 3f;

	float slowRadius = 5f;

	float timeToTarget = 0.1f;
	public GameObject target;
	protected GameObject character;

	protected InfoAgente agente;

	protected Steering steering;

	void Awake() {
		steering = new Steering();
		character = this.gameObject;
		agente = GetComponent<InfoAgente> ();
		steering.isKinematic = false;
	}

	public Steering getSteering() {
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
