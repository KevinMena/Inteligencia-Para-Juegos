using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYoureGoing : Align {

    void Start() {
        Init();
    }

    public override void Init() {
		base.Init();
	}

    public override Steering getSteering() {
        if(agente.velocidad.magnitude > 0.0f) {
           this.GetComponent<InfoAgente>().orientacion = Mathf.Atan2(agente.velocidad.x, agente.velocidad.z) * Mathf.Rad2Deg;

           return base.getSteering();
        }

        return new Steering();
	}
    
	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
