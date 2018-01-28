using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMovement : MonoBehaviour 
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.z = Mathf.Clamp(pos.z, zMin, zMax);

        transform.position = pos;
    }
}
