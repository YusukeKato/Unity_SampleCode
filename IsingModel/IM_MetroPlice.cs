/* Metro Polis */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IM_MetroPlice : MonoBehaviour
{

    float T = 10.0f;    /* degree */
    const float J = 1.0f;    /* int */
    const float H = 0.5f;    /* magnetic field */
    const int range = 100;
    int[,] field = new int[range, range];

    public GameObject plate;
    GameObject[,] goList = new GameObject[range, range];

    Color red = new Color(1.0f, 0.3f, 0.3f, 1.0f);
    Color blue = new Color(0.3f, 0.3f, 1.0f, 1.0f);
    Color gray = new Color(0.6f, 0.6f, 0.6f, 1.0f);

    public Text dgreeText;


    // Use this for initialization
    void Start()
    {
        init();
        create_field();
        dgreeText.text = "メトロポリス法    " + "温度 : " + T.ToString() + "    磁場 : " + H.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        for (int k = 0; k < 2 * range * range; k++)
        {
            int i = Random.Range(1, range - 1);
            int j = Random.Range(1, range - 1);
            field[i, j] = judge(i, j);
            if (field[i, j] == 1)
            {
                goList[i, j].GetComponent<Renderer>().material.color = red;
            }
            else if (field[i, j] == -1)
            {
                goList[i, j].GetComponent<Renderer>().material.color = blue;
            }
            if (i == 0 || i == range - 1 || j == 0 || j == range - 1)
            {
                goList[i, j].GetComponent<Renderer>().material.color = gray;
            }
        }
    }

    int judge(int i, int j)
    {
        float Ev = (-1.0f * -field[i, j] * (field[i, j + 1] + field[i, j - 1] + field[i + 1, j] + field[i - 1, j]) + (-field[i, j]) * H);
        float Eu = (-1.0f * field[i, j] * (field[i, j + 1] + field[i, j - 1] + field[i + 1, j] + field[i - 1, j]) + field[i, j] * H);
        float dE = Ev - Eu;
        //float E = (-1.0f * J * -1.0f * field[i, j] * (field[i, j + 1] + field[i, j - 1] + field[i + 1, j] + field[i - 1, j]) + 1.0f * H);
        int result = 0;
        if (dE < 0)
        {
            result = -field[i, j];
        }
        else
        {
            float ran = Random.Range(0.0f, 1.0f);
            //double KB = 1.38064852 * Mathf.Pow(10, -23);
            //float P = Mathf.Exp(-dE / ((float)KB * T));
            float P = Mathf.Exp(((-dE) / T));
            if (P > ran)
            {
                result = -field[i, j];
            }
            else
            {
                result = field[i, j];
            }
        }
        return result;
    }

    void create_field()
    {
        for (int i = 0; i < range; i++)
        {
            for (int j = 0; j < range; j++)
            {
                GameObject go = (GameObject)Instantiate(plate);
                go.transform.position = new Vector3((float)i / 10.0f - 5.0f, (float)j / 10.0f - 4.0f, 0);
                go.name = i.ToString() + j.ToString();
                goList[i, j] = go;
                if (field[i, j] == 1)
                {
                    goList[i, j].GetComponent<Renderer>().material.color = red;
                }
                else if (field[i, j] == -1)
                {
                    goList[i, j].GetComponent<Renderer>().material.color = blue;
                }
                if (i == 0 || i == range - 1 || j == 0 || j == range - 1)
                {
                    goList[i, j].GetComponent<Renderer>().material.color = gray;
                }
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

    public void UpButton()
    {
        T += 0.5f;
        dgreeText.text = "メトロポリス法    " + "温度 : " + T.ToString() + "    磁場 : " + H.ToString();
    }

    public void DownButton ()
    {
        T -= 0.5f;
        dgreeText.text = "メトロポリス法    " + "温度 : " + T.ToString() + "    磁場 : " + H.ToString();
    }
}
