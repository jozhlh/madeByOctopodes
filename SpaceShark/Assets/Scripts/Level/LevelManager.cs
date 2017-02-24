using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    // Holds references to the segments in the scene
    private SegmentData[] segmentData;
    private List<GameObject> segmentDataObjects = new List<GameObject>();

    [SerializeField]
    // Reference to the player in the scene
    private GameObject player;

    [SerializeField]
    // Reference to the sound manager in the scene
    private GameObject soundManager;

    // Initialization
    void Start ()
    {
        // Get references to the segments in the level, and populate them all
        int numberOfSegments = GetComponentsInChildren<SegmentData>().Length;
        segmentData = new SegmentData[numberOfSegments];
        segmentData = GetComponentsInChildren<SegmentData>();
        float zPos = 0.0f;
        for (int ob = 0; ob < numberOfSegments; ob++)
        {
            zPos = ob * LaneManager.lengthOfSegment;
            segmentData[ob].gameObject.transform.position = new Vector3 (0,0,zPos);
            segmentDataObjects.Add(segmentData[ob].gameObject);
            segmentData[ob].PlaceSegment();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Cull any obstacles behind the player
        foreach (GameObject seg in segmentDataObjects)
        {
            seg.GetComponent<SegmentData>().UpdateSegment(player.transform.position.z - (player.transform.localScale.z));
        }

        CleanUp();
    }

    public void ClearLevel()
    {
        if (segmentDataObjects.Count > 0)
        {
            foreach (GameObject seg in segmentDataObjects)
            {
                seg.GetComponent<SegmentData>().ClearEnemies();
            }
        }

         StateManager.gameState = StateManager.States.dead;
         // Stop engine sounds
         soundManager.GetComponent<SoundManager>().StopEvent("shipEngine", 0, gameObject);
    }

    // Set any inactive objects or enemies to active
    public void ResetLevel()
    {
        if (segmentDataObjects.Count > 0)
        {
            foreach (GameObject seg in segmentDataObjects)
            {
                seg.GetComponent<SegmentData>().ResetObstacles();
            }
        }

        if (StateManager.gameState == StateManager.States.tutorial)
        {
            TutorialManager.DisableUI();
        }

        StateManager.gameState = StateManager.States.dead;

    }

    // Destroy or disable any expired game objects
    public void CleanUp()
    {
        List<GameObject> toBeCleared = new List<GameObject>();
        // Set any hit enemies to inactive
        foreach (GameObject seg in segmentDataObjects)              
        {
            seg.GetComponent<SegmentData>().CleanUpEnemies();
        }
    }
}
