using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	                                    //Controle dos touchs
	private Camera tela;                //Para pegar os touchs
	private Animator animator;          //Controle de animacao
	private Vector2 ponto;              //Coordenada do ultimo touch

										//Controle de itens do personagem
	private int pots;					//numero de pocoes

	                                    //Pega o BoxCollider dos botoes
	private BoxCollider2D attack;       //
	private BoxCollider2D heavy;        //
	private BoxCollider2D defense;      //
	private BoxCollider2D item;         //

	                                    //Cria e controla as barras de ataque
	private float limite_barras;        //Limite de escala das barras - necessario para configurar tamanho e posicao

	private GameObject barra;
	private Barra_controle controle;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();
		this.tela = (Camera)GameObject.Find ("Main Camera").GetComponent<Camera> ();
		this.heavy = (BoxCollider2D)GameObject.Find ("heavy").GetComponent<BoxCollider2D> ();
		this.attack = (BoxCollider2D)GameObject.Find ("light").GetComponent<BoxCollider2D> ();
		this.defense = (BoxCollider2D)GameObject.Find ("defense").GetComponent<BoxCollider2D> ();
		this.item = (BoxCollider2D)GameObject.Find ("item").GetComponent<BoxCollider2D> ();
		this.limite_barras = 1.5f;

		pots = 5;

		barra = (GameObject) Instantiate(Resources.Load("barra"), new Vector3(6.15f, -1.1f, 0), Quaternion.identity);
		controle = barra.GetComponent<Barra_controle>();
		controle.setLimite(limite_barras);
	}
	
	// Update is called once per frame
	void Update () {
		controle.setGo(true);
		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance"))
		{resetStatus();}
		else
		{
			if ( Input.touchCount > 0)
			{
				ponto = tela.ScreenToWorldPoint(Input.GetTouch(Input.touches.Length -1).position);

				if (this.attack.OverlapPoint(ponto) 
				    && animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance")
				    && controle.getEscala() >= 0.75)
				{
					controle.attack(0.75f);
					animator.SetBool("light", true);
				}
				else if (this.heavy.OverlapPoint(ponto)
				     && animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance")
					 && controle.getEscala() == 1.5)
				{
					controle.attack(1.5f);
					animator.SetBool("heavy", true);
				}
				else if (this.item.OverlapPoint(ponto)
				         && animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance")
						 && pots > 0)
				{
					pots =- 1;
					animator.SetBool("use item", true);
				}
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
