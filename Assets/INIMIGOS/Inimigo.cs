using UnityEngine;
using System.Collections;

public interface Inimigo
{
	void recebe_dano(int dano);
	bool foi_selecionado();
	void foi_deselecionado();
	void ataca();
	void criar_HP_interface(Vector2 posicao);
	bool getAtivo();
	void foi_defendido();
	void ataque_defendido();
}
