using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDeleter : MonoBehaviour {

	GameObject imageGenerator;

	// Use this for initialization
	void Start () {
		imageGenerator = GameObject.Find ("ImageGenerator");
	}
	
	// Update is called once per frame
	void Update () {
		if (imageGenerator.GetComponent<ImageGenerator> ().flagBombAt) {
			for (int j = 0; j < imageGenerator.GetComponent<ImageGenerator> ().AtCount; j++) {
				if (Mathf.Sqrt (Mathf.Pow(transform.position.x-imageGenerator.GetComponent<ImageGenerator>().bombAtPosi[j,0],2.0f) + Mathf.Pow(transform.position.y-imageGenerator.GetComponent<ImageGenerator>().bombAtPosi[j,1],2.0f)) < 0.5f) {
					GameObject.Destroy (gameObject);
				}
			}
		}
	}
}
