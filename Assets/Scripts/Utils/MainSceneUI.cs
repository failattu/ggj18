using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using TMPro;

public class MainSceneUI : MonoBehaviour 
{
    public TextMeshProUGUI companyName;
    public TextMeshProUGUI[] companyNameLogo;
    public TextMeshProUGUI enemyCompanyName;
    public Image enemyCompanyLogo;
	public BasicMap basemap;


	void Awake(){
		basemap.location = DoNotDeleteData.Instance.city;
	}
    private void Start()
    {
        companyNameLogo[0].text = DoNotDeleteData.Instance.companyName[0];
        companyNameLogo[1].text = DoNotDeleteData.Instance.companyName[1];

        companyName.text = DoNotDeleteData.Instance.companyName[0] + " " + DoNotDeleteData.Instance.companyName[1];
        enemyCompanyName.text = DoNotDeleteData.Instance.enemyInfo.name.ToUpper();
        enemyCompanyLogo.sprite = DoNotDeleteData.Instance.enemyInfo.sprite;
    }
}
