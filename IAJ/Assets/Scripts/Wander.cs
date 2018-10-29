using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face {

	public float maxAccel= 0.5f;
	public float wanderOffset = 5f;
	public float wanderRadius = 1.0f;
	public float wanderRate = 1.0f;
	private float wanderOrientation;

	public override void Init() {
		target = new GameObject();
		target.transform.position = gameObject.transform.position;
		base.Init();
	}

	void Start() {
		Init();
	}

	float randomBinomial() {
		return Random.Range(0.0f, 1.0f) - Random.Range(0.0f, 1.0f);
	}

	public Vector3 asVector (float orientation) {
		Vector3 vector  = Vector3.zero;
		vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad);
		vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad);
		return vector.normalized;
	}

	public override Steering getSteering() {
		wanderOrientation += randomBinomial() * wanderRate;
		float targetOrientation = wanderOrientation + agente.orientacion;
		Vector3 oriAsVector = asVector(agente.orientacion);
		Vector3 centerCircle = this.transform.position + wanderOffset * oriAsVector;
		Vector3 targetPos = centerCircle + (asVector(targetOrientation) * wanderRadius);
		faceTarget.transform.position = targetPos;
		steering = base.getSteering();
		steering.linear = faceTarget.transform.position - gameObject.transform.position;
		steering.linear = steering.linear.normalized * maxAccel;

		return steering;
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
