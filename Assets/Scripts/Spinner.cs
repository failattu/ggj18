using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    x,
    y,
    z
}

public class Spinner : MonoBehaviour 
{
    public Axis axis;
    public float rotationVelocity;

    private void Update()
    {
        switch(axis)
        {
            case Axis.x:
                transform.Rotate(rotationVelocity, 0, 0, Space.Self);
                break;
            case Axis.y:
                transform.Rotate(0, rotationVelocity, 0, Space.Self);
                break;
            case Axis.z:
                transform.Rotate(0, 0, rotationVelocity, Space.Self);
                break;
        }
    }
}
