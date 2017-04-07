using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlanet : MonoBehaviour {

	[SerializeField]
	private GameObject playButton = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowPlayButton()
	{
		playButton.SetActive(true);
	}

	public void HidePlayButton()
	{
		playButton.SetActive(false);
	}
}
