using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    // When player reaches this, Level is complete
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			StateManager.gameState = StateManager.States.complete;
		}
	}
}
