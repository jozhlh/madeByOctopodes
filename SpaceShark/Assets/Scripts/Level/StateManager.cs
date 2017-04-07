using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    // Possible game states
	public enum States {menu, dead, play, tutorial, complete};

    // Current game state
	public static States gameState;

	[SerializeField]
	private bool testing = true;

	// Use this for initialization
	void Start ()
	{
        // Set starting game state to menu scene
		/*if (testing)
		{
			gameState = States.play;
		}
		else
		{
			gameState = States.menu;
        }*/
		gameState = States.menu;
       
        //gameState = States.play;
        // Make sure the state manager persists across all scenes
        //DontDestroyOnLoad(gameObject);
	}

	public void SetToPlay()
	{
		gameState = States.play;
	}
}
