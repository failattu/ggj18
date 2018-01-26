﻿using System.Collections;
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
    public Vector3 pos = Vector3.zero;
    public GameObject obj;

    public Beacon()
    { }

    public Beacon(GameObject obj, Vector3 pos)
    {
        this.pos = pos;
        this.obj = obj;
    }
}