using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconData
{
    public GameObject obj;
    public Beacon script;

    public BeaconData(GameObject obj, Beacon script)
    {
        this.obj = obj;
        this.script = script;
    }
}

public class Master : MonoBehaviour 
{
    public static Master master; //lel

    public int[] money;

    public List<BeaconData>[] beacons;

    private void Awake()
    {
        master = this;
        beacons = new List<BeaconData>[4];
        for(int i = 0; i < beacons.Length; i++)
        {
            beacons[i] = new List<BeaconData>();
        }
    }

    private void Start()
    {

    }

    int beaconIncome = 0;
    private void Update()
    {
        for(int i = 0; i < beacons.Length; i++)
        {
            for (int j = 0; j < beacons[i].Count; j++)
            {
                money[i] += beacons[i][j].script.IncomeUpdate();
            }
        }
    }

    public void Income(int player, int amount)
    {
        money[player] += amount;
    }

    public bool Pay(int player, int cost)
    {
        if(money[player] > cost)
        {
            money[player] -= cost;
            return true;
        }
        else
            return false;
    }
}
