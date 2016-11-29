using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Obstacle : MonoBehaviour {

    
   // public LaneManager.PlayerLanes inLane;

    public LaneManager.ObstacleLocation location;

    [SerializeField]
    private float zPosition = 0.0f;
    [SerializeField]
    private float obstacleLength = 1.0f;

    Segment seg = new Segment();

    private Vector3 obstaclePosition = new Vector3(0, 0, 0);
    private Vector3 obstacleSize = new Vector3(1, 1, 1);

    bool rotationSet = false;

    // Use this for initialization
    void Start () {
        seg = GetComponentInParent<Segment>();
	}
	
	// Update is called once per frame
	void Update () {
        obstaclePosition.x = LaneManager.obstacleLocationData[(int)location].xPos;
        obstaclePosition.y = LaneManager.obstacleLocationData[(int)location].yPos;
        obstaclePosition.z = zPosition + seg.transform.position.z;

        gameObject.transform.position = obstaclePosition;
        gameObject.transform.eulerAngles = new Vector3(0,0, LaneManager.obstacleLocationData[(int)location].zRot);

/*
        obstacleSize.x = LaneManager.obstacleLocationData[(int)location].scale;
        obstacleSize.y = LaneManager.obstacleLocationData[(int)location].scale;
        obstacleSize.z = LaneManager.obstacleLocationData[(int)location].scale;
        
        gameObject.transform.localScale = obstacleSize;
*/
	}

    public float GetzPosition()
    {
        return zPosition;
    }
}
