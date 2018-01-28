using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainSceneUI : MonoBehaviour 
{
    public TextMeshProUGUI[] companyName;
    public TextMeshProUGUI companyNameLogo;

    private void Start()
    {
        companyName[0].text = DoNotDeleteData.Instance.companyName[0];
        companyName[1].text = DoNotDeleteData.Instance.companyName[1];

        companyNameLogo.text = DoNotDeleteData.Instance.companyName[0] + " " + DoNotDeleteData.Instance.companyName[1];
    }
}
