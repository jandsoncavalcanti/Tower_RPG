using UnityEngine;
using System.Collections;

public class Caveira : MonoBehaviour {
	private Animator animador;
	private float limite;
	private float relogio;

	private Inimigo dono;

	// Use this for initialization
	void Start () {
		this.animador = GetComponent<Animator>();
		relogio = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (animador.GetCurrentAnimatorStateInfo (0).IsName ("idle")) {
			animador.SetBool ("ataca", false);	
		} else if (animador.GetCurrentAnimatorStateInfo (0).IsName ("ataca") && relogio < limite) {
			relogio += (float)Time.deltaTime;
		}
		else if (animador.GetCurrentAnimatorStateInfo (0).IsName ("ataca") && relogio >= limite) {
			animador.SetBool ("ataca", true);
			animador.SetBool ("prepara", false);
			relogio = 0;
			this.dono.ataca();
		}
	}

	public void cria(Inimigo dono) {this.dono = dono;}

	public void setLimite(float tempo) {limite = tempo;}

	public void atacar() {animador.SetBool("prepara", true);}
}
