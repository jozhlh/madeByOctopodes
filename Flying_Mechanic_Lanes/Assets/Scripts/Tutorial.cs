using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	// Use this for initialization
	void Start ()
    { 

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Ship_Movement.shipPosition.z == 70)
        {
            Ship_Movement.gameSpeed = 0;
        }
    }
}
