using UnityEngine;
using System.Collections;

public class EnemyHitBox : MonoBehaviour
{ 
    //[SerializeField]
    // Reference to the sound manager in the scene
    private SoundManager soundManager;
    
    // Used for tracking whether the enemy has been hit and needs destroying
    public bool destroyEnemy = false;

    public bool enemyDestroyed = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        soundManager = GameObject.Find("ScreenManager").GetComponent<SoundManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // If enemy is hit by a bullet or obstacle, destroy it and add score
        if (other.tag == "Obstacle")
        {
            // Kill enemy
            destroyEnemy = true;

            // Wwise Enemy Death Trigger
            soundManager.PlayEvent("enemyDeath", gameObject);
        }
        if (other.tag == "Bullet")
        {
            PlayerScore.EnemyKilled();
            if (other.GetComponent<PlayerBullet>() != null)
            {
                other.GetComponent<PlayerBullet>().destroyThis = true;
            }
            destroyEnemy = true;

            // Wwise Enemy Death Trigger
            soundManager.PlayEvent("enemyDeath", gameObject);
        }
    }


}
