using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    // All of the fragments that make up this enemy
    private Transform[] fragments = null;

    private int iterator;

	// Use this for initialization
	void Start ()
    {
        fragments = GetComponentsInChildren<Transform>();
        iterator = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		for (int i = 0; i < iterator; i++)
        {
            fragments[i].Translate(0, -GameSettings.fragmentSpeed, 0);
        }
        if (iterator < fragments.Length)
        {
            iterator++;
        }
	}
}
