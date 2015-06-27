using UnityEngine;
using System.Collections;

public class Caveira : MonoBehaviour {
	private Animator animador;
	private float limite;
	private float relogio = 0;

	private bool defendeu = false;

	private Inimigo dono;

	// Use this for initialization
	void Start () {
		this.animador = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (animador.GetCurrentAnimatorStateInfo (0).IsName ("idle")) {
			animador.SetBool ("ataca", false);
			defendeu = false;
		} else if (animador.GetCurrentAnimatorStateInfo (0).IsName ("ataca") && relogio < limite) {
			relogio += (float)Time.deltaTime;
		}
		else if (animador.GetCurrentAnimatorStateInfo (0).IsName ("ataca") && relogio >= limite) {
			animador.SetBool ("ataca", true);
			animador.SetBool ("prepara", false);
			relogio = 0;
			if (defendeu) {
				this.dono.ataque_defendido();
			} else {
				this.dono.ataca();
			}
		}
	}

	public void cria(Inimigo dono) {this.dono = dono;}

	public void setLimite(float tempo) {limite = tempo;}

	public void atacar() {animador.SetBool("prepara", true);}

	public void foi_defendido(){
		if (animador.GetCurrentAnimatorStateInfo (0).IsName ("ataca")) {
			defendeu = true;
		}
	}
}
