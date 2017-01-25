using UnityEngine;
using System.Collections;

public class AnimationObstacleFade : MonoBehaviour {

    [Header("Fade Distance Settings")]
    [SerializeField]
    // The lowest level the alpha value will lerp to
    private float lowestTransparency = 0.2f;     
    [SerializeField]
    // The distance from the player an obstacle in the top lane will begin to fade out
    private float playerDistanceTop = 10.0f;
    [SerializeField]
    // The distance from the player an obstacle in the middle lane will begin to fade out
    private float playerDistanceMid = 5.0f;

    [Header("Shader Settings")]
    [SerializeField]
    // A material with a diffuse shader
    private Material solidMaterial = null;
    [SerializeField]
    // A material with the fade shader
    private Material fadeMaterial = null;
    [SerializeField]
    // Used for holding the correct transparency value
    private Color targetColour = Color.magenta;

    // Is the player withing the range specified
    private bool playerInRange = false;
    // The current alpha value
    private float transparency = 1.0f;
    // How many renderers are attached to children
    private int numberOfRenderers;
    // The obstacle class attached to this object
    private Obstacle thisObstacle = null;
    // The renderers attached to all children
    private Renderer[] attachedRenderers = null;
    // Used for updating the rendering with the correct shader
    private Material mat = null;

    // Use this for initialization
    void Start ()
    {
        // Get references to the obstacle object and all renderers in children 
        thisObstacle = gameObject.GetComponent<Obstacle>();
		numberOfRenderers = GetComponentsInChildren<Renderer>().Length;
		attachedRenderers = new Renderer[numberOfRenderers];
		attachedRenderers = GetComponentsInChildren<Renderer>();

        // Apply the opaque mobile shader to all objects in children
		for (int i = 0; i < numberOfRenderers; i++)
		{
			attachedRenderers[i].material = solidMaterial;
			mat = attachedRenderers[i].material;
			mat.SetColor("_Color", targetColour);
		}
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get current position in level
        float playerZ = Ship_Movement.shipPosition.z;

        // If the obstacle intersects with the top lane at any point, check if the player is in range
        if (((int)thisObstacle.location < 5) | (((int)thisObstacle.location > 6) & ((int)thisObstacle.location < 10))) // NW_R, NW_D, N, NE_D, NE_L, SE_U, S, SW_U
        {
            // If the player is in range of this obstacle, get the current fade value, otherwise set the fade value to 1.0f
            //TODO: Linearly Interpolate this value rather than clamping to give a smoother fade
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
        // If the obstacle is just in the middle lane, check if the player is nin range
        else if ((thisObstacle.location == LaneManager.ObstacleLocation.E) | (thisObstacle.location == LaneManager.ObstacleLocation.W)) // E, W
        {
            // If the player is in range of this obstacle, get the current fade value, otherwise set the fade value to 1.0f
            //TODO: Linearly Interpolate this value rather than clamping to give a smoother fade
            if ((thisObstacle.GetzPosition() - playerZ) < playerDistanceMid)
            {
                playerInRange = true;
                transparency = (thisObstacle.GetzPosition() - playerZ) / playerDistanceMid;
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
        else // SE_L, SW_R
        {
            playerInRange = false;
            transparency = 1.0f;
        }

        // If the object is fading, update the alpha value of the material, otherwise apply the solid material
        if (playerInRange)
        {
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
            //TODO: currently assigning solidMaterial each frame for every object, could be optimised
            for (int i = 0; i < numberOfRenderers; i++)
			{
				attachedRenderers[i].material = solidMaterial;
				mat = attachedRenderers[i].material;
			}
        }
    }
}
