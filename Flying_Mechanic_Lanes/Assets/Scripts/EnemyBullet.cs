using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    [SerializeField]
    private float bulletSpeed = 40.0f;

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
        // Destroy bullet if it hits a wall
        if ((other.tag == "Obstacle") | (other.tag == "Player"))
        {
       //     Debug.Log("EnemyBullet hit " + other.tag);
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
        // gameObject.transform.Translate(0.0f, 0.0f, Time.deltaTime * bulletSpeed);
        // Find the distance covered so far and what the % of the dis that is then move the enemy to the next step
        float distCovered = (Time.time - timeStartedMoving) * bulletSpeed;
        //float percentOfJourney = distCovered / Vector3.Distance(startPos, targetLocation);
        //bulletPosition = Vector3.Lerp(startPos, targetLocation, percentOfJourney);

        bulletTrajectory = targetLocation - startPos;

        bulletPosition += (bulletTrajectory * Time.deltaTime);

        bulletPosition.z += Time.deltaTime  * Ship_Movement.gameSpeed;

        if (bulletPosition.z > 60)
        {
            destroyThis = true;
        }

        gameObject.transform.position = bulletPosition;



    }
}
