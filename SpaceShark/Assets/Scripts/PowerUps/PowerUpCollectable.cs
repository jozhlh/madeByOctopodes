using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectable : MonoBehaviour
{
	[SerializeField]
	private PowerUpManager powerUpManager = null;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    /// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			// Give player power up
			other.GetComponentInChildren<PowerUpManager>().SelectPowerUp();
			Destroy(gameObject);
		}
		
	}
}
