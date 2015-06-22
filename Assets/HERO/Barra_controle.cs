using UnityEngine;
using System.Collections;
/*
public class Barra_controle : MonoBehaviour {
	//Metodo para se usar com gui pois se localiza pela posicao em pixels
	
	float escala;
	public Texture2D textura;

	// Use this for initialization
	void Start () {
		textura = Resources.Load<Texture2D> ("barra");
	}
	
	// Update is called once per frame
	void Update () {
		if (escala < 100) {
			escala = escala + Time.deltaTime*2;
		}
	}
	void OnGUI()
	{
		//Inicia um quadro nas cordenadas
		GUI.BeginGroup(new Rect(200, 0, escala, 15));
		//Faz a textura deslizar pela tela, obs: ela so e visivel dentro do quadro 
		GUI.DrawTexture(new Rect(-200 + escala,0, 200, 15), textura);
		//End para o BeginGroup
		GUI.EndGroup();
	}
}
*/

public class Barra_controle : MonoBehaviour {
	// Usado para escalar a imagem no x, fazer efeito de surgir a barra
	private float escala;
	// Auxiliar para nao precisar ficar fazendo comparacoes na logica da escala
	private float aux;
	// Posicao da barra para o personagem poder criar n barras que seguirao ele
	private Vector2 posicao;
	// Velocidade de preenchimento da barra velocidade inicial = 0.02
	private float velocidade;
	// decide se a barra pode comecar a ser preenchida ou nao
	private bool go = false;
	// Limite da escala
	private float limite = 1.5f;
	// Carregar sprite da barra
	private Sprite barra;
	private Sprite barracheia;
	// Renderizar
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		//escala inicia 0
		escala = 0f;
		//velocidade inicial
		velocidade = 0.3f;
		//limite inicial
		//limite = 1.5f;
		transform.localScale = new Vector2(0f,1.5f);
		//posicao = new Vector2(3.1375f, 2.4f);
		posicao = new Vector2(transform.position.x,transform.position.y);
		barra = Resources.Load <Sprite> ("barra2");
		barracheia = Resources.Load <Sprite> ("barra2cheia");
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = barra;
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
			escala = limite;
			spriteRenderer.sprite = barracheia;
		}

		transform.localScale = new Vector2(escala,1.5f);
		//compensa a posicao com metade do tamanho que pode ser pego no box colider
		transform.position = new Vector2(posicao.x + (float)(escala*1.235f), posicao.y);
	}

	public float getEscala() {return escala;}

	public void setVelocidade(float novaVelocidade) {velocidade = novaVelocidade;}

	public void setGo(bool go) {this.go = go;}

	public void setLimite(float novoLimite) {limite = novoLimite;}

	public void attack(float custo)
	{
		escala =- custo;
		transform.localScale = new Vector2(escala,1.5f);
	}
}