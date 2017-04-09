using UnityEngine;
using System.Collections;

public class EndPlate : MonoBehaviour
{
    public static float horizon;

    [SerializeField]
    private float endplateDistance = 500.0f;
    private StateManager state = null;

    // Use this for initialization
    void Start ()
    {
        horizon = endplateDistance;
        state = GameObject.Find("ScreenManager").GetComponent<StateManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (state.GetState() == StateManager.States.play)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, Ship_Movement.shipPosition.z + endplateDistance);
        horizon = transform.position.z;
    }
}
