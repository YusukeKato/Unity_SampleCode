using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject Camera;

    private float span_2 = 0.2f;
    private float delta_2 = 0;
    private float step_2 = 1.0f;

    // Use this for initialization
    void Start()
    {
        Camera.transform.position = new Vector3(0, 10, -30);
        Camera.transform.Rotate(30, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        delta_2 += Time.deltaTime;
        if (delta_2 > span_2)
        {
            delta_2 = 0;
            Camera.transform.Translate(0, step_2, -step_2);
        }
    }
}
