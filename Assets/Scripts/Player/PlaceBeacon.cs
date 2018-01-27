using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBeacon : MonoBehaviour 
{
    private Master master;

    public LayerMask layers;
    public LayerMask beaconLayer;
    public LayerMask buildingLayer;
    public GameObject beaconPrefab;
    public Transform beaconGhost;
    public Material[] beaconGhostMaterials;

    GameObject beaconParent;
    Vector3 hidePos = new Vector3(0, -100f, 0);
    MeshRenderer beaconGhostMesh;
    bool canBePlaced = false;

    [System.NonSerialized]
    public bool placeBeacon;

    public float baseRange;

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
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0) && canBePlaced && master.Pay(500))
        {
            SpawnBeacon();
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

    public void SpawnBeacon()
    {
        SpawnBeacon(beaconGhost.position);
    }

    GameObject tempBeacon;
    Beacon tempBeaconScript;
    Collider[] buildings;
    public void SpawnBeacon(Vector3 pos)
    {
        tempBeacon = Instantiate(beaconPrefab, pos, Quaternion.identity) as GameObject;
        tempBeacon.name = beaconPrefab.name + " " + master.beacons.Count;
        tempBeacon.transform.parent = beaconParent.transform;
        tempBeaconScript = tempBeacon.GetComponent<Beacon>();
        tempBeaconScript.Init(tempBeacon, hit.point);
        master.beacons.Add(new BeaconData(tempBeacon, tempBeaconScript));

        buildings = Physics.OverlapSphere(hit.point, baseRange, buildingLayer);
        float totalSize = 0;
        print(buildings.Length);
        if (buildings.Length > 0)
        {
            for (int i = 0; i < buildings.Length; i++)
            {
                totalSize += (buildings[i].bounds.size.x * buildings[i].bounds.size.y * buildings[i].bounds.size.z) * buildings.Length;
            }
        }

        print("Total people inside this radius: " + Mathf.RoundToInt(totalSize));
    }
}