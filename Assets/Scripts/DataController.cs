using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour {
	public Data[] allData;

	void Start () {
		DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene("Jogo");
	}
		
	public Data GetCurrentRoundData() {
		return allData [0];
	}
}