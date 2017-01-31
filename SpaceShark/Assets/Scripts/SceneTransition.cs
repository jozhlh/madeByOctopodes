using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public void LoadPrototypeLevel()
	{
		SceneManager.LoadScene("prototype_Lava");
		StateManager.gameState = StateManager.States.play;
	}

	public void LoadTutorialLevel()
	{
        StateManager.gameState = StateManager.States.tutorial;
        SceneManager.LoadScene("prototype_Tutorial");

	}

    // Load menu and destroy state manager, as it will persist otherwise, duplicating
	public void LoadMenu()
	{
		Destroy(GameObject.FindGameObjectWithTag("StateManager"));
		SceneManager.LoadScene("menu_scene");
	}

	public void LoadLevel(string targetLevel)
	{
        StateManager.gameState = StateManager.States.play;
        SceneManager.LoadScene(targetLevel);
	}
}
