using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour {

    [Header("References To UI Objects")]
	[SerializeField]
	private GameObject deathUI;
	[SerializeField]
	private GameObject victoryUI;

	// Update is called once per frame
	void Update ()
	{
		switch (StateManager.gameState)
		{
            // If the player is dead show death UI
			case StateManager.States.dead:
				deathUI.SetActive(true);
				break;
            // If the player has won show complete UI
            case StateManager.States.complete:
				victoryUI.SetActive(true);
				break;
            // If the player has not died or won, show no UI overlay
            default:
				if (deathUI.activeInHierarchy)
				{
					deathUI.SetActive(false);
				}
				if (victoryUI.activeInHierarchy)
				{
					victoryUI.SetActive(false);
				}
				break;
		}
	}
}
