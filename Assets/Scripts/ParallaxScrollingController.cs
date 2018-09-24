using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrollingController : MonoBehaviour {

	public Transform background;
	private Transform cam;
	private Vector2 previousCam;
	private float parallaxScale;
	void Start () {
		cam = Camera.main.transform;
		previousCam = (Vector2)cam.position;
		parallaxScale = background.position.z * -1;
	}
	
	// Update is called once per frame
	void Update () {
		float parallax = (previousCam.x - cam.position.x) * parallaxScale;
		float backgrounPositionX = background.position.x + parallax;
		background.position = new Vector3 (backgrounPositionX, background.position.y, background.position.z);
		previousCam = cam.position;
	}
}
