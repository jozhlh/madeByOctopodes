using UnityEngine;
using System.Collections;

// OBSOLETE?

[ExecuteInEditMode]
public class Rock : MonoBehaviour {
    
    public LaneManager.PlayerLanes inLane;

    [SerializeField]
    private float zPosition = 0.0f;
 //   [SerializeField]
 //   private float obstacleLength = 1.0f;

    private Vector3 obstaclePosition = new Vector3(0, 0, 0);
 //   private Vector3 obstacleSize = new Vector3(1, 1, 1);
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

        rotationSpeed = 0;

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

  //      CalculateFade();
    }

    public float GetzPosition()
    {
        return zPosition;
    }
/*
    void CalculateFade()
    {
        float playerZ = player.transform.position.z;
        if ((int)inLane < 3)
        {
            if ((zPosition - playerZ) < playerDistanceTop)
            {
                transparency = (zPosition - playerZ) / playerDistanceTop;
                if (transparency < lowestTransparency)
                {
                    transparency = lowestTransparency;
                }
            }
        }
        else if ((int)inLane < 6)
        {
            if ((zPosition - playerZ) < playerDistanceMid)
            {
                transparency = (zPosition - playerZ) / playerDistanceMid;
                if (transparency < lowestTransparency)
                {
                    transparency = lowestTransparency;
                }
            }
        }
        attachedRenderer.sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f, transparency);
    }*/
}
