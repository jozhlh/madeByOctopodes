using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public void LoadPrototypeLevel()
	{
		SceneManager.LoadScene("prototype_1");
		StateManager.gameState = StateManager.States.play;
	}

	public void LoadTutorialLevel()
	{
		SceneManager.LoadScene("prototype_0");
		StateManager.gameState = StateManager.States.tutorial;
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("menu_scene");
		StateManager.gameState = StateManager.States.menu;
	}
}
