using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiWander : MonoBehaviour {

	float maxWanderSpeed = 2f;
	float maxRotation = 55f;
	public GameObject target;
	protected GameObject character;

	protected InfoAgente agente;

	protected Steering steering;

	void Awake() {
		steering = new Steering();
		character = this.gameObject;
		agente = GetComponent<InfoAgente> ();
	}

	public Vector3 asVector (float orientation) {
		Vector3 vector  = Vector3.zero;
		vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad);
		vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad);
		return vector.normalized;
	}

	float randomBinomial() {
		return Random.Range(0.0f, 1.0f) - Random.Range(0.0f, 1.0f);
	}

	public Steering getSteering() {
		steering.linear = maxWanderSpeed * asVector(agente.orientacion);
		steering.angular = maxRotation * randomBinomial();
		//agente.orientacion = getNewOrientation(agente.orientacion, steering.linear);

		return steering;
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
