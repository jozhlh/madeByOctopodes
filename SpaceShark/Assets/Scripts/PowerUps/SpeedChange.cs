using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : PowerUp 
{
	[SerializeField]
	private float speedMultiplier = 2.0f;

    public override void Activate()
    {
        base.Activate();
        player.GetComponent<Ship_Movement>().ChangeSpeed(speedMultiplier, totalDuration);
		Debug.Log("Speed Change Active");
    }
}
