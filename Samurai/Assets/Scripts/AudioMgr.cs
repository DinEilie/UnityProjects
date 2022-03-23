using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
	//Sounds and Source
	public AudioClip[] Sounds;
	public AudioSource RedAudio;
	public AudioSource Effects;
    
	public void Start()
    {
		RedAudio.Play();

	}
	public void playSound(int index){
		//OpenMenu
		if(index == 0 && !RedAudio.isPlaying)
			RedAudio.PlayOneShot (Sounds[index]);
		//DeathScene
		if(index == 1 && !RedAudio.isPlaying)
			RedAudio.PlayOneShot (Sounds[index]);
		//Heart
		if(index == 2 && !RedAudio.isPlaying)
			RedAudio.PlayOneShot (Sounds[index]);
		//Fall
		if(index == 3 && !RedAudio.isPlaying)
			RedAudio.PlayOneShot (Sounds[index]);
		//Mystery
		if(index == 4 && !RedAudio.isPlaying)
			RedAudio.PlayOneShot (Sounds[index]);
		//DamageEffect
		if(index == 5)
			Effects.PlayOneShot (Sounds[index]);
		//Death
		if(index == 6 && !RedAudio.isPlaying)
			Effects.PlayOneShot (Sounds[index]);
		//key
		if(index == 7 && !RedAudio.isPlaying)
			Effects.PlayOneShot (Sounds[index]);
	}

	public void switchBackgroundMusic()
    {
		RedAudio.Stop();
		RedAudio.clip = Sounds[8];
		RedAudio.Play();

	}
}
