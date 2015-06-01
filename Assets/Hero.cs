using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	                                    //Controle dos touchs
	private Camera tela;                //Para pegar os touchs
	private Animator animator;          //Controle de animacao
	private Vector2 ponto;              //Coordenada do ultimo touch

	                                    //Pega o BoxCollider dos botoes
	private BoxCollider2D attack;       //
	private BoxCollider2D heavy;        //
	private BoxCollider2D defense;      //
	private BoxCollider2D item;         //

	                                    //Cria e controla as barras de ataque
	private int numero_de_barras;       //
	private GameObject[] barra;         //
	private Barra_controle[] controle;  //Script correspondente as barras
	private float limite_barras;
	private float pos;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();
		this.tela = (Camera)GameObject.Find ("Main Camera").GetComponent<Camera> ();
		//this.barra = (Barra_controle)GameObject.Find ("barra").GetComponent<Barra_controle> ();
		this.heavy = (BoxCollider2D)GameObject.Find ("heavy").GetComponent<BoxCollider2D> ();
		this.attack = (BoxCollider2D)GameObject.Find ("light").GetComponent<BoxCollider2D> ();
		this.defense = (BoxCollider2D)GameObject.Find ("defense").GetComponent<BoxCollider2D> ();
		this.item = (BoxCollider2D)GameObject.Find ("item").GetComponent<BoxCollider2D> ();
		this.numero_de_barras = 2;
		this.limite_barras = 1.49f;
		this.pos = 6.145f;

		barra = new GameObject[numero_de_barras];
		controle = new Barra_controle[numero_de_barras];
		barra[0] = (GameObject) Instantiate(Resources.Load("barra"), new Vector3(pos, -1.1f, 0), Quaternion.identity);
		controle[0] = barra[0].GetComponent<Barra_controle>();
		pos = (float) (2.47*(limite_barras/numero_de_barras)) + pos+0.005f;
		barra[1] = (GameObject) Instantiate(Resources.Load("barra"), new Vector3(pos, -1.1f, 0), Quaternion.identity);
		controle[1] = barra[1].GetComponent<Barra_controle>();
		controle[0].setLimite(limite_barras/numero_de_barras);
		controle[1].setLimite(limite_barras/numero_de_barras);
	}
	
	// Update is called once per frame
	void Update () {
		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance"))
		{resetStatus();}
		else
		{
			if ( Input.touchCount > 0)
			{
				ponto = tela.ScreenToWorldPoint(Input.GetTouch(Input.touches.Length -1).position);
				/*
				if (this.attack.OverlapPoint(ponto) && barra.getEscala() >= 1 && !animator.GetBool("light"))
				{animator.SetBool("light", true);barra.setEscala(-1);}
			else if (this.heavy.OverlapPoint(ponto)&& barra.getEscala() >= 2&& !animator.GetBool("heavy"))
				{animator.SetBool("heavy", true);barra.setEscala(-2);}
				*/
			}
		}
	}

	private void resetStatus()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
		{animator.SetBool("light", false);}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy attack"))
		{animator.SetBool("heavy", false);}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("use item"))
		{animator.SetBool("use item", false);}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
		{animator.SetBool("hurt", false);}
	}
}
