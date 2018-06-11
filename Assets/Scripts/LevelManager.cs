using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	
	public void StartOhmanhe() {
		SceneManager.LoadScene ("Ohmanhe");
	}

	public void StartTurishmo () {
		SceneManager.LoadScene ("Turishmo");
	}

	public void StartInglesh () {
		SceneManager.LoadScene ("Inglesh");
	}

	public void StartManezes () {
		SceneManager.LoadScene ("Manezes");
	}

	public void StartMenuTemas () {
		SceneManager.LoadScene ("MenuTemas");
	}
}
