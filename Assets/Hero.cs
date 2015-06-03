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
	private float limite_barras;        //Limite de escala das barras - necessario para configurar tamanho e posicao
	private float pos;                  //Posicao das barras
	private float atual, auxiliar;


	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();
		this.tela = (Camera)GameObject.Find ("Main Camera").GetComponent<Camera> ();
		this.heavy = (BoxCollider2D)GameObject.Find ("heavy").GetComponent<BoxCollider2D> ();
		this.attack = (BoxCollider2D)GameObject.Find ("light").GetComponent<BoxCollider2D> ();
		this.defense = (BoxCollider2D)GameObject.Find ("defense").GetComponent<BoxCollider2D> ();
		this.item = (BoxCollider2D)GameObject.Find ("item").GetComponent<BoxCollider2D> ();
		this.numero_de_barras = 2;
		this.limite_barras = 1.5f - (float) ((numero_de_barras - 1)* 0.005);
		this.pos = 6.15f;

		barra = new GameObject[numero_de_barras];
		controle = new Barra_controle[numero_de_barras];

		for (int counter = 0; counter < barra.Length; counter++)
		{
			barra[counter] = (GameObject) Instantiate(Resources.Load("barra"), new Vector3(pos, -1.1f, 0), Quaternion.identity);
			controle[counter] = barra[counter].GetComponent<Barra_controle>();
			pos = (float) (2.47*(limite_barras/numero_de_barras)) + pos+0.009f;

			controle[counter].setLimite(limite_barras/numero_de_barras);
			if (counter == 0){controle[counter].setGo(true);}
		}
	}
	
	// Update is called once per frame
	void Update () {
		atual = 0;
		for (int counter = 1; counter < barra.Length; counter++)
		{
			auxiliar = controle[counter - 1].getEscala();
			atual += auxiliar;
			if ( auxiliar == limite_barras/numero_de_barras){controle[counter].setGo(true);}
		}
		atual += controle[barra.Length - 1].getEscala();

		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance"))
		{resetStatus();}
		else
		{
			if ( Input.touchCount > 0)
			{
				ponto = tela.ScreenToWorldPoint(Input.GetTouch(Input.touches.Length -1).position);

				if (this.attack.OverlapPoint(ponto) 
				    && animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance")
				    && atual >= limite_barras/numero_de_barras)
				{
					auxiliar = limite_barras/numero_de_barras;
					for (int counter = barra.Length - 1; counter >= 0; counter-- )
					{
						auxiliar -= controle[counter].getEscala();
						if ( auxiliar > 0){controle[counter].setEscala(0);}
						else
						{
							controle[counter].setEscala((-1)*auxiliar);
							auxiliar = 0;
						}
						if (counter > 0) {controle[counter].setGo(false);}
					}
					animator.SetBool("light", true);
				}
				else if (this.heavy.OverlapPoint(ponto)
				     && animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance")
				     && atual >= 2*(limite_barras/numero_de_barras))
				{
					auxiliar = 2*(limite_barras/numero_de_barras);
					for (int counter = barra.Length - 1; counter >= 0; counter-- )
					{
						auxiliar -= controle[counter].getEscala();
						if ( auxiliar > 0){controle[counter].setEscala(0);}
						else
						{
							controle[counter].setEscala((-1)*auxiliar);
							auxiliar = 0;
						}
						if (counter > 0) {controle[counter].setGo(false);}
					}
					animator.SetBool("heavy", true);
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
