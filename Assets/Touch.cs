using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour {
	Vector2 position;
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			position = Input.GetTouch(Input.touches.Length -1).position;
			if (position.x < 128 && position.y < 80) {Application.Quit();}
		}
	}
/*
	void OnGUI() {
		GUI.color = Color.white;
		GUI.Label(new Rect(450, 170, 300, 50), position.x+" "+position.y);
	}
*/
}
