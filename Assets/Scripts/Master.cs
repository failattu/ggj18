using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public int beaconPrice;
    public int startingMoney;
    public int players;

    //[System.NonSerialized]
    public int[] money;
    //[System.NonSerialized]
    public int[] income;
    //[System.NonSerialized]
    public int[] upkeep;
    //[System.NonSerialized]
    public float[] popularity;

    public List<BeaconData>[] beacons;

    public float incomeFrequency = 5f;
    private float incomeCountdown;

	//TextMeshProSHIIT
	public TextMeshProUGUI clients_text;
	public TextMeshProUGUI income_text;
	public TextMeshProUGUI upkeep_text;
	public TextMeshProUGUI my_money_text;

    private void Awake()
    {
        money = new int[players];
        income = new int[players];
        upkeep = new int[players];
        popularity = new float[players];
	
        master = this;
        beacons = new List<BeaconData>[players];
        for(int i = 0; i < players; i++)
        {
            beacons[i] = new List<BeaconData>();
            money[i] = startingMoney;
            upkeep[i] = 0;
            popularity[i] = 0;
        }
		my_money_text.text = startingMoney.ToString();
    }

    private void Start()
    {
        incomeCountdown = incomeFrequency;
    }

    int beaconIncome = 0;
    private void Update()
    {

        incomeCountdown -= Time.deltaTime;
        if (incomeCountdown <= 0)
        {
            incomeCountdown = incomeFrequency;
            for (int i = 0; i < players; i++)
            {
                income[i] = 0;

                for (int j = 0; j < beacons[i].Count; j++)
                {
                    income[i] += beacons[i][j].script.Income();
                }

                money[i] += income[i] - upkeep[i];
            }
			my_money_text.text = money [0].ToString() + " mk";
			upkeep_text.text = upkeep [0].ToString () + " mk";
			income_text.text = income [0].ToString () + " mk";
			clients_text.text = "0";
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
			my_money_text.text = money[0].ToString() + " mk";
            return true;
        }
        else
            return false;
    }
}
