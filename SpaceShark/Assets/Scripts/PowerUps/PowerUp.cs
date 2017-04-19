using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
	public Sprite uiIcon;

	[SerializeField]
	protected float totalDuration = 0.0f;
	[SerializeField]
	protected GameObject player = null;

	private bool playerCanUse = false;
	protected bool inUse = false;

	protected SoundManager soundManager = null;
	

	// Use this for initialization
	void Start ()
	{
		soundManager = GameObject.Find("ScreenManager").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Activate()
	{
		// Use effect
		playerCanUse = false;
		soundManager.PlayEvent("powerupUse", player);
	}

	public void GiveToPlayer()
	{
		if (!playerCanUse)
		{
			soundManager.PlayEvent("powerupCollect", player);
			playerCanUse = true;
			// Update UI with Icon
		}
	}
}
