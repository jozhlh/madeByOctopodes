using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Enemy : MonoBehaviour
{
    [Header("Placement Settings")]
    [SerializeField]
    // The position of the enemy in the segment
    private float zPosition = 0.0f;
    [SerializeField]
    // The lane which the enemy is in
    private LaneManager.PlayerLanes inLane;

    [Header("Object References")]
    [SerializeField]
    // Refernce to the player object
    private Ship_Movement player = null;
    [SerializeField]
    // Reference to the bullet prefab which will be created
    private GameObject enemyBullet = null;

    [Header("Attributes")]
    [SerializeField]
    // The time between shots fired
    private float cooldown = 2;
    // Whether the player is in range of the enemy
    private bool playerInRange = false;
    // The current position of this enemy
    private Vector3 enemyPosition = new Vector3(0, 0, 0);
    // The time until a shot can be fired again
    private float cooldownProgress;
    // A reference to all created bullets
    private List<GameObject> bulletObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        cooldownProgress = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        // Get positional data according to the lane this enemy is in
        enemyPosition.x = LaneManager.laneData[(int)inLane].laneX;
        enemyPosition.y = LaneManager.laneData[(int)inLane].laneY;

        // Advance cooldown
        cooldownProgress -= Time.deltaTime;

        // If the player is in range, move the enemy, otherwise put it in starting position
        if (playerInRange)
        {
            enemyPosition.z += (Ship_Movement.gameSpeed * Time.deltaTime);

            // If the player is in range fire at the player
            if (cooldownProgress < 0)
            {
                Fire();
                cooldownProgress = cooldown;
            }
        }
        else
        {
            enemyPosition.z = zPosition + GetComponentInParent<Segment>().gameObject.transform.position.z;;
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

    void OnTriggerEnter(Collider other)
    {
        // If player has entered detection range, start firing at it
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    // Reset all variables and bullet objects
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
    }

    // Instantiate a bullet object at this enemy's position
    void Fire()
    {
        GameObject newBullet = (GameObject)Instantiate(enemyBullet, transform.position, transform.rotation);

        newBullet.transform.position = transform.position;
        newBullet.GetComponent<EnemyBullet>().Init(Ship_Movement.shipPosition, transform.position);

        // Add the created bullet to this enemy's list of bullets
        bulletObjects.Add(newBullet);
    }

    // Getter for zPosition
    public float GetZPosition()
    {
        return zPosition;
    }

}
