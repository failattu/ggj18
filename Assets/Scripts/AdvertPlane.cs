using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdvertPlane : MonoBehaviour 
{
    public float speed;
    public Vector3 spawnOffsetFromCamera;
    private float startPosX;
    private Master master;

    public TextMeshProUGUI[] flagTMPs;
    public GameObject[] flagImageObjs;
    public Image enemyFlagImg;

    private void Start()
    {
        master = Master.master;
        transform.parent = Camera.main.transform;
        transform.localPosition = spawnOffsetFromCamera;
    }

    private void Update()
    {
        if (transform.localPosition.x > 450f)
            Destroy(gameObject);
        else
            transform.position += Vector3.right * Time.deltaTime * speed;
    }

    public void Init(int player)
    {
        switch(player)
        {
            case 0:
                flagImageObjs[0].SetActive(true);
                flagImageObjs[1].SetActive(false);
                flagTMPs[0].text = DoNotDeleteData.Instance.companyName[0];
                flagTMPs[1].text = DoNotDeleteData.Instance.companyName[1];
                break;
            case 1:
                flagImageObjs[0].SetActive(false);
                flagImageObjs[1].SetActive(true);
                enemyFlagImg.sprite = DoNotDeleteData.Instance.enemyInfo.sprite;
                break;
        }
    }
}
