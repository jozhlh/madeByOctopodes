using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    // Used for the designer to update the obstacle's position in the editor
    private bool placeObstacle = false;
    [Header("Positional Data")]
    [SerializeField]
    // The obstacle's lane
    private LaneManager.ObstacleLocation location = LaneManager.ObstacleLocation.E;
    [SerializeField]
    // The obstacle's z position in the segment
    private float zPosition = 0.0f;

    // The segment the obstacle belongs to
    private SegmentData seg = null;

    // Use this for initialization
    void Start ()
    {
        seg = GetComponentInParent<SegmentData>();
    }

    // Places the obstacle in the correct position in the scene according to the designer's desired location
    public void PlaceObstacle(ObstacleData posInfo)
    {
        location = posInfo.lane;
        zPosition = posInfo.zPosition;
        seg = GetComponentInParent<SegmentData>();
        Vector3 obstaclePosition = new Vector3(0, 0, 0);
        obstaclePosition.x = LaneManager.obstacleLocationData[(int)posInfo.lane].xPos;
        obstaclePosition.y = LaneManager.obstacleLocationData[(int)posInfo.lane].yPos;
        obstaclePosition.z = posInfo.zPosition + seg.transform.position.z;

        gameObject.transform.position = obstaclePosition;
        gameObject.transform.eulerAngles = new Vector3(0, 0, LaneManager.obstacleLocationData[(int)posInfo.lane].zRot);
    }

    // Getters
    public float GetzPosition()
    {
        return zPosition;
    }

    public LaneManager.ObstacleLocation GetLocation()
    {
        return location;
    }
}
