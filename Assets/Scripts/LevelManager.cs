using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	void Start () {
	}

	public void StartCreditos () {
		SceneManager.LoadScene ("Creditos");
	}

	public void StartGame () {
		SceneManager.LoadScene ("Game");
	}

	public void ReturnToMenu () {
		SceneManager.LoadScene ("Menu");
	}
}