using UnityEngine;
using System.Collections;

public class EnemyHitBox : MonoBehaviour
{ 
    [SerializeField]
    // Reference to the sound manager in the scene
    private GameObject soundManager;
    
    // Used for tracking whether the enemy has been hit and needs destroying
    public bool destroyEnemy = false;

    public bool enemyDestroyed = false;

    void OnTriggerEnter(Collider other)
    {
        // If enemy is hit by a bullet or obstacle, destroy it and add score
        if ((other.tag == "Bullet") || (other.tag == "Obstacle"))
        {
            // Kill enemy
            GetComponent<EnemyDeathTrigger>().ActivateTrigger();
            destroyEnemy = true;

            // Wwise Enemy Death Trigger
            soundManager.GetComponent<SoundManager>().PlayEvent("enemyDeath", gameObject);
        }
        if (other.tag == "Bullet")
        {
            PlayerScore.EnemyKilled();
            if (other.GetComponent<PlayerBullet>() != null)
            {
                other.GetComponent<PlayerBullet>().destroyThis = true;
            }
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        // If enemy is hit by a bullet or obstacle, destroy it and add score
        if ((other.tag == "Bullet") || (other.tag == "Obstacle"))
        {
            // Kill enemy
            GetComponent<EnemyDeathTrigger>().ActivateTrigger();
            destroyEnemy = true;

            // Wwise Enemy Death Trigger
            soundManager.GetComponent<SoundManager>().PlayEvent("enemyDeath", gameObject);
        }
        if (other.tag == "Bullet")
        {
            PlayerScore.EnemyKilled();
            if (other.GetComponent<PlayerBullet>() != null)
            {
                other.GetComponent<PlayerBullet>().destroyThis = true;
            }
        }
    }
}
