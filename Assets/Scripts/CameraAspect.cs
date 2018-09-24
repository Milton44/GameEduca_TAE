using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Camera> ().aspect = 1920f / 1080f;
	}
}
