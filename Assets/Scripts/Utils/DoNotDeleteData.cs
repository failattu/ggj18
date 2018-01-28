using UnityEngine;
using System.Collections;
using TMPro;

public class DoNotDeleteData : MonoBehaviour 
{
	public static DoNotDeleteData Instance;
    public string[] companyName;

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
	}
}

