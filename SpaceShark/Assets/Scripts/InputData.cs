using UnityEngine;
using System.Collections;

// Class for storing data from user input
public class InputData
{

    public float duration;
    public Vector3 initialPosition;
    public Vector3 endPosition;
    public float speed;
    public float displacement;

    // Populate class data using known variables
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
