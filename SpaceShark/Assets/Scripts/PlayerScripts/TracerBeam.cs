using UnityEngine;
using System.Collections;

public class TracerBeam : MonoBehaviour
{
    [Header("Game Object References")]
    [SerializeField]
    private GameObject player = null;

    [Header("Tracer Beam Variables")]
    [SerializeField]
    // Distance from the player the tracer beam begins
    private float tracerOffset = 2.0f;
    [SerializeField]
    // Length of the dashes
    private float dashLength = 5.0f;
    [SerializeField]
    // Length the beam goes into the distance if it does not hit anything
    private float defaultTracerLength = 200.0f;

    // The uv coordinates for the dash
    private Vector2 textureTiling = new Vector2(10.0f, 1.0f);
    // The attatched line ready
    private LineRenderer line = null;
    // The forward vector from the player
    private Vector3 fwd = new Vector3(0.0f, 0.0f, 1.0f);
    // The position the tracer begins at offset from the player
    private Vector3 tracerStart = new Vector3();

	// Use this for initialization
	void Start ()
    {
        // Get the attatched line renderer
        line = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Calculate starting position of the beam
        tracerStart = player.transform.position;
        tracerStart.z += tracerOffset;
        line.SetPosition(0, tracerStart);

        // For storing the result of the raycast
        RaycastHit hit;
        
        // If there is anything in front of the player, scale the tracer beam the distance
        if (Physics.Raycast(tracerStart, fwd, out hit, 100.0f))
        {
            // scale the beam so that the dashes are always the same length
            textureTiling.x = hit.distance / dashLength;
            tracerStart.z += hit.distance;
            line.SetPosition(1, tracerStart);
            line.material.mainTextureScale = textureTiling;
        }
        // If there is nothing in fron of the player, scale to a default distance
        else
        {
            textureTiling.x = defaultTracerLength / dashLength;
            tracerStart.z += defaultTracerLength;
            line.SetPosition(1, tracerStart);
            line.material.mainTextureScale = textureTiling;
        }
    }
}
