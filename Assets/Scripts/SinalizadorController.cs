using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SinalizadorController : MonoBehaviour {

	public GameObject Ativado, Desativado;
	public string palavra;
	private CriarLetras criaLetras;
	public SpriteGlow.SpriteGlowEffect[] spriteGlow;
	public UnityEvent ativarEvento, desativarEvento;
	private AudioController audioController;
	void Start(){
		audioController = GetComponent<AudioController> ();
		criaLetras = Object.FindObjectOfType<CriarLetras> ();
		foreach (SpriteGlow.SpriteGlowEffect outlineScript  in spriteGlow) {
            if (outlineScript != null)
            {
                outlineScript.enabled = false;
            }
            
		}
	}
	void OnTriggerEnter2D(Collider2D objeto){
		if (objeto.gameObject.CompareTag ("Player")) {
			Ativado.SetActive (true);
			Desativado.SetActive (false);
			criaLetras.instanciarPalavra (palavra);

			foreach (SpriteGlow.SpriteGlowEffect outlineScript  in spriteGlow) {
                if (outlineScript != null)
                {
                    outlineScript.enabled = true;
                }
            }
			audioController.PlayOneShootAudioClip (0);
			if (ativarEvento != null) {
				ativarEvento.Invoke ();
			}
		}

	}
	void OnTriggerStay2D(Collider2D objeto){
		if (objeto.gameObject.CompareTag ("Player")) {
			criaLetras.instanciarPalavra (palavra);
			foreach (SpriteGlow.SpriteGlowEffect outlineScript  in spriteGlow) {
                if (outlineScript != null)
                {
                    outlineScript.enabled = true;
                }
            }
		}

	}
	void OnTriggerExit2D(Collider2D objeto){
		if (objeto.gameObject.CompareTag ("Player")) {
			Ativado.SetActive (false);
			Desativado.SetActive (true);
			criaLetras.terminarPalavra ();
            desativarEvento.Invoke();
			foreach (SpriteGlow.SpriteGlowEffect outlineScript  in spriteGlow) {
                if (outlineScript != null)
                {
                    outlineScript.enabled = false;
                }
            }
		}

	}
}
