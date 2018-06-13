using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour {

	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = FindObjectOfType<GameController> ();
	}


	public void HandleClick() {
		gameController.AudioButtonClicked ();
	}
}