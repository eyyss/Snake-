using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEvent : MonoBehaviour
{
	public static SnakeEvent instance;

	public GameObject tailPrefab;
	public List<GameObject> tails;
	public int tailSize;

	public static event Action OnGameEnded;

	private void Awake()
	{
		instance = this;
	}

	void Start()
    {
		AddEvent();
		tails.Add(gameObject);
    }

	private void AddEvent()
	{
		Food.OnFoodEat += SnakeEvent_OnFoodEat;
	}

	private void OnDestroy()
	{
		Food.OnFoodEat -= SnakeEvent_OnFoodEat;
	}

	private void SnakeEvent_OnFoodEat()
	{
		// kuyruk olustur
		GameObject tail = Instantiate(tailPrefab);
		tails.Add(tail);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("SnakeTail"))
		{
			OnGameEnded?.Invoke();
		}
	}
}
