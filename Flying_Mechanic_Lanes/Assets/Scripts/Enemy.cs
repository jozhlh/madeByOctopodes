using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Enemy : MonoBehaviour {

    public LaneManager.PlayerLanes inLane;

    List<GameObject> bulletObjects = new List<GameObject>();

    //[SerializeField]
    public float zPosition = 0.0f;
    [SerializeField]
    private float obstacleLength = 1.0f;

    [SerializeField]
    private Ship_Movement player = null;

    [SerializeField]
    private GameObject enemyBullet = null;

    private bool playerInRange = false;

    private Vector3 enemyPosition = new Vector3(0, 0, 0);
    //private Vector3 obstacleSize = new Vector3(1, 1, 1);

    [SerializeField]
    private float cooldown = 2;
    private float cooldownProgress;

    // Use this for initialization
    void Start()
    {
        cooldownProgress = cooldown;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition.x = LaneManager.laneData[(int)inLane].laneX;
        enemyPosition.y = LaneManager.laneData[(int)inLane].laneY;
        cooldownProgress -= Time.deltaTime;

        if (playerInRange)
        {
            enemyPosition.z += (player.shipForwardSpeed * Time.deltaTime);

            
            if (cooldownProgress < 0)
            {
                Fire();
                cooldownProgress = cooldown;
            }
            // fire at player
            // if not on cooldown
            // get player position
            // calculate where player will be for collision
            // shoot there
            // start cooldown
        }
        else
        {
            enemyPosition.z = zPosition;
        }

        //TODO: Enemys to avoid obstacles
        // Raycast behind itself
            // if obstacle within range
                // raycast to all lanes within range
                    // look through lanes to find empty lane
                    // raycast sideways to target lane
                    // if route clear
                        // move to that lane
        
        gameObject.transform.position = enemyPosition;

    }

    public void ResetEnemy()
    {
        cooldownProgress = cooldown;
        enemyPosition.z = zPosition;
        playerInRange = false;
        foreach (GameObject bullet in bulletObjects)
        {
            Destroy(bullet);
        }
        bulletObjects.Clear();
  //      Debug.Log("Reset to position" + zPosition);
    }

    void Fire()
    {
        Vector3 playerPos = new Vector3();
        Vector3 bulletStartPos = new Vector3();

        playerPos = player.transform.position;

        bulletStartPos = GetComponent<Transform>().position;

        Transform bulletStart = GetComponent<Transform>();

        bulletStart.position = bulletStartPos;

        GameObject newBullet = (GameObject)Instantiate(enemyBullet, transform.position, transform.rotation);

        newBullet.transform.position = bulletStartPos;
        newBullet.GetComponent<EnemyBullet>().targetLocation = playerPos;
        newBullet.GetComponent<EnemyBullet>().startPos = bulletStartPos;

        bulletObjects.Add(newBullet);
    }
}
