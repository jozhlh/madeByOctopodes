using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject followTarget = null;
    [SerializeField]
    private float followDistance = 10;

    private Vector3 cameraPosition = new Vector3(0, 0, 0);
    private Transform cameraTransform;
    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    private Vector3 animationOffset = new Vector3(0,15,-10);


    void Awake ()
    {
        followDistance = gameObject.transform.position.z;
        
    }

	// Use this for initialization
	void Start ()
    {
        cameraTransform = gameObject.GetComponent<Transform>();
        cameraPosition = cameraTransform.position;        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void LateUpdate()
    {
        if (transition > 1.0f)
        {
            cameraPosition = cameraTransform.position;
            cameraPosition.z = (followTarget.transform.position.z + followDistance);
            cameraTransform.position = cameraPosition;
        }
        else
        {
            //Animation at start of game
            cameraTransform.position = Vector3.Lerp(cameraPosition + animationOffset, cameraPosition, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            cameraTransform.LookAt(Ship_Movement.shipPosition);
        }
    }
}
