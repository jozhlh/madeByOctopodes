using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageRotate : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed = 90.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
