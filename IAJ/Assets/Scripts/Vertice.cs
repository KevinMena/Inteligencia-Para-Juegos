using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertice {

	int numNodo;
	Vector3 posNodo;

	Vector3[] verticesT;
	float costoHastaAhora;
	float costoEstimado;

	public Vertice(int n, Vector3 p, Vector3[] vertices) {
		this.numNodo = n;
		this.posNodo = p;
		this.verticesT = vertices;
		costoHastaAhora = float.PositiveInfinity;
		costoEstimado = float.PositiveInfinity;
	}

	public Vector3 getPos() {
		return posNodo;
	}

	public int getNum() {
		return numNodo;
	}

	public float getCosto() {
		return costoHastaAhora;
	}
	public float getEstimado() {
		return costoEstimado;
	}

	public Vector3[] getPuntos() {
		return verticesT;
	}

	public void setCosto(float costo) {
		costoHastaAhora = costo;
	}

	public void setEstimado(float costo) {
		costoEstimado = costo;
	}

	public void PintarTriangulo() {
		Debug.DrawLine(verticesT[0] + new Vector3(0, -0.5f, 0), verticesT[1] + new Vector3(0, -0.5f, 0), Color.blue, 30);
		Debug.DrawLine(verticesT[1] + new Vector3(0, -0.5f, 0), verticesT[2] + new Vector3(0, -0.5f, 0), Color.blue, 30);
		Debug.DrawLine(verticesT[2] + new Vector3(0, -0.5f, 0), verticesT[0] + new Vector3(0, -0.5f, 0), Color.blue, 30);
	}
}
