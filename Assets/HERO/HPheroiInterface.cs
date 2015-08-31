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

	public bool recebe_dano(int vezes){
		for (int counter = 1; counter <= vezes; counter++) {
			if (indice >= 0)	{
				if (vida_script [indice].atual > 0) {
					vida_script [indice].atual --;
					vida_script [indice].atualizaSprite ();
				} else {
					counter --;
				}
				if (vida_script [indice].atual <= 0) {
					vida_script [indice].atual = 0;
					indice --;
				}
				if (indice < 0) {
					return true;
				}
			}
			else {
				return true;
			}
		}
		return false;
	}

	public void recupera_HP (int recuperacao) {
		for (int counter = 1; counter <= recuperacao; counter++) {
			if (indice < vida_script.Length) {
				if (vida_script [indice].atual == vida_script [indice].total && indice + 1 < vida_script.Length) {
					indice ++;
				}
				if (vida_script [indice].atual < vida_script [indice].total) {
					vida_script [indice].atual ++;
					vida_script [indice].atualizaSprite ();
				} else if (indice + 1 < vida_script.Length) {
					counter --;
				}
			}
		}
	}
}