using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBeacon : MonoBehaviour 
{
    private Master master;
    private MainSceneUI mainSceneUI;

    public LayerMask layers;
    public LayerMask beaconLayer;
    public LayerMask beaconRadiusLayer;
    public LayerMask buildingLayer;
    public GameObject beaconPrefab;
    public Transform beaconGhost;
    public Material[] beaconGhostMaterials;
	public AudioClip runEffectPlayer;
	public AudioClip runEffectCompetitor;
	public AudioSource source;
	public float AudioVolume;

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
        mainSceneUI = FindObjectOfType<MainSceneUI>();
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

        //Invoke("LateStart", 5f);
    }

    private void LateStart()
    {
        for (int i = 0; i < 20; i++)
        {
            SpawnBeacon(1, new Vector3(Random.Range(-90f, 90f), 5f, Random.Range(-90f, 90f)));
        }
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
        pos.y = 5;

        master.beaconPrice += 250;
        mainSceneUI.ChangeBeaconPrice(master.beaconPrice);
		if (player == 0) {
			source.PlayOneShot(runEffectPlayer,AudioVolume);
		} else {
			source.PlayOneShot(runEffectCompetitor,AudioVolume);
		}
        tempBeacon = Instantiate(beaconPrefab, pos, Quaternion.identity) as GameObject;
        tempBeacon.name = beaconPrefab.name + " " + master.beacons[player].Count;
        tempBeacon.transform.parent = beaconParent[player].transform;
        tempBeaconScript = tempBeacon.GetComponent<Beacon>();
        tempBeaconScript.Init(player, tempBeacon, pos);
        master.beacons[player].Add(new BeaconData(tempBeacon, tempBeaconScript));

        Collider[][] buildingsPlayer = new Collider[master.beacons[0].Count][];
        List<Collider> buildingsPlayerTotal = new List<Collider>();
        Collider[][] buildingsAI = new Collider[master.beacons[1].Count][];
        List<Collider> buildingsAITotal = new List<Collider>();
        List<Collider> buildingsShared = new List<Collider>();

        if (master.beacons[0].Count > 0)
        {
            for (int i = 0; i < buildingsPlayer.Length; i++)
            {
                buildingsPlayer[i] = Physics.OverlapSphere(master.beacons[0][i].obj.transform.position, baseRange, buildingLayer);
                //print(i + " tower: " + buildingsPlayer[i].Length);
            }

            buildingsPlayerTotal.AddRange(buildingsPlayer[0]);
            for (int i = 1; i < buildingsPlayer.Length; i++)
            {
                for(int j = 0; j < buildingsPlayer[i].Length; j++)
                {
                    if (!buildingsPlayerTotal.Contains(buildingsPlayer[i][j]))
                        buildingsPlayerTotal.Add(buildingsPlayer[i][j]);
                }
            }

            //print(buildingsPlayerTotal.Count);
        }

        if (master.beacons[1].Count > 0)
        {
            for (int i = 0; i < buildingsAI.Length; i++)
            {
                buildingsAI[i] = Physics.OverlapSphere(master.beacons[1][i].obj.transform.position, baseRange, buildingLayer);
                //print(i + " tower: " + buildingsAI[i].Length);
            }

            buildingsAITotal.AddRange(buildingsAI[0]);
            for (int i = 1; i < buildingsAI.Length; i++)
            {
                for (int j = 0; j < buildingsAI[i].Length; j++)
                {
                    if (!buildingsAITotal.Contains(buildingsAI[i][j]))
                        buildingsAITotal.Add(buildingsAI[i][j]);
                }
            }

            //print(buildingsAITotal.Count);
        }

        if(buildingsPlayerTotal.Count > 0 && buildingsAITotal.Count > 0)
        {
            for(int i = 0; i < buildingsPlayerTotal.Count; i++)
            {
                for(int j = 0; j < buildingsAITotal.Count; j++)
                {
                    if(buildingsPlayerTotal[i] == buildingsAITotal[j])
                    {
                        buildingsShared.Add(buildingsPlayerTotal[i]);
                    }
                }
            }

            for(int i = 0; i < buildingsShared.Count; i++)
            {
                buildingsPlayerTotal.Remove(buildingsShared[i]);
                buildingsAITotal.Remove(buildingsShared[i]);
            }

            //print("Player: " + buildingsPlayerTotal.Count + " | AI: " + buildingsAITotal.Count + " | Shared: " + buildingsShared.Count);
        }

        float totalSize = 0;
        if(buildingsPlayerTotal.Count > 0)
        {
            for(int i = 0; i < buildingsPlayerTotal.Count; i++)
            {
                totalSize += (buildingsPlayerTotal[i].bounds.size.x * buildingsPlayerTotal[i].bounds.size.y * buildingsPlayerTotal[i].bounds.size.z) / 10f;
            }

            master.customers[0] = Mathf.RoundToInt(totalSize);
        }

        totalSize = 0;
        if (buildingsAITotal.Count > 0)
        {
            for (int i = 0; i < buildingsAITotal.Count; i++)
            {
                totalSize += (buildingsAITotal[i].bounds.size.x * buildingsAITotal[i].bounds.size.y * buildingsAITotal[i].bounds.size.z) / 10f;
            }

            master.customers[1] = Mathf.RoundToInt(totalSize);
        }

        totalSize = 0;
        if (buildingsShared.Count > 0)
        {
            for (int i = 0; i < buildingsShared.Count; i++)
            {
                totalSize += (buildingsShared[i].bounds.size.x * buildingsShared[i].bounds.size.y * buildingsShared[i].bounds.size.z) / 10f;
            }

            master.customersShared = Mathf.RoundToInt(totalSize);
        }

        master.UpdateIncome();

        //float totalSize = 0;
        //otherBeacons = Physics.OverlapSphere(hit.point, baseRange, beaconRadiusLayer);
        //print("OtherBeacons: " + otherBeacons.Length);
        //if(otherBeacons.Length > 0)
        //{
        //    CalculateNewPopulation();
        //}
        //else
        //{
        //    Collider[] buildings = Physics.OverlapSphere(hit.point, baseRange, buildingLayer);

        //    //print(buildings.Length);
        //    if (buildings.Length > 0)
        //    {
        //        for (int i = 0; i < buildings.Length; i++)
        //        {
        //            totalSize += (buildings[i].bounds.size.x * buildings[i].bounds.size.y * buildings[i].bounds.size.z) * buildings.Length / 100f;
        //        }
        //    }
        //}

        //tempBeacon = Instantiate(beaconPrefab, pos, Quaternion.identity) as GameObject;
        //tempBeacon.name = beaconPrefab.name + " " + master.beacons[player].Count;
        //tempBeacon.transform.parent = beaconParent[player].transform;
        //tempBeaconScript = tempBeacon.GetComponent<Beacon>();
        //tempBeaconScript.Init(player, tempBeacon, hit.point, Mathf.RoundToInt(totalSize));
        //master.beacons[player].Add(new BeaconData(tempBeacon, tempBeaconScript));
        //master.upkeep[player] += beaconUpkeep;

        //print("Total people inside this radius: " + Mathf.RoundToInt(totalSize));
    }

    public void CalculateNewPopulation()
    {
        
    }
}