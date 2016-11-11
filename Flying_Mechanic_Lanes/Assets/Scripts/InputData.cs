using UnityEngine;
using System.Collections;

public class InputData {

    public float duration;
    public Vector3 initialPosition;
    public Vector3 endPosition;
    public float speed;
    public float displacement;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void CalculateInput()
    {
        displacement = Vector3.Distance(initialPosition, endPosition);
        if (duration > 0.0f)
        {
            speed = displacement / duration;
        }
        else
        {
            speed = 0.0f;
        }
    }
}
