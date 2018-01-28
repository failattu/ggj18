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
    public RunAdvertisement runAdvertisement;
    private float advetisementCooldown;

    int nextTarget = -1;

    private void Start()
    {
        master = Master.master;
        placeBeacon = FindObjectOfType<PlaceBeacon>();
        runAdvertisement = FindObjectOfType<RunAdvertisement>();
    }

    bool beaconingTime = false;
    Collider[] buildings;
    float totalSize;
    int tries = 0;
    private void Update()
    {
        if(nextTarget == 0)
        {
            if(master.money[1] > master.beaconPrice)
            {
                tries++;
                Vector3 pos = new Vector3(Random.Range(xMin, xMax), 5, Random.Range(zMin, zMax));
                bool tooClose = false;
                for (int i = 0; i < master.beacons[1].Count; i++)
                {
                    if (Vector3.Distance(pos, master.beacons[1][i].obj.transform.position) < 75)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    buildings = Physics.OverlapSphere(pos, placeBeacon.baseRange, placeBeacon.buildingLayer);

                    totalSize = 0;
                    for (int i = 0; i < buildings.Length; i++)
                        totalSize += (buildings[i].bounds.size.x * buildings[i].bounds.size.y * buildings[i].bounds.size.z) / 10f;

                    if (totalSize > 5000)
                    {
                        master.Pay(1, master.beaconPrice);
                        placeBeacon.SpawnBeacon(1, pos);
                        nextTarget = -1;
                        tries = 0;
                    }
                }

                if(tries > 60)
                {
                    nextTarget = -1;
                }
            }
        }
        else if(nextTarget == 1)
        {
            if(master.money[1] > master.campaignPrice && runAdvertisement.cdCounter < 0)
            {
                //master.Pay(1, master.campaignPrice);
                runAdvertisement.RunCampaign(1);
                nextTarget = -1;
            }
        }
        else
        {
            if (master.beacons[1].Count > 5 && master.popularity[1] < 0.75 && Random.value > 0.5f && runAdvertisement.cdCounter < 0)
            {
                nextTarget = 1;
            }
            else
            {
                nextTarget = 0;
            }
        }


        //if(master.money[1] > 3500)
        //{
        //    //print(master.beacons[1].Count + " | " + master.popularity[1] + " | " + runAdvertisement.cdCounters[1]);
        //    if(beaconingTime)
        //    {
        //        Vector3 pos = new Vector3(Random.Range(xMin, xMax), 5, Random.Range(zMin, zMax));
        //        bool tooClose = false;
        //        for(int i = 0; i < master.beacons[1].Count; i++)
        //        {
        //            if(Vector3.Distance(pos, master.beacons[1][i].obj.transform.position) < 100)
        //            {
        //                tooClose = true;
        //                break;
        //            }
        //        }

        //        if(!tooClose)
        //        {
        //            buildings = Physics.OverlapSphere(pos, placeBeacon.baseRange, placeBeacon.buildingLayer);

        //            totalSize = 0;
        //            for (int i = 0; i < buildings.Length; i++)
        //                totalSize += (buildings[i].bounds.size.x * buildings[i].bounds.size.y * buildings[i].bounds.size.z) / 10f;

        //            if (totalSize > 5000)
        //            {
        //                master.Pay(1, 3500);
        //                placeBeacon.SpawnBeacon(1, pos);
        //                beaconingTime = false;
        //            }
        //        }
        //    }
        //    else if(master.beacons[1].Count > 5 && master.popularity[1] < 0.75 && Random.value > 0.5f && runAdvertisement.cdCounters[1] < 0)
        //    {
        //        //print("Advertising!");
        //        master.Pay(1, 3500);
        //        runAdvertisement.RunCampaign(1);
        //    }
        //    else
        //    {
        //        beaconingTime = true;
        //    }
        //}
    }
}
