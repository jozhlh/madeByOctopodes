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
    [SerializeField]
    private Text multiplierText = null;



    // Progress to the next scoring interval
	private float countdown;

	// Track score multiplier
	private static int boostMultiplier = 1;
	private int baseMultiplier = 1;
    private static int scoreMultiplier = 1;
	private static bool boosted = false;
    private static bool scoreMutiplying = false;
	private float boostCountdown = 0.0f;
	private StateManager state = null;

	// Use this for initialization
	void Start ()
    {
		score = 0;
		countdown = scoringInterval;
		state = GameObject.Find("ScreenManager").GetComponent<StateManager>();
		scoreText.text = "";
        multiplierText.text = "";
        scoreMultiplier = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
        // Increment score whilst the player is in game
		if (state.GetState() == StateManager.States.play)
		{
			countdown -= Time.deltaTime;
			if (countdown < 0)
			{
                score += 1 * scoreMultiplier;
				countdown = scoringInterval;
			}
			scoreText.text = score.ToString();
            multiplierText.text = ("X " + scoreMultiplier.ToString());
		}

		if (boosted)
		{
			boostCountdown -= Time.deltaTime;
			if (boostCountdown < 0)
			{
				boosted = false;
				boostMultiplier = baseMultiplier;
			}
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
        if (boosted)
        {
            score += enemyScoreValue * scoreMultiplier * boostMultiplier;
        }
        else
        {
            score += enemyScoreValue * scoreMultiplier;
        }
        scoreMutiplying = true;
        scoreMultiplier += 1;
        Debug.Log(scoreMultiplier);
    }

	public void ScoreBoost(int boostVal, float boostDuration)
	{
		boosted = true;
		boostMultiplier = boostVal;
		boostCountdown = boostDuration;
	}

    public static void BulletMissed()
    {
        scoreMutiplying = false;
        scoreMultiplier = 1;
        Debug.Log(scoreMultiplier);
    }
}
