using UnityEngine;
using System.Collections.Generic;

public class Segment : MonoBehaviour {

	[SerializeField]
	private List<GameObject> obstacleObjects = new List<GameObject>();

	private Rock[] obstacles;

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
        numOfObstacles = GetComponentsInChildren<Rock>().Length;
        obstacles = new Rock[numOfObstacles];
        obstacles = GetComponentsInChildren<Rock>();
        Debug.Log("numberOfRocks: " + numOfObstacles);
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
            Debug.Log("Rock Set Active Called");
            ob.SetActive(true);
        }
	}
}
