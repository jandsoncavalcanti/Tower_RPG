using UnityEngine;
using System.Collections;

public class Barra_controle : MonoBehaviour {
	// Usado para escalar a imagem no x, fazer efeito de surgir a barra
	private float escala = 0f;
	// Auxiliar para nao precisar ficar fazendo comparacoes na logica da escala
	private float aux;
	// Posicao da barra para o personagem poder criar n barras que seguirao ele
	private Vector2 posicao;
	// Velocidade de preenchimento da barra velocidade inicial = 0.02
	private float velocidade = 0.3f;
	// decide se a barra pode comecar a ser preenchida ou nao
	private bool go;
	// Limite da escala
	private float limite = 1.5f;
	// Carregar sprite da barra
	private Sprite barra;
	private Sprite barracheia;
	// Renderizar
	private SpriteRenderer spriteRenderer;

	private Hero heroi;
	public GameObject[] pedras;
	public SpriteRenderer[] sprites_pedras;
	private Vector2 posicao_pedras;
	//private int ultimo = 0;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector2(0f,1.5f);
		posicao = new Vector2(transform.position.x,transform.position.y);
		barra = Resources.Load <Sprite> ("barra2");
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = barra;
		heroi = (Hero) GameObject.Find("Hero").GetComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
		if (go){doProgress();}
	}

	private void doProgress()
	{
		// Poe o tamanho aumentado da barra em uma variavel auxiliar
		aux = escala + (float)(Time.deltaTime*velocidade);
		// Se o tamanho aumentado for menor que um, aumente, se nao, um

		if (aux < limite) {escala = aux;}
		else {
			escala = 0;
			go = false;
			for (int contador = 0; contador < pedras.Length; contador++) {
				sprites_pedras[contador].enabled = true;
			}
			heroi.setBarras();
		}

		transform.localScale = new Vector2(escala,1.5f);
		//compensa a posicao com metade do tamanho que pode ser pego no box colider
		transform.position = new Vector2(posicao.x + (float)(escala*1.235f), posicao.y);
	}

	public void setVelocidade(float novaVelocidade) {velocidade = novaVelocidade;}

	public void setGo(bool go) {this.go = go;}

	public void criar(int total){
		pedras = new GameObject[total];
		sprites_pedras = new SpriteRenderer[total];
		posicao_pedras = new Vector2 (6.57f,-1.11f);
		for (int contador = 0; contador < pedras.Length; contador++) {
			pedras[contador] = (GameObject) Instantiate(Resources.Load("attack stone"), posicao_pedras, Quaternion.identity);
			sprites_pedras[contador] = pedras[contador].GetComponent<SpriteRenderer>();
			sprites_pedras[contador].enabled = false;
			posicao_pedras = new Vector2(posicao_pedras.x+0.85f,posicao_pedras.y);
		}
	}
}