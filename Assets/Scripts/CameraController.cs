using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform targetPoint;

    void LateUpdate()
    {
        transform.position = targetPoint.position;
 
    }
}
