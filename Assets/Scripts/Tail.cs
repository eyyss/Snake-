using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
	public GameObject targetTail;
	public float followDistanceRate=.2f;
	public float followSpeed = 10f;
	public float rotateSpeed = 7f;
	private void Start()
	{
		var tails = SnakeEvent.instance.tails;
		SnakeEvent.instance.tailSize += 1;
		targetTail = tails[SnakeEvent.instance.tailSize];
		transform.position = tails[SnakeEvent.instance.tailSize].transform.position;
		SnakeEvent.OnGameEnded += SnakeEvent_OnGameEnded;
	}
	private void OnDestroy()
	{
		SnakeEvent.OnGameEnded -= SnakeEvent_OnGameEnded;
	}

	private void SnakeEvent_OnGameEnded()
	{
		GetComponent<Renderer>().enabled = false;
	}

	private void LateUpdate()
	{
		if (Vector2.Distance(transform.position,targetTail.transform.position)>followDistanceRate)
		{
			transform.position = Vector3.Slerp(transform.position, targetTail.transform.position, Time.deltaTime * followSpeed);
			transform.right = Vector3.Slerp(transform.right, targetTail.transform.right, Time.deltaTime * rotateSpeed);
		}
	}
}
