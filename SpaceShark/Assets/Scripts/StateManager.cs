using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    // Possible game states
	public enum States {menu, dead, play, tutorial, complete};

    // Current game state
	public static States gameState;

	// Use this for initialization
	void Start ()
	{
        // Set starting game state to menu scene
		gameState = States.menu;
        // Make sure the state manager persists across all scenes
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch ((int)gameState)
		{
			case 0: // menu
			break;
			case 1: // dead
			break;
			case 2: // play
			break;
		}
	}

	public void PlayButtonPressed()
	{
		gameState = States.menu;
        // Name of the scene to load
		SceneManager.LoadScene("prototype_0");
	}
}
