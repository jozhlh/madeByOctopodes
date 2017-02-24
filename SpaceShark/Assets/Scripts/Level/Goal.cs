using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

[SerializeField]
    // Reference to the sound manager in the scene
    private GameObject soundManager;

    // When player reaches this, Level is complete
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			StateManager.gameState = StateManager.States.complete;
			// Stop engine sounds
			soundManager.GetComponent<SoundManager>().StopEvent("shipEngine", 0, gameObject);
		}
	}
}
