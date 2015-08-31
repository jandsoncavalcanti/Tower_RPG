using UnityEngine;
using System.Collections;

public class InimigoHPInterface : MonoBehaviour {
	private GameObject[] vida;
	private InimigoHP[] vida_script;
	private int indice;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void cria(int quantidade, Vector2 posicao ) {
		posicao = new Vector2(posicao.x + 0.57f, posicao.y);
		vida = new GameObject[quantidade];
		vida_script = new InimigoHP[quantidade];
		indice = quantidade - 1;
		for (int contador = 0; contador < quantidade; contador++) {
			vida[contador] = (GameObject) Instantiate(Resources.Load("life inimigo"), posicao, Quaternion.identity);
			vida_script[contador] = vida[contador].GetComponent<InimigoHP>();
			posicao = new Vector2(posicao.x - 0.45f, posicao.y);
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

}
