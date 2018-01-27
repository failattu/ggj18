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

    public int money = 0;

    public List<BeaconData> beacons;

    private void Awake()
    {
        master = this;
        beacons = new List<BeaconData>();
    }

    private void Start()
    {

    }

    int beaconIncome = 0;
    private void Update()
    {
        for(int i = 0; i < beacons.Count; i++)
        {
            money += beacons[i].script.IncomeUpdate();
        }
    }

    public void Income(int amount)
    {
        money += amount;
    }

    public bool Pay(int cost)
    {
        if(money > cost)
        {
            money -= cost;
            return true;
        }
        else
            return false;
    }
}
