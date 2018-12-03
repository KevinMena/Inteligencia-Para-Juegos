using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	float maxSpeed = 3f; 
	float rotateSpeed = 2.5f;
	Rigidbody rb;

	public Vector3 velocidadTarget;

	float velocidadX;
	float velocidadZ;

	InfoAgente agente;

	Animator m_animator;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
		agente = GetComponent< InfoAgente> ();
		m_animator = GetComponent<Animator> ();
	}

	void Update() {
		velocidadX = Input.GetAxis("Horizontal") * maxSpeed;
		velocidadZ = Input.GetAxis("Vertical") * maxSpeed;
	}

	void FixedUpdate() {
		rb.velocity = new Vector3(velocidadX, rb.velocity.y, velocidadZ);
		m_animator.SetFloat("MoveSpeed", rb.velocity.magnitude);
		agente.velocidad = rb.velocity;
		if (Input.GetKey("e")) {
			transform.Rotate(0, rotateSpeed, 0);
		} else if (Input.GetKey("q")) {
			transform.Rotate(0, -rotateSpeed, 0);
		}
	}
}
