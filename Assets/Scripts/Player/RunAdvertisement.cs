using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAdvertisement : MonoBehaviour 
{
    private Master master;

    public GameObject plane;

    public float cooldown;
    public float[] cdCounters;

    private void Start()
    {
        master = Master.master;
        cdCounters = new float[2];
    }

    public void RunCampaign(int player)
    {
        if(cdCounters[player] < 0)
        {
            master.Pay(player, master.campaignPrice);
            master.popularity[player] = Mathf.Clamp(master.popularity[player] + 0.2f, 0f, 1f);
            cdCounters[player] = cooldown;
            GameObject tempPlane = Instantiate(plane);
            tempPlane.GetComponent<AdvertPlane>().Init(player);
        }
    }

    private void Update()
    {
        cdCounters[0] -= Time.deltaTime;
        cdCounters[1] -= Time.deltaTime;
    }
}
