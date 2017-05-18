using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{

    public GameObject BoidPrefab;
    const int NUM = 30;
    Vector3[] BoidPosi = new Vector3[NUM];
    Vector3[] BoidPosi_prv = new Vector3[NUM];
    float[,] BoidVel = new float[NUM, 2];
    const float MAX_SPEED = 0.1f;
    private int count = 0;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        mouse();
    }

    void move()
    {
        for (int i = 0; i < NUM; i++)
        {
            /* 移動のルール */
            rule1(i);
            rule2(i);
            rule3(i);

            /* 速度の上限 */
            if (BoidVel[i, 0] >= MAX_SPEED)
            {
                float r = (float)MAX_SPEED / BoidVel[i, 0];
                BoidVel[i, 0] *= r;
            }
            if (BoidVel[i, 1] >= MAX_SPEED)
            {
                float r = (float)MAX_SPEED / BoidVel[i, 1];
                BoidVel[i, 1] *= r;
            }

            /* 壁の外 */
            if (BoidPosi[i].x > 9.0f || BoidPosi[i].x < -9.0f) BoidVel[i, 0] *= -1.0f;
            if (BoidPosi[i].y > 5.0f || BoidPosi[i].y < -5.0f) BoidVel[i, 1] *= -1.0f;

            /* 移動 */
            BoidPosi[i].x += BoidVel[i, 0];
            BoidPosi[i].y += BoidVel[i, 1];

            /* 描写 */
            GameObject go = (GameObject)Instantiate(BoidPrefab);
            go.transform.position = BoidPosi[i];
            go.name = "Boid" + i.ToString();

            /* 向き */
            var vec = (new Vector3(BoidPosi[i].x + BoidVel[i, 0], BoidPosi[i].y + BoidVel[i, 1], 0) - go.transform.position).normalized;
            var angle = (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) - 90.0f;
            go.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        }
    }

    void rule1(int index)
    {
        float cx = 0;
        float cy = 0;
        for (int i = 0; i < NUM; i++)
        {
            if (i != index)
            {
                cx += BoidPosi[i].x;
                cy += BoidPosi[i].y;
            }
        }
        cx /= NUM - 1;
        cy /= NUM - 1;
        BoidVel[index, 0] += (cx - BoidPosi[index].x) / 8000;
        BoidVel[index, 1] += (cy - BoidPosi[index].y) / 8000;
    }

    void rule2(int index)
    {
        for (int i = 0; i < NUM; i++)
        {
            if (i != index)
            {
                float d = (BoidPosi[i] - BoidPosi[index]).magnitude;
                if (d < 0.2f)
                {
                    BoidVel[index, 0] -= (BoidPosi[i].x - BoidPosi[index].x) / 10;
                    BoidVel[index, 1] -= (BoidPosi[i].y - BoidPosi[index].y) / 10;
                }
            }
        }
    }

    void rule3(int index)
    {
        float pvx = 0;
        float pvy = 0;
        for (int i = 0; i < NUM; i++)
        {
            if (i != index)
            {
                pvx += BoidVel[i, 0];
                pvy += BoidVel[i, 1];
            }
        }
        pvx /= NUM - 1;
        pvy /= NUM - 1;
        BoidVel[index, 0] += (pvx - BoidVel[index, 0]) / 64;
        BoidVel[index, 1] += (pvy - BoidVel[index, 1]) / 64;
    }

    void mouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse");
        }
    }

    void Init()
    {
        for (int i = 0; i < NUM; i++)
        {
            GameObject go = (GameObject)Instantiate(BoidPrefab);
            float r = 4.0f;
            float x = Random.Range(-r, r);
            float y = Random.Range(-r, r);
            go.transform.position = new Vector3(x, y, 0);
            BoidPosi[i] = go.transform.position;
            BoidVel[i, 0] = 0;
            BoidVel[i, 1] = 0;
        }
    }
}
