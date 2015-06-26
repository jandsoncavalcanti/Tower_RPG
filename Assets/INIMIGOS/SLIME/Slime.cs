using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour, Inimigo {
	private Animator animador;

	private float intervalo = 8f, relogio = 0f;

	private GameObject health_obj;
	private HPSlimeInterface health_points;
	private int numero_vidas = 4;

	private bool ativo = true;
	private Menu game_interface;	

	private Hero heroi;

	private GameObject caveira;
	private Caveira controle;

	private GameObject selecionado;

	// Use this for initialization
	void Start () {
		this.animador = GetComponent<Animator>();

		this.heroi = GameObject.Find ("Hero").GetComponent<Hero> ();
		this.game_interface = GameObject.Find ("Menu").GetComponent<Menu> ();

		this.caveira = (GameObject) Instantiate(Resources.Load("caveira"), new Vector2(transform.position.x + 1.4f, transform.position.y+1.3f), Quaternion.identity);
		this.controle = this.caveira.GetComponent<Caveira>();
		this.controle.setLimite (1f);
		this.controle.cria (this);
	}
	
	// Update is called once per frame
	void Update () {

		if (animador.GetCurrentAnimatorStateInfo (0).IsName ("slime idle")) {
			if (relogio < intervalo) {
				relogio += (float)Time.deltaTime;
			} else {
				controle.atacar ();
				relogio = 0;
			}
		} else if (animador.GetCurrentAnimatorStateInfo (0).IsName ("slime attack")) {
			animador.SetBool ("atack", false);
		} else if (animador.GetCurrentAnimatorStateInfo (0).IsName ("hit_slime")) {
			animador.SetBool ("atack", false);
			animador.SetBool ("Hit", false);
		}
	}

	public void criar_HP_interface(Vector2 posicao){
		this.health_obj = (GameObject) Instantiate(Resources.Load("menu life slime"), posicao, Quaternion.identity);
		this.health_points = this.health_obj.GetComponent<HPSlimeInterface>();
		this.health_points.cria (numero_vidas, posicao);
	}

	public void ataca(){
		animador.SetBool ("atack", true);
		heroi.recebe_dano(1);
	}

	public void recebe_dano(int dano)
	{
		animador.SetBool ("Hit", true);
		if (health_points.recebe_dano (dano)) {
			animador.SetBool ("dead", true);
			ativo = false;
			Destroy(this.selecionado);
			game_interface.selecionado = -1;
		}
	}

	public bool foi_selecionado()
	{
		if (ativo) {
			selecionado = (GameObject)Instantiate (Resources.Load ("setinha"), new Vector2 (transform.position.x + 1.4f, transform.position.y + 0.9f), Quaternion.identity);
			return true;
		}
		return false;
	}

	public void foi_deselecionado()
	{Destroy(this.selecionado);}

	public bool getAtivo() {return ativo;}
}
