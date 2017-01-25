using UnityEngine;
using System.Collections;

public class EndPlate : MonoBehaviour {

    private Transform endPlate;
    private Vector3 planePosition = new Vector3(0, 0, 0);


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        endPlate = gameObject.GetComponent<Transform>();
        planePosition = endPlate.position;
        planePosition.z = Ship_Movement.shipPosition.z + 500;
        endPlate.position = planePosition;
	}
}
