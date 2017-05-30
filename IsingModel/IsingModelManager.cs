using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsingModelManager : MonoBehaviour {

    float T = -0.5f;    /* degree */
    const float J = 1.0f;    /* int */
    const float H = 0.1f;    /* magnetic field */
    const int range = 100;
    int[,] field = new int[range, range];

    public GameObject plate;
    GameObject[,] goList = new GameObject[range, range];

    Color red = new Color(1.0f, 0.3f, 0.3f, 1.0f);
    Color blue = new Color(0.3f, 0.3f, 1.0f, 1.0f);
    Color gray = new Color(0.6f, 0.6f, 0.6f, 1.0f);

    public Text dgreeText;


	// Use this for initialization
	void Start () {
        init();
        create_field();
        dgreeText.text = "温度 : " + T.ToString() + "    磁場 : " + H.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 1; i < range - 1; i++)
        {
            for(int j = 1; j < range - 1; j++)
            {
                field[i, j] = judge(i, j);
                if(field[i, j] == 1)
                {
                    goList[i, j].GetComponent<Renderer>().material.color = red;
                }
                else if(field[i, j] == -1)
                {
                    goList[i, j].GetComponent<Renderer>().material.color = blue;
                }
                if(i == 0 || i == range-1 || j == 0 || j == range-1)
                {
                    goList[i, j].GetComponent<Renderer>().material.color = gray;
                }
            }
        }
	}

    int judge(int i, int j)
    {
        float E = (-2.0f * J * (field[i, j + 1] + field[i, j - 1] + field[i + 1, j] + field[i - 1, j]) + 2.0f * H) / T;
        float P = 1 / (1 + Mathf.Exp(E));
        float ran2 = Random.Range(0, 1.0f);
        int result = 0;
        if (P >= ran2)
        {
            result = 1;
        }
        if (P < ran2)
        {
            result = -1;
        }

        return result;
    }

    void create_field()
    {
        for(int i = 0; i < range; i++)
        {
            for(int j = 0; j < range; j++)
            {
                GameObject go = (GameObject)Instantiate(plate);
                go.transform.position = new Vector3((float)i/10.0f - 5.0f, (float)j/10.0f - 4.0f, 0);
                go.name = i.ToString() + j.ToString();
                if (field[i, j] == 1)
                {
                    go.GetComponent<Renderer>().material.color = red;
                }
                else if (field[i, j] == -1)
                {
                    go.GetComponent<Renderer>().material.color = blue;
                }
                if (i == 0 || i == range - 1 || j == 0 || j == range - 1)
                {
                    go.GetComponent<Renderer>().material.color = gray;
                }
                goList[i, j] = go;
            }
        }
    }

    void init()
    {
        for (int i = 0; i < range; i++)
        {
            for (int j = 0; j < range; j++)
            {
                int ran = Random.Range(0, 2);
                if (ran == 0)
                {
                    field[i, j] = -1;
                }
                else if (ran == 1)
                {
                    field[i, j] = 1;
                }
            }
        }
    }
}
