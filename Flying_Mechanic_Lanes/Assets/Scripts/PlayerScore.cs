using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

	public static int score;

	[SerializeField]
	private float scoringInterval = 1.0f;

	[SerializeField]
	private Text scoreText = null;

	private float countdown;

	// Use this for initialization
	void Start () {
	
		score = 0;
		countdown = scoringInterval;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (StateManager.gameState == StateManager.States.play)
		{
			countdown -= Time.deltaTime;
			if (countdown < 0)
			{
				score++;
				countdown = scoringInterval;
			}
			scoreText.text = score.ToString();
		}
	}

	public static void Reset()
	{
		score = 0;
	}
}
