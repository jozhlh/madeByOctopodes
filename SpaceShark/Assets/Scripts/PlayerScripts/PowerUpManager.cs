using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{

	[SerializeField]
	private GameObject powerUpUi = null;
	[SerializeField]
	private GameObject powerUpIcon = null;
	[SerializeField]
	private PowerUp[] availablePowerUps;

	private int equippedPowerUp = -1;

	// Use this for initialization
	void Start () {
		availablePowerUps = GetComponents<PowerUp>();
		powerUpUi.SetActive(false);
		equippedPowerUp = -1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void SelectPowerUp()
	{
		if (equippedPowerUp < 0)
		{
			int chosenPowerUp = Random.Range(0, availablePowerUps.Length);
			availablePowerUps[chosenPowerUp].GiveToPlayer();
			chosenPowerUp = 2;
			equippedPowerUp = chosenPowerUp;
			powerUpUi.SetActive(true);
			powerUpIcon.GetComponent<Image>().sprite = availablePowerUps[chosenPowerUp].uiIcon;
		}
	}

	public void UseEquippedPowerUp()
	{
		if (equippedPowerUp > -1)
		{
			availablePowerUps[equippedPowerUp].Activate();
			equippedPowerUp = -1;
			powerUpUi.SetActive(false);
		}
	}
}
