using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour {

	[SerializeField]
	private GameObject deathUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (StateManager.gameState)
		{
			case StateManager.States.dead:
				deathUI.SetActive(true);
				break;
			default:
				if (deathUI.active)
				{
					deathUI.SetActive(false);
				}
				break;
		}
	}
}
