using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {


	private Animator animator;          //Controle de animacao

	private HPheroiInterface health_points;
	private int numero_vidas = 4;

	private Barra_controle barra;
	private int barras_atuais = 0, barras_totais = 2;

	private Inimigo alvo;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();

		this.health_points = (HPheroiInterface) GameObject.Find ("menu life heroi").GetComponent<HPheroiInterface> ();
		this.health_points.cria (numero_vidas);
		this.barra = (Barra_controle)GameObject.Find ("barra").GetComponent<Barra_controle> ();
		this.barra.criar (barras_totais);
		this.barra.setGo (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
		{animator.SetBool("light", false);}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy attack"))
		{animator.SetBool("heavy", false);}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("use item"))
		{animator.SetBool("use item", false);}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
		{
			animator.SetBool("hurt", false);
			animator.SetBool("light", false);
			animator.SetBool("heavy", false);
			animator.SetBool("use item", false);
		}
	}

	public void setBarras() {barras_atuais = barras_totais;}

	public void Heavy(){
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance") && barras_atuais > 1 && !animator.GetBool("heavy"))
		{
			animator.SetBool("heavy", true);
			barra.sprites_pedras[barras_atuais-1].enabled = false;
			barra.sprites_pedras[barras_atuais-2].enabled = false;
			barras_atuais = barras_atuais - 2;
			alvo.recebe_dano(2);
			if (barras_atuais == 0) {barra.setGo(true);}
		}
	}

	public void Light(){
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance") && barras_atuais > 0 && !animator.GetBool("light"))
		{
			animator.SetBool("light", true);
			barra.sprites_pedras[barras_atuais-1].enabled = false;
			barras_atuais = barras_atuais - 1;
			alvo.recebe_dano(1);
			if (barras_atuais == 0) {barra.setGo(true);}
		}
	}

	public void Item(){
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Stance") && barras_atuais > 0 && !animator.GetBool("use item"))
		{
			animator.SetBool("use item", true);
			barra.sprites_pedras[barras_atuais-1].enabled = false;
			barras_atuais = barras_atuais - 1;
			if (barras_atuais == 0) {barra.setGo(true);}
		}
	}

	public void recebe_dano (int dano) {
		animator.SetBool ("hurt", true);
		if (health_points.recebe_dano (dano)) {
			animator.SetBool ("dead", true);
		}
	}

	public void pega_algo(Inimigo alvo) {this.alvo = alvo;}
}
