using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

[SerializeField]
    // Reference to the sound manager in the scene
    private GameObject soundManager;
    private StateManager state = null;

    void Start()
    {
        state = GameObject.Find("ScreenManager").GetComponent<StateManager>();
    }

    void Update()
    {
        float levelProgress = Ship_Movement.shipPosition.z / transform.position.z;

        // Send level progress RTPC to wwise 
        //soundManager.GetComponent<SoundManager>().SetLevelProgress(gameObject, levelProgress); 
    }

    // When player reaches this, Level is complete
    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			state.SetToComplete();
			// Stop engine sounds
			soundManager.GetComponent<SoundManager>().StopEvent("shipEngine", 0, gameObject);
		}
	}
}
