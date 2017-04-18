using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init : MonoBehaviour {

	[SerializeField]
	private ScreenManager screen = null;

	// Use this for initialization
	void Start () {
		screen.gameObject.GetComponent<StateManager>().SetToLoadMenu();
		screen.LoadScene("splash_scene");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
