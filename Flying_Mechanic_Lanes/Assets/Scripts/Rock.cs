using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Rock : MonoBehaviour {

    public LaneManager.PlayerLanes inLane;

    [SerializeField]
    private float zPosition = 0.0f;
    [SerializeField]
    private float obstacleLength = 1.0f;

    private Vector3 obstaclePosition = new Vector3(0, 0, 0);
    private Vector3 obstacleSize = new Vector3(1, 1, 1);
    public Vector3 rockRotation = new Vector3();
    [SerializeField]
    private float rotationSpeed = 10.0f;

    // Use this for initialization
    void Start()
    {
        rockRotation.x = Random.Range(0, 10);
        rockRotation.y = Random.Range(0, 10);
        rockRotation.z = Random.Range(0, 10);
        rockRotation.Normalize();

        if ((Random.Range(0, 1)) > 0.5)
        {
            rotationSpeed = -1 * rotationSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        obstaclePosition.x = LaneManager.laneData[(int)inLane].laneX;
        obstaclePosition.y = LaneManager.laneData[(int)inLane].laneY;
        obstaclePosition.z = zPosition;

        gameObject.transform.position = obstaclePosition;

        /*
        obstacleSize.x = LaneManager.laneSpacingHorizontal;
        obstacleSize.y = LaneManager.laneSpacingVertical;
        obstacleSize.z = obstacleLength;
        gameObject.transform.localScale = 0.8f * obstacleSize;
        */

        gameObject.transform.Rotate(rockRotation, (Time.deltaTime * rotationSpeed));
    }
}
