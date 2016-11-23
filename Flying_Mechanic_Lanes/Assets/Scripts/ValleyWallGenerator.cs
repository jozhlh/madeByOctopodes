using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Reset Function for walls and camera follow // Stop user input during animation

public class ValleyWallGenerator : MonoBehaviour {

    private float spawnZ = -200.0f;
    private int lastPrefabIndex = 0;

    [SerializeField]
    private float tileLength = 40.0f;
    [SerializeField]
    private int tileOnScreen = 8;

    public GameObject[] valleyWallPrefabs;
    public GameObject[] valleyFloorPrefabs;

    private List<GameObject> activeTiles;

    [SerializeField]
    private float wallGap = 5.0f;
    [SerializeField]
    private float floorGap = 0.0f;

    private float wallXOffset = 5.0f;
    private float wallYOffset = 5.0f;
    private float floorYOffset = 5.0f;

    void Start()
    {
        activeTiles = new List<GameObject>();
        wallXOffset = ((valleyWallPrefabs[0].transform.localScale.x * 0.5f) + (LaneManager.laneSpacingHorizontal * 1.5f)) + wallGap;
        floorYOffset = (-1 * (LaneManager.laneSpacingVertical * 1.5f)) - floorGap;
        wallYOffset = floorYOffset + (valleyWallPrefabs[0].transform.localScale.y * 0.5f);
    }

    void Update()
    {
        if(Ship_Movement.shipPosition.z > (spawnZ - tileOnScreen * tileLength))
        {
            SpawnValleyWallLeft();
            SpawnValleyWallRight();
            SpawnValleyFloor();
            spawnZ += tileLength;
            DeleteValleyWalls();
        }
    }

    private void SpawnValleyWallLeft(int prefabIndex = -1)
    {
        GameObject go;
        Vector3 tilePosition = new Vector3(-1 * wallXOffset,  wallYOffset, 1 * spawnZ);
        go = Instantiate(valleyWallPrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = tilePosition;
        activeTiles.Add(go);
    }

    private void SpawnValleyWallRight(int prefabIndex = -1)
    {
        GameObject go;
        Vector3 tilePosition = new Vector3(wallXOffset, wallYOffset, 1 * spawnZ);
        // Vector3 tileRotation = new Vector3(0, 180, 0);
        go = Instantiate(valleyWallPrefabs[RandomPrefabIndex()]) as GameObject;
        // go.transform.rotation = tileRotation;
        go.transform.SetParent(transform);
        go.transform.position = tilePosition;
        activeTiles.Add(go);
    }

    private void SpawnValleyFloor(int prefabIndex = -1)
    {
        GameObject go;
        Vector3 tilePosition = new Vector3(0, floorYOffset, 1 * spawnZ);
        go = Instantiate(valleyFloorPrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = tilePosition;
        activeTiles.Add(go);
    }


    private void DeleteValleyWalls()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (valleyWallPrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, valleyWallPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

}
