using UnityEngine;
using System.Collections;

public class EnemyHitBox : MonoBehaviour {

    public bool destroyThis = false;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy hit by" + other.tag);
        if ((other.tag == "Bullet") | (other.tag == "Obstacle"))
        {
            // Kill enemy
            destroyThis = true;
        }
    }

    // Update is called once per frame
    void Update () {
	
        
	}

   
}
