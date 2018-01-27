using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBeacon : MonoBehaviour 
{
    private Master master;

    public LayerMask layers;
    public LayerMask beaconLayer;
    public GameObject beaconPrefab;
    public Transform beaconGhost;
    public Material[] beaconGhostMaterials;

    GameObject beaconParent;
    Vector3 hidePos = new Vector3(0, -100f, 0);
    MeshRenderer beaconGhostMesh;
    bool canBePlaced = false;

    [System.NonSerialized]
    public bool placeBeacon;

    private void Start()
    {
        master = Master.master;
        beaconParent = new GameObject();
        beaconParent.name = "Beacons";
        beaconGhost = Instantiate(beaconGhost, new Vector3(0, -100f, 0), Quaternion.identity) as Transform;
        beaconGhostMesh = beaconGhost.GetChild(0).GetComponent<MeshRenderer>();
    }

    Ray ray;
    RaycastHit hit;
    GameObject tempBeacon;
    Beacon tempBeaconScript;
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0) && canBePlaced && master.Pay(500))
        {
            tempBeacon = Instantiate(beaconPrefab, hit.point, Quaternion.identity) as GameObject;
            tempBeacon.name = beaconPrefab.name + " " + master.beacons.Count;
            tempBeacon.transform.parent = beaconParent.transform;
            tempBeaconScript = tempBeacon.GetComponent<Beacon>();
            tempBeaconScript.Init(tempBeacon, hit.point);
            master.beacons.Add(new BeaconData(tempBeacon, tempBeaconScript));

            //if(Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
            //{
            //    if(Physics.OverlapSphere(hit.point, 1f, beaconLayer).Length > 0)
            //        Debug.Log("Popup! NOT SO CLOSE!");
            //    else if(master.Pay(500))
            //    {
            //        tempBeacon = Instantiate(beaconPrefab, hit.point, Quaternion.identity) as GameObject;
            //        tempBeacon.name = beaconPrefab.name + " " + master.beacons.Count;
            //        tempBeacon.transform.parent = beaconParent.transform;
            //        tempBeaconScript = tempBeacon.GetComponent<Beacon>();
            //        tempBeaconScript.Init(tempBeacon, hit.point);
            //        master.beacons.Add(new BeaconData(tempBeacon, tempBeaconScript));
            //    }
            //}
        }

        if(placeBeacon)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
            {
                beaconGhost.position = hit.point;

                if (Physics.OverlapSphere(hit.point, 1f, beaconLayer).Length > 0)
                {
                    canBePlaced = false;
                    beaconGhostMesh.material = beaconGhostMaterials[0];
                }
                else
                {
                    canBePlaced = true;
                    beaconGhostMesh.material = beaconGhostMaterials[1];
                }
            }
            else
            {
                canBePlaced = false;
                beaconGhost.position = hidePos;
            }
        }
    }
    
    public void TogglePlaceBeacon()
    {
        placeBeacon = !placeBeacon;

        if(!placeBeacon)
        {
            beaconGhost.position = hidePos;
            canBePlaced = false;
        }
    }
}