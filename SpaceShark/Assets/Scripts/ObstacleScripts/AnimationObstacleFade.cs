using UnityEngine;
using System.Collections;

public class AnimationObstacleFade : MonoBehaviour
{
    [Header("Shader Settings")]
    [SerializeField]
    // A material with a diffuse shader
    private Material baseMaterial = null;
    [SerializeField]
    // A material with a diffuse shader
    private Material accentMaterial = null;
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
    // Used for lerping the fade value
    float tParam = 0.0f;

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
            if (attachedRenderers[i].gameObject.GetComponent<FadePermissions>().accent)
            {
                attachedRenderers[i].material = accentMaterial;
            }
            else
            {
                attachedRenderers[i].material = baseMaterial;
            }
			//attachedRenderers[i].material = solidMaterial;
			mat = attachedRenderers[i].material;
			mat.SetColor("_Color", targetColour);
		}
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get current position in level
        float playerZ = Ship_Movement.shipPosition.z;
        // Get the obstacles position in the level
        float zPos = thisObstacle.gameObject.transform.position.z;
        // If the obstacle intersects with the top lane at any point, check if the player is in range
        if (((int)thisObstacle.GetLocation() < 5) | (((int)thisObstacle.GetLocation() > 6) & ((int)thisObstacle.GetLocation() < 10))) // NW_R, NW_D, N, NE_D, NE_L, SE_U, S, SW_U
        {
            // If the player is in range of this obstacle, get the current fade value, otherwise set the fade value to 1.0f
            if ((zPos - playerZ) < GameSettings.playerDistanceTop)
            {
                playerInRange = true;
                tParam = 1.0f - ((zPos - playerZ) / GameSettings.playerDistanceTop);                                  //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
                transparency = Mathf.Lerp(1.0f, GameSettings.lowestTransparency, tParam);
            }
            else
            {
                playerInRange = false;
                transparency = 1.0f;
            }
        }
        // If the obstacle is just in the middle lane, check if the player is nin range
        else if ((thisObstacle.GetLocation() == LaneManager.ObstacleLocation.E) | (thisObstacle.GetLocation() == LaneManager.ObstacleLocation.W)) // E, W
        {
            // If the player is in range of this obstacle, get the current fade value, otherwise set the fade value to 1.0f
            if ((zPos - playerZ) < GameSettings.playerDistanceMid)
            {
                playerInRange = true;
                tParam = 1.0f - ((zPos - playerZ) / GameSettings.playerDistanceMid);                                  //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
                transparency = Mathf.Lerp(1.0f, GameSettings.lowestTransparency, tParam);
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
                if (attachedRenderers[i].gameObject.GetComponent<FadePermissions>().canFade)
                {
                    attachedRenderers[i].material = fadeMaterial;
                    mat = attachedRenderers[i].material;
                    targetColour.a = transparency;
                    mat.SetColor("_Color", targetColour);
                }
            }
        }
        else
        {
            //TODO: currently assigning solidMaterial each frame for every object, could be optimised
            for (int i = 0; i < numberOfRenderers; i++)
            {
                if (attachedRenderers[i].gameObject.GetComponent<FadePermissions>().accent)
                {
                    attachedRenderers[i].material = accentMaterial;
                    mat = attachedRenderers[i].material;
                }
                else
                {
                    attachedRenderers[i].material = baseMaterial;
                    mat = attachedRenderers[i].material;
                }
            }
        }
    }
}
