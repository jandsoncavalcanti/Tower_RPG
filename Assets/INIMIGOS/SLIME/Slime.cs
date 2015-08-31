using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour, Inimigo {

	private float limiteRelogio = 8f, caveiraIntervalo = 1f;
	
	private int numeroVidas = 4;

	private string interfaceHP = "menu life slime";
	
	private InimigoBehaviour behaviour;

	// Use this for initialization
	void Start () {
		this.behaviour = this.GetComponent<InimigoBehaviour>();	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public float getLimiteRelogio () {
		return limiteRelogio;
	}

	public float getCaveiraIntervalo () {
		return caveiraIntervalo;
	}

	public int getNumeroVidas () {
		return numeroVidas;
	}

	public void criar_HP_interface(Vector2 posicao) {
		this.behaviour.criar_HP_interface(posicao, interfaceHP);
	}

	public void send_skull() {
		this.behaviour.send_skull();
	}

	public void damage() {
		this.behaviour.damage(1);
	}

	public void resetAttack() {
		this.behaviour.resetAttack();
	}

	public void resetHit() {
		this.behaviour.resetHit();
	}

	public bool seleciona() {
		return false;
	}

	public void deseleciona()
	{}

	public bool getAtivo() {return this.behaviour.getAtivo();}

	public InimigoBehaviour getBehaviour () {
		return this.behaviour;
	}
}
