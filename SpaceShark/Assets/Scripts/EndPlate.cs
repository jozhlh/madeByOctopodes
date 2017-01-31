using UnityEngine;
using System.Collections;

public class EndPlate : MonoBehaviour
{
    public static float horizon;

    [SerializeField]
    private float endplateDistance = 500.0f;

    // Use this for initialization
    void Start ()
    {
        horizon = endplateDistance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Ship_Movement.shipPosition.z + endplateDistance);
    }
}
