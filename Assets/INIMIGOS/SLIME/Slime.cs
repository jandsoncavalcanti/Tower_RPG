using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour, Inimigo {
	private Animator animador;
	private float intervalo = 8f, relogio = 0f;
	private float vida_total = 0f, vida_atual = 0f;

	private GameObject caveira;
	private Caveira controle;

	private GameObject selecionado;

	// Use this for initialization
	void Start () {
		this.animador = GetComponent<Animator>();

		caveira = (GameObject) Instantiate(Resources.Load("caveira"), new Vector2(transform.position.x + 1.4f, transform.position.y+1.8f), Quaternion.identity);
		controle = caveira.GetComponent<Caveira>();
		controle.setLimite (1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (animador.GetCurrentAnimatorStateInfo (0).IsName ("slime idle") && controle.getAtaca ()) {
			animador.SetBool ("atack", true);
		} else {
			animador.SetBool ("atack", false);
		}

		if (relogio < intervalo) {
			relogio += (float)Time.deltaTime;
		}
		else if (animador.GetCurrentAnimatorStateInfo (0).IsName ("slime idle"))
		{
			controle.atacar();
			relogio = 0;
		}
	}

	public void recebe_dano(int dano)
	{}

	public void foi_selecionado()
	{selecionado = (GameObject) Instantiate(Resources.Load("setinha"), new Vector2(transform.position.x + 1.4f, transform.position.y + 1f), Quaternion.identity);}

	public void foi_deselecionado()
	{Destroy(this.selecionado);}
}
