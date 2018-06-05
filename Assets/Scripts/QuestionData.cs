using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData 
{
	
	public Type questionType;
	public string questionText;
	public AudioClip questionAudio;

	public AnswerData[] answers;
}
