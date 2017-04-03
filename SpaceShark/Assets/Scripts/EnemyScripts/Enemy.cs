using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
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
    //[SerializeField]
    //// Refernce to the player object
    //private Ship_Movement player = null;
    [SerializeField]
    // Reference to the bullet prefab which will be created
    private GameObject enemyBullet = null;

	[SerializeField]
    // Reference to the sound manager in the scene
    private GameObject soundManager;

    // Whether the player is in range of the enemy
    [SerializeField]
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
        cooldownProgress = GameSettings.cooldown;
        playerInRange = false;
        Vector3 bound = GetComponent<BoxCollider>().size;
        GetComponent<BoxCollider>().size = new Vector3(bound.x, bound.y, 2.0f * GameSettings.detectionRange);
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
                cooldownProgress = GameSettings.cooldown;
            }
        }
        else
        {
            enemyPosition.z = zPosition + GetComponentInParent<SegmentData>().gameObject.transform.position.z;
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

        if (Ship_Movement.shipPosition.z > (transform.position.z - GameSettings.detectionRange))
        {
            playerInRange = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If player has entered detection range, start firing at it
        if (other.tag == "Player")
        {
           // player in range
        }
    }

    // Reset all variables and bullet objects
    public void ResetEnemy()
    {
        cooldownProgress = GameSettings.cooldown;
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

        // Wwise Enemy Fire Event
        soundManager.GetComponent<SoundManager>().PlayEvent("enemyFire", gameObject);
    }

    // Setters
    public void SetzPosition(float zPos)
    {
        zPosition = zPos;
    }

    public void SetLocation(LaneManager.PlayerLanes lane)
    {
        inLane = lane;
    }

    // Getter for zPosition
    public float GetZPosition()
    {
        return zPosition;
    }

    public bool IsPlayerInRange()
    {
        return playerInRange;
    }

}
