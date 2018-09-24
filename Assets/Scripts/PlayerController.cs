using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioController))]
public class PlayerController : MonoBehaviour {
	public LayerMask layerGound;
	public Transform groundCheckLeft,groundCheckRight;
	public GameObject FinalFase;
	private Player playerScript;
	public bool ativarControlles;
	private bool finalFase = false;
    private RaycastHit2D rayLeft, rayRight;
	void Start () {
		Camera.main.aspect = 1920f / 1080f;
		playerScript = new Player (GetComponent<Animator> (), GetComponent<Rigidbody2D> (), 
            transform, GetComponent<AudioController>());
		AudioManager.PlayAudioLoopScene (true);
	
	}
	void FixedUpdate(){
		playerScript.fixedUpdatePlayer ();
	}
	void Update () {
        //playerScript.isGround = (Physics2D.OverlapCircle (groundCheckRight.position, 0.01f, layerGound) || 
        //Physics2D.OverlapCircle (groundCheckLeft.position, 0.01f, layerGound)) && playerScript.rgbPlayer.velocity.y<=0  ;
        CheckGround();
            if (ativarControlles) {
			if (!finalFase) {
				playerScript.inputControllerPlayer ();
			} else {
				playerScript.stopPlayer ();
			}
		} else {
			playerScript.animPlayer.SetBool ("isGround", playerScript.isGround);
		}
	}
    void CheckGround()
    {
        rayLeft = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, 0.1f, layerGound);
        rayRight = Physics2D.Raycast(groundCheckRight.position, Vector2.down, 0.1f, layerGound);
        playerScript.isGround = (rayLeft.collider || rayRight.collider) && playerScript.rgbPlayer.velocity.y <= 0;
        if (rayLeft.collider && rayLeft.collider.CompareTag("MovimentPlataform"))
        {
            transform.parent = rayLeft.collider.transform;
        }
        if (rayRight.collider && rayRight.collider.CompareTag("MovimentPlataform"))
        {
            transform.parent = rayRight.collider.transform;
        }
        if (!playerScript.isGround)
        {
            transform.parent = null;
        }

    }
	void OnTriggerEnter2D(Collider2D objeto){
		if(objeto.gameObject.CompareTag ("Letra")){
			objeto.gameObject.GetComponent<Animator> ().SetTrigger ("Pegou");
			objeto.GetComponent<AudioController> ().PlayOneShootAll ();
			FinalFase.SetActive (true);
			finalFase = true;
		}
	}
}
