using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiSeek : MonoBehaviour {

	public GameObject target;
	protected GameObject character;

	protected InfoAgente agente;

	protected Steering steering;

	void Awake() {
		steering = new Steering();
		character = this.gameObject;
		agente = GetComponent<InfoAgente> ();
	}

	public float getNewOrientation (float currentOrientation, Vector3 velocity) {

		if (velocity.magnitude > 0.0f) {
			return Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
		} else {
			return currentOrientation;
		}
	}

	public virtual Steering getSteering() {
		steering.linear = target.transform.position - this.transform.position;
		steering.linear.Normalize();
		steering.linear *= agente.maxSpeed;
		agente.orientacion = getNewOrientation(agente.orientacion, steering.linear);

		steering.angular = 0.0f;
		return steering;
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
