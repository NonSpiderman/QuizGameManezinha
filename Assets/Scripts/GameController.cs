using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public SimpleObjectPool answerButtonObjectPool;
	public Text questionDisplayText;
	public Text scoreDisplayText;
	public Text timeRemainingDisplayText;
	public Transform answerButtonParent;

	public GameObject questionDisplay;
	public GameObject roundEndDisplay;
	public GameObject audioButton;
	public Text highScoreDisplay;

	private DataController dataController;
	private RoundData currentRoundData;
	private QuestionData[] questionPool;
	private AudioSource audioSource;

	private bool isRoundActive;
	private float timeRemaining;
	private int questionIndex;
	private int playerScore;
	private List<GameObject> answerButtonGameObjects = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		dataController = FindObjectOfType<DataController> ();
		currentRoundData = dataController.GetCurrentRoundData ();
		questionPool = currentRoundData.questions;
		timeRemaining = currentRoundData.timeLimitInSecond;
		UpdateTimeRemainingDisplay ();

		playerScore = 0;
		questionIndex = 0;

		ShowQuestion ();
		isRoundActive = true;

	}
		
	private void ShowQuestion()
	{
		RemoveAnswerButtons ();
		QuestionData questionData = questionPool [questionIndex];

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

	private void RemoveAnswerButtons()
	{
		while (answerButtonGameObjects.Count > 0) 
		{
			answerButtonObjectPool.ReturnObject (answerButtonGameObjects [0]);
			answerButtonGameObjects.RemoveAt (0);
		}
	}
	public void AnswerButtonClicked(bool isCorrect)
	{
		if (isCorrect) 
		{
			playerScore += currentRoundData.pointsAddedForCorrectAnswer;
			scoreDisplayText.text = "Score: " + playerScore.ToString ();
			timeRemaining += currentRoundData.addedTimeForCorrectAnswer;

		}

		if (questionPool.Length > questionIndex + 1) {
			questionIndex++;
			ShowQuestion ();
		} else 
		{
			EndRound ();
		}

	}

	public void AudioButtonClicked ()
	{ 
		audioSource.Play ();
	}

	public void EndRound()
	{
		isRoundActive = false;
		dataController.SubmitNewPlayerScore (playerScore);
		highScoreDisplay.text = dataController.GetHighestPlayerScore ().ToString ();

		questionDisplay.SetActive (false);
		roundEndDisplay.SetActive (true);
	}

	public void ReturnToMenu ()
	{
		SceneManager.LoadScene ("MenuScreen");
	}


	private void UpdateTimeRemainingDisplay()
	{
		timeRemainingDisplayText.text = Mathf.Round (timeRemaining).ToString ();
	}
	// Update is called once per frame
	void Update ()
	{
		if (isRoundActive) 
		{
			timeRemaining -= Time.deltaTime;
			UpdateTimeRemainingDisplay ();

			if (timeRemaining <= 0f) 
			{
				EndRound ();
			}

		}

	}
}