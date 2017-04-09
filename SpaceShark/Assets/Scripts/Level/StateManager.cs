using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    // Possible game states
	public enum States {menu, dead, play, tutorial, complete};

    // Current game state
	private States gameState;

	[SerializeField]
	private bool testing = true;

	// Use this for initialization
	void Start ()
	{
        // Set starting game state to menu scene
		if (testing)
		{
			SetToPlay();
		}
		else
		{
			SetToMenu();
        }
		//gameState = States.menu;
       
        //gameState = States.play;
        // Make sure the state manager persists across all scenes
        //DontDestroyOnLoad(gameObject);
	}

	
	void Update()
	{
		//Debug.Log(GetState());
	}

	public void SetToPlay()
	{
		Debug.Log("Play Set");
		gameState = States.play;
	}

	public void SetToMenu()
	{
		gameState = States.menu;
	}
	
	public void SetToComplete()
	{
		gameState = States.complete;
	}

	public void SetToDead()
	{
		gameState = States.dead;
	}

	public States GetState()
	{
		return gameState;
	}
}
