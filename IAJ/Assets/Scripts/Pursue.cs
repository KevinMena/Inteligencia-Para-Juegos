using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : KiSeek {

	float maxPrediction = 15f;

	private GameObject pursueTarget;

	void Start() {
		pursueTarget = target;
		target = new GameObject();
	}

	public override Steering getSteering() {
		Vector3 direction = pursueTarget.transform.position - gameObject.transform.position;
		float distance = direction.magnitude;
		float speed = agente.velocidad.magnitude;
		float prediction = 0.0f;

		if (speed <= distance / maxPrediction) {
			prediction = maxPrediction;
		} else {
			prediction = distance/speed;
		}
		target.transform.position = pursueTarget.transform.position;
		Vector3 targetPrediction = pursueTarget.GetComponent<InfoAgente>().velocidad * prediction;
		target.transform.position += targetPrediction;
		return base.getSteering();
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
	
}
