using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEstrella : MonoBehaviour {

	Grafo g;
	public GameObject prefab;

	public GameObject camino;

	void Start() {
		crearGrafo();
	
	}

	public Grafo GetGrafo() {
		return g;
	}

	Grafo ObtenerTriangulos() {
		Grafo newGrafo = new Grafo(transform.childCount * 2);
		
		// Numero que llevara asociado el vertice 
		int i = 0;

		foreach(Transform child in transform ) {
			Mesh meshChild = child.GetComponent<MeshFilter>().mesh;
			Vector3[] vertices = meshChild.vertices;

			List<Vector3> lista = new List<Vector3>();

			for(int k = 0; k < vertices.Length; k ++) {
				if (vertices[k].y >= 0.5f) {
					if(!lista.Contains(vertices[k])) {
						lista.Add(vertices[k]);
					}
				}
			}

			for(int j = 0; j < lista.Count - 2; j ++) {

				Vector3[] puntos = new Vector3[3];
				puntos[0] = child.transform.position + lista[j];
				puntos[1] = child.transform.position + lista[j+1];
				puntos[2] = child.transform.position + lista[j+2];

				Vector3 center = (puntos[0] + puntos[1] + puntos[2]) / 3;
				
				Vertice newV = new Vertice(i, center, puntos);
				newGrafo.nodos[i] = newV;

				i++;
			}

		}

		return newGrafo;
	}

	public void crearGrafo() {

		g = ObtenerTriangulos();

		for(int k =0; k < g.n; k++) {
			g.nodos[k].PintarTriangulo();
			//Vector3 pos = g.nodos[k].getPos();
			//Instantiate(prefab, pos + new Vector3(0, 0.5f, 0), Quaternion.identity);
		}

		for(int i = 0; i < g.n; i++) {
			Vector3[] nodoC = g.nodos[i].getPuntos();
			for(int j = 0; j < g.n; j++) {
				Vector3[] nodoA = g.nodos[j].getPuntos();

				if( (nodoC[0] == nodoA[1] && nodoC[1] == nodoA[2]) || (nodoC[1] == nodoA[0] && nodoC[2] == nodoA[1]) || (nodoC[0] == nodoA[0] && nodoC[2] == nodoA[2]) ) {
					g.agregarArco(g.nodos[i].getNum(), g.nodos[j].getNum());
					Debug.DrawLine(g.nodos[i].getPos(), g.nodos[j].getPos(), Color.red, 30);
				} 
			}
		}

 	}

	void dibujarGrafo(Grafo g) {

		for(int i =0; i < g.n; i++) {
			for(int j =0; j < g.n; j++) {
				if(g.matrizAdyacencia[i,j] == 1) {
					Vector3 pos1 = g.nodos[i].getPos();
					Vector3 pos2 = g.nodos[j].getPos();
					Debug.DrawLine(pos1, pos2, Color.red);
				}
			}
		}
	}

	

	public float distanciaEuclidiana(Vertice v1, Vertice v2){
		Vector3 pos1 = v1.getPos();
		Vector3 pos2 = v2.getPos();
		/*float p1 = pos2.x - pos1.x;
		p1 = Mathf.Pow(p1, 2);
		float p2 = pos2.z - pos1.z;
		p2 = Mathf.Pow(p2, 2);
		
		return Mathf.Sqrt(p1 + p2);*/

		return Vector3.Distance(pos1, pos2);

	}

	public Vertice extraerMin(Hashtable cola, Grafo g) {
		float min = float.PositiveInfinity;
 		Vertice k = new Vertice(0, Vector3.zero, new Vector3[3]);
		
		foreach(DictionaryEntry a in cola) {
			if(min >= (float) a.Value) {
				min = (float) a.Value;
				k = g.nodos[(int) a.Key];
			}
		}

		return k;
	}

	public Path hacerAEstrella(Grafo g, Vertice vi, Vertice vf) {
		vi.setCosto(0f);
		vi.setEstimado(distanciaEuclidiana(vi, vf));

		Path path = new Path();
		Hashtable abiertos = new Hashtable();
		abiertos.Add(vi.getNum(), vi.getEstimado());
		Hashtable cerrados = new Hashtable();
		int[] preds = new int[g.n]; 

		Vertice current =  new Vertice(0, Vector3.zero, new Vector3[3]);

		while(abiertos.Count > 0) {
			current = extraerMin(abiertos, g);
			
			//Debug.Log("Current: " + current.getNum());

			if(current == vf) {
				break;
			}

			abiertos.Remove(current.getNum());
			cerrados.Add(current.getNum(), current.getEstimado());

			List<Lado> conexiones = new List<Lado>();
			for(int i = 0; i < g.n; i++) {
				//Debug.Log("Prueba: " + current.getNum() + "-" + i);
				if(g.matrizAdyacencia[current.getNum(), i] == 1) {
					//Debug.Log("Entre: " + current.getNum() + "-" + i);
					Lado l = g.getLado(current.getNum(), i);
					//Debug.Log("Conexion: " + l.getInicial().getNum() + "-" + l.getFinal().getNum());
					conexiones.Add(l);
				}
			}

			foreach(Lado l in conexiones) {
				Vertice nodoFinal = l.getFinal();
				//Debug.Log("Lado actual: " + l.getInicial().getNum() + "-" + l.getFinal().getNum());

				if(cerrados.ContainsKey(nodoFinal.getNum())) {
					continue;
				}

				float nodoFinalCostoTemt = g.nodos[current.getNum()].getCosto() + l.getCosto();
				

				if(!abiertos.ContainsKey(nodoFinal.getNum())) {
					abiertos.Add(nodoFinal.getNum(), nodoFinal.getEstimado());
				} else if (nodoFinalCostoTemt >= nodoFinal.getCosto()) {
					continue;
				}
				preds[nodoFinal.getNum()] = l.getInicial().getNum();
				nodoFinal.setCosto(nodoFinalCostoTemt);
				nodoFinal.setEstimado(nodoFinal.getCosto() + distanciaEuclidiana(nodoFinal, vf));

			}

		}

		if(current != vf) {
			Debug.Log("No hay camino");
		} else {
			List<int> pathNum = new List<int>();
			int currentNode = current.getNum();
			pathNum.Add(currentNode);
			path.addNode(current.getPos());
			Instantiate(camino, current.getPos() + new Vector3(0, 0.5f, 0), Quaternion.identity);
			while(currentNode != vi.getNum()) {
				pathNum.Add(preds[currentNode]);
				path.addNode(g.nodos[preds[currentNode]].getPos());
				Instantiate(camino, g.nodos[preds[currentNode]].getPos() + new Vector3(0, 0.5f, 0), Quaternion.identity);
				currentNode = preds[currentNode];
			}
			pathNum.Reverse();
			path.nodes.Reverse();
		}

		return path;
	}
}
