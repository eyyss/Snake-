using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public enum GameStat { Play,End}
	public GameStat stat;

	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		StartEvent();
		stat = GameStat.Play;
	}

	private void StartEvent()
	{
		SnakeEvent.OnGameEnded += SnakeEvent_OnGameEnded;
	}
	
	private void SnakeEvent_OnGameEnded()
	{
		stat = GameStat.End;
	}




	private void OnDestroy()
	{
		SnakeEvent.OnGameEnded -= SnakeEvent_OnGameEnded;
	}
}
