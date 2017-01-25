using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    [SerializeField]
    // How quickly the bullet moves toward the player
    private float bulletSpeed = 40.0f;

    // Positional data
    private Vector3 bulletPosition = new Vector3();
    private Vector3 bulletTrajectory = new Vector3();

    // Status data
    private bool destroyThis = false;
    private float timeStartedMoving;


    // Use this for initialization
    void Start()
    {
        timeStartedMoving = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet along the vector toward the player
        bulletPosition += (bulletTrajectory * Time.deltaTime);

        // Move the bullet in the z axis to mimic aiming ahead of the player
        bulletPosition.z += Time.deltaTime * Ship_Movement.gameSpeed;

        // Set the newly calculated position
        gameObject.transform.position = bulletPosition;
    }

    // Calculate the vector between the bullet and the player
    public void Init(Vector3 target, Vector3 start)
    {
        bulletTrajectory = target - start;
        bulletPosition = start;
    }

    void OnTriggerEnter(Collider other)
    {
        // Destroy bullet if it hits a wall or the player
        if ((other.tag == "Obstacle") | (other.tag == "Player"))
        {
            if (other.tag == "Player")
            {
                // Reduce player's health
            }
            destroyThis = true;
        }
    }
}
