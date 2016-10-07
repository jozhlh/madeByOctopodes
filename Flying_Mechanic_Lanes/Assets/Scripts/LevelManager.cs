using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    List<GameObject> obstacleObjects = new List<GameObject>();
    List<GameObject> enemyObjects = new List<GameObject>();

    Enemy[] enemies;
    Obstacle[] obstacles;

    [SerializeField]
    private GameObject player;

    int numberOfObstacles;
    int numberOfEnemies;

	// Use this for initialization
	void Start ()
    {
        numberOfObstacles = GetComponentsInChildren<Obstacle>().Length;
        obstacles = new Obstacle[numberOfObstacles];
        obstacles = GetComponentsInChildren<Obstacle>();
        for (int ob = 0; ob < numberOfObstacles; ob++)
        {
            obstacleObjects.Add(obstacles[ob].gameObject);
        }

        numberOfEnemies = GetComponentsInChildren<Enemy>().Length;
        enemies = new Enemy[numberOfEnemies];
        enemies = GetComponentsInChildren<Enemy>();
        for (int ob = 0; ob < numberOfEnemies; ob++)
        {
            enemyObjects.Add(enemies[ob].gameObject);
        }

    }
	
	// Update is called once per frame
	void Update () {
	    foreach (GameObject obstacle in obstacleObjects)
        {
            if((obstacle.transform.position.z + (obstacle.transform.localScale.z / 2)) < (player.transform.position.z - (player.transform.localScale.z / 2)))
            {
                obstacle.SetActive(false);
            }
        }

        foreach (GameObject enemy in enemyObjects)
        {
            if ((enemy.transform.position.z + (enemy.transform.localScale.z / 2)) < (player.transform.position.z - (player.transform.localScale.z / 2)))
            {
                enemy.SetActive(false);
            }
        }
    }

    public void ResetLevel()
    {
        foreach (GameObject obstacle in obstacleObjects)
        {
            obstacle.SetActive(true);
        }

        foreach (GameObject enemy in enemyObjects)
        {
            enemy.SetActive(true);
        }
    }
}
