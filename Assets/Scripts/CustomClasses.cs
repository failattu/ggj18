using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomClasses : MonoBehaviour 
{
	
}

public class Beacon : MonoBehaviour
{
    public int level = 1;
    public int range = 1;
    public int income = 100;
    public Vector3 position = Vector3.zero;

    public Beacon()
    { }

    public Beacon(Vector3 pos)
    {
        position = pos;
    }
}
