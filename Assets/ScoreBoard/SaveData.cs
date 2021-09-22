using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
	public Text myScore;
	public Text myName;
	public int currentScore;
	
    
	public void SendScore()
	{
		if (currentScore> PlayerPrefs.GetInt("highscore"))
		{
			PlayerPrefs.SetInt("highscore",currentScore);
			HighScores.UploadScore(myName.text,currentScore);
		}
	}
}
