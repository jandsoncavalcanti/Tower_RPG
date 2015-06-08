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
	private float escala, escala_total;
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

	private GameObject proxima_barra;
	private Barra_controle proximo_controle;

	// Use this for initialization
	void Start () {
		//escala inicia 0
		escala = 0f;
		escala_total = 0;
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
		if (escala == limite && proxima_barra != null){proximo_controle.setGo(true);}
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

	public float getEscala()
	{
		if (proximo_controle != null) {escala_total = proximo_controle.getEscala();}
		return escala + escala_total;
	}
/*
	public void setEscala(float novaEscala)
	{
		escala = novaEscala;
		if ( escala < limite )
		{
			spriteRenderer.sprite = barra;
			if (proxima_barra != null){proximo_controle.setGo(false);}
		}
	}
*/
	public void setVelocidade(float novaVelocidade)
	{
		velocidade = novaVelocidade;
		if (proxima_barra != null){proximo_controle.setVelocidade(novaVelocidade);}
	}

	public void setGo(bool go){this.go = go;}

	public void setLimite(float novoLimite) {limite = novoLimite;}

	public void novaBarra(int numero_barras, int atual, float x)
	{
		if (atual < numero_barras)
		{
			proxima_barra = (GameObject) Instantiate(Resources.Load("barra"), new Vector3(x, -1.1f, 0), Quaternion.identity);
			proximo_controle = proxima_barra.GetComponent<Barra_controle>();
			proximo_controle.setLimite(limite);
			Debug.Log(proxima_barra.transform.position);
			proximo_controle.novaBarra(numero_barras, atual + 1, (float)(2.47*limite) + proxima_barra.transform.position.x + 0.009f);
		}
	}

	public void attack(float custo)
	{
		if (proximo_controle != null) {escala_total = proximo_controle.getEscala();}
		float nova_escala = custo - escala - escala_total;
		if (nova_escala <= 0){escala = 0;transform.localScale = new Vector2(escala,1.5f);}
		else if (nova_escala >= limite){escala = limite;transform.localScale = new Vector2(escala,1.5f);}
		else {escala = nova_escala;transform.localScale = new Vector2(escala,1.5f);}
		if (proximo_controle != null){proximo_controle.attack(custo);}
		/*
		float novo_custo = custo;
		if (proximo_controle != null)
		{novo_custo = proximo_controle.attack(custo);}

		if ( novo_custo - escala > 0)
		{
			escala = 0; transform.localScale = new Vector2(escala,1.5f);
			spriteRenderer.sprite = barra;
			if (escala == 0) {go = false;}
			return novo_custo - escala;
		}
		else
		{
			escala = escala - novo_custo;
			transform.localScale = new Vector2(escala,1.5f);
			spriteRenderer.sprite = barra;
			if (escala == 0) {go = false;}
			return 0;
		}
		*/
	}
}