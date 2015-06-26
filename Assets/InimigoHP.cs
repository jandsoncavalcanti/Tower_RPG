using UnityEngine;
using System.Collections;

public class InimigoHP : MonoBehaviour {
	public int total = 2, atual = 2;
	private Sprite hp_cheio;
	private Sprite hp_meio;
	// Renderizar
	private SpriteRenderer renderizador;

	// Use this for initialization
	void Start () {
		hp_cheio = Resources.Load <Sprite> ("life grande");
		hp_meio = Resources.Load <Sprite> ("life pequeno");
		renderizador = GetComponent<SpriteRenderer>();
		atualizaSprite ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void atualizaSprite()
	{
		if (atual == 2) {
			renderizador.sprite = hp_cheio;
		} else if (atual == 1) {
			renderizador.sprite = hp_meio;
		} else {
			renderizador.sprite = null;
		}
	}
}
