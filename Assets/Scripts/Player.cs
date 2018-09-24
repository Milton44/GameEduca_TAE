using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
	public Animator animPlayer;
	public Rigidbody2D rgbPlayer;
	private Vector2 forceJump;
	private Vector3 scaleInicial;
	private float speedPlayer = 10f, horizontal;
	private int idAnimation;
	private Transform playerTransform;
	public bool isGround;
	private bool chave;
	private AudioController audioController;
	public Player(Animator animPlayer, Rigidbody2D rgbPlayer, Transform playerTransform, AudioController audioController){
		this.animPlayer = animPlayer;
		this.rgbPlayer = rgbPlayer;
		this.playerTransform = playerTransform;
		this.forceJump = new Vector2 (0, 600f);
		this.scaleInicial = playerTransform.localScale;
		this.audioController = audioController;
	}
	public void fixedUpdatePlayer(){
		animPlayer.SetFloat ("speedY", rgbPlayer.velocity.y);
	}
	public void inputControllerPlayer(){
		horizontal = Input.GetAxisRaw ("Horizontal");
		if (horizontal > 0) {
			idAnimation = 1;
			playerTransform.localScale = scaleInicial;
			rgbPlayer.velocity = new Vector2 (speedPlayer, rgbPlayer.velocity.y);
			chave = true;
		} else if (horizontal < 0) {
			idAnimation = 1;
			playerTransform.localScale = new Vector3(-1*scaleInicial.x,scaleInicial.y,scaleInicial.z);
			rgbPlayer.velocity = new Vector2 (-speedPlayer, rgbPlayer.velocity.y);
			chave = true;
		} else if (horizontal == 0) {
			if (chave) {
				rgbPlayer.velocity = new Vector2 (0, rgbPlayer.velocity.y);
				idAnimation = 0;
				chave = false;
			}
		}
		if (Input.GetButtonDown ("Jump")&&isGround) {
			rgbPlayer.AddForce (forceJump);
			audioController.PlayOneShootAudioClip (0);
		}
		animPlayer.SetInteger ("idAnimation", idAnimation);
		animPlayer.SetBool ("isGround", isGround);
	}
	public void stopPlayer(){
		rgbPlayer.velocity = new Vector2 (0, rgbPlayer.velocity.y);
		animPlayer.SetInteger ("idAnimation", 0);
		animPlayer.SetBool ("isGround", isGround);
	}

}
