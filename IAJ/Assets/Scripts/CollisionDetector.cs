using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CollisionDetector : MonoBehaviour {

	float lookAhead = 5f;
	float lookSide = 4f;

	float targetRadius = 1f;

	public Vector3 ray1;
	public Vector3 ray2;
	public Vector3 ray3;

	Vector3 ray1End;
	Vector3 ray2End;
	Vector3 ray3End;
	
	private void crearRays() {
		Vector3 posicionCentral = gameObject.transform.position + (Vector3.forward * lookAhead);
		Vector3 posicionlateral = gameObject.transform.position + (Vector3.forward * lookSide);

		ray1 = posicionCentral - gameObject.transform.position;

		ray2 = Quaternion.AngleAxis(-20f, Vector3.up) * (posicionlateral - gameObject.transform.position);
		ray3 = Quaternion.AngleAxis(20f, Vector3.up) * (posicionlateral - gameObject.transform.position);
	}

	private void dibujarRays() {
		Debug.DrawRay(transform.position, ray1, Color.blue);
		Debug.DrawRay(transform.position, ray2, Color.blue);
		Debug.DrawRay(transform.position, ray3, Color.blue);
	}

	private void rotarRays() {
		float anguloPersonaje = this.transform.rotation.eulerAngles.y;

		ray1 = Quaternion.AngleAxis(anguloPersonaje, Vector3.up) * ray1;
		ray2 = Quaternion.AngleAxis(anguloPersonaje, Vector3.up) * ray2;
		ray3 = Quaternion.AngleAxis(anguloPersonaje, Vector3.up) * ray3;

		ray1End = transform.position + ray1;
		ray2End = transform.position + ray2;
		ray3End = transform.position + ray3;
	}

	public Vector3 intersectRay(Vector3 center) {
		if (lineIntersectsCircle(transform.position, ray1End, center, targetRadius)) {
			Debug.Log("Colisiono con ray1");
			return ray1;
		}
		if (lineIntersectsCircle(transform.position, ray2End, center, targetRadius)) {
			Debug.Log("Colisiono con ray2");
			return ray2;
		}
		if (lineIntersectsCircle(transform.position, ray3End, center, targetRadius)) {
			Debug.Log("Colisiono con ray3");
			return ray3;
		}

		return Vector3.positiveInfinity;
	}

	public Vector3 intersectRayWithWall(Vector3 center) {
		if (lineIntersectsCircle(transform.position, ray1End, center, targetRadius)) {
			Debug.Log("Colisiono con ray1");
			return ray1;
		}
		if (lineIntersectsCircle(transform.position, ray2End, center, targetRadius)) {
			Debug.Log("Colisiono con ray2");
			return ray2;
		}
		if (lineIntersectsCircle(transform.position, ray3End, center, targetRadius)) {
			Debug.Log("Colisiono con ray3");
			return ray3;
		}

		return Vector3.positiveInfinity;
	}

	bool lineIntersectsCircle(Vector3 ray, Vector3 rayEnd, Vector3 center, float radio) {
		float distance = HandleUtility.DistancePointLine(center, ray, rayEnd);

		return distance <= radio;
	}


	void Update() {
		crearRays();
		rotarRays();
		dibujarRays();
	}
}
