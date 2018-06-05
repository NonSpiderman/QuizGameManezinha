using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	
	public void StartOmanhe() {
		SceneManager.LoadScene ("Omanhe");
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
}
