using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public float bulletSpeed = 40.0f;

    private Vector3 bulletPosition = new Vector3();

    public bool destroyThis = false;
    public Vector3 targetLocation = new Vector3();
    public Vector3 startPos = new Vector3();
    public Vector3 bulletTrajectory = new Vector3();

    private float timeStartedMoving;


    // Use this for initialization
    void Start()
    {
        bulletPosition = startPos;

        timeStartedMoving = Time.time;
    }

    void OnTriggerEnter(Collider other)
    {
        // Destroy bullet if it hits a wall or the player
        if ((other.tag == "Obstacle") | (other.tag == "Player"))
        {
            if (other.tag == "Player")
            {
                // Reduce player's health
                //other.GetComponent<EnemyHitBox>().destroyThis = true;
            }
            destroyThis = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bulletTrajectory = targetLocation - startPos;

        bulletPosition += (bulletTrajectory * Time.deltaTime);

        bulletPosition.z += Time.deltaTime * Ship_Movement.gameSpeed;

        if (bulletPosition.z > 60)
        {
            destroyThis = true;
        }

        gameObject.transform.position = bulletPosition;



    }
}
