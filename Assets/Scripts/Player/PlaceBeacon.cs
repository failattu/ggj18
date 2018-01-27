using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBeacon : MonoBehaviour 
{
    private Master master;

    public int beaconUpkeep;

    public LayerMask layers;
    public LayerMask beaconLayer;
    public LayerMask beaconRadiusLayer;
    public LayerMask buildingLayer;
    public GameObject beaconPrefab;
    public Transform beaconGhost;
    public Material[] beaconGhostMaterials;

    GameObject[] beaconParent;
    Vector3 hidePos = new Vector3(0, -100f, 0);
    MeshRenderer beaconGhostMesh;
    bool canBePlaced = false;

    [System.NonSerialized]
    public bool placeBeacon;

    public float baseRange;

    private void Start()
    {
        master = Master.master;
        GameObject beaconParentParent = new GameObject();
        beaconParentParent.name = "Beacons";
        beaconParent = new GameObject[4];
        for (int i = 0; i < beaconParent.Length; i++)
        {
            beaconParent[i] = new GameObject();
            beaconParent[i].transform.parent = beaconParentParent.transform;
            beaconParent[i].name = "Player " + i;
        }
        beaconGhost = Instantiate(beaconGhost, new Vector3(0, -100f, 0), Quaternion.identity) as Transform;
        beaconGhostMesh = beaconGhost.GetChild(0).GetComponent<MeshRenderer>();
    }

    Ray ray;
    RaycastHit hit;
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0) && canBePlaced && master.Pay(0, master.beaconPrice))
        {
            SpawnBeacon(0, beaconGhost.position);
            TogglePlaceBeacon();
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

    GameObject tempBeacon;
    Beacon tempBeaconScript;
    //Collider[] buildings;
    Collider[] otherBeacons;
    public void SpawnBeacon(int player, Vector3 pos)
    {
        float totalSize = 0;
        otherBeacons = Physics.OverlapSphere(hit.point, baseRange, beaconRadiusLayer);
        if(otherBeacons.Length > 0)
        {
            CalculateNewPopulation(otherBeacons);
        }
        else
        {
            Collider[] buildings = Physics.OverlapSphere(hit.point, baseRange, buildingLayer);
            
            //print(buildings.Length);
            if (buildings.Length > 0)
            {
                for (int i = 0; i < buildings.Length; i++)
                {
                    totalSize += (buildings[i].bounds.size.x * buildings[i].bounds.size.y * buildings[i].bounds.size.z) * buildings.Length / 100f;
                }
            }
        }

        tempBeacon = Instantiate(beaconPrefab, pos, Quaternion.identity) as GameObject;
        tempBeacon.name = beaconPrefab.name + " " + master.beacons[player].Count;
        tempBeacon.transform.parent = beaconParent[player].transform;
        tempBeaconScript = tempBeacon.GetComponent<Beacon>();
        tempBeaconScript.Init(player, tempBeacon, hit.point, Mathf.RoundToInt(totalSize));
        master.beacons[player].Add(new BeaconData(tempBeacon, tempBeaconScript));
        master.upkeep[player] += beaconUpkeep;

        //print("Total people inside this radius: " + Mathf.RoundToInt(totalSize));
    }

    public void CalculateNewPopulation(Collider[] beacons)
    {
        Collider[][] buildings = new Collider[beacons.Length][];
        Collider[] buildingsTemp;

        //for (int i = 0; i < beacons.Length; i++)
        //    buildingsTemp[i] = Physics.OverlapSphere(hit.point, baseRange, buildingLayer);


    }
}