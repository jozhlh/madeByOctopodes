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

    //TODO: Move this to a gun class
    [SerializeField]
    // Reference to the bullet prefab which the player fires
    private GameObject playerBullet;
    [SerializeField]
    // The distance from the player's transform to the gun on the model (-1.25, -0.4, 1.5)
    private Vector3 bulletOffset =  new Vector3(0,0,0);
    // Holds references to the bullets in the scene
    private List<GameObject> bulletObjects = new List<GameObject>();
    // Whether the player is able to shhot
    private bool playerCanFire = false;

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
            zPos = ob * 160.0f;
            segmentData[ob].gameObject.transform.position = new Vector3 (0,0,zPos);
            segmentDataObjects.Add(segmentData[ob].gameObject);
            segmentData[ob].PlaceSegment();
        }

        // Add callback event for player being able to fire
        playerCanFire = false;
        GameInput.ResetTap();
        if (GameInput.CanAddToTap())
        {
            GameInput.OnTap += PlayerFire;
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

        // When game is in tutorial level restrict player capabilities 
        if (StateManager.gameState == StateManager.States.tutorial)         
        {
            if (Ship_Movement.restrictBullet == true)
            {
                GameInput.OnTap -= PlayerFire;
            }
            // Re-enable player capabilities 
            else
            {
                if (!playerCanFire)                                         
                {
                    GameInput.OnTap += PlayerFire;
                    playerCanFire = true;
                }
            }
        }
        CleanUp();
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

    public void PlayerFire(Vector3 position)
    {
        // Fire bullet from left cannon
        Vector3 bulletStartPos = player.GetComponent<Transform>().position;
        GameObject bulletLeft = (GameObject)Instantiate(playerBullet, transform.position, transform.rotation);
        bulletLeft.transform.position = bulletStartPos + bulletOffset;
        bulletObjects.Add(bulletLeft);

        // Fire bullet from right cannon 
        GameObject bulletRight = (GameObject)Instantiate(playerBullet, transform.position, transform.rotation);
        bulletOffset.x = -1 * bulletOffset.x;
        bulletRight.transform.position = bulletStartPos + bulletOffset;
        bulletObjects.Add(bulletRight);

        GetComponent<FireTrigger>().ActivateTrigger();
        //customTriggers.CustomTrigger();
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

        // Add bullets to clearance list
        foreach (GameObject bullet in bulletObjects)            
        {
            if (bullet.GetComponent<PlayerBullet>().destroyThis)
            {
                toBeCleared.Add(bullet);
            }
        }

        // Destroy bullets 
        foreach (GameObject bullet in toBeCleared)                 
        {
            bulletObjects.Remove(bullet);
            bullet.SetActive(false);
            Destroy(bullet);
        }
        toBeCleared.Clear();
    }
}
