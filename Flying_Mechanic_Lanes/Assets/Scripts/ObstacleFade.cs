using UnityEngine;
using System.Collections;

public class ObstacleFade : MonoBehaviour {

    [SerializeField]
    private GameObject player = null;

    private Rock thisRock = null;

    private Renderer attachedRenderer = null;

    private float transparency = 1.0f;
    private float lowestTransparency = 0.2f;
    private float playerDistanceTop = 10.0f;
    private float playerDistanceMid = 5.0f;


    // Use this for initialization
    void Start () {
        thisRock = gameObject.GetComponent<Rock>();

        attachedRenderer = gameObject.GetComponent<Renderer>();
        attachedRenderer.sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f, transparency);
    }
	
	// Update is called once per frame
	void Update () {
        float playerZ = player.transform.position.z;
        if ((int)thisRock.inLane < 3)
        {
            if ((thisRock.GetzPosition() - playerZ) < playerDistanceTop)
            {
                transparency = (thisRock.GetzPosition() - playerZ) / playerDistanceTop;
                if (transparency < lowestTransparency)
                {
                    transparency = lowestTransparency;
                }
            }
        }
        else if ((int)thisRock.inLane < 6)
        {
            if ((thisRock.GetzPosition() - playerZ) < playerDistanceMid)
            {
                transparency = (thisRock.GetzPosition() - playerZ) / playerDistanceMid;
                if (transparency < lowestTransparency)
                {
                    transparency = lowestTransparency;
                }
            }
        }
        attachedRenderer.sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f, transparency);
    }
}

