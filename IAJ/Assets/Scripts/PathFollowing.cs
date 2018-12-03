using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : Seek {

	Path path;

	Grafo g;
    int currentNode = 2;

	int previousNode = 0;
	Vertice nodoIni;

	Arrive arrive;

	public GameObject GrafoManager;

	void Start() {
		Init();
	}

	public override void Init() {
		base.Init();
		g = GrafoManager.GetComponent<AEstrella>().GetGrafo();
		arrive = transform.GetComponent<Arrive> ();
		arrive.Init();
	}

	public bool setPath(int final) {

		Vertice point = g.nodos[final];
		//Debug.Log("Henry " + transform.position);
		//Debug.Log("Puntos " + point.getPos());

		Vector3 dis = transform.position - point.getPos();
		float d = dis.magnitude;
		if(d <= 0.1f) {
			currentNode = 2;
			//Debug.Log("Entre");
			return true;
		}

		if(previousNode != final) {
			nodoIni = SaberNodo(transform.position);
			//Debug.Log("HenryPos " + nodoIni.getPos());
			path = GrafoManager.GetComponent<AEstrella>().hacerAEstrella(g, nodoIni, point);
			previousNode = final;
		}

		return false;
	}

	public void setCurrent(int nuevo) {
		currentNode = nuevo;
	}

	private float distance(GameObject a, Vector3 b) {
        return Mathf.Sqrt((a.transform.position.x - b.x) * (a.transform.position.x - b.x) + (a.transform.position.z - b.z) * (a.transform.position.z - b.z));
    }

	public Vertice SaberNodo(Vector3 pos) {
		for(int i = 0; i < g.n; i++) {
			Vector3[] puntosNodo = g.nodos[i].getPuntos();
			if(PointInTriangle(pos, puntosNodo[0], puntosNodo[1], puntosNodo[2])) {
				return g.nodos[i];
			}
		}

		return null;
	}

	bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b) {
		Vector3 cp1 = Vector3.Cross(b-a, p1-a);
		Vector3 cp2 = Vector3.Cross(b-a, p2-a);

		if (Vector3.Dot(cp1, cp2) >= 0) {
			return true;
		} else {
			return false;
		}
	}
    

	bool PointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c) {
		if (SameSide(p,a, b,c) && SameSide(p,b, a,c) && SameSide(p,c, a,b)) {
			return true;
		}else {
			return false;
		}
	}



	public override Steering getSteering() {
		Vector3 mTarget = new Vector3();
		
		if(Input.GetButtonDown("Fire1")) {
			nodoIni = SaberNodo(transform.position);
			Vector3 cameraPoint = Vector3.zero;
			RaycastHit hit;
                
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				cameraPoint = hit.point;
			}
			Vertice point = SaberNodo(cameraPoint);
			path = GrafoManager.GetComponent<AEstrella>().hacerAEstrella(g, nodoIni, point);
			currentNode = 1;
		}

		if (path != null) {
			List<Vector3> nodes = path.getNodes();

			mTarget = nodes[currentNode];

			if (distance(character, mTarget) <= 1f) {
				currentNode += 1;

				if (currentNode == nodes.Count) {
					currentNode -= 1;
					//path = null;

					arrive.target.transform.position = mTarget;
					return arrive.getSteering();
				}
			}

			target.transform.position = mTarget;
			return base.getSteering();
		}
		

        return new Steering();
	}
	
	void Update() {
		agente.UpdateInfo(getSteering());
	}
}
