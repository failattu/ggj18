using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    public Master master;
    public int level = 1;
    public int range = 1;
    public int affectedPeople;
    public int affectedPeopleShared;
    public int baseUpgradeCost = 100;
    public Vector3 pos = Vector3.zero;
    public GameObject obj;
    public Transform radius;

    public int player;

    public void Init(int player, GameObject obj, Vector3 pos, int affectedPeople)
    {
        master = Master.master;

        this.player = player;
        this.pos = pos;
        this.obj = obj;
        this.affectedPeople = affectedPeople;
        foreach (Transform child in obj.transform)
        {
            if (child.name.Equals("Radius"))
            {
                radius = child;
                break;
            }
        }
    }

    public void Upgrade()
    {
        if(master.Pay(player, baseUpgradeCost * level))
        {
            level++;
            range++;
            radius.localScale = new Vector3(range, 0, range);
        }
    }

    public int Income()
    {
        return Mathf.RoundToInt((affectedPeople + (affectedPeopleShared * master.popularity[player])) / 1000) * 10;
    }
}
