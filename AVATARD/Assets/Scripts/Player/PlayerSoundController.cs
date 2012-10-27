using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour {
	
	public AudioSource[] audioSources;

	public AudioClip jetSound;
	public float jetSoundVolume = 1; //Between 0 and 1
	
	public AudioClip fallingSound;
	
	private float fallingSoundVolume = 0; //Between 0 and 1
	private bool isPlayingFallingSound = false;
	public float fallingSoundFadeFactor = 0;
	public float timeToPlayFallingSoundOffset = 0;
	
	private float damageVelocity;
	
	// Use this for initialization
	void Start () 
	{
		audioSources = GetComponents<AudioSource>();
		
		audioSources[0].clip = jetSound;
		audioSources[0].volume = jetSoundVolume;
		
		audioSources[1].clip = fallingSound;
		audioSources[1].volume = fallingSoundVolume;
		
		damageVelocity = GetComponent<PlayerController>().maxSafeFallSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void PlayJetSound()
	{
		audioSources[0].Play();
	}
	
	void StopJetSound()
	{
		audioSources[0].Stop();
	}
	
	void PlayFallingSound(float yFallingSpeed)
	{
		
		if(isPlayingFallingSound == false)
		{
			audioSources[1].Play();
			isPlayingFallingSound = true;
		}
		
		if(audioSources[1].volume <= 0)
		{
			isPlayingFallingSound = false;
		}
		
		if(yFallingSpeed < (-damageVelocity + timeToPlayFallingSoundOffset))
		{
			//if we are accelerating downwards
			audioSources[1].volume += Time.deltaTime * fallingSoundFadeFactor;
		}
		if(yFallingSpeed > (-damageVelocity + timeToPlayFallingSoundOffset))
		{
			//if we have slowed
			audioSources[1].volume -= Time.deltaTime * (fallingSoundFadeFactor * 4f);
		}
		
		//clamp volume between 0 and 1 otherwise we get overflows
		audioSources[1].volume = Mathf.Clamp(audioSources[1].volume,0,1);
		
		if(yFallingSpeed >= 0)
		//if we have come to a complete stop
		{
			audioSources[1].volume = 0;
			isPlayingFallingSound = false;
		}

		
	}
			
}
