using UnityEngine;
using System.Collections;

public class TracerBeam : MonoBehaviour {

    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private float tracerOffset = 2.0f;

    [SerializeField]
    private float dashLength = 5.0f;

    private Vector2 textureTiling = new Vector2(10.0f, 1.0f);

    private LineRenderer line = null;

    private Vector3 fwd = new Vector3(0.0f, 0.0f, 1.0f);

    private Vector3 tracerStart = new Vector3();

	// Use this for initialization
	void Start ()
    {
        line = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        tracerStart = player.transform.position;
        tracerStart.z += tracerOffset;
        line.SetPosition(0, tracerStart);

        RaycastHit hit;

        /// Only works in unity version 5.4.2
        if (Physics.Raycast(tracerStart, fwd, out hit, 100.0f, LayerMask.NameToLayer("Ignore Raycast")))
        {
            //Debug.Log("tracer update");
            // test comment
            textureTiling.x = hit.distance / dashLength;
            tracerStart.z += hit.distance;
            line.SetPosition(1, tracerStart);
            line.material.mainTextureScale = textureTiling;
        }

        /// Works in unity version 5.4.0
        if (Physics.Raycast(tracerStart, fwd, out hit, 100.0f))
        {
            //Debug.Log("tracer update");
            // test comment
            textureTiling.x = hit.distance / dashLength;
            tracerStart.z += hit.distance;
            line.SetPosition(1, tracerStart);
            line.material.mainTextureScale = textureTiling;
        }
    }
}
