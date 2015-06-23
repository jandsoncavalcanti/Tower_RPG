using UnityEngine;
using System.Collections;

public class Caveira : MonoBehaviour {
	private Animator animador;
	private float limite;
	private float relogio;
	private bool go;

	// Use this for initialization
	void Start () {
		this.animador = GetComponent<Animator>();
		relogio = 0;
		go = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (animador.GetCurrentAnimatorStateInfo (0).IsName ("prepara") && relogio < limite) {
			animador.SetBool ("prepara", false);
			animador.SetBool ("ataca", false);
			go = true;
		} else if (animador.GetCurrentAnimatorStateInfo (0).IsName ("prepara") && relogio >= limite) {
			animador.SetBool ("ataca", true);
		}
		else {
			go = false;
			relogio = 0;
		}
		if (go) {relogio += (float)Time.deltaTime;}
	}

	public void setLimite(float tempo) {limite = tempo;}

	public void atacar() {animador.SetBool("prepara", true);}
}
