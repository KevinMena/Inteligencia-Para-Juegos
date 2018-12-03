using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : Behaviours {

	float maxAcc = 50f;

	//bool debeHuir = false;

	void Start() {
		Init();
	}
	/* 
	public void setBool(bool debe) {
		Debug.Log(debe);
		debeHuir = debe;
	}*/

	public override void Init() {
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
		steering.linear = this.transform.position - target.transform.position;
		steering.linear.Normalize();
		steering.linear *= maxAcc;

		steering.angular = 0.0f;
		return steering;
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
