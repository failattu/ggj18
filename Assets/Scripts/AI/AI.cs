using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour 
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;

    public Master master;
    public PlaceBeacon placeBeacon;

    private void Start()
    {
        master = Master.master;
        placeBeacon = FindObjectOfType<PlaceBeacon>();
    }

    bool beaconingTime = false;
    Collider[] buildings;
    float totalSize;
    private void Update()
    {
        if(master.money[1] > 3500)
        {
            if(beaconingTime)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax), 5, Random.Range(zMin, zMax));
                bool tooClose = false;
                for(int i = 0; i < master.beacons[1].Count; i++)
                {
                    if(Vector3.Distance(pos, master.beacons[1][i].obj.transform.position) < 100)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if(!tooClose)
                {
                    buildings = Physics.OverlapSphere(pos, placeBeacon.baseRange, placeBeacon.buildingLayer);

                    totalSize = 0;
                    for (int i = 0; i < buildings.Length; i++)
                        totalSize += (buildings[i].bounds.size.x * buildings[i].bounds.size.y * buildings[i].bounds.size.z) / 10f;

                    if (totalSize > 5000)
                    {
                        master.Pay(1, 3500);
                        placeBeacon.SpawnBeacon(1, pos);
                    }
                }
            }
            else if(master.beacons[1].Count > 5 && master.popularity[1] < 0.75 && Random.value > 0.5f)
            {
                //master.Pay(1, 3500);
                //Launch marketing campaing
            }
            else
            {
                beaconingTime = true;
            }
        }
    }
}
