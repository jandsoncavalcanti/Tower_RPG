using UnityEngine;
using System.Collections;

public class HeroiHP : MonoBehaviour {
	public int total = 2, atual = 2;
	private Sprite hp_cheio;
	private Sprite hp_meio;
	// Renderizar
	private SpriteRenderer renderizador;

	// Use this for i1nitialization
	void Start () {
		this.hp_cheio = Resources.Load <Sprite> ("life grande hero");
		this.hp_meio = Resources.Load <Sprite> ("life pequeno hero");
		this.renderizador = GetComponent<SpriteRenderer>();
		this.atualizaSprite ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void atualizaSprite()
	{
		if (this.atual == 2) {
			this.renderizador.sprite = this.hp_cheio;
		} else if (this.atual == 1) {
			this.renderizador.sprite = this.hp_meio;
		} else {
			this.renderizador.sprite = null;
		}
	}
}
