using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongFloat : MonoBehaviour
{
	//[SerializeField]
	private float speed = 0.50f;
	//[SerializeField]
	private float deviance = 2.0f;

	private Vector3 currentPos;
	private Vector3 targetPos;
	private Vector3 upPos;
	private Vector3 downPos;

	private float counter;
	private bool movingUp = true;
	// Use this for initialization
	void Start ()
	{
		currentPos = gameObject.transform.localPosition;
		//currentPos = centrePos;
		upPos = currentPos;
		upPos.y += deviance;
		downPos = currentPos;
		downPos.y -= deviance;
		targetPos = upPos;

		counter = 0.5f * deviance;
	}
	
	// Update is called once per frame
	void Update ()
	{/* 
		currentPos = gameObject.transform.localPosition;
		if (currentPos.y != targetPos.y)
		{
			currentPos.y = Mathf.Lerp(currentPos.y, targetPos.y, speed * Time.deltaTime);
		}
		else
		{
			if (targetPos.y == upPos.y)
			{
				targetPos.y = downPos.y;
			}
			else
			{
				targetPos.y = upPos.y;
			}
		}
		gameObject.transform.localPosition = currentPos;
		*/
		currentPos = gameObject.transform.position;
		
		
		if (movingUp)
		{
			currentPos.y += speed * Time.deltaTime;
		}
		else
		{
			currentPos.y -= speed * Time.deltaTime;
		}

		if (counter < 0)
		{
			if (movingUp)
			{
				movingUp = false;
			}
			else
			{
				movingUp = true;
			}
			counter = deviance;
		}

		gameObject.transform.position = currentPos;

		counter -= Time.deltaTime;
	}
}
