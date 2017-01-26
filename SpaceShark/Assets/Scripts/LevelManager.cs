using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]

public class LevelManager : MonoBehaviour {

    private List<GameObject> segmentObjects = new List<GameObject>();
    private List<GameObject> bulletObjects = new List<GameObject>();
    private List<GameObject> toBeCleared = new List<GameObject>();
    private bool playerCanFire = false;
    private Segment[] segments;
    

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerBullet;
    [SerializeField]
    private Vector3 bulletOffset =  new Vector3(0,0,0);

    public int numberOfSegments;
    public Vector3 bulletStartPos = new Vector3(0, 0, 0);

    // Initialization
    void Start ()
    {
            playerCanFire = false;
            numberOfSegments = GetComponentsInChildren<Segment>().Length;
            segments = new Segment[numberOfSegments];
            segments = GetComponentsInChildren<Segment>();

            for (int ob = 0; ob < numberOfSegments; ob++)
            {
                segmentObjects.Add(segments[ob].gameObject);
                segments[ob].AcquireObstacles();
            }

            GameInput.ResetTap();
            if (GameInput.CanAddToTap())
            {
                GameInput.OnTap += PlayerFire;
            }
    }
	
	// Update is called once per frame
	void Update ()
    {
            foreach (GameObject seg in segmentObjects)
            {
                seg.GetComponent<Segment>().UpdateSegment(player.transform.position.z - (player.transform.localScale.z));
            }

            if (StateManager.gameState == StateManager.States.tutorial)         //when game is in tutorial level restrict player capabilities 
            {
                if (Ship_Movement.restrictBullet == true)
                {
                    GameInput.OnTap -= PlayerFire;
                }
                else
                {
                    if (!playerCanFire)                                         //renable player capabilities 
                    {
                        GameInput.OnTap += PlayerFire;
                        playerCanFire = true;
                    }
                }
            }
            CleanUp();
    }

    //Reset
    public void ResetLevel()
    {
        if (segmentObjects.Count > 0)
        {
            foreach (GameObject seg in segmentObjects)
            {
                seg.GetComponent<Segment>().ResetObstacles();
            }
        }
        
        if (StateManager.gameState == StateManager.States.tutorial)
        {
            TutorialManager.DisableUI();
        }

        StateManager.gameState = StateManager.States.dead;

    }

    public void PlayerFire(Vector3 position)
    {
        //Fire bullet from left cannon
        bulletStartPos = player.GetComponent<Transform>().position;
        GameObject bulletLeft = (GameObject)Instantiate(playerBullet, transform.position, transform.rotation);
        bulletLeft.transform.position = bulletStartPos + bulletOffset;
        bulletObjects.Add(bulletLeft);

        //Fire bullet from right cannon 
        GameObject bulletRight = (GameObject)Instantiate(playerBullet, transform.position, transform.rotation);
        bulletOffset.x = -1 * bulletOffset.x;
        bulletRight.transform.position = bulletStartPos + bulletOffset;
        bulletObjects.Add(bulletRight);
    }

    public void CleanUp()
    {
        foreach (GameObject seg in segmentObjects)              //Clean up enemies 
        {
            seg.GetComponent<Segment>().CleanUpEnemies();
        }

        foreach (GameObject bullet in bulletObjects)            //Add bullets to clearence list
        {
            if (bullet.GetComponent<PlayerBullet>().destroyThis)
            {
                toBeCleared.Add(bullet);
            }
        }
        foreach (GameObject bullet in toBeCleared)                 //Destroy bullets 
        {
            bulletObjects.Remove(bullet);
            bullet.SetActive(false);
            Destroy(bullet);
        }
        toBeCleared.Clear();
    }
}
