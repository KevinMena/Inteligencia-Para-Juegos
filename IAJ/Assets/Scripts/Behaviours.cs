using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviours : MonoBehaviour {

	protected float weight = 1f;
	public GameObject target;
	protected GameObject character;

	protected InfoAgente agente;

	protected Steering steering;

	public float getWeight() {
		return weight;
	}

	void Start() {
		Init();
	}

	public virtual void Init() {
		steering = new Steering();
		steering.isKinematic = false;
		character = this.gameObject;
		agente = GetComponent<InfoAgente> ();
	}

	public virtual Steering getSteering() {
		return steering;
	}

}
