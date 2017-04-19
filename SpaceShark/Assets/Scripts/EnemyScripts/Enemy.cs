using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    // Reference to the bullet prefab which will be created
    private GameObject enemyBullet = null;

	//[SerializeField]
    // Reference to the sound manager in the scene
    private SoundManager soundManager = null;

    // Whether the player is in range of the enemy
    [SerializeField]
    private bool playerInRange = false;
    // The current position of this enemy
    private Vector3 enemyPosition = new Vector3(0, 0, 0);
    // The time until a shot can be fired again
    private float cooldownProgress;
    // A reference to all created bullets
    private List<GameObject> bulletObjects = new List<GameObject>();

     // The backwards vector from the enemy
    private Vector3 bck = new Vector3(0.0f, 0.0f, 1.0f);
    //Distance for the enemy to check obstacles behind them
    [SerializeField]
    private float RayDistance = 60.0f;

    // Use this for initialization
    void Start()
    {
        soundManager = GameObject.Find("ScreenManager").GetComponent<SoundManager>();
        cooldownProgress = GameSettings.cooldown;
        playerInRange = false;   
    }

    // Update is called once per frame
    void Update()
    {
        CheckLane();

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

        gameObject.transform.position = enemyPosition;

        if (Ship_Movement.shipPosition.z > (transform.position.z - GameSettings.detectionRange))
        {
            playerInRange = true;
        }
    }

    private void CheckLane()
    {
        // Get positional data according to the lane this enemy is in
        enemyPosition.x = Mathf.SmoothStep(enemyPosition.x, LaneManager.laneData[(int)inLane].laneX, Time.deltaTime *GameSettings.laneMoveSpeed);
        enemyPosition.y = Mathf.SmoothStep(enemyPosition.y, LaneManager.laneData[(int)inLane].laneY, Time.deltaTime *GameSettings.laneMoveSpeed);

        //Set up the ray casts for enemy AI
        Vector3 C = new Vector3(0.0f, 0.0f, enemyPosition.z);
        Vector3 N = new Vector3(0.0f, LaneManager.staticLaneWidth, enemyPosition.z);
        Vector3 S = new Vector3(0.0f, -LaneManager.staticLaneWidth, enemyPosition.z);
        Vector3 NE = new Vector3(LaneManager.staticLaneHeight, LaneManager.staticLaneWidth, enemyPosition.z);
        Vector3 E = new Vector3(LaneManager.staticLaneHeight, 0.0f, enemyPosition.z);
        Vector3 SE = new Vector3(LaneManager.staticLaneHeight, -LaneManager.staticLaneWidth, enemyPosition.z);
        Vector3 SW = new Vector3(-LaneManager.staticLaneHeight, -LaneManager.staticLaneWidth, enemyPosition.z);
        Vector3 W = new Vector3(-LaneManager.staticLaneHeight, 0.0f, enemyPosition.z);
        Vector3 NW = new Vector3(-LaneManager.staticLaneHeight, LaneManager.staticLaneWidth, enemyPosition.z);


        // For storing the result of the raycast
        RaycastHit hit;

        //raycast behind enemy to check if an obstacle is in its way
        if (Physics.Raycast(enemyPosition, bck, out hit, RayDistance))
        {
           Debug.Log(hit);

            switch (inLane)
            {
                case LaneManager.PlayerLanes.NW:
                    if (Physics.Raycast(N, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(C, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(W, bck, out hit, RayDistance))
                            {
                                Debug.Log(hit); //no where to move
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.W);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.C);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.N);
                    }
                    break;
                case LaneManager.PlayerLanes.N:
                    if (Physics.Raycast(NE, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(E, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(C, bck, out hit, RayDistance))
                            {
                                if (Physics.Raycast(W, bck, out hit, RayDistance))
                                {
                                    if (Physics.Raycast(NW, bck, out hit, RayDistance))
                                    {
                                        Debug.Log(hit); //no where to move
                                    }
                                    else
                                    {
                                        SetLocation(LaneManager.PlayerLanes.NW);
                                    }
                                }
                                else
                                {
                                    SetLocation(LaneManager.PlayerLanes.W);
                                }
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.C);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.E);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.NE);
                    }
                    break;
                case LaneManager.PlayerLanes.NE:
                    if (Physics.Raycast(N, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(C, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(E, bck, out hit, RayDistance))
                            {
                                Debug.Log(hit); //no where to move
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.E);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.C);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.N);
                    }
                    break;
                case LaneManager.PlayerLanes.W:
                    if (Physics.Raycast(NW, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(N, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(C, bck, out hit, RayDistance))
                            {
                                if (Physics.Raycast(S, bck, out hit, RayDistance))
                                {
                                    if (Physics.Raycast(SW, bck, out hit, RayDistance))
                                    {
                                        Debug.Log(hit); //no where to move
                                    }
                                    else
                                    {
                                        SetLocation(LaneManager.PlayerLanes.SW);
                                    }
                                }
                                else
                                {
                                    SetLocation(LaneManager.PlayerLanes.S);
                                }
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.C);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.N);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.NW);
                    }
                    break;
                case LaneManager.PlayerLanes.C:
                    if (Physics.Raycast(NE, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(E, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(SE, bck, out hit, RayDistance))
                            {
                                if (Physics.Raycast(S, bck, out hit, RayDistance))
                                {
                                    if (Physics.Raycast(SW, bck, out hit, RayDistance))
                                    {
                                        if (Physics.Raycast(W, bck, out hit, RayDistance))
                                        {
                                            if (Physics.Raycast(NW, bck, out hit, RayDistance))
                                            {
                                                if (Physics.Raycast(N, bck, out hit, RayDistance))
                                                {
                                                    Debug.Log(hit); //no where to move
                                                }
                                                else
                                                {
                                                    SetLocation(LaneManager.PlayerLanes.N);
                                                }
                                            }
                                            else
                                            {
                                                SetLocation(LaneManager.PlayerLanes.NW);
                                            }
                                        }
                                        else
                                        {
                                            SetLocation(LaneManager.PlayerLanes.W);
                                        }
                                    }
                                    else
                                    {
                                        SetLocation(LaneManager.PlayerLanes.SW);
                                    }
                                }
                                else
                                {
                                    SetLocation(LaneManager.PlayerLanes.S);
                                }
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.SE);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.E);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.NE);
                    }
                    break;
                case LaneManager.PlayerLanes.E:
                    if (Physics.Raycast(NE, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(N, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(C, bck, out hit, RayDistance))
                            {
                                if (Physics.Raycast(S, bck, out hit, RayDistance))
                                {
                                    if (Physics.Raycast(SE, bck, out hit, RayDistance))
                                    {
                                        Debug.Log(hit); //no where to move
                                    }
                                    else
                                    {
                                        SetLocation(LaneManager.PlayerLanes.SE);
                                    }
                                }
                                else
                                {
                                    SetLocation(LaneManager.PlayerLanes.S);
                                }
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.C);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.N);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.NE);
                    }
                    break;
                case LaneManager.PlayerLanes.SW:
                    if (Physics.Raycast(S, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(C, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(W, bck, out hit, RayDistance))
                            {
                                Debug.Log(hit); //no where to move
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.W);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.C);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.S);
                    }
                    break;
                case LaneManager.PlayerLanes.S:
                    if (Physics.Raycast(SW, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(W, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(C, bck, out hit, RayDistance))
                            {
                                if (Physics.Raycast(E, bck, out hit, RayDistance))
                                {
                                    if (Physics.Raycast(SE, bck, out hit, RayDistance))
                                    {
                                        Debug.Log(hit); //no where to move
                                    }
                                    else
                                    {
                                        SetLocation(LaneManager.PlayerLanes.SE);
                                    }
                                }
                                else
                                {
                                    SetLocation(LaneManager.PlayerLanes.E);
                                }
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.C);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.W);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.SW);
                    }
                    break;
                case LaneManager.PlayerLanes.SE:
                    if (Physics.Raycast(S, bck, out hit, RayDistance))
                    {
                        if (Physics.Raycast(C, bck, out hit, RayDistance))
                        {
                            if (Physics.Raycast(E, bck, out hit, RayDistance))
                            {
                                Debug.Log(hit); //no where to move
                            }
                            else
                            {
                                SetLocation(LaneManager.PlayerLanes.E);
                            }
                        }
                        else
                        {
                            SetLocation(LaneManager.PlayerLanes.C);
                        }
                    }
                    else
                    {
                        SetLocation(LaneManager.PlayerLanes.S);
                    }
                    break;
            }
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
        soundManager.PlayEvent("enemyFire", gameObject);
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
