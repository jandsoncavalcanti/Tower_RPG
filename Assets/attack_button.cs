using UnityEngine;
using System.Collections;

public class attack_button : MonoBehaviour {
	private Camera tela;
	private Collider2D colisao;
	private Vector2 ponto;
	private Hero heroi;
	// Use this for initialization
	void Start () {
		tela = (Camera)GameObject.Find ("Main Camera").GetComponent<Camera> ();
		heroi = (Hero)GameObject.Find ("Hero").GetComponent<Hero> ();
		colisao = this.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.touchCount > 0)
		{
			ponto = tela.ScreenToWorldPoint(Input.GetTouch(Input.touches.Length -1).position);
			if (colisao.OverlapPoint(ponto))
			{}
		}
		*/
	}

}
