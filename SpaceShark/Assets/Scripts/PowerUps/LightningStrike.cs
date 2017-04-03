using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : PowerUp
{
	[SerializeField]
	private float duration = 5.0f;

	[SerializeField]
	private float speed = GameSettings.gameSpeed * 1.5f;

	[SerializeField]
	private GameObject energyField = null;

	public override void Activate()
    {
        base.Activate();
		Vector3 fieldSpawnPos = new Vector3(0, 0, -30);
		GameObject field = Instantiate(energyField, Ship_Movement.shipPosition + fieldSpawnPos, transform.rotation);
		//field.GetComponent<EnergyField>().SetMoveSpeed(speed);
		Destroy(field, duration);
		Debug.Log("Lightning Strike Fired Active");
    }
}
