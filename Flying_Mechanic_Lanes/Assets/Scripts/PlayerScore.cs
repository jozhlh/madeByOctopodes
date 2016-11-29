using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

	public static int score;

	[SerializeField]
	private float scoringInterval = 50.0f;

	private float countdown;

	// Use this for initialization
	void Start () {
	
		score = 0;
		countdown = scoringInterval;
	}
	
	// Update is called once per frame
	void Update () 
	{
		countdown -= Time.deltaTime;
		if (countdown < 0)
		{
			score++;
			countdown = scoringInterval;
		}
	}
}
