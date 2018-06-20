using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public SimpleObjectPool answerButtonObjectPool;
	public Text questionDisplayText;
	public Text scoreDisplayText;
	public Transform answerButtonParent;

	public GameObject questionDisplay;
	public GameObject roundEndDisplay;
	public GameObject audioButton;
	public Text highScoreDisplay;

	private DataController dataController;
	private Data currentRoundData;
	private AudioData[] audioPool;
	private AudioSource audioSource;

	private bool isRoundActive;
	private int questionIndex;
	private int playerScore;
	private List<GameObject> answerButtonGameObjects = new List<GameObject>();

	void Start () {
		dataController = FindObjectOfType<DataController> ();
		currentRoundData = dataController.GetCurrentRoundData ();
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
			ScoreCalculator ();
				
		}

		if (audioPool.Length > questionIndex + 1) {
			questionIndex++;
			ShowQuestion ();
		} else 
		{
			EndRound ();
		}
	}

	public void ScoreCalculator () {
		//calculo
		if (playerScore == 0) {
		}

		else if	(playerScore == 1) {
		}
		else if (playerScore == 2) {
		}
		else if (playerScore == 3) {
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
}