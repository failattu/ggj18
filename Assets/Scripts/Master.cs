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

    [Header("Settings")]
    public int beaconPrice;
    public int campaignPrice;
    public int beaconUpkeep;
    public int startingMoney;
    public int players;
    public float incomeFrequency = 5f;

    [Header("Data")]
    //[System.NonSerialized]
    public int[] money;
    //[System.NonSerialized]
    public int[] income;
    //[System.NonSerialized]
    public int[] upkeep;
    //[System.NonSerialized]
    public float[] popularity;
    //[System.NonSerialized]
    public int[] customers;
    //[System.NonSerialized]
    public int customersShared;

    public List<BeaconData>[] beacons;
    
    private float incomeCountdown;

    [Header("TextMeshPro")]
    public TextMeshProUGUI[] customersTMP;
    public TextMeshProUGUI[] beaconsTMP;
    public TextMeshProUGUI[] popularityTMP;
    public TextMeshProUGUI incomeTMP;
	public TextMeshProUGUI upkeepTMP;
	public TextMeshProUGUI playerMoneyTMP;

    [Header("Extra")]
    public float delayAI;
    public GameObject AI;

    private void Awake()
    {
        money = new int[players];
        income = new int[players];
        upkeep = new int[players];
        popularity = new float[players];
        customers = new int[players];
        customersShared = 0;
	
        master = this;
        beacons = new List<BeaconData>[players];
        for(int i = 0; i < players; i++)
        {
            beacons[i] = new List<BeaconData>();
            money[i] = startingMoney;
            upkeep[i] = 0;
            popularity[i] = 0.75f;
            customers[i] = 0;
        }

        AI.SetActive(false);
    }

    private void Start()
    {
        incomeCountdown = incomeFrequency;

        playerMoneyTMP.text = money[0].ToString() + " mk";
        incomeTMP.text = "+ 0 mk";
        upkeepTMP.text = "- 0 mk";

        for (int i = 0; i < players; i++)
        {
            customersTMP[i].text = "0";
            beaconsTMP[i].text = "0";
            popularityTMP[i].text = Mathf.Clamp(Mathf.RoundToInt(popularity[i] * 100f), 0, 100).ToString() + "%";
        }
        
        Invoke("UnshackleAI", delayAI);
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
                money[i] += income[i] - upkeep[i];
            }
			playerMoneyTMP.text = money[0].ToString() + " mk";
        }

        for(int i = 0; i < popularity.Length; i++)
        {
            popularity[i] -= Time.deltaTime * popularity[i] * 0.01f * (customers[i] / 150000f);
            popularityTMP[i].text = Mathf.Clamp(Mathf.RoundToInt(popularity[i] * 100f), 0, 100).ToString() + "%";
        }
    }

    public void UpdateIncome()
    {
        for(int i = 0; i < income.Length; i++)
        {
            income[i] = Mathf.RoundToInt(customers[i] / 100f) * 10 + Mathf.RoundToInt((customersShared * popularity[i]) / 100f) * 10;
            upkeep[i] = beacons[i].Count * beaconUpkeep;
        }

        incomeTMP.text = "+ " + income[0].ToString() + " mk";
        upkeepTMP.text = "- " + upkeep[0].ToString() + " mk";
        for(int i = 0; i < players; i++)
        {
            customersTMP[i].text = (customers[i] + Mathf.RoundToInt(customersShared * popularity[i])).ToString();
            beaconsTMP[i].text = beacons[i].Count.ToString();
            popularityTMP[i].text = Mathf.Clamp(Mathf.RoundToInt(popularity[i] * 100f), 0, 100).ToString() + "%";
        }
    }

    public bool Pay(int player, int cost)
    {
        //print("Player " + player + " is trying to pay " + cost + " mk");
        if(money[player] >= cost)
        {
            money[player] -= cost;
			playerMoneyTMP.text = money[0].ToString() + " mk";
            return true;
        }
        else
            return false;
    }

    public void UnshackleAI()
    {
        AI.SetActive(true);
    }
}
