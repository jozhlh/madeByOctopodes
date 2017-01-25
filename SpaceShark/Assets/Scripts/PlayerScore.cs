using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    // Holds the player's current score
	private static int score;
    [Header("Scoring Variables")]
	[SerializeField]
    // How many seconds before the score is increased by 1
	private float scoringInterval = 1.0f;
    [SerializeField]
    // How much the score is incremented when the player kills an enemy
    private static int enemyScoreValue = 10;
    [Header("Score UI object")]
	[SerializeField]
    // The UI object which displays the score to the player
	private Text scoreText = null;

    // Progress to the next scoring interval
	private float countdown;

	// Use this for initialization
	void Start ()
    {
		score = 0;
		countdown = scoringInterval;
	}
	
	// Update is called once per frame
	void Update () 
	{
        // Increment score whilst the player is in game
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

    // Resets the score when the player dies
	public static void Reset()
	{
		score = 0;
	}

    // Increments the score when the player kills an enemy
    public static void EnemyKilled()
    {
        score += enemyScoreValue;
    }
}
