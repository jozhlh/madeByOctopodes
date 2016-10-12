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
}
