using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSeeEvilClone : Transition {

	float area = 3f;
	GameObject evilClone;

	GameObject henry;

	PathFollowing pathFollowing;

	public CanSeeEvilClone() {
		henry = GameObject.FindGameObjectWithTag("Player");
		evilClone = GameObject.FindGameObjectWithTag("Enemy");
		pathFollowing = henry.GetComponent<PathFollowing>();
	}

	public bool IsTriggered() {
		Vector3 direction = henry.transform.position - evilClone.transform.position;
		float distance = direction.magnitude;

		if(distance <= area) {
			return true;
		} else {
			return false;
		}
	}

	public void GetActions() {
		//pathFollowing.setPath(-1);
		pathFollowing.enabled = false;
	}

	public State GetTargetState() {
		return new RunFrom();
	}
	
}
