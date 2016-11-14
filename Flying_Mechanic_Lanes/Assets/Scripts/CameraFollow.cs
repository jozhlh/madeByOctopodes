using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject followTarget = null;

    [SerializeField]
    private float followDistance = 10;

    private Vector3 cameraPosition = new Vector3(0, 0, 0);

    private Transform cameraTransform;


    void Awake ()
    {
        followDistance = gameObject.transform.position.z;
        
    }

	// Use this for initialization
	void Start () {

        cameraTransform = gameObject.GetComponent<Transform>();
        cameraPosition = cameraTransform.position;
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void LateUpdate()
    {
        
        cameraPosition = cameraTransform.position;
        cameraPosition.z = (followTarget.transform.position.z + followDistance);
        cameraTransform.position = cameraPosition;
    }
}
