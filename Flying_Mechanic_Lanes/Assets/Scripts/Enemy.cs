using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public LaneManager.PlayerLanes inLane;

    [SerializeField]
    private float zPosition = 0.0f;
    [SerializeField]
    private float obstacleLength = 1.0f;

    [SerializeField]
    private Ship_Movement player = null;

    private bool playerInRange = false;

    private Vector3 enemyPosition = new Vector3(0, 0, 0);
    //private Vector3 obstacleSize = new Vector3(1, 1, 1);


    // Use this for initialization
    void Start()
    {

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

        if (playerInRange)
        {
            enemyPosition.z += (player.shipForwardSpeed * Time.deltaTime);

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

        // Raycast behind itself
            // if obstacle within range
                // raycast to all lanes within range
                    // look through lanes to find empty lane
                    // raycast sideways to target lane
                    // if route clear
                        // move to that lane
        
        gameObject.transform.position = enemyPosition;

        
       

    }
}
