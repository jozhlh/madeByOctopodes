using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LaneManager : MonoBehaviour {

    [SerializeField]
    private float laneWidth = 2.0f;
    [SerializeField]
    private float laneHeight = 2.0f;
    [SerializeField]
    private float laneLength = 60.0f;


    public static float laneSpacingHorizontal;
    public static float laneSpacingVertical;
    public static float lengthOfLevel;

    public enum PlayerLanes { NW, N, NE, W, C, E, SW, S, SE };

    public struct LaneInfo
    {
        public PlayerLanes laneID;
        public float laneX;
        public float laneY;
    }

    public static LaneInfo[] laneData = new LaneInfo[9];

    public enum ObstacleLocation {NW_R, NW_D, N, NE_D, NE_L, E, SE_L, SE_U, S, SW_U, SW_R, W}

    public struct ObstacleLocationInfo
    {
        public ObstacleLocation locationID;
        public float xPos;
        public float yPos;
        public float zRot;
        public float scale;
    }

    public static ObstacleLocationInfo[] obstacleLocationData = new ObstacleLocationInfo[12];

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        laneSpacingHorizontal = laneWidth;
        laneSpacingVertical = laneHeight;
        lengthOfLevel = laneLength;
        for (int j = 0; j < 9; j++)
        {
            if (j < 3)
            {
                laneData[j].laneX = (-laneSpacingHorizontal) + (j * laneSpacingHorizontal);
                laneData[j].laneY = laneSpacingVertical;
            }
            else if (j < 6)
            {
                laneData[j].laneX = (-laneSpacingHorizontal) + ((j - 3) * laneSpacingHorizontal);
                laneData[j].laneY = 0;
            }
            else
            {
                laneData[j].laneX = (-laneSpacingHorizontal) + ((j - 6) * laneSpacingHorizontal);
                laneData[j].laneY = -laneSpacingVertical;
            }
            Gizmos.DrawWireCube(new Vector3(laneData[j].laneX, laneData[j].laneY, laneLength / 2), new Vector3(laneSpacingHorizontal, laneSpacingVertical, laneLength));
        }
    }

    void Awake ()
    {
        InitialiseLanes();
        InitialiseObstaclePositions();
    }

    // Use this for initialization
    void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void InitialiseLanes()
    {
        laneSpacingHorizontal = laneWidth;
        laneSpacingVertical = laneHeight;
        lengthOfLevel = laneLength;
        for (int i = 0; i < 9; i++)
        {
            laneData[i].laneID = (PlayerLanes)i;
            if (i < 3)
            {
                laneData[i].laneX = (-laneSpacingHorizontal) + (i * laneSpacingHorizontal);
                laneData[i].laneY = laneSpacingVertical;
            }
            else if (i < 6)
            {
                laneData[i].laneX = (-laneSpacingHorizontal) + ((i - 3) * laneSpacingHorizontal);
                laneData[i].laneY = 0;
            }
            else
            {
                laneData[i].laneX = (-laneSpacingHorizontal) + ((i - 6) * laneSpacingHorizontal);
                laneData[i].laneY = -laneSpacingVertical;
            }
        }
 //       currentLane = lane[4];
//        targetLane = currentLane;
    }

    private void InitialiseObstaclePositions()
    {
        // NW_R, NW_D, N, NE_D, NE_L, E, SE_L, SE_U, S, SW_U, SW_L, W
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
