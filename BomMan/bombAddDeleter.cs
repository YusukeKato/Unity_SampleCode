using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombAddDeleter : MonoBehaviour {
	GameObject imageGenerator;

	// Use this for initialization
	void Start () {
		imageGenerator = GameObject.Find ("ImageGenerator");
		Invoke ("AddDeleteFunc", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AddDeleteFunc() {
		imageGenerator.GetComponent<ImageGenerator> ().flagBombAt = true;//BoxDeleterに知らせる
		GameObject.Destroy (gameObject);
	}
}
