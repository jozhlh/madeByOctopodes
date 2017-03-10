using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
	public Sprite uiIcon;

	[SerializeField]
	protected float totalDuration = 0.0f;

	private bool playerCanUse = false;
	protected bool inUse = false;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Activate()
	{
		// Use effect
		playerCanUse = false;
	}

	public void GiveToPlayer()
	{
		if (!playerCanUse)
		{
			playerCanUse = true;
			// Update UI with Icon
		}
	}
}
