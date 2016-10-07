using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Obstacle : MonoBehaviour {

    public LaneManager.PlayerLanes inLane;

    [SerializeField]
    private float zPosition = 0.0f;
    [SerializeField]
    private float obstacleLength = 1.0f;

    private Vector3 obstaclePosition = new Vector3(0, 0, 0);
    private Vector3 obstacleSize = new Vector3(1, 1, 1);


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        obstaclePosition.x = LaneManager.laneData[(int)inLane].laneX;
        obstaclePosition.y = LaneManager.laneData[(int)inLane].laneY;
        obstaclePosition.z = zPosition;

        gameObject.transform.position = obstaclePosition;

        obstacleSize.x = LaneManager.laneSpacingHorizontal;
        obstacleSize.y = LaneManager.laneSpacingVertical;
        obstacleSize.z = obstacleLength;
        gameObject.transform.localScale = 0.8f * obstacleSize;

	}
}
