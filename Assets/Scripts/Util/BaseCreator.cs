using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreator : MonoBehaviour
{
    public GameObject plane;
    public GameObject cube;
    void Start()
    {



        GameObject instantiatedPlane = Instantiate(plane, new Vector3(50, 0, 25), Quaternion.identity);

        for (float x = 0.5f; x < 100; x++)
        {
            for (float z = 0.5f; z < 50; z++)
            {
                GameObject go = Instantiate(cube, new Vector3(x, Random.Range(-0.5f,0.5f), z), Quaternion.identity);
                go.transform.parent = instantiatedPlane.transform;
            }
        }

    }

}
