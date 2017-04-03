using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRay : MonoBehaviour
{
    [SerializeField]
    private float collisionDistance = 6.0f;

   // [SerializeField]
    private Vector3 downLane = new Vector3(0, 0, 1);
    private Vector3 backUpLane = new Vector3(0, 0, -1);

    private GameObject lastObjectHit = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool CheckCollisionDownLane(string collisionTag)
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin.z -= (collisionDistance * 0.5f);

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << gameObject.layer;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;


        // If something with the correct tag is down the lane, return true
        if (Physics.Raycast(origin, downLane, out hit, collisionDistance, layerMask))
        {
        //    Debug.Log("Looking for " + collisionTag + ". hit: " + hit.collider.tag);
            lastObjectHit = hit.collider.gameObject;
            if (hit.collider.tag == collisionTag)
            {
                return true;
            }
         }

        return false;
    }

    public bool CheckCollisionBackUpLane(string collisionTag)
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin.z += (collisionDistance * 0.5f);

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << gameObject.layer;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        // If something with the correct tag is down the lane, return true
        if (Physics.Raycast(origin, backUpLane, out hit, collisionDistance, layerMask))
        {
            lastObjectHit = hit.collider.gameObject;
            if (hit.collider.tag == collisionTag)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckCollision(string collisionTag, Vector3 direction, float dist = 1.0f)
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin += direction * (collisionDistance * -0.5f);

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << gameObject.layer;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        // If something with the correct tag is down the lane, return true
        if (Physics.Raycast(transform.position, direction, out hit, collisionDistance, layerMask))
        {
            lastObjectHit = hit.collider.gameObject;
            if (hit.collider.tag == collisionTag)
            {
                return true;
            }
        }

        return false;
    }

    public GameObject LastGameObjectHit()
    {
        return lastObjectHit;
    }
}
