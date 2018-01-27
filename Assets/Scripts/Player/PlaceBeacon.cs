using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBeacon : MonoBehaviour 
{
    private Master master;

    public LayerMask layers;
    public LayerMask beaconLayer;
    public GameObject beaconPrefab;

    GameObject beaconParent;

    private void Start()
    {
        master = Master.master;
        beaconParent = new GameObject();
        beaconParent.name = "Beacons";
    }

    Ray ray;
    RaycastHit hit;
    GameObject tempBeacon;
    Beacon tempBeaconScript;
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
                    tempBeacon = Instantiate(beaconPrefab, hit.point, Quaternion.identity) as GameObject;
                    tempBeacon.name = beaconPrefab.name + " " + master.beacons.Count;
                    tempBeacon.transform.parent = beaconParent.transform;
                    tempBeaconScript = tempBeacon.GetComponent<Beacon>();
                    tempBeaconScript.Init(tempBeacon, hit.point);
                    master.beacons.Add(new BeaconData(tempBeacon, tempBeaconScript));
                }
            }
        }
    }
}