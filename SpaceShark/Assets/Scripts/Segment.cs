using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Segment : MonoBehaviour
{
    public struct ElementContainer { public GameObject prefab; GameSettings.LevelTypes level;};
    public struct EnemyContainer
    {
        public GameObject model;
        public LaneManager.PlayerLanes lane;
        public float zPosition;

        public void SetModel(GameObject mod){model = mod;}
        public void SetLane(LaneManager.PlayerLanes lan){lane = lan;}
        public void SetPos(float pos){zPosition = pos;}
    };
    public struct ObstacleContainer {GameObject model; LaneManager.ObstacleLocation lane; float zPosition; };

    [SerializeField]
    private GameSettings.LevelTypes levelType;

    [Header("Enemy And Obstacle Prefabs")]
    [SerializeField]
    private ElementContainer[] enemyPrefabs = new ElementContainer[GameSettings.numberOfLevelTypes];
    [SerializeField]
    private ElementContainer[] obstaclePrefabs = new ElementContainer[GameSettings.numberOfLevelTypes];


    [SerializeField]
    private List<EnemyContainer> placedEnemies = null;
    [SerializeField]
    private List<ObstacleContainer> placedObstacles = null;


    // Containers for the children of the segment
    [SerializeField]
	private List<GameObject> obstacleObjects = null;
    private List<GameObject> enemyObjects = null;
    private Obstacle[] obstacles;
    private Enemy[] enemies;
	private int numOfObstacles;
	private int numberOfEnemies;

    void Start()
    {
        obstacleObjects = new List<GameObject>();
        enemyObjects = new List<GameObject>();
    }

    private void UpdateEnemyModels()
    {
        if (placedEnemies.Count > 0)
        {
            foreach (EnemyContainer placedEnemy in placedEnemies)
            {
                placedEnemy.SetModel(enemyPrefabs[(int)levelType].prefab);
                
                
            }
        }
    }

	public void AcquireObstacles()
	{
        // If there are already obstacles in the container, empty the container
		if (obstacleObjects.Count > 0)
        {
            obstacleObjects.Clear();
        }
        // Get obstacles placed in the segment
        numOfObstacles = GetComponentsInChildren<Obstacle>().Length;
        obstacles = new Obstacle[numOfObstacles];
        obstacles = GetComponentsInChildren<Obstacle>();

        // For each obstacle in the segment, set their position in the scene, store a reference to the gameObject
        for (int ob = 0; ob < numOfObstacles; ob++)
        {
            obstacles[ob].PlaceObstacle();
            obstacleObjects.Add(obstacles[ob].gameObject);
        }
        // Get enemies placed in the segment
		numberOfEnemies = GetComponentsInChildren<Enemy>().Length;
        enemies = new Enemy[numberOfEnemies];
        enemies = GetComponentsInChildren<Enemy>();
        
        // For each enemy in the segment, store a reference to the gameObject
        for (int ob = 0; ob < numberOfEnemies; ob++)
        {
            enemyObjects.Add(enemies[ob].gameObject);
        }
	}

    // If an object is behind the player, cull it
	public void UpdateSegment(float playerBoundary)
	{
		foreach (GameObject ob in obstacleObjects)
        {
			if((ob.transform.position.z + (ob.transform.localScale.z)) < playerBoundary)
            {
            	ob.SetActive(false);
			}
        }

		foreach (GameObject enemy in enemyObjects)
        {
            if ((enemy.transform.position.z + (enemy.transform.localScale.z / 2)) < playerBoundary)
            {
                enemy.SetActive(false);
            }
        }
	}

    // Replace obstacles and enemies in the scene
    public void ResetObstacles()
    {
        foreach (GameObject ob in obstacleObjects)
        {
            ob.SetActive(true);
        }

		int iterator = 0;
        foreach (GameObject enemy in enemyObjects)
        {
            enemy.SetActive(true);
            enemies[iterator].ResetEnemy();
            enemy.GetComponentInChildren<EnemyHitBox>().destroyEnemy = false;
            iterator++;
        }
    }

    // Destroy any enemies that have been killed
	public void CleanUpEnemies()
	{
		 foreach (GameObject enemy in enemyObjects)
        {
            if (enemy.GetComponentInChildren<EnemyHitBox>().destroyEnemy)
            {
                enemy.SetActive(false);
            }
        }
	}

}
