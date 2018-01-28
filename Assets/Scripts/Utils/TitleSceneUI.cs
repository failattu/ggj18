using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneUI : MonoBehaviour 
{
    public GameObject nameCompanyPanel;
    public TextMeshProUGUI[] companyNameTMP;

	public void BtnConnect()
    {
        nameCompanyPanel.SetActive(true);
    }

    public void BtnStartGame()
    {
        DoNotDeleteData.Instance.companyName[0] = companyNameTMP[0].text.ToUpper();
        DoNotDeleteData.Instance.companyName[1] = companyNameTMP[1].text.ToUpper();

        SceneManager.LoadScene("MainScene");
    }
}
