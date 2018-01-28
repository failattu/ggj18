﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("UI Buttons")]
    public Button btnBeacon;
    public Button btnMarketing;

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

    private void Update()
    {
        if(master.money[0] < master.beaconPrice)
        {
            btnBeacon.interactable = false;
        }
        else
        {
            btnBeacon.interactable = true;
        }

        if(master.money[0] < master.campaignPrice || advertisement.cdCounter > 0)
        {
            btnMarketing.interactable = false;
        }
        else
        {
            btnMarketing.interactable = true;
        }
    }
}
