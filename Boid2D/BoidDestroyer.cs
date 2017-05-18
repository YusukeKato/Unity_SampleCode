using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidDestroyer : MonoBehaviour {

    private float span = 0.03f;
    private float delta = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        delta += Time.deltaTime;
        if(delta > span)
        {
            GameObject.Destroy(gameObject);
        }
	}
}
