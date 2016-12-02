using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public Vector3 bulletStartPos = new Vector3(0, 0, 0);

    // List<GameObject> obstacleObjects = new List<GameObject>();
 //   [SerializeField]
 //   List<GameObject> rockObjects = new List<GameObject>();


    private List<GameObject> segmentObjects = new List<GameObject>();

    private List<GameObject> bulletObjects = new List<GameObject>();

    private List<GameObject> toBeCleared = new List<GameObject>();

    
   // Obstacle[] obstacles;
   // Rock[] rocks;
    private Segment[] segments;

    

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject playerBullet;

  //  [SerializeField]
 //   private SceneTransition sceneTransitionController = null;

    // int numberOfObstacles;
    int numberOfRocks;
    
    int numberOfSegments;
    private bool playerCanFire = false;

	// Use this for initialization
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

        if (StateManager.gameState == StateManager.States.tutorial)
        {
            if (Ship_Movement.restrictBullet == true)
            {
                GameInput.OnTap -= PlayerFire;
            }
            else
            {
                if (!playerCanFire)
                {
                    GameInput.OnTap += PlayerFire;
                    playerCanFire = true;
                }
            }
        }

        CleanUp();
    }

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
        bulletStartPos = player.GetComponent<Transform>().position;
        GameObject newBullet = (GameObject)Instantiate(playerBullet, transform.position, transform.rotation);
        newBullet.transform.position = bulletStartPos;
        bulletObjects.Add(newBullet);
    }

    public void CleanUp()
    {
        foreach (GameObject seg in segmentObjects)
        {
            seg.GetComponent<Segment>().CleanUpEnemies();
        }

        foreach (GameObject bullet in bulletObjects)
        {
            if (bullet.GetComponent<PlayerBullet>().destroyThis)
            {
                toBeCleared.Add(bullet);
            }
        }

        foreach (GameObject bullet in toBeCleared)
        {
            bulletObjects.Remove(bullet);
            bullet.SetActive(false);
            Destroy(bullet);
        }

        toBeCleared.Clear();
    }

    
}
