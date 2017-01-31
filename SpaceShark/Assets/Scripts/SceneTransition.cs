﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public void LoadPrototypeLevel()
	{
		SceneManager.LoadScene("prototype_Josh");
		StateManager.gameState = StateManager.States.play;
	}

	public void LoadTutorialLevel()
	{
		SceneManager.LoadScene("prototype_0");
		StateManager.gameState = StateManager.States.tutorial;
	}

    // Load menu and destroy state manager, as it will persist otherwise, duplicating
	public void LoadMenu()
	{
		Destroy(GameObject.FindGameObjectWithTag("StateManager"));
		SceneManager.LoadScene("menu_scene");
	}

	public void LoadLevel(string targetLevel)
	{
		SceneManager.LoadScene(targetLevel);
		StateManager.gameState = StateManager.States.play;
	}
}
