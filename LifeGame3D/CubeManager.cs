using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{

    public GameObject CubePrefab;
    private float span = 0.2f;
    private float delta = 0;
    private float span2 = 0.6f;
    private float delta2 = 0;
    private const int MAX = 50;
    private bool[,] field = new bool[MAX, MAX];
    private float height = 0;
    private int times = 10;

    // Use this for initialization
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta >= span)
        {
            delta = 0;
            height += 1.0f;
            judge();
            create_field();
        }
        delta2 += Time.deltaTime;
        if (delta2 >= span2)
        {
            delta2 = 0;
            set_point();
        }
    }

    private void set_point()
    {
        int rx = Random.Range(1, MAX-1);
        int ry = Random.Range(1, MAX-1);
        field[rx - 1, ry - 1] = false; field[rx + 0, ry - 1] = true; field[rx + 1, ry - 1] = true;
        field[rx - 1, ry + 0] = true;  field[rx + 0, ry + 0] = true; field[rx + 1, ry + 0] = false;
        field[rx - 1, ry + 1] = false; field[rx + 0, ry + 1] = true; field[rx + 1, ry + 1] = false;
    }

    private void create_field()
    {
        for (int i = 0; i < MAX; i++)
        {
            for (int j = 0; j < MAX; j++)
            {
                if (field[i, j] == true)
                {
                    GameObject go = (GameObject)Instantiate(CubePrefab);
                    go.transform.position = new Vector3((float)i, height, (float)j);
                    //float r = Random.Range(0, 1.0f);
                    //float g = Random.Range(0, 1.0f);
                    //float b = Random.Range(0, 1.0f);
                    float r = 0.5f;
                    float g = 0.8f;
                    float b = 0.6f;
                    float a = 0.2f;
                    go.GetComponent<MeshRenderer>().material.color = new Color(r, g, b, a);
                }
            }
        }
    }

    private void judge()
    {
        for (int i = 1; i < MAX - 1; i++)
        {
            for (int j = 1; j < MAX - 1; j++)
            {
                /* 判定 */
                int count = 0;
                for (int m = -1; m <= 1; m++)
                {
                    for (int n = -1; n <= 1; n++)
                    {
                        if (field[i + m, j + n] == true)
                        {
                            count++;
                        }
                    }
                }
                if (field[i, j] == true && (count >= 2 && count <= 3))
                {
                    /* 生存 */
                }
                else if (field[i, j] == false && count == 3)
                {
                    field[i, j] = true;
                }
                else
                {
                    field[i, j] = false;
                }
            }
        }
    }

    private void init()
    {
        /* 初期化 */
        for (int i = 0; i < MAX; i++)
        {
            for (int j = 0; j < MAX; j++)
            {
                field[i, j] = false;
            }
        }
        /* 初期配置 */
        for(int k = 0; k < times; k++)
        {
            set_point();
        }
    }


}
