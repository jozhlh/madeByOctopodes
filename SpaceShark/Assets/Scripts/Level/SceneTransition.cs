using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	[SerializeField]
	private ScreenManager screenManager = null;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
	   screenManager = GameObject.Find("ScreenManager").GetComponent<ScreenManager>();
	}

	public void LoadScreenManagerLevel(string levelName)
	{
		screenManager.gameObject.GetComponent<StateManager>().SetToLoadLevel();
		screenManager.LoadScene(levelName);
	}

	public void LoadScreenManagerMenu()
	{
		screenManager.gameObject.GetComponent<StateManager>().SetToLoadMenu();
		screenManager.LoadScene("splash_scene");
	}
	/* 
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
        //SceneManager.LoadScene(targetLevel);
        SceneManager.LoadSceneAsync(targetLevel);
	}*/
}
