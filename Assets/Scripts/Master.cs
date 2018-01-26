using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour 
{
    public static Master master; //lel

    public List<Beacon> beacons;

    private void Awake()
    {
        master = this;
        beacons = new List<Beacon>();
    }

    private void Update()
    {
        
    }
}
