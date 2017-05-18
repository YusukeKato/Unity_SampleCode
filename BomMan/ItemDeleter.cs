using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeleter : MonoBehaviour {

	GameObject player1;
	GameObject player2;
	GameObject imageGenerator;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("Player1");
		player2 = GameObject.Find ("Player2");
		imageGenerator = GameObject.Find ("ImageGenerator");
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Sqrt (Mathf.Pow (player1.transform.position.x - transform.position.x, 2.0f) + Mathf.Pow (player1.transform.position.y - transform.position.y, 2.0f)) < 0.5f ) {
			imageGenerator.GetComponent<ImageGenerator> ().bombItemCount1 += 1;
			if (imageGenerator.GetComponent<ImageGenerator> ().bombItemCount1 >= 1) {
				imageGenerator.GetComponent<ImageGenerator> ().bombRange11 += 0.5f;
				imageGenerator.GetComponent<ImageGenerator> ().bombRange12 += 1;
				imageGenerator.GetComponent<ImageGenerator> ().bombItemCount1 = 0;
			}
			GameObject.Destroy (gameObject);
		}
		if (Mathf.Sqrt (Mathf.Pow (player2.transform.position.x - transform.position.x, 2.0f) + Mathf.Pow (player2.transform.position.y - transform.position.y, 2.0f)) < 0.5f) {
			imageGenerator.GetComponent<ImageGenerator> ().bombItemCount2 += 1;
			if (imageGenerator.GetComponent<ImageGenerator> ().bombItemCount2 >= 1) {
				imageGenerator.GetComponent<ImageGenerator> ().bombRange21 += 0.5f;
				imageGenerator.GetComponent<ImageGenerator> ().bombRange22 += 1;
				imageGenerator.GetComponent<ImageGenerator> ().bombItemCount2 = 0;
			}
			GameObject.Destroy (gameObject);
		}
	}
}
