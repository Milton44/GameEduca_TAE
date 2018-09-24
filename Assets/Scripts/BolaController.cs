using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BolaController : MonoBehaviour {
	public enum TypeEffect{ OneEffect,ContinuousEffect }
	public TypeEffect typeEffect;
	public LayerMask layerCollision;
	public Vector2 forceEffect;
	private bool firstShot=false;
	void OnTriggerEnter2D(Collider2D objeto){
		if (layerCollision==(layerCollision|(1<<objeto.gameObject.layer))) {
			if (!firstShot) {
				if (typeEffect.Equals (TypeEffect.OneEffect)) {
					GetComponent<Rigidbody2D> ().AddForce (forceEffect);
					firstShot = true;
				} else {
					GetComponent<Rigidbody2D> ().AddForce (forceEffect);
				}
				if(GetComponentInChildren<Collider2D>().Equals(objeto)){
					GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

				}
			}
		}

	}
}
