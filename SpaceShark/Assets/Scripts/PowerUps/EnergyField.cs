using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyField : MonoBehaviour {
	
	private float moveSpeed = GameSettings.gameSpeed * 2.0f;

	// Update is called once per frame
	void Update ()
	{
		Vector3 newPos = transform.position;
		newPos.z += Time.deltaTime * moveSpeed;
		transform.position = newPos;
	}

	public void SetMoveSpeed(float speed)
	{
		moveSpeed = speed;
	}

/* 
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "EnemyHitbox")
		{
			Debug.Log("Lightning Hit Enemy");
			other.GetComponent<EnemyHitBox>().destroyEnemy = true;
		}
	}

 	/// <summary>
	/// OnCollisionStay is called once per frame for every collider/rigidbody
	/// that is touching rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "EnemyHitbox")
		{
			Debug.Log("Lightning Stay Enemy");
			other.GetComponent<EnemyHitBox>().destroyEnemy = true;
		}
	}*/
}
