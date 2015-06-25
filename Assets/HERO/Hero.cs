using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	                                    //Controle dos touchs
	private Camera tela;                //Para pegar os touchs
	private Vector2 ponto;              //Coordenada do ultimo touch

	private Animator animator;          //Controle de animacao

	BoxCollider2D heavy, light;

										//Controle dos inimigos
	private GameObject[] inimigos;		
	private Inimigo[] controles;
	private BoxCollider2D[] selecionador;
	private int selecionado = -1;

	private Barra_controle barra;
	private int barras_atuais = 0, barras_totais = 2;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();
		this.tela = (Camera)GameObject.Find ("Main Camera").GetComponent<Camera> ();

		this.heavy = GameObject.Find ("heavy").GetComponent<BoxCollider2D> ();
		this.light = GameObject.Find ("light").GetComponent<BoxCollider2D> ();

		this.inimigos = GameObject.FindGameObjectsWithTag("Enemy");
		this.controles = new Inimigo[inimigos.Length];
		this.selecionador = new BoxCollider2D[inimigos.Length];

		for (int contador = 0; contador < inimigos.Length; contador++) {
			if (inimigos[contador].name.Contains("slime")) {controles[contador] = (Inimigo) inimigos[contador].GetComponent<Slime>();}

			selecionador[contador] = inimigos[contador].GetComponent<BoxCollider2D>();
		}

		barra = (Barra_controle)GameObject.Find ("barra").GetComponent<Barra_controle> ();
		barra.criar (barras_totais);
		barra.setGo (true);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (barra.name);
		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance"))
		{resetStatus();}
		else
		{
			if ( Input.touchCount > 0)
			{
				ponto = tela.ScreenToWorldPoint(Input.GetTouch(Input.touches.Length -1).position);
				if (heavy.OverlapPoint(ponto)) {Heavy();}
				if (light.OverlapPoint(ponto)) {Light();}

				for (int contador = 0; contador < inimigos.Length; contador++) {
					if (this.selecionador[contador].OverlapPoint(ponto) && contador != selecionado)
					{
						if (selecionado >= 0) {controles[selecionado].foi_deselecionado();}
						selecionado = contador;
						controles[contador].foi_selecionado();
					}
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

	public int getTotal() {return barras_totais;}

	public void setBarras() {barras_atuais = barras_totais;}

	public void Heavy(){
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance") && barras_atuais > 1)
		{
			animator.SetBool("heavy", true);
			barra.sprites_pedras[barras_atuais-1].enabled = false;
			barra.sprites_pedras[barras_atuais-2].enabled = false;
			barras_atuais = barras_atuais - 2;
			if (barras_atuais == 0) {barra.setGo(true);}
		}
	}

	public void Light(){
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance") && barras_atuais > 0)
		{
			animator.SetBool("light", true);
			barra.sprites_pedras[barras_atuais-1].enabled = false;
			barras_atuais = barras_atuais - 1;
			if (barras_atuais == 0) {barra.setGo(true);}

		}
	}
}
