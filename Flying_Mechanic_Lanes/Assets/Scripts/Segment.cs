using UnityEngine;
using System.Collections.Generic;

public class Segment : MonoBehaviour {

	[SerializeField]
	private List<GameObject> obstacleObjects = new List<GameObject>();

	private Obstacle[] obstacles;

	private int numOfObstacles;

	// Use this for initialization
	void Start () {
		//AcquireObstacles();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AcquireObstacles()
	{
		if (obstacleObjects.Count > 0)
        {
            obstacleObjects.Clear();
        }
        numOfObstacles = GetComponentsInChildren<Obstacle>().Length;
        obstacles = new Obstacle[numOfObstacles];
        obstacles = GetComponentsInChildren<Obstacle>();
        for (int ob = 0; ob < numOfObstacles; ob++)
        {
            obstacleObjects.Add(obstacles[ob].gameObject);
        }
	}

	public void CullObstacles(float playerBoundary)
	{
		foreach (GameObject ob in obstacleObjects)
        {
			if((ob.transform.position.z + (ob.transform.localScale.z)) < playerBoundary)
            {
            	ob.SetActive(false);
			}
        }
	}

    public void ResetObstacles()
    {
        foreach (GameObject ob in obstacleObjects)
        {
            ob.SetActive(true);
        }
    }

}
