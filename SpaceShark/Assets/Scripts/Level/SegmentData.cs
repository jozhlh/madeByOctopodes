using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SegmentData : MonoBehaviour
{
    [Header("Models")]
    //[SerializeField]
    //// The obstacle prefab for this level
    //private GameObject obstacle = null;
    //[SerializeField]
    //// The enemy prefab for this level
    //private GameObject enemy = null;
    [SerializeField]
    // The mesh which is used to show the obstacle's position
    private Mesh obMesh = null;

    [Header("Obstacles")]
    [SerializeField]
    // The data used to populate the segment with obstacles
    private List<ObstacleData> obstacleTemplates = new List<ObstacleData>();
    // The obstacles which currently populate the segment
    private List<GameObject> obstacleObjects = new List<GameObject>();

    [Header("Enemies")]
    [SerializeField]
    // The data used to populate the segment with enemies
    private List<EnemyData> enemyTemplates = null;
    // The enemies which currently populate the segment
    private List<GameObject> enemyObjects = new List<GameObject>();
    // The dead enemy fragments
    private List<GameObject> deadEnemyObjects = new List<GameObject>();

    // When the segment is selected, draw a grid to show the lanes, draw all children of the segment
    void OnDrawGizmosSelected()
    {
        // Calculate lane sizes and positions and draw them in magenta
        LaneManager.Recalculate();
        Gizmos.color = Color.magenta;
        for (int j = 0; j < 9; j++)
        {
            Gizmos.DrawWireCube(new Vector3(LaneManager.laneData[j].laneX, LaneManager.laneData[j].laneY, (LaneManager.lengthOfSegment / 2) + transform.position.z), new Vector3(LaneManager.laneSpacingHorizontal, LaneManager.laneSpacingVertical, LaneManager.lengthOfSegment));
        }

        // Calculate obstacle positions and draw them in cyan using the provided mesh
        Gizmos.color = Color.cyan;
        Vector3 obstaclePosition = new Vector3(0, 0, 0);
        Quaternion obstacleRotation = new Quaternion();
        Vector3 obstacleSize = new Vector3(200, 800, 200);
        foreach (ObstacleData ob in obstacleTemplates)
        {
            obstaclePosition.x = LaneManager.obstacleLocationData[(int)ob.lane].xPos;
            obstaclePosition.y = LaneManager.obstacleLocationData[(int)ob.lane].yPos;
            obstaclePosition.z = ob.zPosition + transform.position.z;
            obstacleRotation = Quaternion.Euler(0, 0, LaneManager.obstacleLocationData[(int)ob.lane].zRot);
            Gizmos.DrawMesh(obMesh, obstaclePosition, obstacleRotation, obstacleSize);
        }

        // Calculate the enemy positions and draw them as a red sphere
        Gizmos.color = Color.red;
        foreach (EnemyData en in enemyTemplates)
        {
            obstaclePosition.x = LaneManager.laneData[(int)en.lane].laneX;
            obstaclePosition.y = LaneManager.laneData[(int)en.lane].laneY;
            obstaclePosition.z = en.zPosition + transform.position.z;
            Gizmos.DrawSphere(obstaclePosition, LaneManager.laneSpacingVertical * 0.5f);
        }
    }

    // If the segement has already been populated with children, remove them from the segment and the scene
    private void ClearScene()
    {
        if (obstacleObjects.Count > 0)
        {
            foreach (GameObject ob in obstacleObjects)
            {
                DestroyImmediate(ob);
            }
            obstacleObjects.Clear();
        }

        if (enemyObjects.Count > 0)
        {
            foreach (GameObject ob in enemyObjects)
            {
                DestroyImmediate(ob);
            }
            enemyObjects.Clear();
        }

        if (deadEnemyObjects.Count > 0)
        {
            foreach (GameObject ob in deadEnemyObjects)
            {
                DestroyImmediate(ob);
            }
            deadEnemyObjects.Clear();
        }
    }

    // Remove all children of the segment from the scene, then populate the segment
    public void PlaceSegment()
    {
        ClearScene();
        foreach (ObstacleData ob in obstacleTemplates)
        {
            GameObject newOb = Instantiate(GameSettings.obstacle, transform);
            newOb.GetComponent<Obstacle>().PlaceObstacle(ob);
            obstacleObjects.Add(newOb);
        }

        foreach (EnemyData en in enemyTemplates)
        {
            GameObject newEn = Instantiate(GameSettings.enemy, transform);
            newEn.GetComponent<Enemy>().SetzPosition(en.zPosition);
            newEn.GetComponent<Enemy>().SetLocation(en.lane);
            newEn.GetComponent<Enemy>().ResetEnemy();
            enemyObjects.Add(newEn);
        }

        ResetObstacles();
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

        //foreach (GameObject enemy in enemyObjects)
        //{
        //    if ((enemy.transform.position.z + (enemy.transform.localScale.z / 2)) < playerBoundary)
        //    {
        //        Debug.Log("Deactivate");
        //        enemy.SetActive(false);
        //    }
        //}

        foreach (GameObject enemy in deadEnemyObjects)
        {
            if (enemy.activeInHierarchy)
            {
                if ((enemy.transform.position.z) > EndPlate.horizon)
                {
                    Debug.Log("Destroy Carcass");
                    enemy.SetActive(false);
                }
                if ((enemy.transform.position.y) < -5.0f)
                {
                    Debug.Log("Destroy Carcass");
                    enemy.SetActive(false);
                }
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
            enemy.GetComponentInChildren<EnemyHitBox>().enemyDestroyed = false;
            iterator++;
        }

        if (deadEnemyObjects.Count > 0)
        {
            foreach (GameObject ob in deadEnemyObjects)
            {
                Destroy(ob);
            }
            deadEnemyObjects.Clear();
        }
    }

    // Destroy any enemies that have been killed
    public void CleanUpEnemies()
    {
        foreach (GameObject enemy in enemyObjects)
        {
            if (enemy.GetComponentInChildren<EnemyHitBox>().destroyEnemy & !enemy.GetComponentInChildren<EnemyHitBox>().enemyDestroyed)
            {
                deadEnemyObjects.Add(Instantiate(GameSettings.enemyDeath, enemy.transform.position, enemy.transform.rotation));
                Debug.Log("Spawn Carcass");
                enemy.GetComponentInChildren<EnemyHitBox>().enemyDestroyed = true;
                //Debug.Log("Deactivate");
                enemy.SetActive(false);
                //GetComponent<Animator>().
            }
        }
    }
}
