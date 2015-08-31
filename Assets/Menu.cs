using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	//Controle dos touchs
	private Camera tela;                //Para pegar os touchs
	private Vector2 ponto;              //Coordenada do ultimo touch

	private Hero heroi;
	private BoxCollider2D heavy, light_atack, item, defense, sair;

	//Controle dos inimigos
	private int numero_inimigos;
	private GameObject[] inimigos;		
	private Inimigo[] controles;
	private BoxCollider2D[] selecionador;
	public int selecionado = -1;
	private Vector2 HP_inimigo_pos;

	private bool ativo = false;

	// Use this for initialization
	void Start () {
		this.HP_inimigo_pos = new Vector2 (14.7f, 4.19f);
		this.tela = GameObject.Find ("Main Camera").GetComponent<Camera> ();

		this.heroi = GameObject.Find ("Hero").GetComponent<Hero> ();
		this.heavy = GameObject.Find ("heavy").GetComponent<BoxCollider2D> ();
		this.light_atack = GameObject.Find ("light").GetComponent<BoxCollider2D> ();
		this.item = GameObject.Find ("item").GetComponent<BoxCollider2D> ();
		this.defense = GameObject.Find ("defense").GetComponent<BoxCollider2D> ();
		this.sair = GameObject.Find ("sair").GetComponent<BoxCollider2D> ();


		//TESTES
		numero_inimigos = Mathf.CeilToInt(Random.value*10)%3;
		if (numero_inimigos == 0) {
			numero_inimigos = 3;
		}
		//


		this.inimigos = GameObject.FindGameObjectsWithTag("Enemy");
		this.controles = new Inimigo[inimigos.Length];
		this.selecionador = new BoxCollider2D[inimigos.Length];

		//teste = Random.value;
		Debug.Log(numero_inimigos);

		for (int contador = 0; contador < inimigos.Length; contador++) {
			this.controles[contador] = this.inimigos[contador].GetComponent<Inimigo>();
			this.controles[contador].criar_HP_interface(this.HP_inimigo_pos);
			this.HP_inimigo_pos = new Vector2(HP_inimigo_pos.x,HP_inimigo_pos.y - 0.57f);
			this.selecionador[contador] = this.inimigos[contador].GetComponent<BoxCollider2D>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			this.ponto = this.tela.ScreenToWorldPoint (Input.GetTouch (Input.touches.Length - 1).position);

			if (this.heavy.OverlapPoint (this.ponto) && this.selecionado > -1 && !this.ativo) {
				this.ativo = true;
				this.heroi.Heavy ();
			}
			if (this.light_atack.OverlapPoint (this.ponto) && this.selecionado > -1 && !this.ativo) {
				this.ativo = true;
				this.heroi.Light ();
			}
			if (this.item.OverlapPoint (this.ponto)) {
				this.ativo = true;
				this.heroi.Item ();
			}
			if (this.defense.OverlapPoint (this.ponto) && this.selecionado > -1 && !this.ativo) {
				this.ativo = true;
				this.heroi.Defender ();
			}
			if (this.sair.OverlapPoint(ponto)){
				Application.Quit();
			}
			
			for (int contador = 0; contador < this.inimigos.Length; contador++) {
				if (this.selecionador [contador].OverlapPoint (this.ponto) && contador != this.selecionado) {
					if (this.selecionado >= 0 && this.controles [contador].getAtivo ()) {
						this.controles [selecionado].deseleciona ();
					}

					if (this.controles [contador].seleciona()) {
						this.heroi.pega_alvo (this.controles [contador].getBehaviour());
						this.selecionado = contador;
					}
				}
			}
			Input.ResetInputAxes ();
		} else {
			this.ativo = false;
		}
	}
}