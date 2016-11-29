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
        newBullet.GetComponent<EnemyBullet>().targetLocation = GetTargetPosition(bulletStartPos);
        newBullet.GetComponent<EnemyBullet>().startPos = bulletStartPos;

        bulletObjects.Add(newBullet);
    }

    // Calculate the time when we can hit a target with a bullet
    // Return a negative time if there is no solution
    // http://howlingmoonsoftware.com/wordpress/leading-a-target/
    protected float AimAhead(Vector3 delta, Vector3 vr, float muzzleV)
    {
        // Quadratic equation coefficients a*t^2 + b*t + c = 0
        float a = Vector3.Dot(vr, vr) - muzzleV*muzzleV;
        float b = 2f*Vector3.Dot(vr, delta);
        float c = Vector3.Dot(delta, delta);

        float det = b*b - 4f*a*c;

        // If the determinant is negative, then there is no solution
        if(det > 0f)
        {
            return 2f*c/(Mathf.Sqrt(det) - b);
        } else 
        {
            return -1f;
        }
    }

    private Vector3 GetTargetPosition(Vector3 ourPos)
    {
        Vector3 aimPoint = new Vector3(0,0,0);
        // Find the relative position and velocities
        Vector3 delta = Ship_Movement.shipPosition - ourPos;
        //Vector3 vr = target.velocity - gun.velocity;
        Vector3 vr = new Vector3(0,0,0);

        float bulletSpeed = enemyBullet.GetComponent<EnemyBullet>().bulletSpeed;
        // Calculate the time a bullet will collide
        // if it's possible to hit the target.
        float t = AimAhead(delta, vr, bulletSpeed);

        // If the time is negative, then we didn't get a solution.
        if(t > 0f)
        {
            // Aim at the point where the target will be at the time
            // of the collision.
            aimPoint = Ship_Movement.shipPosition + (t * new Vector3(0,0,1));

            // fire at aimPoint!!!
        }
        return aimPoint;
    }
}
