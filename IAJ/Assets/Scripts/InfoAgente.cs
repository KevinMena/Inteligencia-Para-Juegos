using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoAgente : MonoBehaviour {

	// Variables de informacion del agente
	public float maxSpeed = 10f;
	public Vector3 velocidad;
	public float rotacion;
	public float orientacion;
	
	// Variable que se utilizara para el movimiento del agente
	Vector3 desplazamiento;

	void Start() {
		velocidad = Vector3.zero;
		orientacion = 0.0f;
		rotacion = 0.0f;
	}

	public void UpdateInfo(Steering nSteering) {
		if (nSteering.isKinematic == true) {
			desplazamiento = nSteering.linear * Time.deltaTime;
		} else {
			desplazamiento = velocidad * Time.deltaTime;
		}
		orientacion += rotacion * Time.deltaTime;
		orientacion = orientacion < 0.0f ? 360.0f + orientacion : orientacion;
		orientacion = orientacion > 360.0f ? orientacion - 360.0f : orientacion;
		transform.rotation = new Quaternion();
		transform.Rotate(Vector3.up, orientacion, Space.Self);
		transform.Translate(desplazamiento, Space.World);

		velocidad += nSteering.linear * Time.deltaTime;
		rotacion += nSteering.angular * Time.deltaTime;

		if (nSteering.isKinematic == false) {
			if(velocidad.magnitude > maxSpeed) {
				velocidad.Normalize();
				velocidad *= maxSpeed;
			}
		}

		if (nSteering.angular == 0.0f) {
			rotacion = 0.0f;
		}
	}
}
