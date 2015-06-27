using UnityEngine;
using System.Collections;

public class HPheroiInterface : MonoBehaviour {

	public GameObject[] vida;
	public HeroiHP[] vida_script;
	public Vector2 posicao;
	private int indice;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void cria(int quantidade) {
		posicao = new Vector2 (this.transform.position.x - 0.57f,this.transform.position.y);
		vida = new GameObject[quantidade];
		vida_script = new HeroiHP[quantidade];
		indice = quantidade - 1;
		for (int contador = 0; contador < quantidade; contador++) {
			vida[contador] = (GameObject) Instantiate(Resources.Load("life heroi"), posicao, Quaternion.identity);
			vida_script[contador] = vida[contador].GetComponent<HeroiHP>();
			posicao = new Vector2(posicao.x + 0.45f, posicao.y);
		}
	}

	public bool recebe_dano(int dano){
		if (indice >= 0) {
			for (int contador = indice; contador >= 0; contador--) {
				if (dano >= vida_script [indice].atual) {
					dano = dano - vida_script [indice].atual;
					vida_script [indice].atual = 0;
					vida_script [indice].atualizaSprite ();

					if (indice - 1 < 0) {
						return true;
					} else {
						indice--;
					}
				} else {
					vida_script [indice].atual = vida_script [indice].atual - dano;
					dano = 0;
					vida_script [indice].atualizaSprite ();
				}
			}
			return false;
		}
		return true;
	}

	public void recupera_HP (int recuperacao){
		if (indice < vida_script.Length) {
			for (int contador = indice; contador < vida_script.Length; contador++) {
				if (recuperacao + vida_script [indice].atual >= vida_script [indice].total) {
					recuperacao = recuperacao - (vida_script [indice].total - vida_script [indice].atual);
					vida_script [indice].atual = vida_script [indice].total;
					vida_script [indice].atualizaSprite ();

					if (indice + 1 < vida_script.Length) {
						indice++;
					}
				} else {
					vida_script [indice].atual = vida_script [indice].atual + recuperacao;
					vida_script [indice].atualizaSprite ();
				}
			}
		}
	}	
}