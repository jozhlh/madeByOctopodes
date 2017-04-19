using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoost : PowerUp 
{
	[SerializeField]
	private int scoreMultiplier = 2;

    public override void Activate()
    {
        base.Activate();
        player.GetComponent<PlayerScore>().ScoreBoost(scoreMultiplier, totalDuration);
		Debug.Log("Score Boost Active");
    }
}