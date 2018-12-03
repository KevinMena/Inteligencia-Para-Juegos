using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunFrom : State {

	Flee flee;

	DSeeEvilClone dsevc;
	GameObject henry;

	public RunFrom() {
		dsevc = new DSeeEvilClone();
		henry = GameObject.FindGameObjectWithTag("Player");

		flee = henry.GetComponent<Flee>();
	}

	public void GetActions() {
		flee.enabled = true;
	}

	public List<Transition> GetTransitions() {
		List<Transition> listaTransiciones =  new List<Transition>();

		listaTransiciones.Add(dsevc);

		return listaTransiciones;
	}
}
