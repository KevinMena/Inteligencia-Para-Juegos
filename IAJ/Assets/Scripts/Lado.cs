using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lado {

	Vertice verticeInicial;
	Vertice verticeFinal;
	float costoHastaAhora;


	public Lado(Vertice vi, Vertice vf, float costo) {
		verticeInicial = vi;
		verticeFinal = vf;
		costoHastaAhora = costo;
	}

	public Vertice getInicial() {
		return verticeInicial;
	}

	public Vertice getFinal() {
		return verticeFinal;
	}
	public float getCosto() {
		return costoHastaAhora;
	}
}
