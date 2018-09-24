using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	public AudioClip[] audioClip;
	public void PlayOneShootAudioClip(int positionAudio){
		AudioManager.PlayOneShot (audioClip [positionAudio]);
	}
	public void PlayOneShootAll(){
		AudioManager.PlayOneShot (audioClip);
	}

}
