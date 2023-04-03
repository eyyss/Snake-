using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
	public TextMeshPro ScoreText;
	public TextMeshPro GameOverText;
	public TextMeshPro PressSpaceToStartText;
	public TextMeshPro CurrentAndHighScoreText;
	private void Awake()
	{
        Instance = this;
	}

	private void Start()
	{
		ScoreManager.OnScoreChanged += ScoreManager_OnScoreChanged;
		SnakeEvent.OnGameEnded += SnakeEvent_OnGameEnded;
	}

	private void OnDestroy()
	{
		ScoreManager.OnScoreChanged -= ScoreManager_OnScoreChanged;
		SnakeEvent.OnGameEnded -= SnakeEvent_OnGameEnded;
	}
	private void ScoreManager_OnScoreChanged(int currentScore)
	{
		ScoreText.text = currentScore.ToString();
	}

	private void SnakeEvent_OnGameEnded()
	{
		UpdateCurrentAndHighScoreText();
		CurrentAndHighScoreText.gameObject.SetActive(true);
		ScoreText.gameObject.SetActive(false);
		GameOverText.gameObject.SetActive(true);
		PressSpaceToStartText.gameObject.SetActive(true);
	}

	private void UpdateCurrentAndHighScoreText()
	{
		int currentScore = Convert.ToInt32(ScoreText.text);
		if (PlayerPrefs.HasKey("HighScore"))
		{
			if (currentScore>PlayerPrefs.GetInt("HighScore"))
			{
				PlayerPrefs.SetInt("HighScore",currentScore);
			}
		}
		else
		{
			PlayerPrefs.SetInt("HighScore", currentScore);
		}
		CurrentAndHighScoreText.text = "Score: " + currentScore.ToString() + " HighScore: " + PlayerPrefs.GetInt("HighScore");
	}
}
