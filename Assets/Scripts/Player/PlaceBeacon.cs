using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBeacon : MonoBehaviour 
{
    private Master master;

    public LayerMask layers;
    public LayerMask beaconLayer;
    public GameObject beaconPrefab;

    private void Start()
    {
        master = Master.master;
    }

    Ray ray;
    RaycastHit hit;
    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
            {
                if(Physics.OverlapSphere(hit.point, 1f, beaconLayer).Length > 0)
                    Debug.Log("Popup! NOT SO CLOSE!");
                else
                {
                    Instantiate(beaconPrefab, hit.point, Quaternion.identity);
                    master.beacons.Add(new Beacon(hit.point));
                }
            }
        }
    }
}