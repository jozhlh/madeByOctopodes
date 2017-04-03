using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    // Whether the bullet will need destroying
    public bool destroyThis = false;

    [Header("Bullet Variables")]
    [SerializeField]
    private float bulletSpeed = 40.0f;
    [SerializeField]
    private float bulletCulling = 100.0f;

    private Vector3 bulletPosition = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update ()
    {
        // Move the bullet forwards according to its speed
        bulletPosition = gameObject.transform.position;
        bulletPosition.z += Time.deltaTime * (bulletSpeed * Ship_Movement.gameSpeed);

        // Destroy the bullet if it has reached its culling range
        if (bulletPosition.z > (Ship_Movement.shipPosition.z + bulletCulling))
        {
            destroyThis = true;
        }

        gameObject.transform.position = bulletPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        // Destroy bullet if it hits a wall or enemy
        if ((other.tag == "Obstacle") | (other.tag == "Enemy"))
        {
            destroyThis = true;
            // Check whether it was the enemy's detection box or hit box that was collided with
            if (other.tag == "Enemy")
            {
                if (other.GetComponent<EnemyHitBox>())
                {
                    other.GetComponent<EnemyHitBox>().destroyEnemy = true;
                }
                else
                {
                    destroyThis = false;
                }
            }
        }
    }

}
