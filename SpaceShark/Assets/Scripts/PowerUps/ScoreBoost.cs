using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoost : PowerUp 
{
	[SerializeField]
	private float scoreMultiplier = 2.0f;

    public override void Activate()
    {
        base.Activate();
        player.GetComponent<PlayerScore>().ScoreBoost(scoreMultiplier, totalDuration);
		Debug.Log("Score Boost Active");
    }
}