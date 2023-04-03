using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	private Vector2 screenBounds;
	public GameObject food;
	private void Start()
	{
		StartEvent();
		screenBounds = new ScreenBounds(Camera.main).GetBounds();
		SpawnFood();
	}

	private void StartEvent()
	{
		Food.OnFoodEat += Food_OnFoodEat;
	}

	private void OnDestroy()
	{
		Food.OnFoodEat -= Food_OnFoodEat;
	}

	private void Food_OnFoodEat()
	{
		SpawnFood();
	}
	private void SpawnFood()
	{
		float x = UnityEngine.Random.Range(-screenBounds.x, screenBounds.x);
		float y = UnityEngine.Random.Range(-screenBounds.y, screenBounds.y);
		Instantiate(food, new Vector3(x, y), Quaternion.identity);
	}

}
