using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : MonoBehaviour {

	GameObject[] targets;
	protected GameObject character;

	protected InfoAgente agente;

	protected Steering steering;

	public float threshold = 3.5f;

	public float decayCoefficient = 5f;

	public float maxAccel = 15f;

	void Start() {
		steering = new Steering();
		character = this.gameObject;
		agente = GetComponent<InfoAgente> ();
		targets = GameObject.FindGameObjectsWithTag("Player");
	}

	public Steering getSteering() {
		steering = new Steering();
		for(int i=0; i < targets.Length; i++) {
			Vector3 direction = gameObject.transform.position - targets[i].transform.position;
			float distance = direction.magnitude;
			float strength = 0.0f;
			if (distance < threshold) {
				strength = Mathf.Min(decayCoefficient / (distance *distance), maxAccel);
				steering.linear += direction.normalized * strength;
			}
		} 

		return steering;
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
