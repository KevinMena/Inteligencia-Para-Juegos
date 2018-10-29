using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek {

	CollisionDetector collisionDetector;
    GameObject[] targets;

    public float avoidDistance = 5f;

    void Start() {
        collisionDetector = GetComponent<CollisionDetector> ();
        targets = GameObject.FindGameObjectsWithTag("Wall");
    }

    public override getSteering() {
        for(int i =0; i < targets.Length; i++) {
            Vector3 targetPosition = targets[i].transform.position;
            Vector3 ray = collisionDetector.intersectRay(targetPosition);

            bool collides = ray.magnitude < Vector3.positiveInfinity.magnitude;

            if (collides) {

            }
        }
    }
}
