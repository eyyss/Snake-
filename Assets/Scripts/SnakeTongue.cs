using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class SnakeTongue : MonoBehaviour
{
	public GameObject snakeTongue;
	public Food[] targets;

	public float minDistance;
	public float rotationModifier;
	public float rotateSpeed;

	Transform GetClosestFood(Food[] enemies)
	{
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (Food t in enemies)
		{
			float dist = Vector3.Distance(t.transform.position, currentPos);
			if (dist < minDist)
			{
				tMin = t.transform;
				minDist = dist;
			}
		}
		return tMin;
	}

	private void FixedUpdate()
	{
		targets = FindObjectsOfType<Food>();
		Transform targetTransform = GetClosestFood(targets);
		float distance = Vector2.Distance(transform.position, targetTransform.position);
		if (distance<minDistance&&AngleControl(targetTransform))
		{
			snakeTongue.SetActive(true);
			Rotate(targetTransform);
		}
		else
		{
			snakeTongue.SetActive(false);
		}
	}

	private void Rotate(Transform target)
	{
		Vector3 vectorTotarget =target.position- transform.position;
		float angle = Mathf.Atan2(vectorTotarget.y,vectorTotarget.x)*Mathf.Rad2Deg-rotationModifier;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		snakeTongue.transform.rotation =Quaternion.Slerp(snakeTongue.transform.rotation, q,Time.deltaTime*rotateSpeed);
	}
	private bool AngleControl(Transform target)
	{
		float angle = Vector2.Angle(transform.position, target.position);
		if (angle>20)
		{
			return false;
		}
		return true;
	}
}
