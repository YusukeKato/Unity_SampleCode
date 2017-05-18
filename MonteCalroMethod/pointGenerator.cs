using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointGenerator : MonoBehaviour {

	public GameObject pointIn;
	public GameObject pointOut;

	const int TIMES = 100000;
	const float range = 1.0f;

	public Text text;

	// Use this for initialization
	void Start () {
		int InCount = 0;
		int OutCount = 0;
		for (int j = 0; j < TIMES; j++) {
			float x = Random.Range (-range, range);
			float y = Random.Range (-range, range);
			if (Mathf.Sqrt (Mathf.Pow (x, 2.0f) + Mathf.Pow (y, 2.0f)) <= range) {
				InCount++;
				GameObject go = (GameObject)Instantiate (pointIn);
				go.transform.position = new Vector3 (x, y, 0);
			} else {
				OutCount++;
				GameObject go2 = (GameObject)Instantiate (pointOut);
				go2.transform.position = new Vector3 (x, y, 0);
			}
		}
		float pi = 4.0f * (float)InCount / ((float)InCount + (float)OutCount);
		text.text = "点数 : " + TIMES.ToString() + "\n" + "円周率 : " + pi.ToString () + "\n" + "InCount : " + InCount.ToString () + "\n" + "OutCount : " + OutCount.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
