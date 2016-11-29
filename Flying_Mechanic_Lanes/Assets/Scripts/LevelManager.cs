using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public Vector3 bulletStartPos = new Vector3(0, 0, 0);

    // List<GameObject> obstacleObjects = new List<GameObject>();
    [SerializeField]
    List<GameObject> rockObjects = new List<GameObject>();
    [SerializeField]
    List<GameObject> segmentObjects = new List<GameObject>();
    List<GameObject> enemyObjects = new List<GameObject>();
    List<GameObject> bulletObjects = new List<GameObject>();

    List<GameObject> toBeCleared = new List<GameObject>();

    Enemy[] enemies;
   // Obstacle[] obstacles;
    Rock[] rocks;
    Segment[] segments;

    private float[] enemyZStart;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private SceneTransition sceneTransitionController = null;

    // int numberOfObstacles;
    int numberOfRocks;
    int numberOfEnemies;
    int numberOfSegments;

	// Use this for initialization
	void Start ()
    {
        /*
        numberOfObstacles = GetComponentsInChildren<Obstacle>().Length;
        obstacles = new Obstacle[numberOfObstacles];

        obstacles = GetComponentsInChildren<Obstacle>();
        for (int ob = 0; ob < numberOfObstacles; ob++)
        {
            obstacleObjects.Add(obstacles[ob].gameObject);
        }
        */

        numberOfSegments = GetComponentsInChildren<Segment>().Length;
        Debug.Log(numberOfSegments + " segments found");
        segments = new Segment[numberOfSegments];
        segments = GetComponentsInChildren<Segment>();
        for (int ob = 0; ob < numberOfSegments; ob++)
        {
            segmentObjects.Add(segments[ob].gameObject);
            segments[ob].AcquireObstacles();
        }

        /*numberOfRocks = GetComponentsInChildren<Rock>().Length;
        rocks = new Rock[numberOfRocks];
        rocks = GetComponentsInChildren<Rock>();
        for (int ob = 0; ob < numberOfRocks; ob++)
        {
            rockObjects.Add(rocks[ob].gameObject);
        }*/

        numberOfEnemies = GetComponentsInChildren<Enemy>().Length;
        enemies = new Enemy[numberOfEnemies];
        enemyZStart = new float[numberOfEnemies];
        enemies = GetComponentsInChildren<Enemy>();
        for (int ob = 0; ob < numberOfEnemies; ob++)
        {
            enemyObjects.Add(enemies[ob].gameObject);
            enemyZStart[ob] = enemies[ob].zPosition;
        }
        GameInput.OnTap += PlayerFire;
    }
	
	// Update is called once per frame
	void Update () {

        // Cull enemies and objects lose to the camera
	   /* foreach (GameObject rock in rockObjects)
        {
            if((rock.transform.position.z + (rock.transform.localScale.z)) < (player.transform.position.z - (player.transform.localScale.z)))
            {
                rock.SetActive(false);
            }
        }*/

        foreach (GameObject seg in segmentObjects)
        {
            
            seg.GetComponent<Segment>().CullObstacles(player.transform.position.z - (player.transform.localScale.z));
            //if((seg.transform.position.z + (seg.transform.localScale.z)) < (player.transform.position.z - (player.transform.localScale.z)))
            //{
            //    seg.SetActive(false);
            //}
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

    private void AcquireRocks()
    {
        if (rockObjects.Count > 0)
        {
            rockObjects.Clear();
        }
        numberOfRocks = GetComponentsInChildren<Rock>().Length;
        rocks = new Rock[numberOfRocks];
        rocks = GetComponentsInChildren<Rock>();
        Debug.Log("numberOfRocks: " + numberOfRocks);
        for (int ob = 0; ob < numberOfRocks; ob++)
        {
            rockObjects.Add(rocks[ob].gameObject);
        }
    }

    public void ResetLevel()
    {
        Debug.Log(numberOfSegments + " segments found");
        //AcquireRocks();
        //Debug.Log(rockObjects.Count);
        //foreach (GameObject rock in rockObjects)
        //{
        //    Debug.Log("Rock Set Active Called");
        //    rock.SetActive(true);
        //}
        Debug.Log("LevelManager.ResetLevel Called");
        if (segmentObjects.Count > 0)
        {
            foreach (GameObject seg in segmentObjects)
            {
                seg.GetComponent<Segment>().ResetObstacles();
            }
        }
        else
        {
            Debug.Log("Lost segments");
        }
        int iterator = 0;
        foreach (GameObject enemy in enemyObjects)
        {
            enemy.SetActive(true);
            enemies[iterator].ResetEnemy();
            enemy.GetComponentInChildren<EnemyHitBox>().destroyThis = false;
            iterator++;
        }
        //sceneTransitionController.LoadMenu();
        StateManager.gameState = StateManager.States.dead;
    }

    public void PlayerFire(Vector3 position)
    {
       
        bulletStartPos = player.GetComponent<Transform>().position;

        Transform bulletStart = player.GetComponent<Transform>();

        bulletStart.position = bulletStartPos;

        GameObject newBullet = (GameObject)Instantiate(playerBullet, transform.position, transform.rotation);

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
