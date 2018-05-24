﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour {
	public RoundData[] allRoundData;
	public PlayerProgress playerProgress;
	private string gameDataFileName = "data.json";

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		LoadGameData ();
		LoadPlayerProgress ();
		SceneManager.LoadScene("MenuScreen");
	}

	public RoundData GetCurrentRoundData() {
		return allRoundData [0];
	}

	public void SubmitNewPlayerScore(int newScore) {
		if (newScore > playerProgress.highestScore) {
			playerProgress.highestScore = newScore;
			SavePlayerProgress ();
		}
	}

	public int GetHighestPlayerScore () {
		return playerProgress.highestScore;
	}

	private void LoadPlayerProgress() {
		playerProgress = new PlayerProgress ();
		if (PlayerPrefs.HasKey ("highestScore")) {
		}
	}
	private void SavePlayerProgress () {
		PlayerPrefs.SetInt ("highestScore", playerProgress.highestScore);
	}

	private void LoadGameData () {
		string filePath = Path.Combine (Application.streamingAssetsPath, gameDataFileName);
		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			GameData loadedData = JsonUtility.FromJson<GameData> (dataAsJson);
			allRoundData = loadedData.allRoundData;
		} else {
			Debug.LogError ("Cannot load game data!");
		}
	}
}