using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Segment : MonoBehaviour
{
    // Containers for the children of the segment
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
            if ((ob.transform.position.z + (ob.transform.localScale.z)) < playerBoundary)
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