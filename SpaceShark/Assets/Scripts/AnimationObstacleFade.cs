using UnityEngine;
using System.Collections;

public class AnimationObstacleFade : MonoBehaviour {

//  [SerializeField]
  //  private GameObject player = null;

    private Obstacle thisObstacle = null;

    private Renderer[] attachedRenderers = null;

    private Material mat = null;

    [SerializeField]
    private Material solidMaterial = null;

    [SerializeField]
    private Material fadeMaterial = null;

    [SerializeField]
    private Color targetColour = Color.magenta;

    public float transparency = 1.0f;
    [SerializeField]
    private float lowestTransparency = 0.2f;
    [SerializeField]
    private float playerDistanceTop = 10.0f;
    [SerializeField]
    private float playerDistanceMid = 5.0f;

	private int numberOfRenderers;

    private bool playerInRange = false;

    // Use this for initialization
    void Start () {
        thisObstacle = gameObject.GetComponent<Obstacle>();

        //attachedRenderer = gameObject.GetComponent<Renderer>();
        // attachedRenderer.sharedMaterial.color = targetColour;
       // gameObject.GetComponent<Renderer>().material = solidMaterial;
       // mat = gameObject.GetComponent<Renderer>().material;
        //mat.SetColor("_Color", targetColour);

		numberOfRenderers = GetComponentsInChildren<Renderer>().Length;
		attachedRenderers = new Renderer[numberOfRenderers];
		attachedRenderers = GetComponentsInChildren<Renderer>();
		for (int i = 0; i < numberOfRenderers; i++)
		{
			attachedRenderers[i].material = solidMaterial;
			mat = attachedRenderers[i].material;
			mat.SetColor("_Color", targetColour);
		}
    }
	
	// Update is called once per frame
	void Update () {
        float playerZ = Ship_Movement.shipPosition.z;

        if (((int)thisObstacle.location < 5) | (((int)thisObstacle.location > 6) & ((int)thisObstacle.location < 10))) // NW_R, NW_D, N, NE_D, NE_L, SE_U, S, SW_U
        {
            if ((thisObstacle.GetzPosition() - playerZ) < playerDistanceTop)
            {
                playerInRange = true;
                transparency = (thisObstacle.GetzPosition() - playerZ) / playerDistanceTop;
                if (transparency < lowestTransparency)
                {
                    transparency = lowestTransparency;
                }
            }
            else
            {
                playerInRange = false;
                transparency = 1.0f;
            }
        }
        else if ((thisObstacle.location == LaneManager.ObstacleLocation.E) | (thisObstacle.location == LaneManager.ObstacleLocation.W)) // E, W
        {
            if ((thisObstacle.GetzPosition() - playerZ) < playerDistanceMid)
            {
                playerInRange = true;
                transparency = (thisObstacle.GetzPosition() - playerZ) / playerDistanceMid;
                if (transparency < lowestTransparency)
                {
                    transparency = lowestTransparency;
                }
            }
            else // SE_L, SW_R
            {
                playerInRange = false;
                transparency = 1.0f;
            }
        }
        else
        {
            playerInRange = false;
            transparency = 1.0f;
        }

        if (playerInRange)
        {
			/*
            gameObject.GetComponent<Renderer>().material = fadeMaterial;
            mat = gameObject.GetComponent<Renderer>().material;
            targetColour.a = transparency;
            mat.SetColor("_Color", targetColour);
*/
			for (int i = 0; i < numberOfRenderers; i++)
			{
				attachedRenderers[i].material = fadeMaterial;
				mat = attachedRenderers[i].material;
				targetColour.a = transparency;
            	mat.SetColor("_Color", targetColour);
			}
        }
        else
        {
            /*
			gameObject.GetComponent<Renderer>().material = solidMaterial;
            mat = gameObject.GetComponent<Renderer>().material;
			*/
			for (int i = 0; i < numberOfRenderers; i++)
			{
				attachedRenderers[i].material = solidMaterial;
				mat = attachedRenderers[i].material;
			}
        }
               
        
        //attachedRenderer.sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f, transparency);
    }
}
