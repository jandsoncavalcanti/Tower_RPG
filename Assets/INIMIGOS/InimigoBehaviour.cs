using UnityEngine;
using System.Collections;

public class InimigoBehaviour : MonoBehaviour {

	//Controle de animacoes
	private Animator animador;
	private AttackBehaviour Attack;
	private HitBehaviour Hit;

	//Controle HP
	private GameObject health_obj;
	private InimigoHPInterface health_points;

	//Controle caveira
	private GameObject caveira;
	private Caveira controle;

	//Comunicacao heroi
	private Hero heroi;
	private Inimigo inimigo;

	int numeroVidas;
	float relogio = 0, limiteRelogio, caveiraIntervalo;
	private bool defesa = false, ativo = true;

	// Use this for initialization
	void Start () {
		this.inimigo = this.GetComponent<Inimigo>();
		this.animador = this.GetComponent<Animator>();
		this.Attack = this.animador.GetBehaviour<AttackBehaviour>();
		this.Attack.owner = this.inimigo;
		this.Hit = this.animador.GetBehaviour<HitBehaviour>();
		this.Hit.owner = this.inimigo;

		this.limiteRelogio = this.inimigo.getLimiteRelogio();
		this.numeroVidas = this.inimigo.getNumeroVidas();

		this.caveira = (GameObject) Instantiate(Resources.Load("caveira"), new Vector2(this.transform.position.x + 1.4f, this.transform.position.y+2.3f), Quaternion.identity);
		this.controle = this.caveira.GetComponent<Caveira>();
		this.controle.setLimite(this.inimigo.getCaveiraIntervalo());
		this.controle.cria (this);
		
		this.heroi = GameObject.Find ("Hero").GetComponent<Hero> ();
	}

	public bool getAtivo() {
		return this.ativo;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.animador.GetCurrentAnimatorStateInfo (0).IsName ("parado")) {
			if (this.relogio < this.limiteRelogio) {
				this.relogio += (float) Time.deltaTime;
			} else {
				this.controle.enterPrepare ();
				this.relogio = 0;
			}
		}
	}

	public void criar_HP_interface(Vector2 posicao, string HPinterface){
		this.health_obj = (GameObject) Instantiate(Resources.Load(HPinterface), posicao, Quaternion.identity);
		this.health_points = this.health_obj.GetComponent<InimigoHPInterface>();
		this.health_points.cria (this.numeroVidas, posicao);
	}

	public void send_skull() {
		this.controle.realizeAttack();
	}

	public void ataca(bool defesa){
		this.animador.SetBool ("attack", true);
		this.defesa = defesa;
	}

	public void recebe_dano(int dano)
	{
		this.animador.SetBool ("hit", true);
		if (this.health_points.recebe_dano (dano)) {
			this.animador.SetBool ("dead", true);
			this.ativo = false;
		}
	}

	public void foi_defendido() {
		this.controle.foi_defendido ();
	}

	public void damage(int dano) {
		if (!this.defesa) {
			this.heroi.recebe_dano(dano);
		} else {
			this.heroi.ataque_defendido();
			this.defesa = false;
		}
	}

	public void resetAttack() {
		this.animador.SetBool ("attack", false);
	}
	
	public void resetHit() {
		this.animador.SetBool ("hit", false);
	}
}
