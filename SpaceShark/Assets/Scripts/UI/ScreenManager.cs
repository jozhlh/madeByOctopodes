using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
	[SerializeField]
	private Fader m_blackScreenCover;
	[SerializeField]
	private float m_minDuration = 1.5f;
	
	void Update()
	{/* 
		if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine(LoadSceneAsync("GameScreen"));
		}*/
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneAsync(sceneName));
	}
	
	public IEnumerator LoadSceneAsync(string sceneName)
	{
		// Fade to black
		yield return StartCoroutine(m_blackScreenCover.FadeIn());
		
		// Load loading screen
		yield return SceneManager.LoadSceneAsync("LoadingScene");
		
		// !!! unload old screen (automatic)
		
		// Fade to loading screen
		yield return StartCoroutine(m_blackScreenCover.FadeOut());
		
		float endTime = Time.time + m_minDuration;
		
		// Load level async
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		
		
		while (Time.time < endTime)
			yield return null;
		
		// Play music or perform other misc tasks
		
		// Fade to black
		yield return StartCoroutine(m_blackScreenCover.FadeIn());
		
		// !!! unload loading screen
		LoadingScene.UnloadLoadingScene();
		
		// Fade to new screen
		yield return StartCoroutine(m_blackScreenCover.FadeOut());
	}
}
