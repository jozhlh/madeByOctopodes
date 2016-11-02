using UnityEngine;
using System.Collections;

public class ObstacleFade : MonoBehaviour {

    [SerializeField]
    private GameObject player = null;

    private Rock thisRock = null;

    private Renderer attachedRenderer = null;

    private Material mat = null;

    [SerializeField]
    private Material solidMaterial = null;

    [SerializeField]
    private Material fadeMaterial = null;

    [SerializeField]
    private Color targetColour = Color.magenta;

    public float transparency = 1.0f;
    private float lowestTransparency = 0.2f;
    private float playerDistanceTop = 10.0f;
    private float playerDistanceMid = 5.0f;

    private bool playerInRange = false;

    // Use this for initialization
    void Start () {
        thisRock = gameObject.GetComponent<Rock>();

        //attachedRenderer = gameObject.GetComponent<Renderer>();
        // attachedRenderer.sharedMaterial.color = targetColour;
        gameObject.GetComponent<Renderer>().material = solidMaterial;
        mat = gameObject.GetComponent<Renderer>().material;
        mat.SetColor("_Color", targetColour);
    }
	
	// Update is called once per frame
	void Update () {
        float playerZ = player.transform.position.z;

        if ((int)thisRock.inLane < 3)
        {
            if ((thisRock.GetzPosition() - playerZ) < playerDistanceTop)
            {
                playerInRange = true;
                transparency = (thisRock.GetzPosition() - playerZ) / playerDistanceTop;
                if (transparency < lowestTransparency)
                {
                    transparency = lowestTransparency;
                }
            }
            else
            {
                transparency = 1.0f;
            }
        }
        else if ((int)thisRock.inLane < 6)
        {
            if ((thisRock.GetzPosition() - playerZ) < playerDistanceMid)
            {
                playerInRange = true;
                transparency = (thisRock.GetzPosition() - playerZ) / playerDistanceMid;
                if (transparency < lowestTransparency)
                {
                    transparency = lowestTransparency;
                }
            }
            else
            {
                transparency = 1.0f;
            }
        }
        else
        {
            transparency = 1.0f;
        }

        if (playerInRange)
        {
            gameObject.GetComponent<Renderer>().material = fadeMaterial;
            mat = gameObject.GetComponent<Renderer>().material;
            targetColour.a = transparency;
            mat.SetColor("_Color", targetColour);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = solidMaterial;
            mat = gameObject.GetComponent<Renderer>().material;
        }
               
        
        //attachedRenderer.sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f, transparency);
    }
}

