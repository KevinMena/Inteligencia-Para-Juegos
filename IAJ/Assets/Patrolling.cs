using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : State {


	PathFollowing pathFollowing;

	CanSeeEvilClone canSeeEvilClone;

	GameObject henry;

	Steering steering;

	int currentPoint;

	int[] puntosFinales = {42, 171, 221, 377, 428, 103, 324};

	public Patrolling() {
		henry = GameObject.FindGameObjectWithTag("Player");
		currentPoint = 0;
		canSeeEvilClone = new CanSeeEvilClone();
		pathFollowing = henry.GetComponent<PathFollowing>();
	}

	public void GetActions() {

		if(currentPoint == 7) {
			currentPoint = 0;
		}
		//Debug.Log(puntosFinales[currentPoint]);
		bool cambio = pathFollowing.setPath(puntosFinales[currentPoint]);
		if(cambio == true) {
			currentPoint++;
		}
	}

	public List<Transition> GetTransitions() {
		List<Transition> listaTransiciones =  new List<Transition>();

		listaTransiciones.Add(canSeeEvilClone);

		return listaTransiciones;
	}
}
