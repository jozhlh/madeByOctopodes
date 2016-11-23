using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValleyWallGenerator : MonoBehaviour {

    private Transform playerTransform;
    private float spawnZ = -40.0f;
    private int lastPrefabIndex = 0;
    [SerializeField]
    private float tileLength = 40.0f;
    [SerializeField]
    private int tileOnScreen = 8;

    public GameObject[] valleyWallPrefabs;
    public GameObject[] valleyFloorPrefabs;

    private List<GameObject> activeTiles;


    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for(int i = 0; i < tileOnScreen; i++)
        {
            SpawnValleyWallLeft();
            SpawnValleyWallRight();
            SpawnValleyFloor();
        }
    }

    void Update()
    {
        if(playerTransform.position.z > (spawnZ - tileOnScreen * tileLength))
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
        Vector3 tilePosition = new Vector3(-16,0,1 * spawnZ);
        go = Instantiate(valleyWallPrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = tilePosition;
        activeTiles.Add(go);
    }

    private void SpawnValleyWallRight(int prefabIndex = -1)
    {
        GameObject go;
        Vector3 tilePosition = new Vector3(16, 0, 1 * spawnZ);
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
        Vector3 tilePosition = new Vector3(0, -10, 1 * spawnZ);
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
