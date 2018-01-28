using UnityEngine;
using System.Collections;

public class PlaySong : MonoBehaviour
{
	public AudioClip soundEffect;
	public AudioSource source;
	//private float volLowRange = .5f;
	//private float volHighRange = 1.0f;
    
	public void PlayAudio()
	{
	    //float vol = Random.Range (volLowRange, volHighRange);
	    source.PlayOneShot(soundEffect);
	}
}
