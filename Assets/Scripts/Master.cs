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

    public int startingMoney;
    public int players;

    //[System.NonSerialized]
    public int[] money;
    //[System.NonSerialized]
    public int[] income;
    //[System.NonSerialized]
    public int[] upkeep;
    //[System.NonSerialized]
    public int[] popularity;

    public List<BeaconData>[] beacons;

    private void Awake()
    {
        money = new int[players];
        income = new int[players];
        upkeep = new int[players];
        popularity = new int[players];

        master = this;
        beacons = new List<BeaconData>[players];
        for(int i = 0; i < players; i++)
        {
            beacons[i] = new List<BeaconData>();
            money[i] = startingMoney;
            upkeep[i] = 0;
            popularity[i] = 0;
        }
    }

    private void Start()
    {

    }

    int beaconIncome = 0;
    private void Update()
    {
        for(int i = 0; i < players; i++)
        {
            for (int j = 0; j < beacons[i].Count; j++)
            {
                money[i] += beacons[i][j].script.IncomeUpdate();

            }

            money[i] -= upkeep[i];
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
