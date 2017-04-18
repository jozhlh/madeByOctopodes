using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
	[SerializeField]
	private Fader m_blackScreenCover;
	[SerializeField]
	private CanvasFader m_canvasCover;
	[SerializeField]
	private float m_minDuration = 3.0f;

	private StateManager state = null;
	
	void Awake()
	{
		//StartCoroutine(m_canvasCover.FadeOut());
		state = GetComponent<StateManager>();
	}

	void Update()
	{/* 
		if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine(LoadSceneAsync("GameScreen"));
		}*/
	}

	public void LoadScene(string sceneName)
	{
		if (state.GetState() != StateManager.States.loadMenu)
		{
			state.SetToLoadLevel();
		}
		StartCoroutine(LoadSceneAsync(sceneName));
	}
	
	public IEnumerator LoadSceneAsync(string sceneName)
	{
		if (m_canvasCover)
		{
			m_canvasCover.gameObject.SetActive(true);
			StartCoroutine(m_canvasCover.FadeIn());
		}
		// Fade to black
		yield return StartCoroutine(m_blackScreenCover.FadeIn());
		
		// Load loading screen
		yield return SceneManager.LoadSceneAsync("LoadingScene");
		
		// !!! unload old screen (automatic)
		
		// Fade to loading screen
		yield return StartCoroutine(m_blackScreenCover.FadeOut());
		
		float endTime = Time.time + m_minDuration;
		
		Debug.Log("Showing Loading Screen");

		// Load level async
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		
		
		while (Time.time < endTime)
			yield return null;
		
		// Play music or perform other misc tasks
		Debug.Log("Loading Time Up");
		
		// Fade to black
		yield return StartCoroutine(m_blackScreenCover.FadeIn());
		
		// !!! unload loading screen
		LoadingScene.UnloadLoadingScene();

		Debug.Log("Deleted Loading Scene");

		if (state.GetState() == StateManager.States.loadLevel)
		{
			Debug.Log("Setting To Play");
			state.SetToPlay();
		}
		if (state.GetState() == StateManager.States.loadMenu)
		{
			state.SetToMenu();
		}
		
		
		// Fade to new screen
		yield return StartCoroutine(m_blackScreenCover.FadeOut());

		//Destroy(gameObject);
	}
}
