using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Segment : MonoBehaviour {

	[SerializeField]
	private List<GameObject> obstacleObjects = new List<GameObject>();

	private Obstacle[] obstacles;

	[SerializeField]
	private List<GameObject> enemyObjects = new List<GameObject>();

	private Enemy[] enemies;

	private float[] enemyZStart;

	private int numOfObstacles;
	private int numberOfEnemies;

	// Use this for initialization
	void Start () {
		//AcquireObstacles();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AcquireObstacles()
	{
		if (obstacleObjects.Count > 0)
        {
            obstacleObjects.Clear();
        }
        numOfObstacles = GetComponentsInChildren<Obstacle>().Length;
        obstacles = new Obstacle[numOfObstacles];
        obstacles = GetComponentsInChildren<Obstacle>();
        for (int ob = 0; ob < numOfObstacles; ob++)
        {
            obstacleObjects.Add(obstacles[ob].gameObject);
        }

		numberOfEnemies = GetComponentsInChildren<Enemy>().Length;
        enemies = new Enemy[numberOfEnemies];
        enemyZStart = new float[numberOfEnemies];
        enemies = GetComponentsInChildren<Enemy>();
        for (int ob = 0; ob < numberOfEnemies; ob++)
        {
			
            enemyObjects.Add(enemies[ob].gameObject);
            enemyZStart[ob] = enemies[ob].zPosition;
        }
	}

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
            enemy.GetComponentInChildren<EnemyHitBox>().destroyThis = false;
            iterator++;
        }
    }

	public void CleanUpEnemies()
	{
		 foreach (GameObject enemy in enemyObjects)
        {
            if (enemy.GetComponentInChildren<EnemyHitBox>().destroyThis)
            {
                enemy.SetActive(false);
            }
        }
	}

}
