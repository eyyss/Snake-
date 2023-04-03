using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	public static event Action OnFoodEat;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("SnakeTongue"))
		{
			OnFoodEat?.Invoke();
			Destroy(gameObject);
		}
	}

}
