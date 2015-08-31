using UnityEngine;
using System.Collections;

public class Caveira : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	private Animator animador;

	private float limite;
	private float relogio = 0;

	private bool defendeu = false;

	private InimigoBehaviour dono;

	private StoppedSkullBehaviour stopped;

	// Use this for initialization
	void Start () {
		this.spriteRenderer = this.GetComponent<SpriteRenderer>();
		this.animador = this.GetComponent<Animator>();
		this.stopped = this.animador.GetBehaviour<StoppedSkullBehaviour>();
		this.stopped.skull = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.animador.GetCurrentAnimatorStateInfo (0).IsName ("ataca") ) {
			if (this.relogio < this.limite) {
				this.relogio += (float)Time.deltaTime;
			}
			else {
				this.relogio = 0;
				this.dono.ataca(defendeu);
			}
		}
	}

	public void enterIdle() {
		this.spriteRenderer.enabled = false;
		this.animador.SetBool ("ataca", false);
		this.animador.SetBool ("prepara", false);
		this.defendeu = false;
	}

	public void exitIdle() {
		this.spriteRenderer.enabled = true;
	}

	public void enterPrepare() {
		this.animador.SetBool ("prepara", true);
	}

	public void realizeAttack() {
		this.animador.SetBool ("ataca", true);
	}

	public void cria(InimigoBehaviour dono) {
		this.dono = dono;
	}

	public void setLimite(float tempo) {
		this.limite = tempo;
	}

	public void foi_defendido(){
		if (this.animador.GetCurrentAnimatorStateInfo (0).IsName ("ataca")) {
			this.defendeu = true;
		}
	}
}
