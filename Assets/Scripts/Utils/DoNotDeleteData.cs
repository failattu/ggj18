using UnityEngine;
using System.Collections;
using TMPro;

public class DoNotDeleteData : MonoBehaviour 
{
	public static DoNotDeleteData Instance;
	public string dataWeWant; 
	public TextMeshProUGUI textualData;

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
	void Update(){
		dataWeWant = textualData.text;
	}
}

