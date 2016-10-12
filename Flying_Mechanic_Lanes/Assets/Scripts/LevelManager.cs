using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public Vector3 bulletStartPos = new Vector3(0, 0, 0);

    List<GameObject> obstacleObjects = new List<GameObject>();
    List<GameObject> enemyObjects = new List<GameObject>();
    List<GameObject> bulletObjects = new List<GameObject>();

    List<GameObject> toBeCleared = new List<GameObject>();

    Enemy[] enemies;
    Obstacle[] obstacles;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject playerBullet;

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
        GameInput.OnTap += PlayerFire;
    }
	
	// Update is called once per frame
	void Update () {

        // Cull enemies and objects lose to the camera
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

        CleanUp();
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

    public void PlayerFire(Vector3 position)
    {
       
        bulletStartPos = player.GetComponent<Transform>().position;

        Transform bulletStart = player.GetComponent<Transform>();

        bulletStart.position = bulletStartPos;

        GameObject newBullet = (GameObject)Instantiate(playerBullet, transform);

        newBullet.transform.position = bulletStartPos;

        bulletObjects.Add(newBullet);
    }

    public void CleanUp()
    {
        foreach (GameObject enemy in enemyObjects)
        {
            if (enemy.GetComponentInChildren<EnemyHitBox>().destroyThis)
            {
                enemy.SetActive(false);
            }
            
        }
        
        foreach (GameObject bullet in bulletObjects)
        {
            if (bullet.GetComponent<PlayerBullet>().destroyThis)
            {
                toBeCleared.Add(bullet);
            }
        }

        foreach (GameObject bullet in toBeCleared)
        {
            bulletObjects.Remove(bullet);
            bullet.SetActive(false);
            Destroy(bullet);
        }

        toBeCleared.Clear();
    }

    
}
