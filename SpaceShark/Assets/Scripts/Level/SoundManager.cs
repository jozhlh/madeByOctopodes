using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private uint bankID;
	[SerializeField]
	private string soundbankName;

	// Use this for initialization
	void Start () {
		// Import Soundbank
		AkSoundEngine.LoadBank(soundbankName, AkSoundEngine.AK_DEFAULT_POOL_ID,out bankID);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Play Event
	public void PlayEvent(string eventName, GameObject go)
	{
		AkSoundEngine.PostEvent (eventName, go);
	}

}
