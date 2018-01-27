using UnityEngine;
using System.Collections;

public class DoNotDeleteData : MonoBehaviour 
{
	public static DoNotDeleteData Instance;

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
	}
}

