using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

	Patrolling initialState;
	RunFrom rf;
	List<State> states = new List<State>();

	State currentState;
	
	void Start() {

		initialState = new Patrolling();
		rf = new RunFrom();
		states.Add(initialState);
		states.Add(rf);

		currentState = initialState;
	}

	void Update() {
		Transition triggeredTransition = null;

		foreach(Transition t in currentState.GetTransitions()) {
			if(t.IsTriggered()) {
				triggeredTransition = t;
				break;
			}
		}

		if(triggeredTransition != null) {
			State targetState = triggeredTransition.GetTargetState();

			triggeredTransition.GetActions();

			currentState = targetState; 
		}

		currentState.GetActions();
	}
}
