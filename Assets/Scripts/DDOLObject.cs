using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLObject : MonoBehaviour {

	// Static variables
	// > Por que essa variável _tem_ que ser estática?
	// O que aconteceria se ela não fosse static.
	private static DDOLObject _instance;

	// Use this for initialization
	void Awake () {
		if (!_instance) {
			_instance = this;
		} else {
			// > Por que esse Destroy tá aqui?
			// O que ocorre se não tiver o Destroy nesse pedaço do código?
			Destroy (this.gameObject);
		}

		// > Por que o DontDestroyOnLoad tá no Awake?
		// O que acontece se ele não for usado?
		DontDestroyOnLoad (this.gameObject);
	}
}
