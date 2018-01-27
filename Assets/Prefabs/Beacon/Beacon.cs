using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    public Master master;
    public int level = 1;
    public int range = 1;
    public int income = 100;
    public int baseUpgradeCost = 100;
    public Vector3 pos = Vector3.zero;
    public GameObject obj;
    public Transform radius;
    public float incomeFrequency = 5f;
    private float incomeCountdown;

    public void Init(GameObject obj, Vector3 pos)
    {
        master = Master.master;

        this.pos = pos;
        this.obj = obj;
        foreach (Transform child in obj.transform)
        {
            if (child.name.Equals("Radius"))
            {
                radius = child;
                break;
            }
        }

        incomeCountdown = incomeFrequency;
    }

    public void Upgrade(int player)
    {
        if(master.Pay(player, baseUpgradeCost * level))
        {
            level++;
            range++;
            radius.localScale = new Vector3(range, 0, range);
        }
    }

    public int IncomeUpdate()
    {
        incomeCountdown -= Time.deltaTime;
        if (incomeCountdown <= 0)
        {
            incomeCountdown = incomeFrequency;
            return income;
        }
        else
            return 0;
    }
}
