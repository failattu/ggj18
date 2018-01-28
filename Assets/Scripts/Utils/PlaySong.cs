using UnityEngine;
using System.Collections;

public class PlaySong : MonoBehaviour
{
	public AudioClip soundEffect;
	public AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;

	void Awake () {

	}
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonUp(0))
		{
			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot(soundEffect,vol);
		}
	}
}
