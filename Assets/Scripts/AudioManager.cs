using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : Singleton<AudioManager> {
	//public AudioClip loopAudioScene;
	private static AudioSource audioInScene;
	void Start () {
		if (audioInScene == null && base.SingletonStart ()) {
			audioInScene = GetComponentInChildren<AudioSource> ();
		} else {
			Destroy (gameObject);
		}
		//audioInScene.clip = loopAudioScene;
		//audioInScene.Play ();
	}
	public static void PlayOneShot(AudioClip audio){
		audioInScene.PlayOneShot (audio);
	}
	public static void PlayOneShot(AudioClip[] audio){
		foreach (AudioClip clip in audio) {
			audioInScene.PlayOneShot (clip);
		}

	}
	public static void PlayAudioLoopScene(bool ativar){
		if (ativar)
			audioInScene.Play ();
		else 
			audioInScene.Stop();
	}
}
