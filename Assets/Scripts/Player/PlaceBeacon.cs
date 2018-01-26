using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBeacon : MonoBehaviour 
{
    Ray ray;
    RaycastHit hit;
    public LayerMask layers;
    public GameObject beaconPrefab;

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
            {
                Instantiate(beaconPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
