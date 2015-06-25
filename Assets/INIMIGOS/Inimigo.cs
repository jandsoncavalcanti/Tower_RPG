using UnityEngine;
using System.Collections;

public interface Inimigo
{
	void recebe_dano(int dano);
	void foi_selecionado();
	void foi_deselecionado();
}
