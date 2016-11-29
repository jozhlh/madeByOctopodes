using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public void LoadPrototypeLevel()
	{
		SceneManager.LoadScene("prototype_0");
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("menu_scene");
	}
}
