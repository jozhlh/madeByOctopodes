using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    // Holds references to the segments in the scene
   // private SegmentData[] segmentData;
  //  private List<GameObject> segmentDataObjects = new List<GameObject>();

    private List<GameObject> activeSegments = new List<GameObject>();

    [Header("Segment Prefabs")]
    [SerializeField]
    private GameObject[] veryEasySegments = null;

    [SerializeField]
    private GameObject[] easySegments = null;

    [SerializeField]
    private GameObject[] mediumSegments = null;

    [SerializeField]
    private GameObject[] hardSegments = null;

    [SerializeField]
    private GameObject[] veryHardSegments = null;

    [SerializeField]
    private const int totalSegments = 30;

    [Header("Difficulty Curve")]
    [SerializeField]
    private float startOffset = 20.0f;
    [Range(0, totalSegments)]
    [SerializeField]
    private int veryEasyNum = 0;
    [Range(0, totalSegments)]
    [SerializeField]
    private int easyNum = 0;
    [Range(0, totalSegments)]
    [SerializeField]
    private int mediumNum = 0;
    [Range(0, totalSegments)]
    [SerializeField]
    private int hardNum = 0;
    [Range(0, totalSegments)]
    [SerializeField]
    private int veryHardNum = 0;

    [Header("Scene References")]
    [SerializeField]
    // Reference to the player in the scene
    private GameObject player;

    [SerializeField]
    // Reference to the player in the scene
    private GameObject playerExplosion;

    [SerializeField]
    // Reference to the sound manager in the scene
    private GameObject soundManager;

    private int lastPrefabIndex = 0;

    private float spawnZ = 0.0f;
    int veryEasyToSpawn, easyToSpawn, mediumToSpawn, hardToSpawn, veryHardToSpawn = 0;

    // Initialization
    void Start ()
    {
        // Get references to the segments in the level, and populate them all
        //int numberOfSegments = GetComponentsInChildren<SegmentData>().Length;
        //segmentData = new SegmentData[numberOfSegments];
        //segmentData = GetComponentsInChildren<SegmentData>();
        //float zPos = 0.0f;
        //for (int ob = 0; ob < numberOfSegments; ob++)
        //{
        //    zPos = ob * LaneManager.lengthOfSegment;
        //    segmentData[ob].gameObject.transform.position = new Vector3 (0,0,zPos);
        //    segmentDataObjects.Add(segmentData[ob].gameObject);
        //    segmentData[ob].PlaceSegment();
        //}
        Init();
        player.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (spawnZ < (EndPlate.horizon + Ship_Movement.shipPosition.z))
        {
            SpawnSegment();
            spawnZ += LaneManager.lengthOfSegment;       //move the spawn position forwards the length of one tile 
        }
        
        CleanUp();
    }

    private void SpawnSegment(int prefabIndex = -1)
    {
        GameObject go;
        Vector3 segmentPosition = new Vector3(0, 0, 1 * spawnZ);
        if (veryEasyToSpawn > 0)
        {
            go = Instantiate(veryEasySegments[RandomPrefabIndex(veryEasySegments.Length)], segmentPosition, transform.rotation, transform);
            veryEasyToSpawn--;
        }
        else if (easyToSpawn > 0)
        {
            go = Instantiate(easySegments[RandomPrefabIndex(easySegments.Length)], segmentPosition, transform.rotation, transform);
            easyToSpawn--;
        }
        else if (mediumToSpawn > 0)
        {
           go = Instantiate(mediumSegments[RandomPrefabIndex(mediumSegments.Length)], segmentPosition, transform.rotation, transform);
           mediumToSpawn--;
        }
        else if (hardToSpawn > 0)
        {
           go = Instantiate(hardSegments[RandomPrefabIndex(hardSegments.Length)], segmentPosition, transform.rotation, transform);
           hardToSpawn--;
        }
        else if (veryHardToSpawn > 0)
        {
           go = Instantiate(veryHardSegments[RandomPrefabIndex(veryHardSegments.Length)], segmentPosition, transform.rotation, transform);
           veryHardToSpawn--;
        }
        else
        {
           go = Instantiate(veryEasySegments[RandomPrefabIndex(veryEasySegments.Length)], segmentPosition, transform.rotation, transform);
        }
        
        //go.transform.SetParent(transform);
        //go.transform.position = segmentPosition;
        activeSegments.Add(go);                    //add segment to active list
        go.GetComponent<SegmentData>().PlaceSegment();
    }

    //this function will return a random prefab to be placed 
    private int RandomPrefabIndex(int poolSize)
    {
        if (poolSize <= 1)
        {
            //error
            return 0;
        }
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, poolSize);    //looks at the valley wall prefabs and selects a random one
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    public void ClearLevel()
    {
        if (activeSegments.Count > 0)
        {
            foreach (GameObject seg in activeSegments)
            {
                seg.GetComponent<SegmentData>().ClearEnemies();
                
            }
        }

         StateManager.gameState = StateManager.States.dead;

        player.SetActive(false);

        GameObject explosion = Instantiate(playerExplosion, player.transform.position, transform.rotation);
        Destroy(explosion, 2.0f);
        // Stop engine sounds
        soundManager.GetComponent<SoundManager>().StopEvent("shipEngine", 0, player);
       // Init();
    }

    public void Init()
    {
       // if (activeSegments.Count > 0)
       // {
       //     List<GameObject> deadSegs = new List<GameObject>();
       //     foreach (GameObject seg in activeSegments)
       //     {
        //        activeSegments.Remove(seg);
        //        deadSegs.Add(seg);
        //    }
        //    foreach(GameObject seg in deadSegs)
        //    {
         //       Destroy(seg);
         //   }
         //   deadSegs.Clear();
         //   activeSegments.Clear();
       // }
        spawnZ = startOffset;
        lastPrefabIndex = 0;
        veryEasyToSpawn = veryEasyNum;
        easyToSpawn = easyNum;
        mediumToSpawn = mediumNum;
        hardToSpawn = hardNum;
        veryHardToSpawn = veryHardNum;

    }

    // Set any inactive objects or enemies to active
    public void ResetLevel()
    {
        if (activeSegments.Count > 0)
        {
            foreach (GameObject seg in activeSegments)
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
        // Cull any obstacles behind the player
        foreach (GameObject seg in activeSegments)
        {
            seg.GetComponent<SegmentData>().UpdateSegment(player.transform.position.z - (player.transform.localScale.z));
        }
        List<GameObject> toBeCleared = new List<GameObject>();
        // Set any hit enemies to inactive
        foreach (GameObject seg in activeSegments)              
        {
            seg.GetComponent<SegmentData>().CleanUpEnemies();
        }
    }
}
