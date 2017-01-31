using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Reset Function for walls and camera follow 
// Stop user input during animation

public class ValleyWallGenerator : MonoBehaviour
{

    private float spawnZ = -300.0f;     //start distance of spawning valley tiles
    private int lastPrefabIndex = 0;

    [SerializeField]
    private float tileLength = 40.0f;

    public GameObject[] valleyWallPrefabs;
    public GameObject[] valleyFloorPrefabs;

    private List<GameObject> activeTiles;
    private List<GameObject> toBeCleared;

    [SerializeField]
    private float wallGap = 5.0f;
    [SerializeField]
    private float floorGap = 0.0f;
    // wall and floor offsets from the lane grid
    private float wallXOffset = 5.0f;
    private float wallYOffset = 5.0f;
    private float floorYOffset = 5.0f;

    void Start()
    {
        activeTiles = new List<GameObject>();       //create active list
        toBeCleared = new List<GameObject>();       //create clearance list
        wallXOffset = ((valleyWallPrefabs[0].transform.localScale.x * 0.5f) + (LaneManager.laneSpacingHorizontal * 1.5f)) + wallGap;    // determine the X offset 
        floorYOffset = (-1 * (LaneManager.laneSpacingVertical * 1.5f)) - floorGap;      //determine the Y offset for the floor
        wallYOffset = floorYOffset + (valleyWallPrefabs[0].transform.localScale.y * 0.5f);  //determine the Y offset for the floor
    }

    void Update()
    {
        if (spawnZ < (EndPlate.horizon + Ship_Movement.shipPosition.z))
        {
            SpawnValleyWallLeft();
            SpawnValleyWallRight();
            SpawnValleyFloor();
            spawnZ += tileLength;       //move the spawn position forwards the length of one tile 
            CleanUp();
        }
    }

    //reset alll assets 
    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Obstacle") | (other.tag == "Laser"))
        {
            spawnZ = -300.0f;
            lastPrefabIndex = 0;
        }
    }

    //
    void CleanUp()
    {
        foreach (GameObject wall in activeTiles.ToArray())
        {
            if (wall.transform.position.z < (Ship_Movement.shipPosition.z - tileLength)) //once player has passed a wall move it to the clearence list, delete it from the active list 
            {
                toBeCleared.Add(wall);
                activeTiles.Remove(wall);
            }
        }
        foreach (GameObject wall in toBeCleared.ToArray())
        {
            toBeCleared.Remove(wall);                       //delete walls in clearence list
            wall.SetActive(false);
            Destroy(wall);
        }
    }


    private void SpawnValleyWallLeft(int prefabIndex = -1)
    {
        GameObject go;
        Vector3 tilePosition = new Vector3(-1 * wallXOffset, wallYOffset, 1 * spawnZ);
        Vector3 tileRotation = new Vector3(0, 180, 0); //rotate walls so they face towards center of the valley

        go = Instantiate(valleyWallPrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.Rotate(tileRotation);
        go.transform.position = tilePosition;
        activeTiles.Add(go);                        //add valley wall to active list
    }

    private void SpawnValleyWallRight(int prefabIndex = -1)
    {
        GameObject go;
        Vector3 tilePosition = new Vector3(wallXOffset, wallYOffset, 1 * spawnZ);
        go = Instantiate(valleyWallPrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = tilePosition;
        activeTiles.Add(go);                      //add valley wall to active list
    }

    private void SpawnValleyFloor(int prefabIndex = -1)
    {
        GameObject go;
        Vector3 tilePosition = new Vector3(0, floorYOffset, 1 * spawnZ);
        go = Instantiate(valleyFloorPrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = tilePosition;
        activeTiles.Add(go);                    //add valley floor to active list
    }


    //this function will return a random prefab to be placed 
    private int RandomPrefabIndex()
    {
        if (valleyWallPrefabs.Length <= 1)
        {
            //error
            return 0;
        }
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, valleyWallPrefabs.Length);    //looks at the valley wall prefabs and selects a random one
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

}
