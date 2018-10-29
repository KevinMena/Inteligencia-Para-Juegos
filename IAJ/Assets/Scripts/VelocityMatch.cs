using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatch : MonoBehaviour {

	float maxAcc= 55f;

	float timeToTarget = 0.1f;
	public GameObject target;
	protected GameObject character;

	protected InfoAgente agente;

	protected Steering steering;

	Vector3 targetVel;

	void Awake() {
		steering = new Steering();
		character = this.gameObject;
		agente = GetComponent<InfoAgente> ();
		steering.isKinematic = false;
	}

	public Steering getSteering() {
		targetVel = target.GetComponent<InfoAgente>().velocidad;
		steering.linear = targetVel - agente.velocidad;
		steering.linear /= timeToTarget;

		if(steering.linear.magnitude > maxAcc){
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
