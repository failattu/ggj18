using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunAdvertisement : MonoBehaviour 
{
    private Master master;

    public GameObject plane;

    public float cooldown;
    public float cdCounter;
	public AudioClip runEffectPlayer;
	public AudioClip runEffectCompetitor;
	public AudioSource source;
	public float AudioVolume;

    //public float[] cdCounters;

    public Button marketingButton;

    private void Start()
    {
        master = Master.master;
        //cdCounters = new float[2];
    }

    public void RunCampaign(int player)
    {
        //if(cdCounters[player] < 0)
        if(cdCounter < 0 && master.Pay(player, master.campaignPrice))
        {
			if (player == 0) {
				source.PlayOneShot(runEffectPlayer,AudioVolume);
			} else {
				source.PlayOneShot(runEffectCompetitor,AudioVolume);
			}
            master.popularity[player] = Mathf.Clamp(master.popularity[player] + 0.2f, 0f, 1f);
            //cdCounters[player] = cooldown;
            cdCounter = cooldown;
            GameObject tempPlane = Instantiate(plane);
            tempPlane.GetComponent<AdvertPlane>().Init(player);
            master.UpdateIncome();
        }
    }

    private void Update()
    {
        //cdCounters[0] -= Time.deltaTime;
        //cdCounters[1] -= Time.deltaTime;
        cdCounter -= Time.deltaTime;
    }
}
