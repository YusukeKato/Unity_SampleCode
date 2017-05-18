using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombDeleter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("deleteFunc", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void deleteFunc() {
		GameObject.Destroy (gameObject);
	}
}
