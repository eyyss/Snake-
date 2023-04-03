using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMovement : MonoBehaviour
{
	public float gridSize = 0.5f;
	public float moveSpeed = 300f;
	public float rotateSpeed = 1f;

	private float _MoveSpeed;
	private float _RotateSpeed;


	private SnakeEvent snakeEvent;
	public static event Action OnGameStarted;


	private void Start()
	{
		_MoveSpeed = moveSpeed;
		_RotateSpeed = rotateSpeed;
		StartRandomPosition();
		snakeEvent = GetComponent<SnakeEvent>();
		StartEvent();
	}

	private void StartEvent()
	{
		SnakeEvent.OnGameEnded += SnakeEvent_OnGameEnded;
		OnGameStarted += SnakeEvent_OnGameStarted;
	}

	private void OnDestroy()
	{
		SnakeEvent.OnGameEnded -= SnakeEvent_OnGameEnded;
		OnGameStarted-= SnakeEvent_OnGameStarted;
	}
	public void Move()
	{
		snakeEvent.tails[0].GetComponent<Rigidbody2D>().velocity=
			transform.right* moveSpeed *Time.deltaTime;
	}
	private void Rotate()
	{
		float x = -Input.GetAxis("Horizontal");
		if (x == 0) return;
		snakeEvent.tails[0].transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime * x));
	}
	private void FixedUpdate()
	{
		Move();
		Rotate();
		FrameControl();
	}
	private void Update()
	{
		if (GameManager.instance.stat != GameManager.GameStat.End) return;
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnGameStarted?.Invoke();
		}
	}
	private void FrameControl()
	{

		if (transform.position.x< -21)
		{
			SetPostitonX(20.5f);
		}
		if (transform.position.x > 20.5f)
		{
			SetPostitonX(-21);
		}
		if (transform.position.y < -13.5f)
		{
			SetPostitonY(13.5f);
		}
		if (transform.position.y >13.5f)
		{
			SetPostitonY(-13.5f);
		}

	}
	private void SetPostitonX(float x)
	{
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}
	private void SetPostitonY(float y)
	{
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	private void StartRandomPosition()
	{
		ScreenBounds screenBounds = new ScreenBounds(Camera.main);
		Vector2 bounds = screenBounds.GetBounds();
		transform.position = new Vector3
		{
			x = UnityEngine.Random.Range(-bounds.x, bounds.x),
			y = UnityEngine.Random.Range(-bounds.y, bounds.y),
			z = 1f
		};
	}
	private void SnakeEvent_OnGameEnded()
	{
		moveSpeed = 0f;
		rotateSpeed = 0f;
		GetComponent<Renderer>().enabled = false;
	}

	private void SnakeEvent_OnGameStarted()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
