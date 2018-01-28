using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using TMPro;

public class MainSceneUI : MonoBehaviour 
{
    private Master master;
    private RunAdvertisement advertisement;

    [Header("Company info")]
    public TextMeshProUGUI companyName;
    public TextMeshProUGUI[] companyNameLogo;
    public TextMeshProUGUI enemyCompanyName;
    public Image enemyCompanyLogo;
	public BasicMap basemap;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip beaconAvailable;
    public AudioClip advertisementAvailable;

	void Awake(){
		basemap.location = DoNotDeleteData.Instance.city;
	}

    [Header("UI Buttons")]
    public Button btnBeacon;
    public Button btnMarketing;
    public TextMeshProUGUI beaconPrice;

    private void Start()
    {
        master = Master.master;
        advertisement = FindObjectOfType<RunAdvertisement>();

        companyNameLogo[0].text = DoNotDeleteData.Instance.companyName[0];
        companyNameLogo[1].text = DoNotDeleteData.Instance.companyName[1];

        companyName.text = DoNotDeleteData.Instance.companyName[0] + " " + DoNotDeleteData.Instance.companyName[1];
        enemyCompanyName.text = DoNotDeleteData.Instance.enemyInfo.name.ToUpper();
        enemyCompanyLogo.sprite = DoNotDeleteData.Instance.enemyInfo.sprite;
    }

    bool audioPlayedBeacon = true;
    bool audioPlayedAdvertisement = true;
    private void Update()
    {
        if(master.money[0] < master.beaconPrice)
        {
            audioPlayedBeacon = false;
            btnBeacon.interactable = false;
        }
        else
        {
            if(!audioPlayedBeacon)
            {
                audioPlayedBeacon = true;
                audioSource.PlayOneShot(beaconAvailable, 1f);
            }
            btnBeacon.interactable = true;
        }

        if(master.money[0] < master.campaignPrice || advertisement.cdCounter > 0)
        {
            //print("wut");
            audioPlayedAdvertisement = false;
            btnMarketing.interactable = false;
        }
        else
        {
            if(!audioPlayedAdvertisement)
            {
                audioPlayedAdvertisement = true;
                audioSource.PlayOneShot(advertisementAvailable, 1f);
            }
            btnMarketing.interactable = true;
        }
    }

    public void ChangeBeaconPrice(int newPrice)
    {
        beaconPrice.text = newPrice.ToString() + " mk";
    }
}
