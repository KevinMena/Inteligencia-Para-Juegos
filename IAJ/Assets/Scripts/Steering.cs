using System.Collections;
using UnityEngine;

public class Steering {

	// Variables de informacion del steering
	public Vector3 linear;
	public float angular;

	public bool isKinematic;

	public Steering() {
		linear = Vector3.zero;
		angular = 0.0f;
		isKinematic = true;
	}
}
