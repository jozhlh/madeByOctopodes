using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
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
   // private Segment seg = new Segment();

    private SegmentData seg = null;

    // Use this for initialization
    void Start ()
    {
  //      seg = GetComponentInParent<Segment>();
        seg = GetComponentInParent<SegmentData>();
    }

    // Update is called once per frame
    void Update()
    {
        // If designer selects placeObstacle from inspector, the obstacle will be moved to its correct location
        //if (placeObstacle)
        //{
            PlaceObstacle();
        //    placeObstacle = false;
       // }
    }

    // Places the obstacle in the correct position in the scene according to the designer's desired location
    public void PlaceObstacle()
    {
        seg = GetComponentInParent<SegmentData>();
        Vector3 obstaclePosition = new Vector3(0, 0, 0);
        obstaclePosition.x = LaneManager.obstacleLocationData[(int)location].xPos;
        obstaclePosition.y = LaneManager.obstacleLocationData[(int)location].yPos;
        obstaclePosition.z = zPosition + seg.transform.position.z;

        gameObject.transform.position = obstaclePosition;
        gameObject.transform.eulerAngles = new Vector3(0, 0, LaneManager.obstacleLocationData[(int)location].zRot);
    }

    // Setters
    public void SetzPosition(float zPos)
    {
        zPosition = zPos;
    }

    public void SetLocation(LaneManager.ObstacleLocation inLane)
    {
        location = inLane;
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
