using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	//states of the game
	public enum GameState { Menu, Playing, Paused, GameOver }
	private GameState currentState;

	//simpleobjectpool
	public SimpleObjectPool answerButtonObjectPool;

	//canvas
	public Text questionDisplayText;
	public Text scoreDisplayText;
	public Text highScoreDisplay;
	public GameObject backToGameIcon;
	public Transform answerButtonParent;
	public GameObject questionDisplay;
	public GameObject roundEndDisplay;
	public GameObject audioButton;
	public Image pauseMenu;

	//data
	private DataController dataController;
	private Data currentRoundData;
	private AudioData[] audioPool;
	private AudioSource audioSource;
	private bool isRoundActive;
	private int questionIndex;
	private int playerScore;
	private List<GameObject> answerButtonGameObjects = new List<GameObject>();
	private GameSettings gameSettings;

	void Start () {
		dataController = FindObjectOfType<DataController> ();
		currentRoundData = dataController.GetCurrentRoundData ();
		gameSettings = GameObject.FindGameObjectWithTag ("GameSettings").GetComponent<GameSettings> ();

		audioPool = currentRoundData.audios;
		playerScore = 0;
		questionIndex = 0;
		ShowQuestion ();
		isRoundActive = true;
	}

	void Update () {
		
	}
		
	private void ShowQuestion() {
		RemoveAnswerButtons ();
		AudioData questionData = audioPool [questionIndex];

		audioSource = audioButton.GetComponent<AudioSource>();
		audioSource.clip = questionData.questionAudio;
		audioSource.Play ();

		for (int i = 0; i < questionData.answers.Length; i++)
		{
			GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
			answerButtonGameObjects.Add(answerButtonGameObject);
			answerButtonGameObject.transform.SetParent(answerButtonParent);

			AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
			answerButton.transform.localScale.Set (1, 1, 1);
			answerButton.Setup(questionData.answers[i]);
		}
	}

	private void RemoveAnswerButtons() {
		while (answerButtonGameObjects.Count > 0) 
		{
			answerButtonObjectPool.ReturnObject (answerButtonGameObjects [0]);
			answerButtonGameObjects.RemoveAt (0);
		}
	}
	public void AnswerButtonClicked(bool isCorrect) {
		if (isCorrect) 
		{
			playerScore += currentRoundData.pointsAddedForCorrectAnswer;
			scoreDisplayText.text = "Score: " + playerScore.ToString ();				
		}

		if (audioPool.Length > questionIndex + 1) {
			questionIndex++;
			ShowQuestion ();
		} else 
		{
			EndRound ();
		}
	}

	public void AudioButtonClicked () {
		audioSource.Play ();
	}

	public void EndRound() {
		isRoundActive = false;
		questionDisplay.SetActive (false);
		roundEndDisplay.SetActive (true);
	}

	public void PauseButton () {
		currentState = GameState.Paused;
		//pauseMenu.transform.Find ("PauseButton").GetComponent<Image> ().sprite = gameSettings.buttonPause;
		if (currentState == GameState.Paused) {
			pauseMenu.enabled = true;
		}
	}

	public void ReturnGameButton () {
		currentState = GameState.Playing;
		if (currentState == GameState.Playing) {
			pauseMenu.enabled = false;
		}
	}
}