using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Mapbox.Utils;

[System.Serializable]
public class EnemyInfo
{
    public string name;
    public Sprite sprite;
}

public class DoNotDeleteData : MonoBehaviour 
{
	public static DoNotDeleteData Instance;
    public string[] companyName;
    public EnemyInfo[] enemyInfos;
    public EnemyInfo enemyInfo;
	public Vector2d city;

    void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}

        companyName = new string[2];
        enemyInfo = enemyInfos[Random.Range(0, enemyInfos.Length)];
	}
}

