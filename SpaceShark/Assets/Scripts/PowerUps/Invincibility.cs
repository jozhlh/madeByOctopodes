using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUp 
{
    public override void Activate()
    {
        base.Activate();
        player.GetComponent<Ship_Movement>().SetInvincible(totalDuration);
		Debug.Log("Invincibility Active");
    }
}