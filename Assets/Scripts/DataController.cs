using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour {
	public Data[] allData;

	void Start () {
		DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene("MenuInicial");
	}
		
	public Data GetCurrentRoundData() {
		return allData [0];
	}
}