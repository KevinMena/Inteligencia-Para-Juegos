using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align {
    protected GameObject faceTarget;

    void Start() {
        Init();
    }

    public override void Init() {
        base.Init();
        faceTarget = target;
        target = new GameObject();
        target.AddComponent<InfoAgente> ();
    }

    public override Steering getSteering() {
		Vector3 direction = faceTarget.transform.position - gameObject.transform.position;
        if(direction.magnitude > 0.0f) {
           this.GetComponent<InfoAgente>().orientacion = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        }


        return base.getSteering();
	}

	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
