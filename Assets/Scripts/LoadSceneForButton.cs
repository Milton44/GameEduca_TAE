using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneForButton : MonoBehaviour {
	public void loadScene(string sceneLoad){
		Singleton<SceneController>.getInstance ().LoadScene (sceneLoad);
		AudioManager.PlayAudioLoopScene (false);
	}
		
}
