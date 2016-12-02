using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

	public enum States {menu, dead, play, tutorial};

	public static States gameState;

	[SerializeField]
	//private Scene menuScene;


	// Use this for initialization
	void Start ()
	{
		gameState = States.menu;
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
		SceneManager.LoadScene("prototype_0");
	}
}
