using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour {

    Vector3 collisionRay;

    Vector3 maxAvoidForce = new Vector3(5f, 0f, 5f);

    GameObject[] targets;

    CollisionDetector collisionDetector;

    public GameObject target;
	protected GameObject character;

	protected InfoAgente agente;

	protected Steering steering;

    void Start() {
		targets = GameObject.FindGameObjectsWithTag("Player");
        collisionDetector = GetComponent<CollisionDetector> ();
        steering = new Steering();
		steering.isKinematic = false;
		character = this.gameObject;
		agente = GetComponent<InfoAgente> ();
	}

    public Steering getSteering() {
		
        GameObject mostThreatening = findObstacle();

        if( mostThreatening != null) {
            steering.linear.x = collisionRay.x - mostThreatening.transform.position.x;
            steering.linear.z = collisionRay.z - mostThreatening.transform.position.z;

            steering.linear.Normalize();
            steering.linear.Scale(maxAvoidForce);
        } else {
            steering.linear.Scale(Vector3.zero);
        }

        return steering;
	}

    GameObject findObstacle() {
        GameObject mostThreatening = null;

        for(int i =0; i < targets.Length; i++) {
            Vector3 targetPosition = targets[i].transform.position;
            Vector3 ray = collisionDetector.intersectRay(targetPosition);

            bool collides = ray.magnitude < Vector3.positiveInfinity.magnitude;

            if( collides && (mostThreatening == null || Vector3.Distance(transform.position, targetPosition) < Vector3.Distance(transform.position, mostThreatening.transform.position))) {
                mostThreatening = target;
                collisionRay = ray;
            }
        }

        return mostThreatening;
    }

	void Update() {
		agente.UpdateInfo(getSteering());
	}
	
}
