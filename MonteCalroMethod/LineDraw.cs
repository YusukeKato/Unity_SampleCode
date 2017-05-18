using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour {

	GameObject Line1;
	GameObject Line2;
	GameObject Line3;
	GameObject Line4;
	LineRenderer LineRen1;
	LineRenderer LineRen2;
	LineRenderer LineRen3;
	LineRenderer LineRen4;

	// Use this for initialization
	void Start () {
		Line1 = GameObject.Find ("Line1");
		Line2 = GameObject.Find ("Line2");
		Line3 = GameObject.Find ("Line3");
		Line4 = GameObject.Find ("Line4");
		LineRen1 = Line1.GetComponent<LineRenderer> ();
		LineRen2 = Line2.GetComponent<LineRenderer> ();
		LineRen3 = Line3.GetComponent<LineRenderer> ();
		LineRen4 = Line4.GetComponent<LineRenderer> ();
		LineRen1.SetWidth (0.1f, 0.1f);
		LineRen1.SetVertexCount (5);
		LineRen1.SetPosition (0, new Vector3 (-1.0f, 1.0f, 0));
		LineRen1.SetPosition (1, new Vector3 (1.0f, 1.0f, 0));
		LineRen1.SetPosition (2, new Vector3 (1.0f, -1.0f, 0));
		LineRen1.SetPosition (3, new Vector3 (-1.0f, -1.0f, 0));
		LineRen1.SetPosition (4, new Vector3 (-1.0f, 1.0f, 0));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
