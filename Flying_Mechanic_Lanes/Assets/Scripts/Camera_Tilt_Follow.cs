using UnityEngine;
using System.Collections;

public class Camera_Tilt_Follow : MonoBehaviour
{

    [SerializeField]
    private GameObject followTarget = null;

    [SerializeField]
    private float followDistance = 10;

    private Vector3 cameraPosition = new Vector3(0, 0, 0);
    private Vector3 cameraRotation = new Vector3(1, 0, 0);

    private Transform cameraTransform;


    void Awake()
    {
        followDistance = gameObject.transform.position.z;

    }

    // Use this for initialization
    void Start()
    {

        cameraTransform = gameObject.GetComponent<Transform>();
        cameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(Ship_Movement.shipPosition.x, 0, Ship_Movement.shipPosition.z);
        cameraTransform.LookAt(Ship_Movement.shipPosition);
        cameraTransform.LookAt(targetPostition);
    }

    void LateUpdate()
    {
        cameraPosition = cameraTransform.position;
        cameraPosition.z = (followTarget.transform.position.z + followDistance);
        cameraTransform.position = cameraPosition;
    }
}
