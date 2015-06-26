using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
										//Controle dos touchs
	private Camera tela;                //Para pegar os touchs
	private Vector2 ponto;              //Coordenada do ultimo touch

	private Hero heroi;
	private BoxCollider2D heavy, light_atack, item;

	//Controle dos inimigos
	private GameObject[] inimigos;		
	private Inimigo[] controles;
	private BoxCollider2D[] selecionador;
	public int selecionado = -1;
	private Vector2 HP_inimigo_pos;

	// Use this for initialization
	void Start () {
		this.HP_inimigo_pos = new Vector2 (14.7f, 4.19f);
		this.tela = (Camera)GameObject.Find ("Main Camera").GetComponent<Camera> ();

		this.heroi = GameObject.Find ("Hero").GetComponent<Hero> ();
		this.heavy = GameObject.Find ("heavy").GetComponent<BoxCollider2D> ();
		this.light_atack = GameObject.Find ("light").GetComponent<BoxCollider2D> ();
		this.item = GameObject.Find ("item").GetComponent<BoxCollider2D> ();
		
		
		this.inimigos = GameObject.FindGameObjectsWithTag("Enemy");
		this.controles = new Inimigo[inimigos.Length];
		this.selecionador = new BoxCollider2D[inimigos.Length];

		for (int contador = 0; contador < inimigos.Length; contador++) {
			if (inimigos[contador].name.Contains("slime")) {controles[contador] = (Inimigo) inimigos[contador].GetComponent<Slime>();}

			controles[contador].criar_HP_interface(HP_inimigo_pos);
			HP_inimigo_pos = new Vector2(HP_inimigo_pos.x,HP_inimigo_pos.y - 0.57f);
			selecionador[contador] = inimigos[contador].GetComponent<BoxCollider2D>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.touchCount > 0)
		{
			ponto = tela.ScreenToWorldPoint(Input.GetTouch(Input.touches.Length -1).position);
			if (heavy.OverlapPoint(ponto) && selecionado > -1) {heroi.Heavy();}
			if (light_atack.OverlapPoint(ponto) && selecionado > -1) {heroi.Light();}
			if (item.OverlapPoint(ponto)) {heroi.Item();}
			
			for (int contador = 0; contador < inimigos.Length; contador++) {
				if (this.selecionador[contador].OverlapPoint(ponto) && contador != selecionado)
				{
					if (selecionado >= 0 && controles[contador].getAtivo()) {controles[selecionado].foi_deselecionado();}

					if (controles[contador].foi_selecionado()) {
						heroi.pega_algo(controles[contador]);
						selecionado = contador;
					}
				}
			} 
		}
	}
}
