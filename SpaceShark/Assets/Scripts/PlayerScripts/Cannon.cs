using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

	[SerializeField]
    // Reference to the player in the scene
    private GameObject player;
	[SerializeField]
    // Reference to the bullet prefab which the player fires
    private GameObject playerBullet;
    [SerializeField]
    // The distance from the player's transform to the gun on the model (-1.25, -0.4, 1.5)
    private Vector3 bulletOffset =  new Vector3(0,0,0);
    // Holds references to the bullets in the scene
    private List<GameObject> bulletObjects = new List<GameObject>();
    // Whether the player is able to shhot
    private bool playerCanFire = false;

	// Use this for initialization
	void Start () 
	{
		// Add callback event for player being able to fire
        playerCanFire = false;
        GameInput.ResetTap();
        if (GameInput.CanAddToTap())
        {
            GameInput.OnTap += PlayerFire;
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		// When game is in tutorial level restrict player capabilities 
        if (StateManager.gameState == StateManager.States.tutorial)         
        {
            if (Ship_Movement.restrictBullet == true)
            {
                GameInput.OnTap -= PlayerFire;
            }
            // Re-enable player capabilities 
            else
            {
                if (!playerCanFire)                                         
                {
                    GameInput.OnTap += PlayerFire;
                    playerCanFire = true;
                }
            }
        }
		else if (StateManager.gameState == StateManager.States.dead)
		{
			GameInput.ResetTap();
		}
        CleanUp();
	}

	public void PlayerFire(Vector3 position)
    {
        // Fire bullet from left cannon
        Vector3 bulletStartPos = player.GetComponent<Transform>().position;
        GameObject bulletLeft = (GameObject)Instantiate(playerBullet, transform.position, transform.rotation);
        bulletLeft.transform.position = bulletStartPos + bulletOffset;
        bulletObjects.Add(bulletLeft);

        // Fire bullet from right cannon 
        GameObject bulletRight = (GameObject)Instantiate(playerBullet, transform.position, transform.rotation);
        bulletOffset.x = -1 * bulletOffset.x;
        bulletRight.transform.position = bulletStartPos + bulletOffset;
        bulletObjects.Add(bulletRight);

        GetComponent<FireTrigger>().ActivateTrigger();
        //customTriggers.CustomTrigger();
    }

	// Destroy or disable any expired game objects
    public void CleanUp()
    {
        List<GameObject> toBeCleared = new List<GameObject>();

        // Add bullets to clearance list
        foreach (GameObject bullet in bulletObjects)            
        {
            if (bullet.GetComponent<PlayerBullet>().destroyThis)
            {
                toBeCleared.Add(bullet);
            }
        }

        // Destroy bullets 
        foreach (GameObject bullet in toBeCleared)                 
        {
            bulletObjects.Remove(bullet);
            bullet.SetActive(false);
            Destroy(bullet);
        }
        toBeCleared.Clear();
    }
}
