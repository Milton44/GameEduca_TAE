using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMoviment : MonoBehaviour {
	public float speedPlataform;
    public float delayTime;
	public enum Direction{ Left, Right, Up, Down};
	public Direction directionBegin;
	public Transform plataform, pointA, pointB;
	private Vector2 vectorSpeed;
	private Vector2 aux;
    private bool chave = true;
	void Start(){
		vectorSpeed.Set (speedPlataform, 0);
		switch (directionBegin) {
		case Direction.Left:
			aux = pointA.position;
			break;
		case Direction.Right:
			aux = pointB.position;
			break;
        case Direction.Up:
                aux = pointA.position;
                break;
            case Direction.Down:
                aux = pointB.position;
                break;

		}
	}
	void FixedUpdate(){
        if (chave)
        {
                if (directionBegin == Direction.Left || directionBegin == Direction.Right)
            {
                if (plataform.position.x <= pointA.position.x)
                {
                    aux = pointB.position;
                    StartCoroutine(DelayTime());
                }
                else if (plataform.position.x >= pointB.position.x)
                {
                    aux = pointA.position;
                    StartCoroutine(DelayTime());
                }
            }
            else
            {
                
                if (plataform.position.y >= pointA.position.y)
                {
                    aux = pointB.position;
                    StartCoroutine(DelayTime());
                }
                else if (plataform.position.y <= pointB.position.y)
                {
                    aux = pointA.position;
                    StartCoroutine(DelayTime());
                }
            }
        
            plataform.position = Vector2.MoveTowards(plataform.position, aux, speedPlataform);
        }
	}
    IEnumerator DelayTime()
    {
        chave = false;
        yield return new WaitForSeconds(delayTime);
        chave = true;
        
    }
}
