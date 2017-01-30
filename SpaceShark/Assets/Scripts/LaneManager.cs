using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LaneManager : MonoBehaviour
{
    // Enums for player and obstacle lane placement / orientation
    public enum ObstacleLocation { NW_R, NW_D, N, NE_D, NE_L, E, SE_L, SE_U, S, SW_U, SW_R, W };
    public enum PlayerLanes { NW, N, NE, W, C, E, SW, S, SE };

    // Containers for positional information of each lane
    public struct ObstacleLocationInfo
    {
        public ObstacleLocation locationID;
        public float xPos;
        public float yPos;
        public float zRot;
        public float scale;
    }

    public struct LaneInfo
    {
        public PlayerLanes laneID;
        public float laneX;
        public float laneY;
    }

    // All of the positional information for all lanes
    public static LaneInfo[] laneData = new LaneInfo[9];
    public static ObstacleLocationInfo[] obstacleLocationData = new ObstacleLocationInfo[12];

    [Header("Lane Size Adjustment")]
    [SerializeField]
    // The width of an individual lane
    private float laneWidth = 2.0f;
    [SerializeField]
    // The height of an individual lane
    private float laneHeight = 2.0f;
    [SerializeField]
    // The length of the level
    private float laneLength = 60.0f;
    [SerializeField]
    // The length of the level
    private bool recalculateLane = false;


    public static float laneSpacingHorizontal = 0.0f;
    public static float laneSpacingVertical = 0.0f;
    public static float lengthOfLevel = 0.0f;

    

    // When the lane manager is selected, draw a grid to show the lanes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        laneSpacingHorizontal = laneWidth;
        laneSpacingVertical = laneHeight;
        lengthOfLevel = laneLength;
        // For each lane, calculate the position and size of its boundaries
        for (int j = 0; j < 9; j++)
        {
            laneData[j] = CalculateLaneData(j);
            Gizmos.DrawWireCube(new Vector3(laneData[j].laneX, laneData[j].laneY, laneLength / 2), new Vector3(laneSpacingHorizontal, laneSpacingVertical, laneLength));
        }
    }

    void Awake ()
    {
        InitialiseLanes();
        InitialiseObstaclePositions();
    }

    void Update()
    {
        if(recalculateLane)
        {
            InitialiseLanes();
            InitialiseObstaclePositions();
            recalculateLane = false;
        }
    }

    // For each lane, calculate the position of its boundaries
    private void InitialiseLanes()
    {
        laneSpacingHorizontal = laneWidth;
        laneSpacingVertical = laneHeight;
        lengthOfLevel = laneLength;
        for (int i = 0; i < 9; i++)
        {
            laneData[i] = CalculateLaneData(i);
        }
    }

    // Calcuate the positional information of a lane
    private LaneInfo CalculateLaneData(int laneNum)
    {
        LaneInfo lane = new LaneInfo();
        lane.laneID = (PlayerLanes)laneNum;
        if (laneNum < 3)
        {
            lane.laneX = (-laneSpacingHorizontal) + (laneNum * laneSpacingHorizontal);
            lane.laneY = laneSpacingVertical;
        }
        else if (laneNum < 6)
        {
            lane.laneX = (-laneSpacingHorizontal) + ((laneNum - 3) * laneSpacingHorizontal);
            lane.laneY = 0;
        }
        else
        {
            lane.laneX = (-laneSpacingHorizontal) + ((laneNum - 6) * laneSpacingHorizontal);
            lane.laneY = -laneSpacingVertical;
        }
        return lane;
    }

    // Calcuate the positional information of an obstacle in each lane
    private void InitialiseObstaclePositions()
    {
        float offset = 0.0f;
        for (int i = 0; i < 12; i++)
        {
            obstacleLocationData[i].locationID = (ObstacleLocation)i;
            if ((i > 0) & (i < 4)) // NW_D, N, NE_D
            {
                obstacleLocationData[i].zRot = 180;
                obstacleLocationData[i].yPos = (1.5f * laneHeight) + offset;
                obstacleLocationData[i].scale = 1.0f;
            }
            else if ((i > 3) & (i < 7)) // NE_L, E, SE_L
            {
                obstacleLocationData[i].zRot = 90;
                obstacleLocationData[i].xPos = (1.5f * laneWidth) + offset;
                obstacleLocationData[i].scale = 2.0f;
            }
            else if ((i > 6) & (i < 10)) // SE_U, S, SW_U
            {
                obstacleLocationData[i].zRot = 0;
                obstacleLocationData[i].yPos = -1.0f * ((1.5f * laneHeight) + offset);
                obstacleLocationData[i].scale = 1.0f;
            }
            else // SW_L, W, NW_R
            {
                obstacleLocationData[i].zRot = 270;
                obstacleLocationData[i].xPos = -1.0f * ((1.5f * laneWidth) + offset);
                obstacleLocationData[i].scale = 2.0f;
            }

            if ((i == 0) | (i == 4))
            {
                obstacleLocationData[i].yPos = laneHeight;
            }
            else if ((i == 1) | (i == 9))
            {
                obstacleLocationData[i].xPos = -1.0f * laneWidth;
            }
            else if ((i == 2) | (i == 8))
            {
                obstacleLocationData[i].xPos = 0.0f;
            }
            else if ((i == 3) | (i == 7))
            {
                obstacleLocationData[i].xPos = laneWidth;
            }
            else if ((i == 5) | (i == 11))
            {
                obstacleLocationData[i].yPos = 0.0f;
            }
            else
            {
                obstacleLocationData[i].yPos = -1.0f * laneHeight;
            }
        }
    }
}
