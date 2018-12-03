using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedSteering : Behaviours {

	public Behaviours[] behaviours;
	Vector3 maxAcc = new Vector3(15f, 0f, 15f);
	float maxRot = 50f;

	void Start() {
		base.Init();
	}

	public override Steering getSteering() {
		int i = 0;
		foreach(Behaviours behaviour in behaviours) {
			Debug.Log("Behaviour " + i + " " + behaviour.getSteering().linear);
			steering.linear += behaviour.getWeight() * behaviour.getSteering().linear;
			steering.angular += behaviour.getWeight() * behaviour.getSteering().angular;
			i++;
		}

		steering.linear = Vector3.Min(steering.linear, maxAcc);
		steering.angular = Mathf.Min(steering.angular, maxRot);

		return steering;
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
