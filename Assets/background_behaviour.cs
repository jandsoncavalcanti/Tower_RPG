using UnityEngine;
using System.Collections;

public class background_behaviour : MonoBehaviour {
	bool dado;
	int valor;

	// Use this for initialization
	void Start () {
		dado = false;
		valor = 0;
		print(Screen.width+" "+Screen.height);
		transform.localScale = new Vector3 (Screen.width/349.6f, Screen.height/233f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (dado) {
			valor++;
		}
		if (valor > 6) {
			valor = 0;
		}
	}

	private void OnMouseDown()
	{	
		dado = !dado;
		if (dado) {
			print(valor);
		}
	}
}
