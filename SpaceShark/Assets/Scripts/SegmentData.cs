using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SegmentData : MonoBehaviour {

    [SerializeField]
    private GameObject obstacle = null;
    [SerializeField]
    private List<ObstacleData> obstacleTemplates = null;
    private List<GameObject> obstacleObjects = null;

    [SerializeField]
    private GameObject enemy = null;
    [SerializeField]
    private List<EnemyData> enemyTemplates = null;
    private List<GameObject> enemyObjects = null;

    [SerializeField]
    private bool clearObjectsFromScene = false;
    [SerializeField]
    private bool placeObjectsInScene = false;

    void Start()
    {
        obstacleObjects = new List<GameObject>();
        enemyObjects = new List<GameObject>();
   //     obstacleTemplates = new List<ObstacleData>();
    }

    void Update()
    {
        // If designer selects clearObjectsFromScene from inspector, the scene will be cleared of this segment's objects
        if (clearObjectsFromScene)
        {
            ClearScene();
            clearObjectsFromScene = false;
        }
        // If designer selects placeObjectsInScene from inspector, the scene will be populated with this segment's objects
        if (placeObjectsInScene)
        {
            PlaceSegment();
            placeObjectsInScene = false;
        }
        int count = obstacleTemplates.Count;
        if (obstacleObjects.Count == count)
        {
            for (int i = 0; i < count; i++)
            {
                obstacleObjects[i].GetComponent<Obstacle>().SetzPosition(obstacleTemplates[i].zPosition);
                obstacleObjects[i].GetComponent<Obstacle>().SetLocation(obstacleTemplates[i].lane);
                // obstacleObjects[i].GetComponent<Obstacle>().SetzPosition(obstacleTemplates[i].zPosition);
            }
        }
     //   else { Debug.Log("Inconsistent Lists"); }


        //count = enemyTemplates.Count;
        //for (int i = 0; i < count; i++)
        //{
        //   // if (!enemyObjects[i].GetComponent<Enemy>().IsPlayerInRange())
        //    //{
        //        enemyObjects[i].GetComponent<Enemy>().SetzPosition(enemyTemplates[i].zPosition);
        //        enemyObjects[i].GetComponent<Enemy>().SetLocation(enemyTemplates[i].lane);
        //   // }
            
        //    // obstacleObjects[i].GetComponent<Obstacle>().SetzPosition(obstacleTemplates[i].zPosition);
        //}


        UpdateSegment();
    }

    private void ClearScene()
    {
        foreach (GameObject ob in obstacleObjects)
        {
            DestroyImmediate(ob);
        }
        obstacleObjects.Clear();

        foreach (GameObject ob in enemyObjects)
        {
            DestroyImmediate(ob);
        }
        enemyObjects.Clear();
    }

    public void UpdateSegment()
    {
        if (obstacleObjects.Count > 0)
        {
            foreach (GameObject ob in obstacleObjects)
            {
                if (ob.GetComponent<Obstacle>())
                {
                    ob.GetComponent<Obstacle>().PlaceObstacle();
                }
            }
        }
    }

    public void PlaceSegment()
    {
        foreach (ObstacleData ob in obstacleTemplates)
        {
            GameObject newOb = Instantiate(obstacle, transform);
            newOb.GetComponent<Obstacle>().SetzPosition(ob.zPosition);
            newOb.GetComponent<Obstacle>().SetLocation(ob.lane);
            newOb.GetComponent<Obstacle>().PlaceObstacle();
            obstacleObjects.Add(newOb);
        }

        foreach (EnemyData en in enemyTemplates)
        {
            GameObject newEn = Instantiate(enemy, transform);
            newEn.GetComponent<Enemy>().SetzPosition(en.zPosition);
            newEn.GetComponent<Enemy>().SetLocation(en.lane);
            newEn.GetComponent<Enemy>().ResetEnemy();
            obstacleObjects.Add(newEn);
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
            enemy.GetComponent<Enemy>().ResetEnemy();
            //enemies[iterator].ResetEnemy();
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
