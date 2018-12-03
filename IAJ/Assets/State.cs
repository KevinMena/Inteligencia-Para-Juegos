using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State {

	void GetActions();
	
	//List<Behaviours> GetEntryActions();

	//List<Behaviours> GetExitActions();

	List<Transition> GetTransitions();
}
