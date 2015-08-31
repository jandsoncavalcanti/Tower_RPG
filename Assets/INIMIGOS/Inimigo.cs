using UnityEngine;
using System.Collections;

public interface Inimigo
{
	void send_skull();
	void damage();
	void resetAttack();
	void resetHit();
	bool getAtivo();
	bool seleciona();
	void deseleciona();
	void criar_HP_interface(Vector2 posicao);
	InimigoBehaviour getBehaviour();
	float getLimiteRelogio();
	float getCaveiraIntervalo ();
	int getNumeroVidas ();
}
