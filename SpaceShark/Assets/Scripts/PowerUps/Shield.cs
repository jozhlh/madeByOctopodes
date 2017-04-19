using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp 
{
    public override void Activate()
    {
        base.Activate();
        player.GetComponent<Ship_Movement>().SetSheild();
        soundManager.PlayEvent("life", player);
    }
}
