using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    // All of the fragments that make up this enemy
    //private Transform[] fragments = null;

    // private float iterator;
    private float startFall = 0.0f;
    private float counter = 0.0f;
    private bool backward = true;

	// Use this for initialization
	void Start ()
    {
       // fragments = GetComponentsInChildren<Transform>();
       // Debug.Log("Fragments: " + fragments.Length);
       // iterator = 0;
        backward = GameSettings.blowBack;
       // counter = -0.1f;
       // startFall = Random.Range(GameSettings.fallTime, GameSettings.fallTime + GameSettings.fallPeriod);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //  if (counter < startFall)
        //  {
        //      counter += Time.deltaTime;
        //  }
        //  else
        //  {
        if (!backward)
        {
            transform.Translate(new Vector3(0, -GameSettings.fragmentSpeed * Time.deltaTime, 0));
        }
        else
        {
            transform.Translate(new Vector3(0, 0, GameSettings.fragmentSpeed * Time.deltaTime));
        }
        //transform.Translate(new Vector3(0, 0, -GameSettings.fragmentSpeed * Time.deltaTime));
        //    }
        //for (int i = 0; i < iterator; i++)
        //      {
        //          // fragments[i].Translate(0, -GameSettings.fragmentSpeed, 0);
        //          fragments[i].position.Set(fragments[i].position.x, fragments[i].position.y - GameSettings.fragmentSpeed, fragments[i].position.z);
        //      }
        //      if (iterator < fragments.Length)
        //      {
        //          iterator++;
        //      }



    }
}
