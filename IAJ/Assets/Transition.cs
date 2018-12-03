using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Transition {

	bool IsTriggered();

	State GetTargetState();

	void GetActions();
	
}
