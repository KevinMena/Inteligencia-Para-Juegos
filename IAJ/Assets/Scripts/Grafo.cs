using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grafo {

	public int n;
	public float[,] matrizAdyacencia;
	public Vertice[] nodos;

	public List<Lado> lados;

	public Grafo(int num) {
		this.n = num;
		this.matrizAdyacencia = new float[num,num];
		this.nodos = new Vertice[num];
		lados = new List<Lado>();
	}

	public void agregarArco(int vi, int vf) {
		matrizAdyacencia[vi, vf] = 1;
		Vertice v1 = nodos[vi];
		Vertice v2 = nodos[vf];
		float costoNuevoLado = distanciaEuclidiana(v1, v2);
		//Debug.Log(costoNuevoLado);
		Lado l = new Lado(v1, v2, costoNuevoLado);
		lados.Add(l);
	}

	public Lado getLado(int vi, int vf) {
		Vertice v1 = nodos[vi];
		Vertice v2 = nodos[vf];
		Lado l = new Lado(v1, v2, 0f);
		
		for(int k = 0; k < lados.Count; k++) {
			if(lados[k].getInicial().getNum() == v1.getNum() && lados[k].getFinal().getNum() == v2.getNum()) {
				l = lados[k];
				//Debug.Log("Lado: " + l.getInicial().getNum() + "-" + l.getFinal().getNum());
				break;
			}
		}

		return l;
	}

	float distanciaEuclidiana(Vertice v1, Vertice v2){
		Vector3 pos1 = v1.getPos();
		Vector3 pos2 = v2.getPos();
		float p1 = pos2.x - pos1.x;
		p1 = Mathf.Pow(p1, 2);
		float p2 = pos2.z - pos1.z;
		p2 = Mathf.Pow(p2, 2);

		return Mathf.Sqrt(p1 + p2);

	}
}
