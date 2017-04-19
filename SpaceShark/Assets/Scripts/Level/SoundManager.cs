using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private uint bankID;
	private uint initBankID;
	[SerializeField]
	private string soundbankName;

	// Use this for initialization
	void Start () {
		// Import Soundbank
		AkSoundEngine.LoadBank("Init", AkSoundEngine.AK_DEFAULT_POOL_ID,out initBankID);	
		// Import Soundbank
		AkSoundEngine.LoadBank("Soundbank1", AkSoundEngine.AK_DEFAULT_POOL_ID,out bankID);	
		PlayEvent("menuSwipe", gameObject);
		//PlayEvent("playerFire", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Play Events
	
	public void PlayEvent(string eventName, GameObject go)
	
	{
		AkSoundEngine.PostEvent (eventName, go);
	}

	// Stop Events

	public void StopEvent(string eventName, int fadeOut, GameObject go)
	
	{
		uint eventID;
		eventID=AkSoundEngine.GetIDFromString (eventName);
		AkSoundEngine.ExecuteActionOnEvent (eventID, AkActionOnEventType.AkActionOnEventType_Stop,go,fadeOut, AkCurveInterpolation.AkCurveInterpolation_Sine);

	}
	public void StopAllEvents()
	{
		AkSoundEngine.StopAll ();
	}

	// Switch States

	public void SetFloor(GameObject player, string material)
	{
		if (material == "Metal")
		{
			AkSoundEngine.SetSwitch("Material", "Metal", player);
		}
		if (material == "Wood")
		{
			AkSoundEngine.SetSwitch("Material", "Wood", player);
		}
	}
	
	// RTPCs

	public void SetHealth(GameObject player, float health)
	{
		AkSoundEngine.SetRTPCValue ("Health", health);
	}
}
