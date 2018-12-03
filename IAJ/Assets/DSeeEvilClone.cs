using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSeeEvilClone : Transition {

	float area = 3f;
	GameObject evilClone;

	GameObject henry;

	Flee flee;
	PathFollowing pathFollowing;

	public DSeeEvilClone() {
		henry = GameObject.FindGameObjectWithTag("Player");
		evilClone = GameObject.FindGameObjectWithTag("Enemy");
		pathFollowing = henry.GetComponent<PathFollowing>();
		flee = henry.GetComponent<Flee> ();
	}

	public bool IsTriggered() {
		Vector3 direction = henry.transform.position - evilClone.transform.position;
		float distance = direction.magnitude;

		if(distance >= area) {
			return true;
		} else {
			return false;
		}
	}

	public void GetActions() {
		//flee.setBool(false);
		flee.enabled = false;
		pathFollowing.enabled = true;
		pathFollowing.setCurrent(2);
	}

	public State GetTargetState() {
		return new Patrolling();
	}
}
