using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {


	private Animator animator;          //Controle de animacao
	private LightAttackBehaviour LightAttack;
	private HeavyAttackBehaviour HeavyAttack;
	private HurtBehaviour Hurt;
	private DefenseBehaviour Defense;
	private ItemBehaviour UseItem;

	private HPheroiInterface health_points;
	private int numeroVidas = 4;

	private Barra_controle barra;
	private int barras_atuais = 0, barras_totais = 2;

	private InimigoBehaviour alvo;

	// Use this for initialization
	void Start () {
		this.animator = this.GetComponent<Animator>();
		this.LightAttack = this.animator.GetBehaviour<LightAttackBehaviour>();
		this.LightAttack.heroi = this;
		this.HeavyAttack = this.animator.GetBehaviour<HeavyAttackBehaviour>();
		this.HeavyAttack.heroi = this;
		this.Hurt = this.animator.GetBehaviour<HurtBehaviour>();
		this.Hurt.heroi = this;
		this.Defense = this.animator.GetBehaviour<DefenseBehaviour>();
		this.Defense.heroi = this;
		this.UseItem = this.animator.GetBehaviour<ItemBehaviour>();
		this.UseItem.heroi = this;

		this.health_points = GameObject.Find ("menu life heroi").GetComponent<HPheroiInterface> ();
		this.health_points.cria (this.numeroVidas);
		this.barra = GameObject.Find ("barra").GetComponent<Barra_controle> ();
		this.barra.criar (this.barras_totais);
		this.barra.setGo (true);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setBarras() {barras_atuais = barras_totais;}

	public void setSpriteBarras() {
		barra.sprites_pedras[barras_atuais-1].enabled = false;
		barras_atuais--;
		if (barras_atuais == 0) {barra.setGo(true);}
	}

	public void ataque(int vezes) {
		alvo.recebe_dano (vezes);
	}

	public void Light(){
		if (barras_atuais > 0) {
			animator.SetBool ("light", true);
		}
	}

	public void resetAllButNoDamage() {
		animator.SetBool ("light", false);
		animator.SetBool("heavy", false);
		animator.SetBool ("use item", false);
	}

	public void resetHurt() {
		animator.SetBool ("hurt", false);
	}

	public void resetDefense() {
		animator.SetBool ("defense", false);
	}
	
	public void Heavy(){
		if ( barras_atuais > 1) {
			animator.SetBool("heavy", true);
		}
	}

	public void Item(){
		if ( barras_atuais > 0) {
			animator.SetBool("use item", true);
		}
	}

	public void Defender() {
		if ((animator.GetCurrentAnimatorStateInfo (0).IsName ("Battle Stance") || animator.GetCurrentAnimatorStateInfo (0).IsName ("Hurt")) && barras_atuais > 0) {
			alvo.foi_defendido();
			setSpriteBarras();
		}
	}

	public void recebe_dano (int dano) {
		animator.SetBool ("hurt", true);
		if (health_points.recebe_dano (dano)) {
			animator.SetBool ("dead", true);
		}
	}
	public void recuperaHp(int vezes) {
		health_points.recupera_HP(vezes);
	}

	public void pega_alvo(InimigoBehaviour alvo) {this.alvo = alvo;}

	public void ataque_defendido() {animator.SetBool ("defense", true);}
}
