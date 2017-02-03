using UnityEngine;
using System.Collections;

public class EnemyHitBox : MonoBehaviour
{ 
    // Used for tracking whether the enemy has been hit and needs destroying
    public bool destroyEnemy = false;

    public bool enemyDestroyed = false;

    void OnTriggerEnter(Collider other)
    {
        // If enemy is hit by a bullet or obstacle, destroy it and add score
        if ((other.tag == "Bullet") | (other.tag == "Obstacle"))
        {
            // Kill enemy
            GetComponent<EnemyDeathTrigger>().ActivateTrigger();
            destroyEnemy = true;
        }
        if (other.tag == "Bullet")
        {
            PlayerScore.EnemyKilled();
        }
    }
}
