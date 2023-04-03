using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	private int score;

	public static event Action<int> OnScoreChanged;

	private void Start()
	{
		Food.OnFoodEat += Food_OnFoodEat;
	}


	private void OnDestroy()
	{
		Food.OnFoodEat -= Food_OnFoodEat;
	}
	private void Food_OnFoodEat()
	{
		score = score += 10;
		OnScoreChanged?.Invoke(score);
	}
}
